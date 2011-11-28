<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RosterIframe.aspx.cs" Inherits="Admin_Doctor_RosterIframe" %>

<%@ Register Src="~/Admin/UserControls/RosterForm.ascx" TagName="RegisterRoster"
    TagPrefix="Scheduler" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/jquery-1.4.2.min.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/json2.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/myscript/codebase/dhtmlxscheduler.js") %>"
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
    <script src="GRNEditt.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {
            // Load weekday
            var weekday = <%=Constants.Weekdays %>;

            initSchedule(weekday);
        });
    </script>
</head>
<body>
    <Scheduler:RegisterRoster ID="scheduler" runat="server" />
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
