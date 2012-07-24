var CurrentEventId = "-1";
var CurrentAppointment;
var miniCalendar;

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

// Refesh current view for mark_now can auto update
setInterval(function() { scheduler.setCurrentView(); }, 60000);

// Init Schedule
function initSchedule(weekday) {
    var currentDate = new Date();

    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.mark_now = true;

    var arrAjax = [];
    $.each(floors, function(i, item) {
        scheduler.locale.labels[item.Id + "_tab"] = item.ShortTitle;
        arrAjax.push($.ajax({
            type: "POST",
            url: "Default.aspx/GetDoctorList",
            data: "{serviceId: '" + item.Id + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = JSON.parse(response.d);

                if (obj.result == "true") {
                    var elements = obj.data;
                    if (elements) {
                        scheduler.createUnitsView({
                            name: item.Id,
                            property: "section_id",
                            list: elements
                        });
                    }
                } else {
                    alert(obj.message);
                }
            },
            fail: function() {
                alert("Unknow error!");
            },
            complete: function() {
            }
        }));
    });
    $.when.apply($, arrAjax).done(function() {
        scheduler.attachEvent("onBeforeDrag", BeforeDrag);
        scheduler.attachEvent("onClick", BlockReadonly);
        scheduler.attachEvent("onEventChanged", EventChanged);

        scheduler.attachEvent("onViewChange", function(mode, date) {
            LoadAppointment(mode, date);
        });

        // Load roster
        if (floors.length > 0) {
            scheduler.init('scheduler_here', currentDate, floors[0].Id);
            //LoadRoster(floors[0].Id, date);
            ShowMinical();
        }
    });
}
/****************************DHTMLX - End******************************/

/****************************Scheduler - Start******************************/
// Set readonly property for event
// Get patient's info and fill in form info
function BlockReadonly(id) {
    GetPatientInfo(id);
    if (!id) return true;
    return !scheduler.getEvent(id).readonly;
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
    if (scheduler._drag_mode && scheduler._drag_mode == 'move') {
        MoveAppointment(eventId, objEvent);
    }
}

scheduler.showLightbox = function(id) {
    // Get current appointmentId
    var ev = scheduler.getEvent(id);
    CurrentEventId = id;

    // Clear all value
    $("#hdId").val("");
    $("#txtNote").val("");
    $("#lblDoctor").val("");
    $("#txtPatient").tokenInput("clear");

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

    $("#hdId").val(id);

    // Get available room list
    GetRoom();

    if (ev.start_date < new Date()) {
        if (ev.isnew == "false") {
            alert("You cannot change a passed appointment.");
        } else {
            scheduler.deleteEvent(id);
            alert("You cannot create new appointment a passed day.");
        }
        return false;
    }

    // Load data
    if (ev.isnew == false) {
        // Update case, get info from server
        //        $("#RosterForm").dialog({
        //            autoOpen: false,
        //            height: 320,
        //            width: 550,
        //            modal: true,
        //            zIndex: $.maxZIndex() + 1,
        //            resizable: false,
        //            buttons: {
        //                Delete: function() {
        //                    DeleteRoster(id);
        //                },
        //                Cancel: function() {
        //                    $(this).dialog("close");
        //                },
        //                Save: function() {
        //                    UpdateRoster();
        //                }
        //            },
        //            close: function() {
        //                CancelAppointment();
        //            }
        //        });

        //        requestdata = JSON.stringify({ appointmentId: id });
        //        arrAjax.push($.ajax({
        //            type: "POST",
        //            url: "Default.aspx/GetAppointment",
        //            data: requestdata,
        //            dataType: "json",
        //            contentType: "application/json; charset=utf-8",
        //            success: function(response) {
        //                var obj = eval(response.d)[0];
        //                if (obj.result == "true") {
        //                    var data = obj.data;
        //                    if (data) {
        //                        $("#txtPatient").tokenInput("add", { id: data.PatientId, propertyToSearch: data.PatientInfo });
        //                        $("#txtDoctor").tokenInput("add", { id: data.DoctorUserName, propertyToSearch: data.DoctorInfo });
        //                        $("#txtRoom").tokenInput("add", { id: data.RoomId, propertyToSearch: data.RoomInfo });
        //                        $("#txtNote").val(data.note);
        //                    }
        //                } else {
        //                    alert(obj.message);
        //                }
        //            },
        //            fail: function() {
        //                alert("Unknow error!");
        //            },
        //            complete: function() {
        //            }
        //        }));
    }
    else {
        // New mode
        $("#RosterForm").dialog({
            autoOpen: false,
            height: 320,
            width: 550,
            modal: true,
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

    $("#RosterForm").dialog("open");
    $(".token-input-dropdown").css("z-index", $.maxZIndex() + 1); // Bring to front

    scheduler.startLightbox(id, html("RosterForm"));
    return true;
};
/****************************Scheduler - End******************************/

/****************************Form - Start******************************/
// Init input token for searching patient, doctor, room
function GetToken() {
    $("#txtPatient").tokenInput(
        'Default.aspx/SearchPatient',
        {
            preventDuplicates: true,
            tokenLimit: 1,
            resultsFormatter: function(item) { return "<li>" + item.propertyToSearch + "</li>"; },
            tokenFormatter: function(item) { return "<li>" + item.FirstName + " " + item.LastName + "</li>"; },
            propertyToSearch: "propertyToSearch",
            noResultsText: "Cannot find patient",
            searchingText: "Searching...",
            hintText: "Name, Birthday, Address, Phone"
        }
    );
}

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
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Unknow error!");
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
    if (currentId) {
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
                        $("#divFirstname").html(arr.FirstName);
                        $("#divLastname").html(arr.LastName);
                        $("#divCellPhone").html(arr.CellPhone);
                        $("#divBirthday").html(arr.Birthday);
                        $("#divAddress").html(arr.Address);
                    }
                }
                else {
                    alert(obj.message);
                }
                blStaff = true;
            },
            fail: function() {
                alert("Cannot load Patient's info. Please try again or contact Administrator.");
            },
            complete: function() {
            }
        });
    }
}

// Create new patient
function CreateSimplePatient(firstname, lastname, cellPhone, adddress, dialog) {
    var requestdata = JSON.stringify({
        firstname: $(firstname).val(),
        lastname: $(lastname).val(),
        cellPhone: $(cellPhone).val(),
        address: $(adddress).val(),
        isFemale: $('input[name=radSex]:checked').val()
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/CreateSimplePatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                $("#txtPatient").tokenInput("clear");
                $("#txtPatient").tokenInput("add", obj.data[0]);
                $(dialog).dialog("close");
            } else {
                updateTips(obj.message);
            }
        },
        fail: function() {
            updateTips("Error");
        }
    });
}
/****************************Patient - End******************************/

/****************************Appointment - Start******************************/
// Save moved appointment
function MoveAppointment(eventId, objEvent) {
    var requestdata = JSON.stringify({ id: eventId, startTime: objEvent.start_date
                , endTime: objEvent.end_date, doctorId: objEvent.section_id
    });
    console.log(CurrentAppointment);
    console.log(objEvent);
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
                scheduler.deleteEvent(eventId);
                scheduler.addEvent(CurrentAppointment);
                CurrentAppointment = null; // set null after use
            }
        },
        fail: function() {
            ShowDialog("", "", "Unknow error!", "");
            scheduler.deleteEvent(eventId);
            scheduler.addEvent(CurrentAppointment);
            CurrentAppointment = null; // set null after use
        }
    });
}
/****************************Appointment - End******************************/

function LoadAppointment(mode, date) {
    var requestdata = JSON.stringify({ mode: mode, currentDateView: date });
    scheduler.clearAll();
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetAppointments",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);

            if (obj.result == "true") {
                var evs = obj.data;
                if (evs) {
                    AddAppointment(evs);
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
        }
    });
}

// Add new appointment
function NewAppointment() {
    // Check if patient is not selected
    var patient = $("#txtPatient").tokenInput("get");
    if (patient.length <= 0) {
        alert("You must choose patient.");
        $("#txtPatient").focus();
        return;
    }

    $("#dialog-modal").show();
    DisableAllElements($("#tblContent"), false);

    var requestdata = JSON.stringify({
        patientId: patient[0].id,
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
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                $("#RosterForm").dialog("close");
                scheduler.endLightbox(false, html("RosterForm"));
                AddAppointment(obj.data);
            }
            else {
                ShowDialog("", "", obj.message, "");
            }
        },
        fail: function() {
            ShowDialog("", "", "Unknow error!", "");
        },
        complete: function() {
            DisableAllElements($("#tblContent"), true);
            $("#dialog-modal").hide();
        }
    });
}

function AppointmentBusiness(patient) {
    var _id = $("#hdId").val();
    var ev = scheduler.getEvent(_id);
    var _fromTime = $("#cboFromHour").val();
    var _toTime = $("#cboToHour").val();
    var status = $("[id$=cboStatus]").val();
    var _fromDate = $("#txtFromDate").datepicker("getDate");
    var _toDate = $("#txtToDate").datepicker("getDate");
    var _note = $("#txtNote").val();
    var _doctor = $("#txtDoctor").tokenInput("get");
    var _doctorUserName = '';
    var _room = $("#cboToHour").val();
    var _roomId = '';

    if (_doctor.length > 0) _doctorUserName = _doctor[0].id;
    if (_room.length > 0) _roomId = _room[0].id;

    // Check datetime
    var _validateFromTime = _fromDate.format("yyyy/mm/dd ") + _fromTime;
    var _validateToTime = _toDate.format("yyyy/mm/dd ") + _toTime;
    if (_validateFromTime >= _validateToTime) {
        alert("From date, time must be less than to date, time");
        $("#cboFromHour").focus();
        return;
    }

    // Check if staff is not selected
    if (_doctorUserName == "") {
        alert("You must choose doctor.");
        return;
    }

    // Check if room is not selected
    if (_roomId == "") {
        alert("You must choose room.");
        return;
    }

    var requestdata;
    if (ev.isnew == "false") {
        // Update
        $("#dialog-modal").show();
        DisableAllElements($("#tblContent"), false);

        requestdata = JSON.stringify({
            Id: _id,
            PatientId: patient,
            Note: _note,
            StartTime: _fromTime,
            EndTime: _toTime,
            StartDate: _fromDate,
            EndDate: _toDate,
            DoctorUsername: _doctorUserName,
            RoomId: _roomId,
            Status: status
        });
        UpdateRoster(requestdata, 'UpdateEventSave', _id);
    }
    else {
    }
}

function SaveRoster() {
    // Check if patient is not selected
    var patient = $("#txtPatient").tokenInput("get");
    if (patient.length <= 0) {
        alert("You must choose patient.");
        $("#txtPatient").focus();
        return;
    }
    AppointmentBusiness(patient[0].id);
}

function UpdateRoster(requestdata, method, _id) {
    $.ajax({
        type: "POST",
        url: "Default.aspx/" + method,
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var evs = obj.data;
                scheduler.deleteEvent(_id);
                AddAppointment(evs);
                scheduler.endLightbox(false, html("RosterForm"));
            }
            else {
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
            DisableAllElements($("#tblContent"), true);
            $("#dialog-modal").hide();
        }
    });
}

function AddAppointment(evs) {
    $.each(evs, function(i, item) {
        scheduler.addEvent(item);
    });
}

// Cancel appointment
function CancelAppointment() {
    scheduler.endLightbox(false, html("RosterForm"));
}

function DeleteRoster() {
    if (!confirm("Do you want to delete this appointment?"))
        return;

    DisableAllElements($("#tblContent"), false);
    $("#dialog-modal").show();
    var ev = scheduler.getEvent($("#hdId").val());
    if (ev.isnew == "false") {
        var requestdata = JSON.stringify({ Id: ev.id });
        $.ajax({
            type: "POST",
            url: "Default.aspx/DeleteEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];
                if (obj.result == "true") {
                    alert("Appointment has been deleted.");
                    scheduler.deleteEvent(ev.id);
                    scheduler.endLightbox(false, html("RosterForm"));
                }
                else {
                    alert(obj.message);
                }
            },
            fail: function() {
                alert("Unknow error!");
            },
            complete: function() {
                DisableAllElements($("#tblContent"), true);
                $("#dialog-modal").hide();
            }
        });
    }
}

$(document).ready(function() {
    BindTime();

    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
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

    // Call init input token
    GetToken();

    $("#dialog:ui-dialog").dialog("destroy");

    var txtFirstName = $("#txtFirstName"),
                txtLastName = $("#txtLastName"),
                radSex = $("input[name=radSex]"),
                txtCellPhone = $("#txtCellPhone"),
                txtAddress = $("#txtAddress"),
                allFields = $([]).add(name).add(txtFirstName).add(txtLastName).add(radSex).add(txtCellPhone).add(txtAddress);

    $("#dialog-form").dialog({
        autoOpen: false,
        height: 350,
        width: 550,
        modal: true,
        zIndex: $.maxZIndex() + 1,
        resizable: false,
        buttons: {
            Cancel: function() {
                $(this).dialog("close");
            },
            "Create patient": function() {
                var bValid = true;
                allFields.removeClass("ui-state-error");

                //bValid = bValid && checkRegexp(txtFirstName, /^[a-z]([0-9a-z_])+$/i, "Firstname may consist of a-z.");
                //bValid = bValid && checkRegexp(txtLastName, /^[a-z]([0-9a-z_])+$/i, "Lastname may consist of a-z.");

                if (bValid) {
                    CreateSimplePatient(txtFirstName, txtLastName, txtCellPhone, txtAddress, this);
                }
            }
        },
        close: function() {
            allFields.val("").removeClass("ui-state-error");
        }
    });

    $("#create-user")
        .click(function() {
            $("#dialog-form").dialog("open");
            $(txtFirstName).val("");
            $(txtLastName).val("");
            $(txtCellPhone).val("");
            $(txtAddress).val("");
            $("#radMale").val("false");
            $("#radFemale").val("true");
            $("#radMale").attr("checked", "checked");
        });
});

// Initialize form, clear value, set default value
function InitForm() {
    $("#dialog-modal").hide();

    $("#cboFromHour").show();
    $('#spanFromDate').show();

    $("#cboToHour").show();
    $('#spanToDate').show();

    // Set empty
    $("#hdId").val("");
    $("#txtNote").val("");

    DisableAllElements($("#tblContent"), true);
}

function DisableAllElements(obj, disabled) {
    if (disabled) {
        $("input, textarea, select", obj).removeAttr('disabled');
    }
    else {
        $("input, textarea, select", obj).attr('disabled', true);
    }
}

function checkDateTime() {
    var _fromTime = $("#cboFromHour").val();
    var _toTime = $("#cboToHour").val();
    var _fromDate = $("#txtFromDate").datepicker("getDate");
    var _toDate = $("#txtToDate").datepicker("getDate");

    // Check datetime
    var _validateFromTime = _fromDate.format("yyyy/mm/dd ") + _fromTime;
    var _validateToTime = _toDate.format("yyyy/mm/dd ") + _toTime;
    if (_validateFromTime >= _validateToTime) {
        return false;
    }
    return true;
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
