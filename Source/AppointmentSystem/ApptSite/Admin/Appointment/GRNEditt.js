// Ten bien chua id cac form
var formId = "form-dhtmlx";
var classSelectedTab = 'ui-tabs-selected';

var CurrentEventId = "-1";
var CurrentAppointment;
var miniCalendar;

// This variable use for scrolling to current time in current date
var scrollToCurrent = true;

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
        handler: function(date, calendar) {
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

    // Tao unit view voi danh sach doctor duoc chon
    scheduler.createUnitsView({
        name: "unit",
        property: "section_id",
        list: scheduler.serverList("unit", [])
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

    scheduler.attachEvent("onBeforeDrag", BeforeDrag);
    scheduler.attachEvent("onClick", BlockReadonly);
    scheduler.attachEvent("onEventChanged", EventChanged);
    scheduler.attachEvent("onViewChange", function(mode, date) {
        if (scrollToCurrent && mode == "unit" && (new Date()).toLocaleDateString() == date.toLocaleDateString()) {
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
            LoadAppointment(mode, date);
        }
        BlockTimespan(scheduler, date, mode);
    });

    // Load roster
    scheduler.init('scheduler_here', currentDate, "unit");
    ShowMinical();

    // Refesh current view for mark_now can auto update
    setInterval(function() {
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
    if (scheduler._drag_mode && (scheduler._drag_mode == 'move' || scheduler._drag_mode == 'resize')) {
        MoveAppointment(eventId, objEvent);
    }
}

scheduler.showLightbox = function(id) {
    isLightbox = true;

    // Get current appointmentId
    var ev = scheduler.getEvent(id);
    CurrentEventId = id;

    if (ev.start_date < new Date()) {
        if (ev.isnew == false) {
            ShowDialog("", "", "You cannot change a passed appointment.", "");
        } else {
            DeleteAppointment("Lightbox");
            scheduler.deleteEvent(id);
            ShowDialog("", "", "You cannot create new appointment a passed day.", "");
        }
        return false;
    }
    // Clear all value
    $("#hdId").val("");
    $("#txtNote").val("");
    $("#lblDoctor").val("");
    $("#txtPatient").tokenInput("clear");

    // Button create user and change user
    $("[id$=createUser]").show();
    $("[id$=changeUser]").hide();

    // Get doctor's info
    $("#hdfDoctor").val(ev.section_id);
    $.each(scheduler._props[scheduler._mode].options, function(i, item) {
        // Get name of doctor by key
        if (item.key == ev.section_id) {
            $("#lblDoctor").html("Dr. " + item.label);
            return;
        }
    });

    // Set room, do not get info
    SetTime(ev);

    // Set init value
    $("#txtFromDate").datepicker("setDate", new Date(ev.start_date.getFullYear(), ev.start_date.getMonth(), ev.start_date.getDate()));
    $("#txtToDate").datepicker("setDate", new Date(ev.end_date.getFullYear(), ev.end_date.getMonth(), ev.end_date.getDate()));
    if (ev.RoomId) {
        $("#hdfRoom").val(ev.RoomId);
    }
    if (ev.PatientCode) {
        $("#txtPatient").tokenInput("add", { id: ev.PatientCode, PatientInfo: ev.PatientInfo });
    }

    // Get available room list
    GetRoom();

    // Load data
    if (ev.isnew == false) {
        $("[id$=createUser]").hide();
        $("[id$=changeUser]").show();

        // Set current user to global variable
        CurrentAppointment = ev;

        // Update case, get info from server
        $("#RosterForm").dialog({
            autoOpen: true,
            height: 320,
            width: 550,
            modal: false,
            zIndex: $.maxZIndex() + 1,
            resizable: false,
            buttons: {
                Delete: function() {
                    DeleteAppointment(id);
                },
                Cancel: function() {
                    $(this).dialog("close");
                },
                Save: function() {
                    UpdateAppointment(id);
                }
            },
            close: function() {
                CancelAppointment();
            }
        });
    }
    else {
        // New mode
        $("#RosterForm").dialog({
            autoOpen: true,
            height: 350,
            width: 550,
            modal: false,
            zIndex: $.maxZIndex() + 1,
            resizable: false,
            buttons: {
                Cancel: function() {
                    $(this).dialog("close");
                },
                Save: function() {
                    NewAppointment();
                }
            },
            close: function() {
                CancelAppointment();
            }
        });
    }

    $(".token-input-dropdown").css("z-index", $.maxZIndex() + 10); // Bring to front

    currentForm = $('#RosterForm');
    scheduler.startLightbox(id, html("RosterForm"));
    return true;
};
/****************************Scheduler - End******************************/

/****************************Form - Start******************************/
// Get available rooms
function GetRoom() {
    var requestdata = JSON.stringify({ appointmentId: CurrentEventId, doctorId: $("#hdfDoctor").val(),
        startTime: $("#cboFromHour").val(), endTime: $("#cboToHour").val(),
        startDate: $("#txtFromDate").val(), endDate: $("#txtToDate").val()
    });

    $("#cboRoom").html("");
    $("#cboRoom").append("<option value=''>--Choose Room--</option>");
    $("#cboRoom").attr("disabled", "disabled");
    $("#loadingRoom").show();

    $.ajax({
        type: "POST",
        url: "Default.aspx/GetRoom",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);

            if (obj.result == "true") {
                if (obj.data) {
                    $.each(obj.data, function(i, item) {
                        var option = document.createElement("option");
                        $(option).val(item.Id);
                        $(option).text(item.Title);
                        if ($("#hdfRoom").val() == item.Id) {
                            $(option).attr("selected", "selected");
                        }
                        $("#cboRoom").append(option);
                    });
                }
            }
            else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            ShowDialog("", "", "Unknow error!", "");
        },
        complete: function() {
            $("#cboRoom").removeAttr("disabled");
            $("#loadingRoom").hide();
        }
    });
}
/****************************Form - End******************************/

/****************************DateTime - Start******************************/
// Set time to time in Appointment form
function SetTime(ev) {
    var startTime = ev.start_date.format("HH:MM");
    var endTime = ev.end_date.format("HH:MM");
    $("#cboFromHour").val(startTime + ":00");
    $("#cboToHour").val(endTime + ":00");
}

// Bind time to time box in form
function BindTime() {
    for (var h = 0; h < maxHour; h++) {
        var m = 0;
        while (m < maxMinute) {
            var minute = m.toString();
            var hour = h.toString();
            if (m < 10) minute = "0" + m.toString();
            if (h < 10) hour = "0" + h.toString();
            var key = hour + ":" + minute + ":00";
            var label = hour + ":" + minute;
            m += minuteStep;
            $("#cboFromHour").append('<option value="' + key + '">' + label + '</option>');
            $("#cboToHour").append('<option value="' + key + '">' + label + '</option>');
        }
    }
}
/****************************DateTime - End******************************/

/****************************Patient - Start******************************/
// Get Patient's info by appointment id
function GetPatientInfo(currentId) {
    var ev = scheduler.getEvent(currentId);
    if (currentId && ev) {
        $("#divNote").html(ev.note + " &nbsp;");
        var requestdata = JSON.stringify({ appointmentId: currentId });
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetPatientByAppointmentId",
            data: requestdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                var obj = JSON.parse(response.d);
                if (obj.result == "true") {
                    if (obj.data) {
                        var arr = obj.data[0]; // Get first item
                        $("#divFirstname").html(arr.FirstName + " &nbsp;");
                        $("#divLastname").html(arr.LastName + " &nbsp;");
                        $("#divCellPhone").html(arr.CellPhone + " &nbsp;");
                        $("#divBirthday").html(arr.Birthday);
                    }
                }
                else {
                    ShowDialog("", "", obj.message, "");
                }
            },
            fail: function() {
                ShowDialog("", "", "Cannot load Patient's info. Please try again or contact Administrator.", "");
            }
        });
    }
}

// Create new patient
function CreateSimplePatient(firstname, middleName, lastname, dob, nationality, remark, mobilePhone, dialog) {
    var requestdata = JSON.stringify({
        firstName: $(firstname).val(),
        middleName: $(middleName).val(),
        lastName: $(lastname).val(),
        dob: $(dob).datepicker('getDate'),
        nationality: $(nationality).val(),
        mobilePhone: $(mobilePhone).val(),
        sex: $('input[name=radSex]:checked').val(),
        remark: $(remark).val()
    });
    ShowProgress();
    $.ajax({
        type: "POST",
        url: "Default.aspx/CreateSimplePatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                $("#txtPatient").tokenInput("clear");
                $("#txtPatient").tokenInput("add", { id: obj.data[0].PatientCode, PatientInfo: obj.data[0].PatientInfo });
                $(dialog).dialog("close");
            } else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            CloseProgress();
            ShowDialog("", "", "Cannot create patient. Please contact Administrator", "");
        }
    });
}

// Create new patient
function UpdateSimplePatient(id, firstname, middleName, lastname, dob, nationality, remark, mobilePhone, dialog) {
    var requestdata = JSON.stringify({
        id: id,
        firstName: $(firstname).val(),
        middleName: $(middleName).val(),
        lastName: $(lastname).val(),
        dob: $(dob).datepicker('getDate'),
        nationality: $(nationality).val(),
        mobilePhone: $(mobilePhone).val(),
        sex: $('input[name=radSex]:checked').val(),
        remark: $(remark).val()
    });
    ShowProgress();

    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateSimplePatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                $("#txtPatient").tokenInput("clear");
                $("#txtPatient").tokenInput("add", { id: obj.data[0].PatientCode, PatientInfo: obj.data[0].PatientInfo });

                // Set new value for patient's info in appointment
                CurrentAppointment.PatientInfo = obj.data[0].PatientInfo;
                CurrentAppointment.PatientCode = obj.data[0].PatientCode;

                $(dialog).dialog("close");
            } else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            CloseProgress();
            ShowDialog("", "", "Cannot update patient's info. Please contact Administrator", "");
        }
    });
}
/****************************Patient - End******************************/

/****************************Appointment - Start******************************/

// Save moved, resize appointment
function MoveAppointment(eventId, objEvent) {
    var requestdata = JSON.stringify({ id: eventId, startTime: objEvent.start_date
                , endTime: objEvent.end_date, doctorId: objEvent.section_id
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/MoveAppointment",
        async: true,
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);

            if (obj.result != "true") {
                ShowDialog("", "", obj.message, "");
                alert("Move Appointment");
                scheduler.deleteEvent(eventId);
                scheduler.addEvent(CurrentAppointment);
            }
        },
        fail: function() {
            ShowDialog("", "", "Unknow error!", "");
            alert("Move Appointment2");
            scheduler.deleteEvent(eventId);
            scheduler.addEvent(CurrentAppointment);
        },
        complete: function() {
            CurrentAppointment = null; // set null after use
        }
    });
}

function LoadAppointment(mode, date) {
    var requestdata = JSON.stringify({ mode: mode, date: date });
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetAppointments",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                // Xoa cac appointment hien tai
                scheduler.clearAll();
                var evs = obj.data;
                if (evs) {
                    // Goi ham bind tab va tao unit view, timeline view
                    BuildTabs(evs.Sections, mode);

                    if (mode == "month") {
                        AddTimespanMonth(evs.Events);
                    } else {
                        AddAppointment(evs.Events);
                    }
                }
            }
            else {
                ShowMessage(obj.message);
            }
        },
        fail: function() {
            ShowMessage("Unknow error!");
        },
        complete: function() {
            isUsing = false;
        }
    });
}

// Add new appointment
function NewAppointment() {
    // Check if patient is not selected
    var patient = $("#txtPatient").tokenInput("get");
    if (patient.length <= 0) {
        ShowDialog("", "", "You must choose patient.", "");
        $("#txtPatient").focus();
        return;
    }

    ShowProgress();
    DisableAllElements($("#tblContent"), false);

    var requestdata = JSON.stringify({
        patientCode: patient[0].id,
        note: $("#txtNote").val(),
        startTime: $("#cboFromHour").val(),
        endTime: $("#cboToHour").val(),
        startDate: $("#txtFromDate").datepicker("getDate"),
        endDate: $("#txtToDate").datepicker("getDate"),
        doctorId: $("#hdfDoctor").val(),
        roomId: $("#cboRoom").val(),
        status: $("[id$=cboStatus]").val()
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/NewAppointment",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                AddAppointment(obj.data);
                $("#RosterForm").dialog("close");
            }
            else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            CloseProgress();
            ShowDialog("", "", "Unknow error!", "");
        },
        complete: function() {
            DisableAllElements($("#tblContent"), true);
        }
    });
}

function UpdateAppointment(id) {
    // Check if patient is not selected
    var patient = $("#txtPatient").tokenInput("get");
    if (patient.length <= 0) {
        ShowDialog("", "", "You must choose patient.", "");
        $("#txtPatient").focus();
        return;
    }

    DisableAllElements($("#tblContent"), false);
    ShowProgress();

    var requestdata = JSON.stringify({
        id: id,
        patientCode: patient[0].id,
        note: $("#txtNote").val(),
        startTime: $("#cboFromHour").val(),
        endTime: $("#cboToHour").val(),
        startDate: $("#txtFromDate").datepicker("getDate"),
        endDate: $("#txtToDate").datepicker("getDate"),
        doctorId: $("#hdfDoctor").val(),
        roomId: $("#cboRoom").val(),
        status: $("[id$=cboStatus]").val()
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateAppointment",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                var evs = obj.data;
                scheduler.deleteEvent(id);
                AddAppointment(evs);
                ShowDialog("", "", "Appointment has been updated.", "");
                $("#RosterForm").dialog("close");
            }
            else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            CloseProgress();
            ShowDialog("", "", "Unknow error!", "");
        },
        complete: function() {
            DisableAllElements($("#tblContent"), true);
        }
    });
}

function AddAppointment(evs) {
    $.each(evs, function(i, item) {
        scheduler.addEvent(item);
    });
}

// Add roster into scheduler
function AddTimespanMonth(evs) {
    $.each(evs, function(i, item) {
        var date = new Date(item.Date);
        var options = {
            start_date: date,
            end_date: scheduler.date.add(date, 1, "day"),
            type: "dhx_time_block", /* creating events on those dates will be disabled - dates are blocked */
            css: "have_roster",
            html: "Have Appointment"
        };
        scheduler.addMarkedTimespan(options);

        if (date < new Date())
            scheduler.addMarkedTimespan({
                start_date: date,
                end_date: scheduler.date.add(date, 1, "day"),
                css: 'small_lines_section',
                type: "dhx_time_block"
            });
    });
    scheduler.updateView();
}

// Ham tao tabs va tao unit view, timeline view
function BuildTabs(sections, mode) {
    // Lay id cua tab dang duoc select [neu co]
    var selectedId = $('#service-tabs li.ui-tabs-selected').attr('id');

    // Bien chua doi tuong selected tab tam
    var tmpSelectedItem = { Doctors: [] };

    // Lay doi tuong chua tab sau do remove tat ca tab
    var $tabs = $('#service-tabs');
    $('li', $tabs).remove();

    // Append cai moi
    $.each(sections, function(i, item) {
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
        $.each(tmpSelectedItem.Doctors, function(i, doctor) {
            // Bien luu nhung khoang thoi gian available, dung de tao block time
            var availableTime = [];
            var day = new Date();
            if (doctor.Rosters.length) {
                $.each(doctor.Rosters, function(j, roster) {
                    var startTime = new Date(roster.startTime);
                    var endTime = new Date(roster.endTime);
                    scheduler.addMarkedTimespan({
                        start_date: startTime,
                        end_date: endTime,
                        bcolor: roster.color,
                        sections: {
                            timeline: doctor.key, // list of sections
                            unit: doctor.key
                        }
                    });
                    availableTime.push(startTime.getHours() * 60 + startTime.getMinutes());
                    availableTime.push(endTime.getHours() * 60 + endTime.getMinutes());
                    day = new Date(startTime.getFullYear(), startTime.getMonth(), startTime.getDate());
                });
            }
            // Block toan bo thoi gian trong ngay
            scheduler.addMarkedTimespan({
                days: day,
                zones: availableTime,
                invert_zones: true,
                type: "dhx_time_block",
                sections: {
                    timeline: doctor.key, // list of sections
                    unit: doctor.key
                }
            });
        });
    }

    // Bind event cho link
    $('li a', $tabs).click(function() {
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
    scheduler.updateView();
}

// Cancel appointment
function CancelAppointment() {
    $(".token-input-dropdown").css("z-index", 0); // Bring to front
    scheduler.endLightbox(false, html("RosterForm"));
    isLightbox = false;
}

function DeleteAppointment(id) {
    // Display confirmation box ask delete appointment
    $("#spanMessage-content").html("Do you want to delete this appointment?");
    $("#dialog-message-title").attr("title", "Confirmation");
    $("#dialog-message-title").dialog({
        resizable: false,
        modal: false,
        buttons: {
            No: function() {
                $(this).dialog("close");
            },
            Yes: function() {
                $(this).dialog("close");

                var ev = scheduler.getEvent(id);
                if (ev.isnew == false) {
                    DisableAllElements($("#tblContent"), false);
                    ShowProgress();
                    var requestdata = JSON.stringify({ id: ev.id });
                    $.ajax({
                        type: "POST",
                        url: "Default.aspx/DeleteAppointment",
                        data: requestdata,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function(response) {
                            CloseProgress();
                            var obj = JSON.parse(response.d);
                            if (obj.result == "true") {
                                DeleteAppointment("UpdateAppointment");
                                scheduler.deleteEvent(ev.id);
                                ShowDialog("", "", "Appointment has been deleted.", "");
                                $("#RosterForm").dialog("close");
                            } else {
                                ShowDialog("", "", obj.message, "");
                            }
                        },
                        fail: function() {
                            CloseProgress();
                            ShowDialog("", "", "Unknow error!", "");
                        },
                        complete: function() {
                            DisableAllElements($("#tblContent"), true);
                        }
                    });
                }
            }
        }
    });
}
/****************************Appointment - End******************************/

/****************************Initialize - Start******************************/
$(document).ready(function() {
    BindTime();

    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        dateFormat: "m/d/yy",
        selectOtherMonths: true
    });

    var currentMonth = new Date();
    var nextMonth = new Date(currentMonth.getFullYear(), currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker({
        showOn: "button",
        buttonImage: "../../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "mm-yy",
        minDate: nextMonth
    });

    initEvents();
});

function DisableAllElements(obj, disabled) {
    if (disabled) {
        $("input, textarea, select", obj).removeAttr('disabled');
    }
    else {
        $("input, textarea, select", obj).attr('disabled', true);
    }
}

function initEvents() {
    $(".dhx_cal_today_button:not(#btnToday)").click(function() {
        $("#btnToday").click();
        scheduler.updateCalendar(miniCalendar, scheduler._date);
    });

    $("#cboFromHour, #txtFromDate, #cboToHour, #txtToDate").change(function() {
        GetRoom();
    });
}
/****************************Initialize - End******************************/

/****************************Methods - Start******************************/
function updateTips(t) {
    $(".validateTips")
				    .text(t)
				    .addClass("ui-state-highlight");
    setTimeout(function() {
        $(".validateTips").removeClass("ui-state-highlight", 1500);
    }, 500);
}

function checkLength(o, n, min, max) {
    if (o.val().length > max || o.val().length < min) {
        o.addClass("ui-state-error");
        updateTips("Length of " + n + " must be between " +
					    min + " and " + max + ".");
        return false;
    } else {
        return true;
    }
}

function checkRegexp(o, regexp, n) {
    if (!(regexp.test(o.val()))) {
        o.addClass("ui-state-error");
        updateTips(n);
        return false;
    } else {
        return true;
    }
}
/****************************Methods - End******************************/

/****************************Events - Start******************************/
$("[id$=createUser]").click(function() {
    $("#dialog:ui-dialog").dialog("destroy");

    var txtFirstName = $("#txtFirstName"),
        txtMiddileName = $("#txtMiddileName"),
        txtLastName = $("#txtLastName"),
        txtDob = $("#txtDob"),
        txtNationality = $("#txtNationality"),
        txtRemark = $("#txtRemark"),
        radSex = $("input[name=radSex]"),
        txtMobilePhone = $("#txtMobilePhone"),
        allFields = $([]).add(name).add(txtFirstName).add(txtLastName).add(radSex).add(txtMobilePhone).add(txtMiddileName).add(txtDob)
            .add(txtNationality).add(txtRemark);

    var tmpForm = $('#RosterForm').parent();
    $(tmpForm).hide();

    $("#dialog-form").dialog({
        autoOpen: true,
        height: 320,
        width: 600,
        modal: false,
        zIndex: $.maxZIndex() + 1,
        resizable: false,
        buttons: {
            Cancel: function() {
                $(this).dialog("close");
            },
            Create: function() {
                var bValid = true;
                allFields.removeClass("ui-state-error");

                if (bValid) {
                    CreateSimplePatient(txtFirstName, txtMiddileName, txtLastName, txtDob
                        , txtNationality, txtRemark, txtMobilePhone, this);
                }
            }
        },
        close: function() {
            $(".token-input-dropdown").css("z-index", $.maxZIndex() + 10); // Bring to front
            $(tmpForm).show();
            allFields.val("").removeClass("ui-state-error");
        }
    });

    $(txtFirstName).val("");
    $(txtLastName).val("");
    $(txtMobilePhone).val("");
    $(txtMiddileName).val("");
    $(txtDob).val("");
    $(txtNationality).val("");
    $(txtRemark).val("");
    $("#radMale").val("M");
    $("#radFemale").val("F");
    $("#radMale").attr("checked", "checked");
});

$("[id$=changeUser]").click(function() {
    $("#dialog:ui-dialog").dialog("destroy");

    // Check if patient is not selected
    var patient = $("#txtPatient").tokenInput("get");
    if (patient.length <= 0) {
        ShowDialog("", "", "You must choose patient.", "");
        $("#txtPatient").focus();
        return;
    }

    var txtFirstName = $("#txtFirstName"),
        txtMiddileName = $("#txtMiddileName"),
        txtLastName = $("#txtLastName"),
        txtDob = $("#txtDob"),
        txtNationality = $("#txtNationality"),
        txtRemark = $("#txtRemark"),
        radSex = $("input[name=radSex]"),
        txtMobilePhone = $("#txtMobilePhone"),
        allFields = $([]).add(name).add(txtFirstName).add(txtLastName).add(radSex).add(txtMobilePhone).add(txtMiddileName).add(txtDob)
            .add(txtNationality).add(txtRemark);

    ShowProgress();
    var requestdata = JSON.stringify({ id: patient[0].id });

    $.ajax({
        type: "POST",
        url: "Default.aspx/GetPatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            CloseProgress();
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                var tmpForm = $('#RosterForm').parent();
                $(tmpForm).hide();

                $("#dialog-form").dialog({
                    autoOpen: true,
                    height: 320,
                    width: 600,
                    modal: false,
                    zIndex: $.maxZIndex() + 1,
                    resizable: false,
                    buttons: {
                        Cancel: function() {
                            $(this).dialog("close");
                        },
                        Save: function() {
                            var bValid = true;
                            allFields.removeClass("ui-state-error");

                            if (bValid) {
                                UpdateSimplePatient(patient[0].id, txtFirstName, txtMiddileName, txtLastName, txtDob
                                    , txtNationality, txtRemark, txtMobilePhone, this);
                            }
                        }
                    },
                    close: function() {
                        $(".token-input-dropdown").css("z-index", $.maxZIndex() + 10); // Bring to front
                        $(tmpForm).show();
                        allFields.val("").removeClass("ui-state-error");
                    }
                });

                $(txtFirstName).val(obj.data[0].FirstName);
                $(txtMiddileName).val(obj.data[0].MiddleName);
                $(txtLastName).val(obj.data[0].LastName);
                $(txtMobilePhone).val(obj.data[0].CellPhone);
                $(txtDob).datepicker('setDate', obj.data[0].DateOfBirth);
                $(txtNationality).val(obj.data[0].Nationality);
                $(txtRemark).val(obj.data[0].Remark);
                $("#radMale").val("M");
                $("#radFemale").val("F");
                if (obj.data[0].Sex == "M") {
                    $("#radMale").attr("checked", "checked");
                    $("#radFemale").removeAttr("checked");
                } else {
                    $("#radFemale").attr("checked", "checked");
                    $("#radMale").removeAttr("checked");
                }

            } else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            CloseProgress();
            ShowDialog("", "", "Unknow error!", "");
        }
    });
});
/****************************Events - End******************************/
