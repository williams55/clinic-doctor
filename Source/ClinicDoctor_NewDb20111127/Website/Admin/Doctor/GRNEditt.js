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
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.details_on_create = true;
    scheduler.config.time_step = stepTime;

    scheduler.init('scheduler_here', null, "week");

    function block_readonly(id) {
        if (!id) return true;
        return !this.getEvent(id).readonly;
    }
    scheduler.attachEvent("onBeforeDrag", block_readonly)
    scheduler.attachEvent("onClick", block_readonly)

    scheduler.addEvent({
        start_date: "29-11-2011 11:15:00",
        end_date: "29-11-2011 11:30:00",
        text: "Some",
        isnew: "false",
        readonly: true,
        color: "#ccc"
    });

    scheduler.addEvent({
        start_date: "30-11-2011 04:15:00",
        end_date: "30-11-2011 05:30:00",
        text: "Some1",
        isnew: "false",
        color: "#ff0000"
    });

    scheduler.addEvent({
        start_date: "1-12-2011 04:15:00",
        end_date: "1-12-2011 05:30:00",
        text: "Some2",
        isnew: "false",
        color: "#ff0000"
    });

    scheduler.addEvent({
        start_date: "1-12-2011 06:15:00",
        end_date: "1-12-2011 07:30:00",
        text: "Some3",
        isnew: "false",
        color: "#ff0000"
    });

    // When event changed, check and update
    scheduler.attachEvent("onBeforeEventChanged", function(event_object, native_event, is_new) {
        // If update roster
        if (is_new == false) {
            var evs = scheduler.getEvents(event_object.start_date, event_object.end_date);
            if (evs && evs.length > 1) {
                alert("Khong the tao roster bi trung");
                return false;
            }

            if (event_object.start_date <= new Date()) {
                alert("You cannot change roster to a passed date");
                return false;
            }
        }

        // Thuc hien luu cap nhat
        alert(event_object.isnew);

        return true;
    });
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

    scheduler.startLightbox(id, html("RosterForm"));

    loadRosterType();
    loadHour(ev);

    $("#hdId").val(id);
    $("#txtFromDate").datepicker("setDate", ev.start_date);
    $("#txtToDate").datepicker("setDate", ev.end_date);
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
        }
    });
}

function initForm() {
    $("#cboRosterType").hide();
    $("#loadingRosterType").show();

    $("#divWeekday").hide();

    $("#cboFromHour").hide();
    $("#loadingFromHour").show();

    $("#cboToHour").hide();
    $("#loadingToHour").show();

    $("#hdId").val("");
}

function SaveRoster() {
    //    var ev = scheduler.getEvent(scheduler.getState().lightbox_id);
    scheduler.endLightbox(false, html("RosterForm"));
    //    var requestdata = JSON.stringify({ RosterType: event_object.RosterType, StartTime: event_object.start_date, EndTime: event_object.end_date, Note: event_object.Note, RepeatRoster: event_object.RepeatRoster, Weekday: event_object.Weekday });

    //    $.ajax({
    //        type: "POST",
    //        url: "RosterIframe.aspx/SaveEvent",
    //        data: requestdata,
    //        dataType: "json",
    //        contentType: "application/json; charset=utf-8",
    //        success: function (response) {
    //            var drug = response.d;
    //            alert(drug);
    //        }
    //    });
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

    $("#chkRepeat").click(function () {
        $('#divWeekday').toggle($(this).attr('checked'));
        $("#spanWeekday").find("span").remove();
        $.each(weekday, function (i, item) {
            $("#spanWeekday").append('<span style="float:left; padding-right:5px; width:95px;"><input type="checkbox" id="chk' + item.key + '" /> ' + item.label + '</span>');
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
});
