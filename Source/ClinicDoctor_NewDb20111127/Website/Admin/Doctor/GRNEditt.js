// Load calendar for Schedule
function show_minical() {
    if (scheduler.isCalendarVisible())
        scheduler.destroyCalendar();
    else
        scheduler.renderCalendar({
            position: "dhx_minical_icon",
            date: scheduler._date,
            navigation: true,
            handler: function (date, calendar) {
                scheduler.setCurrentView(date);
                scheduler.destroyCalendar()
            }
        });
}


// Init Schedule
function initSchedule(weekday) {
    var _date = new Date();
    var _mode = "week";
        
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.time_step = stepTime;

    scheduler.init('scheduler_here', _date, _mode);

    function block_readonly(id) {
        if (!id) return true;
        return !this.getEvent(id).readonly;
    }
    scheduler.attachEvent("onBeforeDrag", block_readonly)
    scheduler.attachEvent("onClick", block_readonly)

    // When event changed, check and update
    scheduler.attachEvent("onBeforeEventChanged", function (event_object, native_event, is_new) {
        var evs = scheduler.getEvents(event_object.start_date, event_object.end_date);
        if (evs && evs.length > 1) {
            alert("You can not create roster on another existed roster.");
            return false;
        }

        // If update roster
        if (is_new == false) {
            if (event_object.start_date <= new Date()) {
                alert("You can not change roster to passed or current date.");
                return false;
            }

            // Update data
            alert(event_object.isnew);
        }

        return true;
    });

    // When view changed, reload roster
    scheduler.attachEvent("onViewChange", function (mode, date) {
        //any custom logic here
        LoadRoster(mode, date);
    });

    // Load roster
    LoadRoster(_mode, _date);
}

scheduler.showLightbox = function (id) {
    initForm();

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

    if (ev.isnew == "false") {
        $("#trRepeat").hide();
        $("#tdTitle").text("Edit roster");
    }

    scheduler.startLightbox(id, html("RosterForm"));

    loadRosterType();
    loadHour(ev);

    $("#hdId").val(id);
    $("#txtFromDate").datepicker("setDate", ev.start_date);
    $("#txtToDate").datepicker("setDate", ev.end_date);
}

function LoadRoster(mode, date) {
    var requestdata = JSON.stringify({ Mode: mode, CurrentDateView: date });
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/LoadRoster",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
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
        fail: function () {
            alert("Unknow error!");
        },
        complete: function () {
        }
    });
}

function loadRosterType() {
    // Load data
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetRosterType",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var rosterType = eval(msg.d);
            $("#cboRosterType").find("option").remove();

            $.each(rosterType, function (i, item) {
                $("#cboRosterType").append('<option value="' + item.key + '">' + item.label + '</option>');
            });

            $("#cboRosterType").fadeIn();
            $("#cboRosterType").focus();
        },
        fail: function () {
            alert("Cannot load Roster Type data!");
        },
        complete: function () {
            $("#loadingRosterType").fadeOut();
        }
    });
}

function loadHour(ev) {
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetTime",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var arr = eval(msg.d);
            $("#cboFromHour").find("option").remove();

            var startTime = ev.start_date.format("HH:MM");
            var endTime = ev.end_date.format("HH:MM");
            $.each(arr, function (i, item) {
                $("#cboFromHour").append('<option value="' + item.key + '">' + item.label + '</option>');
                $("#cboToHour").append('<option value="' + item.key + '">' + item.label + '</option>');
            });

            $("#cboFromHour").val(startTime + ":00");
            $("#cboToHour").val(endTime + ":00");

            $("#cboFromHour").fadeIn();
            $("#loadingFromHour").fadeOut();
            $("#cboToHour").fadeIn();
            $("#loadingToHour").fadeOut();
        },
        fail: function () {
            alert("Cannot load Hour data!");
        },
        complete: function () {
            $("#loadingFromHour").fadeOut();
            $("#loadingToHour").fadeOut();
        }
    });
}

function initForm() {
    $("#dialog-modal").hide();

    $("#cboRosterType").hide();
    $("#loadingRosterType").show();

    $("#divWeekday").hide();
    $("#spanMonth").hide();

    $("#cboFromHour").hide();
    $("#loadingFromHour").show();
    $('#spanFromDate').show();

    $("#cboToHour").hide();
    $("#loadingToHour").show();
    $('#spanToDate').show();

    $("#trRepeat").show();

    // Set empty
    $("#chkRepeat").attr("checked", false);
    $("#hdId").val("");
    $("#tdTitle").text("New roster");
    $("#txtNote").val("");
}

function SaveRoster() {
    var ev = scheduler.getEvent($("#hdId").val());
    var _rosterType = $("#cboRosterType").val();
    var _fromTime = $("#txtFromDate").val() + " " + $("#cboFromHour").val();
    var _toTime = $("#txtToDate").val() + " " + $("#cboToHour").val();
    var _note = $("#txtNote").val();

    if (ev.isnew == "false") {
        // Update roster
    }
    else {
        // Add new roster
        var _repeat = 'false';
        var _weekday = '';
        var _month = '';

        if ($("#chkRepeat").attr("checked")) {
            var allVals = [];
            $("input[type=checkbox]:checked", "#spanWeekday").each(function () {
                allVals.push($(this).val());
            });

            _repeat = 'true';
            _weekday = allVals.join(";");
            _month = $("#txtMonth").val();
        }

        $("#dialog-modal").show();
        disableAllElements($("#tblContent"), false)

        var requestdata = JSON.stringify({ RosterType: _rosterType, StartTime: _fromTime, EndTime: _toTime, Note: _note, RepeatRoster: _repeat, Weekday: _weekday, Month: _month });
        $.ajax({
            type: "POST",
            url: "RosterIframe.aspx/SaveEvent",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
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
            fail: function () {
                alert("Unknow error!");
            },
            complete: function () {
                disableAllElements($("#tblContent"), true)
                $("#dialog-modal").hide();
            }
        });
    }
}

function addRoster(evs) {
    $.each(evs, function (i, item) {
        scheduler.addEvent(item);
    });
}

function CancelRoster() {
    scheduler.endLightbox(false, html("RosterForm"));
}

function DeleteRoster() {
    var ev = scheduler.getEvent($("#hdId").val());
    if (ev.isnew == "false") {
        scheduler.deleteEvent(ev.id)
    }
    scheduler.endLightbox(false, html("RosterForm"));
}

$(document).ready(function () {
    $("input, textarea").addClass("idle");
    $("input, textarea").focus(function () {
        $(this).addClass("activeField").removeClass("idle");
    }).blur(function () {
        $(this).removeClass("activeField").addClass("idle");
    });
    initSchedule(weekday);

    $(".datePicker").datepicker({
        showOn: "button",
        buttonImage: "../resources/scripts/codebase/imgs/calendar.gif",
        defaultDate: new Date(),
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "dd/mm/yy",
        minDate: "+1D",
        maxDate: "+1D +1M"
    });

    $("#txtMonth").datepicker({
        showOn: "button",
        buttonImage: "../resources/scripts/codebase/imgs/calendar.gif",
        defaultDate: new Date(),
        buttonImageOnly: true, changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: "mm/yy",
        minDate: "+1D",
        maxDate: "+1D +1M"
    });

    $("#chkRepeat").click(function () {
        $('#divWeekday').toggle($(this).attr('checked'));
        $('#spanMonth').toggle($(this).attr('checked'));

        $('#spanFromDate').toggle(!$(this).attr('checked'));
        $('#spanToDate').toggle(!$(this).attr('checked'));

        $("#spanWeekday").find("span").remove();
        $.each(weekday, function (i, item) {
            $("#spanWeekday").append('<span style="float:left; padding-right:5px; width:95px;"><input type="checkbox" value=' + item.key
            + ' id="chk' + item.key + '" /> ' + item.label + '</span>');
        });
    });

    $("#btnSave").click(function () {
        SaveRoster();
    });

    $("#btnCancel").click(function () {
        CancelRoster();
    });

    $("#btnDelete").click(function () {
        DeleteRoster();
    });

    $("#btnDelete").keydown(function (e) {
        var key;

        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 9) {
            $("#cboRosterType").focus();
            return false;
        }
    });

});

function disableAllElements(obj, disabled) {
    if (disabled) {
        $("input, textarea, select", obj).removeAttr('disabled');
    }
    else {
        $("input, textarea, select", obj).attr('disabled', true);
    }
}
