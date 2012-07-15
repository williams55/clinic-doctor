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
            var obj = JSON.parse(response.d);

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
        scheduler.attachEvent("onEventChanged", EventChanged);

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

// Function will be raise when event changed

function EventChanged(eventId, objEvent) {
    // Case move event
    if (scheduler._drag_mode && scheduler._drag_mode == 'move') {
        MoveRoster(eventId, objEvent);
    }
}

scheduler.showLightbox = function(id) {

    if (!blRosterType || !blRosterType) {
        scheduler.deleteEvent(id);
        alert("Loading data is not complete. Please wait for a few moment.");
        return false;
    }

    $("#txtDoctor").tokenInput("clear");
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
    var currentMonth = new Date();
    var nextMonth = new Date(currentMonth.getFullYear(), currentMonth.getMonth() + 1, 1);
    $("#txtMonth").datepicker("setDate", nextMonth);
    $("#txtNote").val(ev.note);

    // Get doctor info and input into doctor field
    if (ev.section_id) {
        var requestdata = JSON.stringify({ doctorId: ev.section_id });
        $.ajax({
            type: "POST",
            url: "Default.aspx/GetDoctorById",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(response) {
                var obj = JSON.parse(response.d);
                if (obj.result == "true" && obj.data) {
                    $("#txtDoctor").tokenInput("add", { id: obj.data.DoctorId, DisplayName: obj.data.DisplayName });
                } else {
                    alert(obj.message);
                }
            },
            fail: function() {
                alert("Unknow error!");
            }
        });
    }

    // If it's edit mode
    if (ev.isnew == false) {
        $("#trRepeat").hide();

        if (ev.RosterTypeId) {
            $("#cboRosterType").val(ev.RosterTypeId);
        }
        
        $("#RosterForm").dialog({
            autoOpen: false,
            height: 300,
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
                    UpdateRoster();
                }
            },
            close: function() {
                CancelRoster();
            }
        });
    }
    else {
        // New mode
        $("#RosterForm").dialog({
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
                Save: function() {
                    NewRoster();
                }
            },
            close: function() {
                CancelRoster();
            }
        });
    }

    $("#RosterForm").dialog("open");
    $(".token-input-dropdown").css("z-index", $.maxZIndex() + 1); // Bring to front

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

/****************************Token input - End******************************/
// Init input token for searching patient, doctor, room
function GetToken() {
    $("#txtDoctor").tokenInput(
        'Default.aspx/SearchDoctor',
        {
            preventDuplicates: true,
            tokenLimit: 1,
            resultsFormatter: function(item) { return "<li>" + item.DisplayName + "</li>"; },
            tokenFormatter: function(item) { return "<li>" + item.DisplayName + "</li>"; },
            propertyToSearch: "DisplayName",
            noResultsText: "Cannot find doctor",
            searchingText: "Searching...",
            hintText: "Display name, Firstname, Lastname"
        }
    );
}
/****************************Token input - End******************************/

/****************************Roster - Start******************************/
// Load roster to scheduler
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
                    AddRoster(evs);
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

// Save a new roster
function NewRoster() {
    var rosterType = $("#cboRosterType").val();
    var fromTime = $("#cboFromHour").val();
    var toTime = $("#cboToHour").val();
    var fromDate = $("#txtFromDate").datepicker("getDate");
    var toDate = $("#txtToDate").datepicker("getDate");
    var note = $("#txtNote").val();
    var doctor = $("#txtDoctor").tokenInput("get");

    if (doctor.length <= 0) {
        alert("You must choose doctor.");
        return;
    }

    // Add new roster
    var repeat = 'false';
    var weekday = '';

    if ($("#chkRepeat").attr("checked")) {
        var allVals = [];
        $("input[type=checkbox]:checked", "#spanWeekday").each(function() {
            allVals.push($(this).val());
        });

        if (allVals.length == 0) {
            alert("You have to choose a weekday at least.");
            return;
        }

        repeat = 'true';
        weekday = allVals.join(";");
    }

    DisableAllElements($("#tblContent"), false);
    var requestdata = JSON.stringify({ doctorId: doctor[0].id, rosterTypeId: rosterType, startTime: fromTime, endTime: toTime,
        startDate: fromDate, endDate: toDate, note: note, repeatRoster: repeat, weekday: weekday
    });
    $.ajax({
        type: "POST",
        url: "Default.aspx/NewRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                var evs = obj.data;
                AddRoster(evs);

                $("#RosterForm").dialog("close");
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

// Save moved roster
function MoveRoster(eventId, objEvent) {
    var requestdata = JSON.stringify({ id: objEvent.id, doctorId: objEvent.section_id, startTime: objEvent.start_date, endTime: objEvent.end_date });
    $.ajax({
        type: "POST",
        url: "Default.aspx/MoveRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
            } else {
                alert(obj.message);
                scheduler.deleteEvent(eventId);
                AddRoster(obj.data);
            }
        },
        fail: function() {
            alert("Unknow error!");
        }
    });
}

// Update roster
function UpdateRoster() {
    var id = $("#hdId").val();
    var rosterType = $("#cboRosterType").val();
    var fromTime = $("#cboFromHour").val();
    var toTime = $("#cboToHour").val();
    var fromDate = $("#txtFromDate").datepicker("getDate");
    var toDate = $("#txtToDate").datepicker("getDate");
    var note = $("#txtNote").val();
    var doctor = $("#txtDoctor").tokenInput("get");

    if (doctor.length <= 0) {
        alert("You must choose doctor.");
        return;
    }

    DisableAllElements($("#tblContent"), false);
    var requestdata = JSON.stringify({ id: id, doctorId: doctor[0].id, rosterTypeId: rosterType,
        startTime: fromTime, endTime: toTime, startDate: fromDate, endDate: toDate, note: note
    });

    $.ajax({
        type: "POST",
        url: "Default.aspx/UpdateRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                var evs = obj.data;
                scheduler.deleteEvent(id);
                AddRoster(evs);
                $("#RosterForm").dialog("close");
                scheduler.endLightbox(false, html("RosterForm"));
            } else {
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

// Add roster into scheduler
function AddRoster(evs) {
    $.each(evs, function(i, item) {
        scheduler.addEvent(item);
    });
}

// Cancel roster
function CancelRoster() {
    scheduler.endLightbox(false, html("RosterForm"));
}

// Delete roster by Id
function DeleteRoster(id) {
    if (!confirm("Do you want to delete this roster?"))
        return;

    var ev = scheduler.getEvent(id);
    var requestdata = JSON.stringify({ id: id });
    $.ajax({
        type: "POST",
        url: "Default.aspx/DeleteRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var obj = JSON.parse(response.d);
            if (obj.result == "true") {
                scheduler.deleteEvent(id);
                $("#RosterForm").dialog("close");
                scheduler.endLightbox(false, html("RosterForm"));
            }
            alert(obj.message);
        },
        fail: function() {
            alert("Unknow error!");
        },
        complete: function() {
        }
    });
}

// Load roster type for roster form
function LoadRosterType() {
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
        }
    });
}
/****************************Roster - End******************************/


function SetTime(ev) {
    var startTime = ev.start_date.format("HH:MM");
    var endTime = ev.end_date.format("HH:MM");
    $("#cboFromHour").val(startTime + ":00");
    $("#cboToHour").val(endTime + ":00");
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
    $("#txtNote").val("");

    $("#cboStaff").focus();

    DisableAllElements($("#tblContent"), true);
}

$(document).ready(function() {
    BindTime();
    LoadRosterType();
    GetToken();

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
        selectOtherMonths: true
    });

    $("#spanWeekday").find("span").remove();
    $.each(weekday, function(i, item) {
        $("#spanWeekday").append('<span style="float:left; padding-right:5px; width:95px;"><input type="checkbox" value=' + item.label
            + ' id="chk' + item.key + '" /> ' + item.label + '</span>');
    });

    $("#chkRepeat").click(function() {
        $('#divWeekday').toggle();
    });

    $("#btnDelete").keydown(function(e) {
        var key;

        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 9) {
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
