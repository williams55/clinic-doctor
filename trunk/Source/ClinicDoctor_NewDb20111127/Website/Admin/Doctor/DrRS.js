var blHour = false;
var blStaff = false;
var blRosterType = false;

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
                scheduler.destroyCalendar()
            }
        });
}


// Init Schedule
function initSchedule() {
    var _date = new Date();
    var _mode = "week";

    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.config.details_on_dblclick = true;
    scheduler.config.dblclick_create = false;

    scheduler.init('scheduler_here', _date, _mode);

    scheduler.config.readonly_form = true;
    scheduler.attachEvent("onBeforeDrag", function () { return false; })
    scheduler.attachEvent("onClick", function () { return false; })

    scheduler.attachEvent("onBeforeLightbox", function (event_id) {
        return false;
    });

    // Load roster
    LoadRoster(_mode, _date);
}

function LoadRoster(mode, date) {
    var requestdata = JSON.stringify({ Mode: mode, CurrentDateView: date });
    $.ajax({
        type: "POST",
        url: "WatchRosterIframe.aspx/LoadRoster",
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

function addRoster(evs) {
    $.each(evs, function (i, item) {
        scheduler.addEvent(item);
    });
}

$(document).ready(function () {
    initSchedule();
});

