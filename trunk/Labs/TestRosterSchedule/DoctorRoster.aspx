<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorRoster.aspx.cs" Inherits="DoctorRoster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title>Doctor Roster</title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="Scripts/codebase/dhtmlxscheduler_debug.js" type="text/javascript" charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_minical.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_recurring.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_serialize.js" type="text/javascript"
        charset="utf-8"></script>
 	<script src="Scripts/codebase/ext/dhtmlxscheduler_editors.js" type="text/javascript" charset="utf-8"></script>
   <link rel="stylesheet" href="Scripts/codebase/dhtmlxscheduler.css" type="text/css"
        media="screen" title="no title" charset="utf-8" />
    <style type="text/css" media="screen">
        html, body
        {
            margin: 0;
            padding: 0;
            height: 100%;
            overflow: hidden;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        function init() {
            scheduler.config.xml_date = "%Y-%m-%d %H:%i";
            scheduler.config.prevent_cache = true;
            scheduler.config.time_step = 30;
            scheduler.config.details_on_create = true;
            scheduler.config.details_on_dblclick = true;

            scheduler.locale.labels.section_subject = "Subject";
            scheduler.locale.labels.section_description = "Name";

            scheduler.attachEvent("onViewChange", function (mode, date) {

            });

            scheduler.attachEvent("onEventSave", function (id, data, is_new_event) {
                //                alert(scheduler.toXML());
                return true;
            });

            scheduler.attachEvent("onEventAdded", function (event_id, event_object) {
                //any custom logic here
                alert(scheduler.toJSON());
                scheduler.deleteEvent(event_id);
            });

            var subject = [
                { key: 'appt', label: 'Appointment' },
                { key: 'english', label: 'English' },
                { key: 'math', label: 'Math' },
                { key: 'science', label: 'Science' }
            ];

            var doctor = [
                { key: '1', label: 'Phat' },
                { key: '2', label: 'Vy' },
                { key: '3', label: 'Huy' },
                { key: '4', label: 'Not' }
            ];

            scheduler.config.lightbox.sections = [
                { name: "description", height: 43, map_to: "text", type: "textarea", focus: true },
//                { name: "recurring", height: 115, type: "recurring", map_to: "rec_type", button: "recurring" },
                { name: "subject", height: 20, type: "select", options: subject, map_to: "subject" },
                { name: "doctor", height: 70, type: "checkbox", checked_value: "1", unchecked_value: "0" },
                { name: "time", height: 72, type: "time", map_to: "auto" }
            ];

            //scheduler.init('scheduler_here', new Date(2010, 0, 20), "month");
            scheduler.init('scheduler_here', new Date(), "month");

            scheduler.data_attributes = function () {
                var empty = function (a) { return a || ""; }
                return [["id"],
               ["text"],
               ["start_date", scheduler.templates.xml_format],
               ["end_date", scheduler.templates.xml_format],
               ["rec_type", empty],
               ["event_length", empty],
               ["event_pid", empty]];
            }
//            scheduler.load("TextFile.txt", "json");
            //            scheduler.load("data_r.xml");

            //            scheduler.addEvent({ start_date: "22-11-2011 17:00", end_date: "22-11-2011 17:30", text: "fdhdf", id: "7567", subject: "science" });
            //            scheduler.addEvent({ start_date: "11-21-2011 07:00", end_date: "11-21-2011 07:30", text: "Test repeat1", id: "44", subject: "science", rec_type: "week_1___1,4,5#no" });
            //            scheduler.addEvent({ id: "44#123" });
            //            scheduler.addEvent({ id: "44#6342" });

            //            scheduler.addEvent({ start_date: "22-11-2011 07:00", end_date: "22-11-2011 07:30", text: "New event", id: "1269366989368", event_pid: "", event_length: "300", rec_type: "week_1___2#no"});
            //            scheduler.addEvent({ start_date: "22-11-2011 07:00", end_date: "22-11-2011 07:30", text: "Test repeat2", id: "1269366989372", event_pid: "1269366989368", event_length: "1263247200", rec_type: "" });
            //            scheduler.addEvent({ id: "1269366989368", text: "New event", start_date: "2010-01-04 00:00", end_date: "2010-01-04 00:05", rec_type: "week_1___2#no", event_length: "300", event_pid: "" });
            //            scheduler.addEvent({ id: "1269366989372", text: "New event", start_date: "2010-01-14 00:00", end_date: "2010-01-14 00:05", rec_type: "", event_length: "1263247200", event_pid: "1269366989368" });
        }

        $(document).ready(function () {
            init();
        });

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
    </script>
</head>
<body>
    <input type="button" value="Click here" id="btn" />
    <div id="scheduler_here" class="dhx_cal_container" style='width: 100%; height: 100%;'>
        <div class="dhx_cal_navline">
            <div class="dhx_cal_prev_button">
                &nbsp;</div>
            <div class="dhx_cal_next_button">
                &nbsp;</div>
            <div class="dhx_cal_today_button">
            </div>
            <div class="dhx_cal_date">
            </div>
            <div class="dhx_minical_icon" id="dhx_minical_icon" onclick="show_minical()">
                &nbsp;</div>
            <div class="dhx_cal_tab" name="day_tab" style="right: 204px;">
            </div>
            <div class="dhx_cal_tab" name="week_tab" style="right: 140px;">
            </div>
            <div class="dhx_cal_tab" name="month_tab" style="right: 76px;">
            </div>
        </div>
        <div class="dhx_cal_header">
        </div>
        <div class="dhx_cal_data">
        </div>
    </div>
</body>
</html>
<script type="text/javascript" charset="utf-8">
    $("#btn").click(function () {
        evs = scheduler.getEvents(new Date(2011, 11, 1), new Date(2011, 11, 30));
        for (var i = 0; i < evs.length; i++)
            alert(evs[i].text);
    });
</script>