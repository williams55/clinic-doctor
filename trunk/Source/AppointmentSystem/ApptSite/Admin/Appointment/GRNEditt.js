// Ten bien chua id cac form
var formId = "form-scheduler";
var patientFormId = "form-patient";
var classSelectedTab = 'ui-tabs-selected';

/****************************Get object - Start******************************/
// Appointment
var $hdfPatientName = $('#patient-name'),
    $hdfPatientCode = $('#patient-code'),
    $txtPatientSearch = $('#patient-search'),
    $imgPatientError = $('#patient-error');
/****************************Get object - Start******************************/

var CurrentAppointment;
var miniCalendar;

// This variable use for scrolling to current time in current date
var scrollToCurrent = true;

// Danh sach cac timespan cu
var listTs = [];

// Dung de danh dau la dang chay de khong chay lai su kien view change
var isUsing = false;

// True if lightbox is using, do not refesh page
var isLightbox = false;

// True if lightbox is using, do not refesh page
var currentForm;

/****************************DHTMLX - Start******************************/
// Load calendar for Schedule
function ShowMinical() {
    miniCalendar = scheduler.renderCalendar({
        container: "datepicker",
        date: scheduler._date,
        navigation: true,
        handler: function (date, calendar) {
            scheduler.setCurrentView(date);
        }
    });
}

// Init Schedule
function initSchedule(weekday) {
    var currentDate = new Date();
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.time_step = stepTime;
    scheduler.config.mark_now = false;
    scheduler.locale.labels.unit_tab = "Unit";
    scheduler.locale.labels.timeline_tab = "Timeline";
    scheduler.xy.min_event_height = 0;
    scheduler.config.multi_day = true; // rest of multiday events would be displayed at the top

    // Them nut chuc nang dua vao quyen
    scheduler.config.icons_select = [];
    if ($('[id$=divUpdate]').length) {
        scheduler.config.icons_select.push('icon_edit');
    }
    if ($('[id$=divDelete]').length) {
        scheduler.config.icons_select.push('icon_delete');
    }

    // Thay the title cua event
    scheduler.renderEvent = function (container, event) {
        var bg_color = (event.color ? ("background:" + event.color + ";") : "background: #1796B0");
        var color = (event.textColor ? ("color:" + event.textColor + ";") : "");
        var w = parseInt(container.style.width);
        var h = parseInt(container.style.height);
        var bottom = container.className.indexOf('dhx_cal_select_menu') > 0;

        var inner_html = '<div class="dhx_event_move" style="cursor: pointer; width:' + (w - (this._quirks ? 4 : 14))
        // Chieu cao giam di de cho border
            + 'px; height:' + (h - 3) + 'px;' + bg_color + '' + color + '">' +
            '<div style="padding: 5px; text-align: center; font-weight: bold;">' +
            (event.PatientInfo ? event.PatientInfo + '<br/>' : '') +
        //scheduler.templates.event_date(event.start_date) + " - " + scheduler.templates.event_date(event.end_date) +
            '</div></div>'; // +2 css specific, moved from render_event
        var footer_class = "dhx_event_resize";
        if (bottom)
            footer_class = "dhx_resize_denied";

        inner_html += '<div class="' + footer_class + '" style=" width:' + (w - 14) + 'px;' + (bottom ? ' margin-top:-1px;' : '') + '' + bg_color + '' + color + '" ></div>';

        container.innerHTML = inner_html;
        return true; // required, true - we've created custom form; false - display default one instead
    };

    // Thay tooltip
    scheduler.templates.tooltip_date_format = scheduler.date.date_to_str("%H:%i");
    scheduler.templates.tooltip_text = function (start, end, event) {
        return (event.PatientInfo ? '<b>Patient:</b> ' + event.PatientInfo + '<br/>' : '') +
                    "<b>Time:</b> " + scheduler.templates.tooltip_date_format(start) + " - " + scheduler.templates.tooltip_date_format(end) + "<br/><b>Note:</b> " + event.note;
    };

    // Thay doi su kien nut click
    scheduler._click.buttons = {
        "delete": function (id) {
            DeleteAppointment(id);
        },
        edit: function (id) { scheduler.showLightbox(id); }
    };

    // Set lai chieu cao, dinh dang cot time
    var step = stepTime;
    var format = scheduler.date.date_to_str("%H:%i");
    scheduler.config.hour_size_px = (60 / step) * 22;
    scheduler.templates.hour_scale = function (date) {
        var htmltime = "";
        for (var i = 0; i < 60 / step; i++) {
            htmltime += "<div style='height:21px;line-height:21px;'>" + format(date) + "</div>";
            date = scheduler.date.add(date, step, "minute");
        }
        return htmltime;
    };

    // Tao unit view voi danh sach doctor duoc chon
    // Co du lieu de tranh bi loi khi init view unit
    scheduler.createUnitsView({
        name: "unit",
        property: "section_id",
        list: scheduler.serverList("unit", [{
            key: "",
            label: ""
        }])
    });

    // Tao timeline view voi danh sach doctor duoc chon
    scheduler.createTimelineView({
        section_autoheight: false,
        name: "timeline",
        x_unit: "minute",
        x_date: "%H:%i",
        x_step: 60,
        x_size: 24,
        x_start: 0,
        x_length: 24,
        y_unit: scheduler.serverList("timeline", []),
        y_property: "section_id",
        render: "tree",
        dx: 150,
        folder_dy: 20,
        dy: 40
    });
    scheduler.templates["timeline_date"] = function (datea, dateb) {
        return scheduler.templates.day_date(datea);
    };

    scheduler.attachEvent("onBeforeDrag", BeforeDrag);
    scheduler.attachEvent("onClick", BlockReadonly);
    scheduler.attachEvent("onEventChanged", EventChanged);
    scheduler.attachEvent("onViewChange", function (mode, date) {
        if (scrollToCurrent && (mode == "day" || mode == "unit") && (new Date()).toLocaleDateString() == date.toLocaleDateString()) {
            // Get y position
            var top = date.getHours() * $('.dhx_scale_hour:first').outerHeight();
            $('div.dhx_cal_data').scrollTo({ top: top + 'px', left: '0px' }, 800);
            scrollToCurrent = false;
        }
        if (!isUsing && !isLightbox) {
            // Lam cai load roster cua doctor,
            // neu doctor co roster thi hien thi
            // khi hien thi, dua vao thoi gian cua roster ma to mau
            isUsing = true;

            // Clone list cu ra
            var tmpListTs = listTs.slice();
            listTs = [];

            // Danh sach chua cac list ajax
            var arrAjax = [];
            LoadAppointment(arrAjax, mode, date);
            BlockTimespan(scheduler, date, mode, listTs);
            DeleteTimespan(scheduler, tmpListTs);

            $.when.apply($, arrAjax).done(function () {
                scheduler.updateView();
                isUsing = false;
            });
        }
    });

    // Load roster
    scheduler.init('scheduler_here', currentDate, "unit");

    ShowMinical();

    // Refesh current view for mark_now can auto update
    setInterval(function () {
        if (!isLightbox) {
            scheduler.setCurrentView();
        }
    }, 60000);
}
/****************************DHTMLX - End******************************/

/****************************Scheduler - Start******************************/
// Set readonly property for event
// Get patient's info and fill in form info
function BlockReadonly(id) {
    GetPatientInfo(id);
    if (!id) return true;
    return !scheduler.getEvent(id).ReadOnly;
}

// Function will be raise when event changed
function BeforeDrag(id) {
    // Get event and clone it before drag, it's useful in case failure drag
    CurrentAppointment = $.extend(true, {}, scheduler.getEvent(id));
    return BlockReadonly(id);
}

// Function will be raise when event changed
function EventChanged(eventId, objEvent) {
    // Case move event
    //if (scheduler._drag_mode && (scheduler._drag_mode == 'move' || scheduler._drag_mode == 'resize')) {
    MoveAppointment(eventId, objEvent);
    //}
}

scheduler.showLightbox = function (id) {
    isLightbox = true;
    InitForm();
    var ev = scheduler.getEvent(id);
    CurrentAppointment = scheduler.getEvent(id);

    var d = html("drag-title");
    d.onmousedown = scheduler._ready_to_dnd;
    d.onselectstart = function () { return false; };
    d.style.cursor = "pointer";
    scheduler._init_dnd_events();
    scheduler.startLightbox(id, html(formId));

    // Reset variable
    room = -1;
    patient = '';
    doctor = '';
    cboRoom.SetValue(room);

    // Set init value
    $("[id$=hdId]").val(id);
    startDate.SetDate(ev.start_date);
    endDate.SetDate(ev.end_date);
    startTime.SetDate(ev.start_date);
    endTime.SetDate(ev.end_date);
    $("#txtNote").val(ev.note);
    cboStatus.SetSelectedIndex(0);
    cboService.SetSelectedItem(cboService.FindItemByValue($('#service-tabs li.ui-tabs-selected').attr('id').replace('tab_', '')));

    $hdfPatientName.val('');
    $hdfPatientCode.val('');
    $txtPatientSearch.val('');

    grid.PerformCallback('Refresh');

    // Set value
    RefreshDoctorList(ev.section_id);
    // Load data
    if (ev.isnew == false) {
        CurrentAppointment = null;
        $('#drag-title .dhx_time').text('Edit appoinment ' + id);
        $("#repeater-section, [id$=divSave]").hide();
        $("[id$=divUpdate], [id$=divDelete]").show();
        if (ev.status) {
            cboStatus.SetSelectedItem(cboStatus.FindItemByValue(ev.status));
        }
        if (ev.service) {
            cboService.SetSelectedItem(cboService.FindItemByValue(ev.service));
            RefreshDoctorList(ev.service);
        }
        if (ev.patient) {
            $('#patient-search, #patient-name').val(ev.PatientInfo);
            $hdfPatientCode.val(ev.patient);
        }
        if (ev.room) {
            RefreshRoomList(ev.room);
        }
    }
    else {
        $('#drag-title .dhx_time').text('New appoinment');
        $("#repeater-section, [id$=divSave]").show();
        $("[id$=divUpdate], [id$=divDelete]").hide();
    }

    // Goi ham validate form khi form hien thi
    ValidateForm();

    // Neu event la readonly thi disable cac control
    if (ev.ReadOnly) {
        $("[id$=divSave], [id$=divUpdate]").hide();
        $("[id$=divDelete]").hide();
    }

    $('#patient-search').focus();
    
    return true;
};
/****************************Scheduler - End******************************/

/****************************Patient - Start******************************/
// Get Patient's info by appointment id
function SaveRemark() {
    var requestdata = JSON.stringify({ patient: $("#hdPatient").val(), remark: $("#apptRemark").val() });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateRemark",
        data: requestdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj.result != "true") {
                ShowMessage(obj.message);
            }
        },
        fail: CallError,
        error: CallError,
        complete: CloseProgress
    });
}

// Get Patient's info by appointment id
function GetPatientInfo(currentId) {
    var ev = scheduler.getEvent(currentId);
    if (currentId && ev) {
        var requestdata = JSON.stringify({ appointmentId: currentId });
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetPatientByAppointmentId",
            data: requestdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response.d);
                if (obj.result == "true") {
                    if (obj.data) {
                        var arr = obj.data[0]; // Get first item
                        $("#hdPatient").val(arr.Id);
                        $("#divFirstname").html(arr.FirstName + " &nbsp;");
                        $("#divLastname").html(arr.LastName + " &nbsp;");
                        $("#divCellPhone").html(arr.CellPhone + " &nbsp;");
                        $("#divBirthday").html(arr.Birthdate);
                        $("#divExpDate").html(arr.ExpDate);
                        $("#divNationality").html(arr.Nationality);
                        $("#apptRemark").val(arr.Remark);
                    }
                }
                else {
                    ShowMessage(obj.message);
                }
            },
            fail: CallError,
            error: CallError,
            complete: CloseProgress
        });
    }
}

// Ham mo form CRUD patient
function OpenPatient(isUpdate) {
    // Neu la update thi lay thong tin patient
    if (isUpdate && $hdfPatientCode.val()) {
        $('#drag-title2 .dhx_time').text('Edit patient');
        txtPatientCode.SetValue($hdfPatientCode.val());
        var requestdata = JSON.stringify({ id: $hdfPatientCode.val() });
        ShowProgress();
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetPatient",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var obj = JSON.parse(response.d);
                if (obj.result == "true") {
                    txtFirstName.SetValue(obj.data[0].FirstName);
                    txtMiddleName.SetValue(obj.data[0].MiddleName);
                    txtLastName.SetValue(obj.data[0].LastName);
                    txtDob.SetValue(new Date(obj.data[0].DateOfBirth));
                    cboNationality.SetSelectedItem(cboNationality.FindItemByValue(obj.data[0].Nationality));
                    cboCompany.SetSelectedItem(cboCompany.FindItemByValue(obj.data[0].CompanyCode));
                    txtMobilePhone.SetValue(obj.data[0].MobilePhone);
                    txtHomePhone.SetValue(obj.data[0].HomePhone);
                    $('#txtRemark').val(obj.data[0].Remark);
                    if (obj.data[0].Sex == "M") {
                        radMale.SetChecked(true);
                        radFemale.SetChecked(false);
                    } else {
                        radFemale.SetChecked(true);
                        radMale.SetChecked(false);
                    }

                } else {
                    ShowMessage(obj.message);
                }
            },
            complete: function () {
                CloseProgress();
                StartPatient();
            }
        });
    } else {
        $('#drag-title2 .dhx_time').text('New patient');
        txtPatientCode.SetValue('Auto generate');
        txtFirstName.SetValue('');
        txtMiddleName.SetValue('');
        txtLastName.SetValue('');
        txtDob.SetValue(new Date());
        cboNationality.SetValue('');
        cboCompany.SetValue('');
        txtMobilePhone.SetValue('');
        txtHomePhone.SetValue('');
        $('#txtRemark').val('');
        radMale.SetChecked(true);
        radFemale.SetChecked(false);
        StartPatient();
    }
}

// Mo form patient, tam tat form appt
function StartPatient() {
    scheduler.endLightbox(false, html(formId));
    if (CurrentAppointment) scheduler.addEvent(CurrentAppointment);

    ValidatePatient();

    var d = html("drag-title2");
    d.onmousedown = scheduler._ready_to_dnd;
    d.onselectstart = function () { return false; };
    d.style.cursor = "pointer";
    scheduler._init_dnd_events();
    scheduler.startLightbox('patient', html(patientFormId));
}

// Tat form patient, mo lai form appt
function ClosePatient() {
    scheduler.endLightbox(false, html(patientFormId));
    var d = html("drag-title");
    d.onmousedown = scheduler._ready_to_dnd;
    d.onselectstart = function () { return false; };
    d.style.cursor = "pointer";
    scheduler._init_dnd_events();
    scheduler.startLightbox($("[id$=hdId]").val(), html(formId));
    if (CurrentAppointment) scheduler._new_event = CurrentAppointment;
    
    $('#patient-search').focus();
}

// Ham kiem tra form co valid khong
function ValidatePatient() {
    txtPatientCode.Validate();
    txtFirstName.Validate();
    txtLastName.Validate();
    txtDob.Validate();
    cboNationality.Validate();
    txtMobilePhone.Validate();
    return txtPatientCode.isValid && txtFirstName.isValid && txtLastName.isValid
    && txtDob.isValid && cboNationality.isValid && txtMobilePhone.isValid;
}

// Create new patient
function SavePatient() {
    if (!ValidatePatient()) {
        ShowMessage('Please input required fields first.');
        return;
    }

    var requestdata = JSON.stringify({
        id: txtPatientCode.GetValue(),
        firstName: txtFirstName.GetValue(),
        middleName: txtMiddleName.GetValue(),
        lastName: txtLastName.GetValue(),
        dob: txtDob.GetValue(),
        nationality: cboNationality.GetValue(),
        company: cboCompany.GetValue(),
        mobilePhone: txtMobilePhone.GetValue(),
        homePhone: txtHomePhone.GetValue(),
        sex: radFemale.GetChecked() ? "F" : "M",
        remark: $('#txtRemark').val()
    });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/SavePatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                patient = obj.data[0].PatientCode;
                $txtPatientSearch.val(obj.data[0].PatientInfo);
                $hdfPatientName.val(obj.data[0].PatientInfo);
                $hdfPatientCode.val(obj.data[0].PatientCode);
                ValidateSearchPatient();
                ClosePatient();
            } else {
                ShowMessage(obj.message);
            }
        },
        fail: CallError,
        error: CallError,
        complete: CloseProgress
    });
}
/****************************Patient - End******************************/

/****************************Appointment - Start******************************/

// Save moved, resize appointment
function MoveAppointment(eventId, objEvent) {
    var requestdata = JSON.stringify({
        id: eventId,
        startTime: objEvent.start_date,
        endTime: objEvent.end_date,
        doctorId: objEvent.section_id,
        mode: scheduler._mode
    });
    isUsing = true;
    $.ajax({
        type: "POST",
        url: "Default.aspx/MoveAppointment",
        async: true,
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var obj = JSON.parse(response.d);

            if (obj.result != "true") {
                ShowMessage(obj.message);
                scheduler.deleteEvent(eventId);
                scheduler.addEvent(CurrentAppointment);
            }
        },
        fail: function () {
            CallError();
            scheduler.deleteEvent(eventId);
            scheduler.addEvent(CurrentAppointment);
        },
        complete: function () {
            CurrentAppointment = null; // set null after use
            isUsing = false;
        }
    });
}

function LoadAppointment(arrAjax, mode, date) {
    var requestdata = JSON.stringify({ mode: mode, date: date });
    arrAjax.push($.ajax({
        type: "POST",
        url: "Default.aspx/GetAppointments",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj.result) {
                // Xoa cac appointment hien tai
                scheduler.clearAll();
                var evs = obj.data;
                if (evs) {
                    // Goi ham bind tab va tao unit view, timeline view
                    BuildTabs(scheduler, evs.Sections, mode);

                    if (mode == "month") {
                        AddTimespanMonth(evs.Events);
                    } else {
                        AddAppointment(evs.Events);
                    }
                }
            }
            else {
                RenderMessage(obj.code);
            }
        },
        fail: CallError,
        error: CallError,
        complete: CloseProgress
    }));
}

// Add new appointment
function NewAppointment() {
    if (!ValidateForm()) {
        ShowMessage('Please input required fields first.');
        return;
    }

    var requestdata = JSON.stringify({
        patientCode: $hdfPatientCode.val(),
        note: $("#txtNote").val(),
        startTime: startTime.GetValue(),
        endTime: endTime.GetValue(),
        startDate: startDate.GetValue(),
        endDate: endDate.GetValue(),
        doctorId: cboDoctor.GetValue(),
        roomId: cboRoom.GetValue(),
        status: cboStatus.GetValue(),
        mode: scheduler._mode
    });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/NewAppointment",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                AddAppointment(obj.data);
                CancelAppointment();
            }
            else {
                ShowMessage(obj.message);
            }
        },
        fail: CallError,
        error: CallError,
        complete: CloseProgress
    });
}

function UpdateAppointment() {
    if (!ValidateForm()) {
        ShowMessage('Please input required fields first.');
        return;
    }

    var id = $("[id$=hdId]").val();
    var requestdata = JSON.stringify({
        id: id,
        patientCode: $hdfPatientCode.val(),
        note: $("#txtNote").val(),
        startTime: startTime.GetValue(),
        endTime: endTime.GetValue(),
        startDate: startDate.GetValue(),
        endDate: endDate.GetValue(),
        doctorId: cboDoctor.GetValue(),
        roomId: cboRoom.GetValue(),
        status: cboStatus.GetValue(),
        mode: scheduler._mode
    });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateAppointment",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var oldEvent = scheduler.getEvent(id);
            if (obj.result == "true" && obj.data && oldEvent) {
                var evs = obj.data[0];
                //scheduler.deleteEvent(id);
                //AddAppointment(evs);
                oldEvent.section_id = cboDoctor.GetValue();
                oldEvent.note = evs.note;
                oldEvent.text = evs.text;
                oldEvent.start_date = new Date(evs.start_date);
                oldEvent.end_date = new Date(evs.end_date);
                oldEvent.patient = $hdfPatientCode.val();
                oldEvent.PatientInfo = $hdfPatientName.val();
                oldEvent.roomId = cboRoom.GetValue();
                oldEvent.status = cboStatus.GetValue();
                oldEvent.color = evs.color;
                oldEvent.isnew = evs.isnew;
                CancelAppointment();
                scheduler.updateView();
            }
            else {
                ShowMessage(obj.message);
            }
        },
        fail: CallError,
        error: CallError,
        complete: CloseProgress
    });
}

// Cancel appointment
function CancelAppointment() {
    scheduler.endLightbox(false, html(formId));
    scheduler._lightbox = null;
    isLightbox = false;
    CurrentAppointment = null;
}

function DeleteAppointment(appt) {
    if (!confirm("Do you want to delete this appointment?"))
        return;

    var id = $("[id$=hdId]").val();
    if (appt) id = appt; // Neu co doi tuong appt truyen vao thi lay id cua no
    var requestdata = JSON.stringify({ id: id });
    ShowProgress();
    isUsing = true;
    $.ajax({
        type: "POST",
        url: "Default.aspx/DeleteAppointment",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                scheduler.deleteEvent(id);
                CancelAppointment();
            }
            else {
                ShowMessage(obj.message);
            }
        },
        fail: CallError,
        error: CallError,
        complete: function () {
            isUsing = false;
            CloseProgress();
        }
    });
}

function AddAppointment(evs) {
    $.each(evs, function (i, item) {
        item.start_date = new Date(item.start_date);
        item.end_date = new Date(item.end_date);
        var serviceId = $('#service-tabs li.ui-tabs-selected').attr('id').replace('tab_', '');
        if (serviceId == item.ServicesId) {
            scheduler.addEvent(item);
        }
    });
}

// Add roster into scheduler
function AddTimespanMonth(evs) {
    $.each(evs, function (i, item) {
        var date = new Date(item.Date);
        var options = {
            start_date: date,
            end_date: scheduler.date.add(date, 1, "day"),
            type: "dhx_time_block", /* creating events on those dates will be disabled - dates are blocked */
            css: "have_roster",
            html: "Have Appt"
        };
        listTs.push(scheduler.addMarkedTimespan(options));

        if (date < new Date())
            listTs.push(scheduler.addMarkedTimespan({
                start_date: date,
                end_date: scheduler.date.add(date, 1, "day"),
                css: 'small_lines_section',
                type: "dhx_time_block"
            }));
    });
}

// Ham tao tabs va tao unit view, timeline view
function BuildTabs(scheduler, sections, mode) {
    // Lay id cua tab dang duoc select [neu co]
    var selectedId = $('#service-tabs li.ui-tabs-selected').attr('id');

    // Bien chua doi tuong selected tab tam
    var tmpSelectedItem = { Doctors: [] };

    // Lay doi tuong chua tab sau do remove tat ca tab
    var $tabs = $('#service-tabs');
    $('li', $tabs).remove();

    // Append cai moi
    $.each(sections, function (i, item) {
        $tabs.append('<li id="tab_' + item.Id + '"><a href="#tab_' + item.Id + '">' + item.Title + '</a></li>');

        // Kiem tra xem danh sach moi co tab dang duoc select ko
        // Neu co thi gan vao bien selected
        if (selectedId == 'tab_' + item.Id) {
            tmpSelectedItem = item;
        }
    });

    // Tim doi tuong da select truoc do
    var $selectedTab = $('#' + selectedId);
    if ($selectedTab.length > 0) {
        $selectedTab.addClass(classSelectedTab);
    } else {
        $('li:first', $tabs).addClass(classSelectedTab);
        tmpSelectedItem = sections.length > 0 ? sections[0] : tmpSelectedItem;
    }

    // Update list cho unit, timeline view
    scheduler.updateCollection("unit", tmpSelectedItem.Doctors);
    scheduler.updateCollection("timeline", tmpSelectedItem.Doctors);

    // Hien thi mau available cua tung user
    if (mode == "timeline" || mode == "unit") {
        $.each(tmpSelectedItem.Doctors, function (i, doctor) {
            // Bien luu nhung khoang thoi gian available, dung de tao block time
            var availableTime = [];
            var day = new Date();
            if (doctor.Rosters.length) {
                $.each(doctor.Rosters, function (j, roster) {
                    var varStartTime = new Date(roster.startTime);
                    var varEndTime = new Date(roster.endTime);
                    listTs.push(scheduler.addMarkedTimespan({
                        start_date: varStartTime,
                        end_date: varEndTime,
                        bcolor: roster.color,
                        sections: {
                            timeline: doctor.key, // list of sections
                            unit: doctor.key
                        }
                    }));
                    availableTime.push(varStartTime.getHours() * 60 + varStartTime.getMinutes());
                    availableTime.push(varEndTime.getHours() * 60 + varEndTime.getMinutes());
                    day = new Date(varStartTime.getFullYear(), varStartTime.getMonth(), varStartTime.getDate());
                });
            }
            // Block toan bo thoi gian trong ngay
            listTs.push(scheduler.addMarkedTimespan({
                days: day,
                zones: availableTime,
                invert_zones: true,
                type: "dhx_time_block",
                sections: {
                    timeline: doctor.key, // list of sections
                    unit: doctor.key
                }
            }));
        });
    }

    // Bind event cho link
    $('li a', $tabs).click(function () {
        // Lay li cua link vua click
        // Neu li dang duoc select thi khong lam gi,
        // nguoc lai thi xoa selected class cua li hien tai, sau do add selected class cho li moi
        var $currentLi = $(this).parent();
        if (!$currentLi.hasClass(classSelectedTab) && !isLightbox && !isUsing) {
            // Xoa class selected
            $('li', $tabs).removeClass(classSelectedTab);
            $currentLi.addClass(classSelectedTab);
            scheduler.setCurrentView();
        }
        return false;
    });

    $tabs.sortable({
        placeholder: "ui-state-highlight",
        update: UpdateSortList
    });
}

// Update index cua sort list
function UpdateSortList(event, ui) {
    var $tabs = $('#service-tabs li');
    var tabIds = [];

    // Lay danh sach id va dua vao mang
    $.each($tabs, function (i, item) {
        tabIds.push($(item).attr('id').replace('tab_', ''));
    });

    var requestdata = JSON.stringify({ ids: tabIds });
    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateIndex",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    });
}
/****************************Appointment - End******************************/

/****************************Initialize - Start******************************/
$(document).ready(function () {
    initSchedule();
    initEvents();

    // Call init autocomplete function
    InitAutocompletePatient();
    
    $('#form-scheduler').keydown(function (e) {
        var key = e.which ? e.which : e.keyCode;

        if (key == 78 && e.ctrlKey) {
            OpenPatient();
            e.preventDefault();
            return false;
        }
        
        var isIE = (document.all ? true : false);
        
        switch (key) {
            case 112:
                // F1
                if (isIE) {
                    document.onhelp = function () {
                        return (false);
                    };
                    window.onhelp = function () {
                        return (false);
                    };
                }
                cboStatus.SetValue(AppStatusCode.CheckedIn);
                e.preventDefault();
                e.stopPropagation();
                return false;
            case 113:
                // F2
                cboStatus.SetValue(AppStatusCode.Completed);
                e.preventDefault();
                e.stopPropagation();
                return false;
            case 114:
                // F3
                e.originalEvent.keyCode = 0;
                cboStatus.SetValue(AppStatusCode.Cancelled);
                e.preventDefault();
                e.stopPropagation();
                return false;
            case 13:
                // Enter
                var ev = scheduler.getEvent($("[id$=hdId]").val());
                if (ev.isnew == false) UpdateAppointment(); else NewAppointment();
                return false;
            case 27:
                // Esc
                CancelAppointment();
                return false;
            default:
                break;
        }
    });

    $('#form-patient').keydown(function (e) {
        var key = e.which ? e.which : e.keyCode;
        switch (key) {
            case 13:
                // Enter
                SavePatient();
                return false;
            case 27:
                // Esc
                ClosePatient();
                return false;
            default:
                break;
        }
    });

    $(document).keydown(function (e) {
        var key = e.which ? e.which : e.keyCode;
        switch (key) {
            case 13:
                //dhx_cal_select_menu
                // Enter
                var $menu = $('.dhx_cal_select_menu:first');
                if ($menu.length > 0) {
                    scheduler.showLightbox($menu.attr('event_id'));
                    return false;
                }
            default:
                break;
        }
    });
});

function initEvents() {
    $(".dhx_cal_today_button:not(#btnToday)").click(function () {
        $("#btnToday").click();
        scheduler.updateCalendar(miniCalendar, scheduler._date);
    });
}
/****************************Initialize - End******************************/

/**************************** Form ******************************/
function InitForm() {
    // Set empty
    $("[id$=hdId]").val("");
    $("#txtNote").val("");
}

var doctor;
var patient;
function RefreshDoctorList(id) {
    doctor = cboDoctor.GetValue();
    cboDoctor.PerformCallback('Refresh');
    if (id) doctor = id;
    RefreshRoomList();
}

var room;
function RefreshRoomList(id) {
    room = cboRoom.GetValue();
    cboRoom.PerformCallback('Refresh');
    if (id) room = id;
}


/**************************** Appointment Form Start ******************************/

/******* Validate Start *******/
// Validate form
function ValidateForm() {
    ValidateSearchPatient();
    cboStatus.Validate();
    cboDoctor.Validate();
    startTime.Validate();
    startDate.Validate();
    endTime.Validate();
    endDate.Validate();
    return cboStatus.isValid && cboDoctor.isValid && ValidateSearchPatient()
        && startTime.isValid && startDate.isValid && endTime.isValid && endDate.isValid;
}

function ValidateSearchPatient() {
    if ($hdfPatientCode.val() == '') {
        $imgPatientError.show();
        return false;
    }
    $imgPatientError.hide();
    return true;
}
/******* Validate End *******/

/******* Init Autocomplete Start *******/

function InitAutocompletePatient() {
    $('#patient-search').autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "Default.aspx/SearchPatient",
                data: JSON.stringify({ keyword: request.term }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var obj = JSON.parse(data.d);
                    if (obj.result == "true") {
                        response($.map(obj.data,
                            function (item) {
                                return {
                                    label: item.PatientInfo,
                                    value: item.PatientInfo,
                                    PatientCode: item.PatientCode,
                                    FirstName: item.FirstName,
                                    LastName: item.LastName,
                                    DateOfBirth: item.DateOfBirth,
                                    Sex: item.Sex
                                };
                            }
                        ));
                    }
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $hdfPatientName.val(ui.item.label);
            $hdfPatientCode.val(ui.item.PatientCode);
            ValidateForm();
        }
    }).blur(function () {
        if ($txtPatientSearch.val()) {
            $txtPatientSearch.val($hdfPatientName.val());
        } else {
            $hdfPatientName.val('');
            $hdfPatientCode.val('');
        }
        ValidateForm();
    }).data("autocomplete")._renderItem = function (ul, item) {
        var html = "<a class='autocomplete'><div class='col' style='width:90px;'>" + item.PatientCode + "</div>"
            + "<div class='col' style='width:120px;'>" + item.FirstName + "</div>"
            + "<div class='col' style='width:120px;'>" + item.LastName + "</div>"
            + "<div class='col' style='width:70px;'>" + item.DateOfBirth + "</div>"
            + "<div class='col' style='width:50px;'>" + item.Sex + "</div>"
            + "<div style='clear:both;'></div></a>";
        return $("<li>")
            .data("item.autocomplete", item)
            .append(html)
            .appendTo(ul);
    };
}

/******* Init Autocomplete End *******/

/**************************** Appointment Form End ******************************/
