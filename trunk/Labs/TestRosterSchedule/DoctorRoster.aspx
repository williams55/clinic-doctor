<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorRoster.aspx.cs" Inherits="DoctorRoster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title>Doctor Roster</title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="Scripts/codebase/dhtmlxscheduler.js" type="text/javascript" charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_minical.js" type="text/javascript"
        charset="utf-8"></script>
    <script src="Scripts/codebase/ext/dhtmlxscheduler_recurring.js" type="text/javascript"
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
            scheduler.config.details_on_create = true;
            scheduler.config.details_on_dblclick = true;

            scheduler.locale.labels.section_subject = "Subject";
            scheduler.locale.labels.section_description = "Name";

            scheduler.attachEvent("onViewChange", function (mode, date) {
                
            });
            scheduler.attachEvent("onEventSave", function (id, data, is_new_event) {
                alert(id);
                return true;
            })

            scheduler.init('scheduler_here', new Date(), "week");

            scheduler.addEvent("22-11-2011 12:35", "22-11-2011 12:55", "Test repeat", "123#1234", { data1: "abc", data2: "xyz" });
            scheduler.addEvent("23-11-2011 12:35", "23-11-2011 12:55", "Test repeat", "123#6342", { data1: "hhh", data2: "uuu" });
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
