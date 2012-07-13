var blRosterType = false;
var blStaff = false;

/****************************DHTMLX - Start******************************/
// Load calendar for Schedule
function ShowMinical() {
    if (scheduler.isCalendarVisible())
        scheduler.destroyCalendar();
    else
        scheduler.renderCalendar({
            position: "dhx_minical_icon",
            date: scheduler._date,
            navigation: true,
            handler: function(date, calendar) {
                scheduler.setCurrentView(date);
                scheduler.destroyCalendar();
            }
        });
}

// Refesh current view for mark_now can auto update
setInterval(function() { scheduler.setCurrentView(); }, 60000);

// Init Schedule
function initSchedule(weekday) {
    var currentDate = new Date();
    var currentMode = "timeline";

    scheduler.locale.labels.timeline_tab = "Timeline";
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.time_step = stepTime;
    scheduler.config.mark_now = true;

    var arrAjax = [];
    arrAjax.push($.ajax({
        type: "POST",
        url: "Default.aspx/GetDoctorTree",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = eval(response.d)[0];

            if (obj.result == "true") {
                var elements = obj.data;
                if (elements) {
                    scheduler.createTimelineView({
                        section_autoheight: false,
                        name: "timeline",
                        x_unit: "hour",
                        x_date: "%m/%d %H:%i",
                        x_step: 12,
                        x_size: 10,
                        x_start: 0,
                        x_length: 60,
                        y_unit: elements,
                        y_property: "section_id",
                        render: "tree",
                        folder_dy: 20,
                        dy: 60
                    });
                }
            } else {
                currentMode = "week";
                alert(obj.message);
            }
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
        }
    }));

    $.when.apply($, arrAjax).done(function() {
        scheduler.init('scheduler_here', currentDate, currentMode);

        scheduler.attachEvent("onBeforeDrag", BlockReadonly);
        scheduler.attachEvent("onClick", BlockReadonly);

        // When event changed, check and update
        scheduler.attachEvent("onBeforeEventChanged", ValidatingBeforeEventChanged);

        // Save roster when resized or moved
        scheduler.attachEvent("onEventChanged", ValidatingResizeMove);

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
        LoadRoster(currentMode, currentDate);
    });
}

/****************************DHTMLX - End******************************/

/****************************Scheduler - End******************************/
// Set readonly property for event
function BlockReadonly(id) {
    if (!id) return true;
    return !this.getEvent(id).readonly;
}

// Validate before changing event
function ValidatingBeforeEventChanged(event_object, native_event, is_new) {
    // If update roster
    if (is_new == false) {
        if (event_object.start_date <= new Date()) {
            alert("You can not change roster to passed or current date.");
            return false;
        }
    }
    return true;
}

// Validate when resize or move event
function ValidatingResizeMove(eventId, objEvent) {
    // Update roster
    $("#dialog-modal").show();
    DisableAllElements($("#tblContent"), false);
    var requestdata = JSON.stringify({ Id: objEvent.id, DoctorUsername: objEvent.section_id, RosterTypeId: objEvent.RosterTypeId
        , RosterTitle: objEvent.RosterTypeTitle, StartTime: objEvent.start_date, EndTime: objEvent.end_date, Note: objEvent.note
    });
    UpdateRoster(requestdata, 'UpdateEventMove', objEvent.id);
}

scheduler.showLightbox = function(id) {

    if (!blRosterType || !blRosterType) {
        scheduler.deleteEvent(id);
        alert("Loading data is not complete. Please wait for a few moment.");
        return false;
    }

    InitForm();

    var ev = scheduler.getEvent(id);

    if (ev.start_date <= new Date()) {
        if (ev.isnew == "false") {
            alert("You cannot change a passed roster.");
        }
        else {
            scheduler.deleteEvent(id);
            alert("You have to create new roster in the day after current date");
        }
        return false;
    }

    SetTime(ev);

    // Set init value
    $("#hdId").val(id);
    $("#txtFromDate").datepicker("setDate", ev.start_date);
    $("#txtToDate").datepicker("setDate", ev.end_date);
    var _currentMonth = new Date();
    var _nextMonth = new Date(_currentMonth.getFullYear(), _currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker("setDate", _nextMonth);
    $("#txtNote").val(ev.note);
    $("#hdStaff").val("");

    // Staff id
    if (ev.section_id) {
        $("#hdStaff").val(ev.section_id);
        $("#cboStaff").val(ev.section_id);
    }

    if (ev.isnew == "false") {
        $("#trRepeat").hide();
        $("#tdTitle").text("Edit roster");

        if (ev.RosterTypeId) {
            $("#cboRosterType").val(ev.RosterTypeId);
        }
    }

    $("#RosterForm").dialog({
        autoOpen: false,
        height: 450,
        width: 550,
        modal: true,
        zIndex: $.maxZIndex() + 1,
        resizable: false,
        buttons: {
            Delete: function() {
                DeleteRoster(id);
            },
            Cancel: function() {
                $(this).dialog("close");
            },
            Save: function() {
                SaveRoster();
            }
        },
        close: function() {
            CancelRoster();
        }
    });
    $("#RosterForm").dialog("open");

    scheduler.startLightbox(id, html("RosterForm"));
    return true;
};
/****************************Scheduler - End******************************/

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

function LoadRoster(mode, date) {
    var requestdata = JSON.stringify({ mode: mode, currentDateView: date });
    $.ajax({
        type: "POST",
        url: "Default.aspx/LoadRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
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

function loadRosterType() {
    // Load data
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetRosterType",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            var rosterType = eval(msg.d);
            $("#cboRosterType").find("option").remove();

            $.each(rosterType, function(i, item) {
                $("#cboRosterType").append('<option value="' + item.key + '">' + item.label + '</option>');
            });
            blRosterType = true;
        },
        fail: function() {
            alert("Cannot load Roster Type data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
        }
    });
}

function SetTime(ev) {
    var startTime = ev.start_date.format("HH:MM");
    var endTime = ev.end_date.format("HH:MM");
    $("#cboFromHour").val(startTime + ":00");
    $("#cboToHour").val(endTime + ":00");
}

function GetDoctorList() {
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetStaffs",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var obj = eval(response.d)[0];
            if (obj.result == "true") {
                var arr = eval(obj.data);
                $("#cboStaff").find("option").remove();

                $.each(arr, function(i, item) {
                    $("#cboStaff").append('<option value="' + item.key + '">' + item.label + '</option>');
                });

                $("#cboStaff").show();
            }
            else {
                alert(obj.message);
            }
            blStaff = true;
        },
        fail: function() {
            alert("Cannot load Doctor data. Please try again or contact Administrator.");
            scheduler.endLightbox(false, html("RosterForm"));
        },
        complete: function() {
        }
    });
}

function InitForm() {
    $("#dialog-modal").hide();

    $("#cboRosterType").show();
    $("#loadingRosterType").hide();

    $("#divWeekday").hide();
    $("#spanMonth").hide();

    $("#cboFromHour").show();
    $("#loadingFromHour").hide();
    $('#spanFromDate').show();

    $("#cboToHour").show();
    $("#loadingToHour").hide();
    $('#spanToDate').show();

    $("#trRepeat").show();

    // Set empty
    $("#chkRepeat").attr("checked", false);
    $("#hdId").val("");
    $("#tdTitle").text("New roster");
    $("#txtNote").val("");

    $("#cboStaff").focus();

    DisableAllElements($("#tblContent"), true);
}

function SaveRoster() {
    // Validate date time
    if ($("#chkRepeat").attr("checked")) {
        if ($("#cboFromHour").val() >= $("#cboToHour").val()) {
            alert("From time must be less than to time");
            return;
        }
    }
    else {
        var _validateFromTime = $("#txtFromDate").datepicker("getDate").format("yyyy/mm/dd ") + $("#cboFromHour").val();
        var _validateToTime = $("#txtToDate").datepicker("getDate").format("yyyy/mm/dd ") + $("#cboToHour").val();
        if (_validateFromTime >= _validateToTime) {
            alert("From date, time must be less than to date, time");
            return;
        }
    }

    var _id = $("#hdId").val();
    var ev = scheduler.getEvent(_id);
    var _rosterType = $("#cboRosterType").val();
    var _rosterTitle = $("#cboRosterType option:selected").text();
    var _fromTime = $("#cboFromHour").val();
    var _toTime = $("#cboToHour").val();
    var _fromDate = $("#txtFromDate").datepicker("getDate");
    var _toDate = $("#txtToDate").datepicker("getDate");
    var _note = $("#txtNote").val();
    var _doctorUserName = $("#cboStaff").val();
    var requestdata;

    // Check if staff is null
    if (_doctorUserName == null) {
        alert("You must choose staff.");
        return;
    }

    if (ev.isnew == "false") {
        // Update roster
        $("#dialog-modal").show();
        DisableAllElements($("#tblContent"), false);
        requestdata = JSON.stringify({ Id: _id, DoctorUsername: _doctorUserName, RosterTypeId: _rosterType, RosterTitle: _rosterTitle,
            StartTime: _fromTime, EndTime: _toTime, StartDate: _fromDate, EndDate: _toDate, Note: _note
        });
        UpdateRoster(requestdata, 'UpdateEventSave', _id);
    }
    else {
        // Add new roster
        var _repeat = 'false';
        var _weekday = '';
        var _month = '';

        if ($("#chkRepeat").attr("checked")) {
            var allVals = [];
            $("input[type=checkbox]:checked", "#spanWeekday").each(function() {
                allVals.push($(this).val());
            });

            if (allVals.length == 0) {
                alert("You have to choose a weekday at least.");
                return;
            }

            _repeat = 'true';
            _weekday = allVals.join(";");
            _month = $("#txtMonth").datepicker("getDate");
        }

        $("#dialog-modal").show();
        DisableAllElements($("#tblContent"), false);
        requestdata = JSON.stringify({ DoctorId: _doctorUserName, RosterTypeId: _rosterType, RosterTitle: _rosterTitle, StartTime: _fromTime, EndTime: _toTime,
            StartDate: _fromDate, EndDate: _toDate, Note: _note, RepeatRoster: _repeat, Weekday: _weekday, Month: _month
        });
        $.ajax({
            type: "POST",
            url: "Default.aspx/SaveEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = JSON.parse(response.d);
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
                $("#RosterForm").dialog("close");
            }
        });
    }
}

function UpdateRoster(requestdata, method, id) {
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
                scheduler.deleteEvent(id);
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
            $("#RosterForm").dialog("close");
        }
    });
}

function addRoster(evs) {
    $.each(evs, function(i, item) {
        scheduler.addEvent(item);
    });
}

function CancelRoster() {
    scheduler.endLightbox(false, html("RosterForm"));
}

function DeleteRoster(id) {
    if (!confirm("Do you want to delete this roster?"))
        return;

    var ev = scheduler.getEvent(id);
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
                    scheduler.deleteEvent(ev.id);
                    alert("Roster has been deleted.");
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
    scheduler.endLightbox(false, html("RosterForm"));
}

$(document).ready(function() {
    BindTime();
    GetDoctorList();
    loadRosterType();
    
    $("input, textarea").addClass("idle");
    $("input, textarea").focus(function() {
        $(this).addClass("activeField").removeClass("idle");
    }).blur(function() {
        $(this).removeClass("activeField").addClass("idle");
    });
    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        minDate: "+1D"
    });

    var _currentMonth = new Date();
    var _nextMonth = new Date(_currentMonth.getFullYear(), _currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker({
        showOn: "button",
        buttonImage: "../../resources/css/ui-lightness/images/calendar.gif",
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "mm/yy",
        minDate: _nextMonth
    });

    $("#spanWeekday").find("span").remove();
    $.each(weekday, function(i, item) {
        $("#spanWeekday").append('<span style="float:left; padding-right:5px; width:95px;"><input type="checkbox" value=' + item.label
            + ' id="chk' + item.key + '" /> ' + item.label + '</span>');
    });
    $("#chkRepeat").click(function() {
        $('#divWeekday').toggle();
        $('#spanMonth').toggle();

        $('#spanFromDate').toggle();
        $('#spanToDate').toggle();
    });

    $("#btnDelete").keydown(function(e) {
        var key;

        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 9) {
            $("#cboStaff").focus();
            return false;
        }
        return true;
    });

});

function DisableAllElements(obj, disabled) {
    if (disabled) {
        $("input, textarea, select", obj).removeAttr('disabled');
    }
    else {
        $("input, textarea, select", obj).attr('disabled', true);
    }
}
