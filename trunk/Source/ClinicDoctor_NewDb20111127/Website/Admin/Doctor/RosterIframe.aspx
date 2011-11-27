<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RosterIframe.aspx.cs" Inherits="Admin_Doctor_RosterIframe" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/jquery-1.4.2.min.js") %>"
        type="text/javascript"></script>

    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/json2.js") %>" type="text/javascript"></script>

    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/codebase/dhtmlxscheduler.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/codebase/ext/dhtmlxscheduler_minical.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/codebase/ext/dhtmlxscheduler_editors.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/codebase/ext/dhtmlxscheduler_multiselect.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/myscript/codebase/dhtmlxscheduler.css") %>"
        type="text/css" media="screen" title="no title" charset="utf-8" />
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
            scheduler.config.time_step = 15;
            scheduler.config.wide_form = true;
            scheduler.config.details_on_create = true;
            scheduler.config.details_on_dblclick = true;

            scheduler.locale.labels.section_weekday = "Weekday";

            // Load data
            var weekday;
            $.ajax({
                type: "POST",
                url: "RosterIframe.aspx/GetData",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    weekday = eval(msg.d);
                    scheduler.config.lightbox.sections = [
			            { name: "description", height: 50, map_to: "text", type: "textarea", focus: true },
			            { name: "weekday", height: 22, map_to: "user_id", type: "multiselect", options: weekday, vertical: "false" },
			            { name: "location", height: 43, type: "textarea", map_to: "details" },
			            { name: "time", height: 72, type: "time", map_to: "auto" }
		            ];
                    scheduler.init('scheduler_here', new Date(), "week");
                },
                error: function(msg1) { alert(msg1); }
            });

            //            var weekday = [
            //                { key: 'Sun', label: 'Sunday' },
            //                { key: 'Mon', label: 'Monday' },
            //                { key: 'Tue', label: 'Tuesday' },
            //                { key: 'Wed', label: 'Wednesday' },
            //                { key: 'Thu', label: 'Thursday' },
            //                { key: 'Fri', label: 'Friday' },
            //                { key: 'Sat', label: 'Saturday' }
            //            ];
            //            alert(scheduler.locale.date.day_full.length);

        }

        $(document).ready(function() {
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
                    handler: function(date, calendar) {
                        scheduler.setCurrentView(date);
                        scheduler.destroyCalendar()
                    }
                });
        }
    </script>

</head>
<body>
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
