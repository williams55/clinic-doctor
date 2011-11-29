<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RosterIframe.aspx.cs" Inherits="Admin_Doctor_RosterIframe" %>

<%@ Register Src="~/Admin/Doctor/RosterForm.ascx" TagName="RegisterRoster" TagPrefix="Scheduler" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/resources/css/ui-lightness/jquery-ui-1.7.3.custom.css") %>" />
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/jquery-1.6.2.min.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.core.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.widget.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.datepicker.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/json2.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/dhtmlxscheduler.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/dhtmlxscheduler.css") %>"
        type="text/css" media="screen" title="no title" charset="utf-8" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/myscript/css/scheduler.css") %>"
        type="text/css" media="screen" title="no title" charset="utf-8" />
    <style type="text/css" media="screen">
        html, body
        {
            margin: 0;
            padding: 0;
            height: 100%;
            overflow: hidden;
            font-size: 13px;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        // Load weekday
        var weekday = <%=Constants.Weekdays %>;
        var html = function (id) { return document.getElementById(id); }; //just a helper

    </script>
    <script src="GRNEditt.js" type="text/javascript"></script>
</head>
<body>
    <div id="RosterForm" class="schedulerForm">
        <table cellpadding="3" width="100%">
            <tr>
                <td colspan="2" class="title">
                    New event
                </td>
            </tr>
            <tr>
                <td class="header" width="80">
                    Roster Type
                </td>
                <td>
                    <select id="cboRosterType">
                    </select><span id="loadingRosterType" class="loading"></span>
                </td>
            </tr>
            <tr>
                <td class="header">
                    Is repeat
                </td>
                <td>
                    <input id="chkRepeat" type="checkbox" />
                </td>
            </tr>
            <tr id="divWeekday">
                <td class="header">
                    Weekday
                </td>
                <td id="spanWeekday">
                </td>
            </tr>
            <tr>
                <td class="header">
                    From
                </td>
                <td>
                    <select id="cboFromHour">
                    </select><span id="loadingFromHour" class="loading"></span>
                    <input type="text" id="txtFromDate" class="datePicker" />
                </td>
            </tr>
        </table>
        <div>
            <label for="txtComment">
                Comment</label>
            <textarea id="Textarea6" rows="4" cols="30"></textarea></div>
    </div>
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
