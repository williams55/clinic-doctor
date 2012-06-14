<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RosterIframe.aspx.cs" Inherits="Admin_Doctor_RosterIframe" %>

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
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.dialog.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/ui/jquery.ui.datepicker.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/date.format.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/json2.js") %>"
        type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/dhtmlxscheduler.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_timeline.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_treetimeline.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_minical.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_readonly.js") %>"
        type="text/javascript" charset="utf-8"></script>
    <script src="<%= Page.ResolveClientUrl("~/Admin/resources/scripts/codebase/ext/dhtmlxscheduler_tooltip.js") %>"
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
        .one_line
        {
            white-space: nowrap;
            overflow: hidden;
            padding-top: 5px;
            padding-left: 5px;
            text-align: left !important;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        // Load weekday
        var weekday = <%=Constants.Weekdays %>;
        var stepTime = <%=ClinicDoctor.Settings.BusinessLayer.ServiceFacade.SettingsHelper.MinuteStep %>;
        var html = function (id) { return document.getElementById(id); }; //just a helper

    </script>
    <script src="GRNEditt.js" type="text/javascript"></script>
</head>
<body>
    <div id="RosterForm" class="schedulerForm">
        <input type="hidden" id="hdId" value="" />
        <div class="title" id="dialog-modal" style="width: 100%; text-align: center;">
            <span class="loading"></span>
        </div>
        <table cellpadding="3" width="100%" id="tblContent">
            <tr>
                <td colspan="2" class="title" id="tdTitle">
                </td>
            </tr>
            <tr>
                <td class="header" width="80">
                    Doctor
                </td>
                <td>
                    <select id="cboStaff" style="width: 200px;">
                    </select>
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
            <tr id="trRepeat">
                <td class="header">
                    Is repeat
                </td>
                <td>
                    <input id="chkRepeat" type="checkbox" value="repeated" checked="checked" style="float: left;" />
                    <span id="spanMonth">
                        <input type="text" id="txtMonth" readonly="readonly" style="padding: 4px 2px 4px 2px;" /></span>
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
                    </select><span id="loadingFromHour" class="loading"></span> <span id="spanFromDate">
                        <input type="text" id="txtFromDate" class="datePicker" readonly="readonly" style="padding: 4px 2px 4px 2px;" /></span>
                </td>
            </tr>
            <tr>
                <td class="header">
                    To
                </td>
                <td>
                    <select id="cboToHour">
                    </select><span id="loadingToHour" class="loading"></span> <span id="spanToDate">
                        <input type="text" id="txtToDate" class="datePicker" readonly="readonly" style="padding: 4px 2px 4px 2px;" /></span>
                </td>
            </tr>
            <tr>
                <td class="header">
                    Note
                </td>
                <td>
                    <textarea id="txtNote" cols="10" rows="3" style="width: 100%;"></textarea>
                </td>
            </tr>
            <tr>
                <td class="header" colspan="2">
                    <input type="submit" value="Save" id="btnSave" style="float: left;" />
                    <input type="submit" value="Cancel" id="btnCancel" style="float: left;" />
                    <input type="submit" value="Delete" id="btnDelete" style="float: right;" />
                </td>
            </tr>
        </table>
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
            <div class="dhx_minical_icon" id="dhx_minical_icon" onclick=" ShowMinical()">
                &nbsp;</div>
            <div class="dhx_cal_tab" name="week_tab" style="right: 140px;">
            </div>
            <div class="dhx_cal_tab" name="timeline_tab" style="right: 280px;">
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
