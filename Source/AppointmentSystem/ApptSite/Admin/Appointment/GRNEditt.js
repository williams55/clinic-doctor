var CurrentEventId = "-1";
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

// Init Schedule
function initSchedule(weekday) {
    var currentDate = new Date();

    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.mark_now = true;

    var arrAjax = [];
    $.each(floors, function(i, item) {
        scheduler.locale.labels[item.Id + "_tab"] = item.Title;
        arrAjax.push($.ajax({
            type: "POST",
            url: "Default.aspx/GetRoomByFloor",
            data: "{floorId: '" + item.Id + "'}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];

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
        // OnClick event, get patient's info
        function blockReadonly(id) {
            GetPatientInfo(id);
            if (!id) return true;
            return this.getEvent(id).ReadOnly == "false";
        }

        scheduler.attachEvent("onBeforeDrag", blockReadonly);
        scheduler.attachEvent("onClick", blockReadonly);

        // When event changed, check and update
        scheduler.attachEvent("onBeforeEventChanged", function(event_object, native_event, is_new) {
            // If update roster
            if (is_new == false) {
                if (event_object.start_date <= new Date()) {
                    alert("You can not change appointment to passed or current date.");
                    return false;
                }
            }
            return true;
        });

        scheduler.attachEvent("onViewChange", function(mode, date) {
            LoadRoster(mode, date);
        });

        // Save roster when resized or moved
        scheduler.attachEvent("onEventChanged", function(event_id, event_object) {
            //any custom logic here
            var _id = event_object.id;
            var _doctorUserName = event_object.DoctorUserName;
            var _rosterType = event_object.RosterTypeId;
            var _rosterTitle = event_object.RosterTypeTitle;
            var _fromTime = event_object.start_date;
            var _toTime = event_object.end_date;
            var _note = event_object.note;
            var _room = event_object.section_id;

            var requestdata = JSON.stringify({ Id: _id, DoctorUsername: _doctorUserName, StartTime: _fromTime
                , EndTime: _toTime, RoomId: _room
            });
            $.ajax({
                type: "POST",
                url: "Default.aspx/UpdateEventMove",
                async: true,
                data: requestdata,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function(response) {
                    var obj = eval(response.d)[0];

                    var evs = obj.data;

                    if (obj.result == "false") {
                        alert(obj.message);
                    }

                    scheduler.deleteEvent(_id);
                    addRoster(evs);
                },
                fail: function() {
                    alert("Unknow error!");
                    return false;
                },
                complete: function() {
                }
            });
        });

        dhtmlxEvent(document, (_isOpera ? "keypress" : "keydown"), function(e) {

            if (e.keyCode == 37) {
                //left
                scheduler.matrix.timeline.x_start -= 1;
                if (scheduler.matrix.timeline.x_start < 0)
                    scheduler.matrix.timeline.x_start = 0;
                scheduler.callEvent("onOptionsLoad", []);
            } else if (e.keyCode == 39) {
                //right
                scheduler.matrix.timeline.x_start += 1;
                if (scheduler.matrix.timeline.x_start > 24)
                    scheduler.matrix.timeline.x_start = 24;
                scheduler.callEvent("onOptionsLoad", []);
            }
        });

        // Load roster
        if (floors.length > 0) {
            scheduler.init('scheduler_here', currentDate, floors[0].Id);
            //LoadRoster(floors[0].Id, date);
            ShowMinical();
        }
    });
}

scheduler.showLightbox = function(id) {
    // Clear all value
    $("#hdId").val("");
    $("#tdTitle").text("New appointment");
    $("#txtNote").val("");
    $("#txtPatient").tokenInput("clear");
    $("#txtDoctor").tokenInput("clear");
    $("#txtRoom").tokenInput("clear");

    // Get current appointmentId
    var ev = scheduler.getEvent(id);
    CurrentEventId = id;
    if (ev.start_date < new Date()) {
        if (ev.isnew == "false") {
            alert("You cannot change a passed appointment.");
        } else {
            scheduler.deleteEvent(id);
            alert("You cannot create new appointment a passed day.");
        }
        return false;
    }

    // Set room, do not get info
    SetTime(ev);
    // Set init value
    $("#txtFromDate").datepicker("setDate", new Date(ev.start_date.getFullYear(), ev.start_date.getMonth(), ev.start_date.getDate()));
    $("#txtToDate").datepicker("setDate", new Date(ev.end_date.getFullYear(), ev.end_date.getMonth(), ev.end_date.getDate()));

    var arrAjax = [];
    $("#hdId").val(id);
    // Load data
    var requestdata;
    if (ev.isnew == "false") {
        // Update case, get info from server
        $("#tdTitle").text("Edit Appointment");
        requestdata = JSON.stringify({ appointmentId: id });
        arrAjax.push($.ajax({
            type: "POST",
            url: "Default.aspx/GetAppointment",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];
                if (obj.result == "true") {
                    var data = obj.data;
                    if (data) {
                        $("#txtPatient").tokenInput("add", { id: data.PatientId, propertyToSearch: data.PatientInfo });
                        $("#txtDoctor").tokenInput("add", { id: data.DoctorUserName, propertyToSearch: data.DoctorInfo });
                        $("#txtRoom").tokenInput("add", { id: data.RoomId, propertyToSearch: data.RoomInfo });
                        $("#txtNote").val(data.note);
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
    }
    else {
        // Get room id then call server to get room info
        if (ev.section_id) {
            requestdata = JSON.stringify({ roomId: ev.section_id });
            arrAjax.push($.ajax({
                type: "POST",
                url: "Default.aspx/GetRoom",
                data: requestdata,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function(response) {
                    var obj = eval(response.d)[0];
                    if (obj.result == "true") {
                        $("#txtRoom").tokenInput("add", obj.data);
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
        }
    }

    $.when.apply($, arrAjax).done(function() {
        scheduler.startLightbox(id, html("RosterForm"));
    });
    return true;
};
/****************************DHTMLX - End******************************/

/****************************Form - Start******************************/
function buildURIDoctor() {
    return 'Default.aspx/SearchDoctor?fromDate=' + $("#txtFromDate").val()
                        + '&toDate=' + $("#txtToDate").val()
                        + '&fromTime=' + $("#cboFromHour").val()
                        + '&toTime=' + $("#cboToHour").val()
                        + '&appointmentId=' + CurrentEventId;
}

function buildURIRoom() {
    var doctorUsername = "";
    var doctor = $("#txtDoctor").tokenInput("get");
    if (doctor.length > 0) {
        doctorUsername = doctor[0].id;
    }
    return 'Default.aspx/SearchRoom?fromDate=' + $("#txtFromDate").val()
                        + '&toDate=' + $("#txtToDate").val()
                        + '&fromTime=' + $("#cboFromHour").val()
                        + '&toTime=' + $("#cboToHour").val()
                        + '&doctorUserName=' + doctorUsername
                        + '&appointmentId=' + CurrentEventId;
}

// Init input token for searching patient, doctor, room
function GetToken() {
    $("#txtPatient").tokenInput(
        'Default.aspx/SearchPatient',
        {
            theme: "facebook",
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

    $("#txtDoctor").tokenInput(
        buildURIDoctor,
        {
            theme: "facebook",
            preventDuplicates: true,
            tokenLimit: 1,
            resultsFormatter: function(item) { return "<li>" + item.propertyToSearch + "</li>"; },
            tokenFormatter: function(item) { return "<li>" + item.propertyToSearch + "</li>"; },
            propertyToSearch: "propertyToSearch",
            noResultsText: "Cannot find doctor",
            searchingText: "Searching...",
            hintText: "Shortname, Specialty",
            onAdd: function() {
                GetRoom();
            }
        }
    );

    $("#txtRoom").tokenInput(
        buildURIRoom,
        {
            theme: "facebook",
            preventDuplicates: true,
            tokenLimit: 1,
            resultsFormatter: function(item) { return "<li>" + item.propertyToSearch + "</li>"; },
            tokenFormatter: function(item) { return "<li>" + item.propertyToSearch + "</li>"; },
            propertyToSearch: "propertyToSearch",
            noResultsText: "Cannot find room",
            searchingText: "Searching...",
            hintText: "Room name"
        }
    );
}

// Get a room fill into room token input after choose a doctor
function GetRoom() {
    var doctorId = "";
    var doctor = $("#txtDoctor").tokenInput("get");
    if (doctor.length > 0) {
        doctorId = doctor[0].id;
    }

    var requestdata = JSON.stringify({ appointmentId: CurrentEventId, doctorUserName: doctorId,
        startTime: $("#cboFromHour").val(), endTime: $("#cboToHour").val(),
        startDate: $("#txtFromDate").val(), endDate: $("#txtToDate").val()
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetRoom",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];

            if (obj.result == "true") {
                var evs = obj.data;
                if (evs) {
                    $("#txtRoom").tokenInput("add", obj.data); ;
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
    var requestdata = JSON.stringify({ appointmentId: currentId });
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetPatientByAppointmentId",
        data: requestdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                if (obj.data) {
                    var arr = eval(obj.data)[0]; // Get first item
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

// Create new patient
function CreateSimplePatient(radSex, firstname, lastname, cellPhone, adddress, dialog) {
    var requestdata = JSON.stringify({ FirstName: $(firstname).val(), LastName: $(lastname).val(), CellPhone: $(cellPhone).val()
        , Address: $(adddress).val(), IsFemale: $(radSex).val()
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/CreateSimplePatient",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                $("#txtPatient").tokenInput("clear");
                $("#txtPatient").tokenInput("add", obj.data);
                $(dialog).dialog("close");
            } else {
                updateTips(obj.message);
            }
        },
        fail: function() {
            updateTips("Error");
        },
        complete: function() {
        }
    });
}
/****************************Patient - End******************************/

function LoadRoster(mode, date) {
    var requestdata = JSON.stringify({ mode: mode, currentDateView: date });
    scheduler.clearAll();
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetAppointments",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];

            if (obj.result == "true") {
                var evs = obj.data;
                if (evs) {
                    addRoster(evs);
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
    var _room = $("#txtRoom").tokenInput("get");
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
        // Add new 
        $("#dialog-modal").show();
        DisableAllElements($("#tblContent"), false);

        requestdata = JSON.stringify({
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
        $.ajax({
            type: "POST",
            url: "Default.aspx/SaveEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = eval(response.d)[0];
                if (obj.result == "true") {
                    var evs = obj.data;
                    addRoster(evs);
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
                addRoster(evs);
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

function addRoster(evs) {
    $.each(evs, function(i, item) {
        $.each(scheduler._props[scheduler._mode].options, function(j, option) {
            if (option.key == item.section_id)
                scheduler.addEvent(item);
        });
    });
}

function CancelRoster() {
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

    $("input, textarea").addClass("idle");
    $("input, textarea").focus(function() {
        $(this).addClass("activeField").removeClass("idle");
    }).blur(function() {
        $(this).removeClass("activeField").addClass("idle");
    });

    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "yy-mm-dd"
    });

    var _currentMonth = new Date();
    var _nextMonth = new Date(_currentMonth.getFullYear(), _currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker({
        showOn: "button",
        buttonImage: "../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "mm-yy",
        minDate: _nextMonth
    });

    initEvents();
});

//
function InitForm() {
    $("#dialog-modal").hide();

    $("#cboFromHour").show();
    $('#spanFromDate').show();

    $("#cboToHour").show();
    $('#spanToDate').show();

    // Set empty
    $("#hdId").val("");
    $("#tdTitle").text("New appointment");
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
    $("#btnSave").click(function() {
        SaveRoster();
    });

    $("#btnCancel").click(function() {
        CancelRoster();
    });

    $("#btnDelete").click(function() {
        DeleteRoster();
    });

    $("#btnDelete").keydown(function(e) {
        var key;

        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 9) {
            $("#chkNewPatient").focus();
            return false;
        }
    });

    $("#cboFromHour, #txtFromDate").change(function() {
        if (!checkDateTime()) {
            $("#cboToHour").val($("#cboFromHour").val());
            $("#txtToDate").val($("#txtFromDate").val());
        }

        $("#cboStaff").change();
        GetDoctorList();
    });

    $("#cboToHour, #txtToDate").change(function() {
        if (!checkDateTime()) {
            $("#cboFromHour").val($("#cboToHour").val());
            $("#txtFromDate").val($("#txtToDate").val());
        }

        $("#cboStaff").change();
        GetDoctorList();
    });

    // Call Load room function
    $("#cboStaff").change(function() {
        loadRooms();
    });

}