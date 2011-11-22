<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title>Test</title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="Scripts/codebase/dhtmlxscheduler.js" type="text/javascript" charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_multiselect.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_recurring.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_limit.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_collision.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_serialize.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_readonly.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_minical.js" type="text/javascript"
        charset="utf-8"></script>
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
        
        /*event in day or week view*/
        .dhx_cal_event.past_event div
        {
            background-color: black !important;
            color: white !important;
        }
        /*multi-day event in month view*/
        .dhx_cal_event_line.past_event
        {
            background-color: black !important;
            color: white !important;
        }
        /*event with fixed time, in month view*/
        .dhx_cal_event_clear.past_event
        {
            color: black !important;
        }
        
        .dhx_cal_event.event_math div, .dhx_cal_event_line.event_math
        {
            background-color: orange !important;
            color: blue !important;
        }
        .dhx_cal_event_clear.event_math
        {
            color: blue !important;
        }
        
        .dhx_cal_event.event_science div, .dhx_cal_event_line.event_science
        {
            background-color: #add8e6 !important;
            color: #8b0000 !important;
        }
        .dhx_cal_event_clear.event_science
        {
            color: #8b0000 !important;
        }
        
        .dhx_cal_event.event_english div, .dhx_cal_event_line.event_english
        {
            background-color: #e0ffff !important;
            color: #008b8b !important;
        }
        .dhx_cal_event_clear.event_english
        {
            color: #008b8b !important;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        function init() {
            scheduler.config.xml_date = "%Y-%m-%d %H:%i";
            scheduler.config.prevent_cache = true;
            scheduler.config.time_step = 30;
            scheduler.config.multi_day = true;
            scheduler.config.drag_create = true;
            scheduler.config.details_on_create = true;
            scheduler.config.details_on_dblclick = true;

            scheduler.locale.labels.section_subject = "Subject";
            scheduler.locale.labels.section_description = "Name";

            //            scheduler.blockTime(new Date(2011, 4, 30), "fullday");
            //            scheduler.blockTime(new Date(2011, 4, 3), [0, 10 * 60]);
            //            scheduler.blockTime(6, [0, 8 * 60, 18 * 60, 24 * 60]);
            //            scheduler.blockTime(0, "fullday", "weekly");

            var control_date = new Date(2011, 3, 20);
            scheduler.templates.event_class = function (start, end, event) {

                if (start < control_date) // event start before control date
                    return "past_event";

                if (event.subject) // if event has subject property then special class should be assigned
                    return "event_" + event.subject;

                return ""; // default return

                /*
                Note that it is possible to create more complex checks
                events with the same properties could have different CSS classes depending on the current view:

                var mode = scheduler.getState().mode;
                if(mode == "day"){
                // custom logic here
                }
                else {
                // custom logic here
                }
                */
            };

            function block_readonly(id) {
                if (!id) return true;
                return !this.getEvent(id).readonly;
            }
            scheduler.attachEvent("onBeforeDrag", block_readonly)
            scheduler.attachEvent("onClick", block_readonly)
            scheduler.attachEvent("onViewChange", function (mode, date) {
                alert("haha");
            });
            var subject = [
                { key: '', label: 'Appointment' },
                { key: 'english', label: 'English' },
                { key: 'math', label: 'Math' },
                { key: 'science', label: 'Science' }
            ];

            scheduler.config.lightbox.sections = [
                { name: "description", height: 43, map_to: "text", type: "textarea", focus: true },
                { name: "recurring", height: 115, type: "recurring", map_to: "rec_type", button: "recurring" },
                { name: "subject", height: 20, type: "select", options: subject, map_to: "subject" },
                { name: "time", height: 72, type: "time", map_to: "auto" }
            ];

            scheduler.init('scheduler_here', new Date(2011, 3, 18), "week");

            scheduler.parse([
                { start_date: "2011-04-18 09:00", end_date: "2011-04-18 12:00", text: "English lesson1", subject: 'english', readonly: true },
                { start_date: "2011-04-20 10:00", end_date: "2011-04-21 16:00", text: "Math exam", subject: 'math' },
                { start_date: "2011-04-21 10:00", end_date: "2011-04-21 14:00", text: "Science lesson", subject: 'science' },
                { start_date: "2011-04-23 16:00", end_date: "2011-04-23 17:00", text: "English lesson", subject: 'english' },
                { start_date: "2011-04-24 09:00", end_date: "2011-04-24 17:00", text: "Usual event" }
            ], "json");

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
    <input type="button" id="mybutton" value="abc" />
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
