// Load calendar for Schedule
function show_minical() {
    if (scheduler.isCalendarVisible())
        scheduler.destroyCalendar();
    else
        scheduler.renderCalendar({
            position: "dhx_minical_icon",
            date: scheduler._date,
            navigation: true,
            handler: function(date, calendar) {
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

    scheduler.init('scheduler_here', null, "week");

    //    scheduler.locale.labels.section_rosterType = "Type";
    //    scheduler.locale.labels.section_repeat = "Is repeated";
    //    scheduler.locale.labels.section_weekday = "Weekday";

    //    // When click save button
    //    scheduler.attachEvent("onEventSave", function (id, data, is_new_event) {
    //        // If user do not choose Roster Type
    //        if (isNaN(data.RosterType) == true) {
    //            alert("You must choose Roster Type.");
    //            return false;
    //        }

    //        // If user wanna repeat Roster
    //        if (data.RepeatRoster == "true") {
    //            if (confirm("Do you want to repeat Roster in this month?") == false)
    //                return false;
    //            if (data.Weekday == "") {
    //                alert("You must choose weekday for repeating.");
    //                return false;
    //            }
    //        }

    //        return true;
    //    });

    //    // After added event
    //    scheduler.attachEvent("onEventAdded", function (event_id, event_object) {
    //        SaveRoster(event_object);
    //    });

    //    // Before lightbox is loaded
    //    scheduler.attachEvent("onBeforeLightbox", function (event_id) {
    //        //any custom logic here
    //        var ev = scheduler.getEvent(event_id);
    //        alert(ev.RosterType);
    //        ev.Note = "fhaskj";
    //        return true;
    //    });

    //    // Load data
    //    $.ajax({
    //        type: "POST",
    //        url: "RosterIframe.aspx/GetData",
    //        data: "{}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {
    //            var rosterType = eval(msg.d);

    //            // Set default
    //            scheduler.attachEvent("onEventCreated", function (id) {
    //                if (rosterType.length > 0)
    //                    scheduler.getEvent(id).RosterType = rosterType[0].key;
    //            });

    //            scheduler.config.lightbox.sections = [
    //			            { name: "description", height: 50, map_to: "Note", type: "textarea", focus: true },
    //				        { name: "rosterType", height: 22, options: rosterType, map_to: "RosterType", type: "radio", vertical: false },
    //                        { name: "repeat", height: 22, map_to: "RepeatRoster", type: "checkbox", checked_value: "true", unchecked_value: "false" },
    //			            { name: "weekday", height: 22, map_to: "Weekday", type: "multiselect", options: weekday, vertical: "false" },
    //			            { name: "time", height: 72, type: "time", map_to: "auto" }
    //		            ];
    //            scheduler.init('scheduler_here', new Date(), "week");
    //        }
    //    });
}

scheduler.showLightbox = function(id) {
    initForm();

    var ev = scheduler.getEvent(id);
    scheduler.startLightbox(id, html("RosterForm"));

    loadRosterType();
}

function save_form() {
    var ev = scheduler.getEvent(scheduler.getState().lightbox_id);

    scheduler.endLightbox(true, html("RosterForm"));
}

function close_form(argument) {
    scheduler.endLightbox(false, html("RosterForm"));
}

function loadRosterType() {
    // Load data
    $.ajax({
        type: "POST",
        url: "RosterIframe.aspx/GetData",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            var rosterType = eval(msg.d);
            $("#cboRosterType").find("option").remove();

            $.each(rosterType, function(i, item) {
                $("#cboRosterType").append('<option value="' + item.key + '">' + item.label + '</option>');
            });

            $("#cboRosterType").fadeIn();
            $("#loadingRosterType").fadeOut();
        }
    });
}

function initForm() {
    $("#cboRosterType").attr("style", "display: none");
    $("#loadingRosterType").attr("style", "display: block");

    $("#divWeekday").attr("style", "display: none");
}

//function SaveRoster(event_object) {
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
//}

$(document).ready(function() {
    $("input, textarea").addClass("idle");
    $("input, textarea").focus(function() {
        $(this).addClass("activeField").removeClass("idle");
    }).blur(function() {
        $(this).removeClass("activeField").addClass("idle");
    });
    initSchedule(weekday);

    $("#chkRepeat").click(function() {
        $('#divWeekday').toggle($(this).attr('checked'));
        $("#spanWeekday").find("span").remove();
        $.each(weekday, function(i, item) {
            $("#spanWeekday").append('<span style="float:left; padding-right:5px; width:95px;"><input type="checkbox" id="chk' + item.key + '" /> ' + item.label + '</span>');
        });
    });
});
