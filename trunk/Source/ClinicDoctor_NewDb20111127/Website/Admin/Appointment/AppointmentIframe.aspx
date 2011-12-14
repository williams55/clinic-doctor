<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentIframe.aspx.cs"
    Inherits="Admin_Appointment_AppointmentIframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <div id="RosterForm" class="schedulerForm" style="width: 650px">
        <input type="hidden" id="hdId" value="" />
        <div class="title" id="dialog-modal" style="width: 100%; text-align: center;">
            <span class="loading"></span>
        </div>
        <table cellpadding="3" width="100%" id="tblContent">
            <tr>
                <td colspan="4" class="title" id="tdTitle">
                </td>
            </tr>
            <tr>
                <td colspan="4" class="subtitle">
                    Patient
                </td>
            </tr>
            <tr>
                <td class="header">
                    Is New Patient
                </td>
                <td colspan="3">
                    <input  tabindex="100" id="chkNewPatient" type="checkbox" value="true" checked="checked" style="float: left;" />
                </td>
            </tr>
            <tr id="trNewPatient">
                <td class="header">
                    Select Patient
                </td>
                <td>
                    <select id="cboPatient" style="width: 95%;"  tabindex="101">
                    </select>
                </td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr id="trNewPatient1">
                <td class="header">
                    First Name
                </td>
                <td>
                    <input type="text" id="txtFirstName" style="width: 100%;" maxlength="200"  tabindex="102" />
                </td>
                <td class="header">
                    Last Name
                </td>
                <td>
                    <input type="text" id="txtLastName" style="width: 100%;" maxlength="200"  tabindex="103" />
                </td>
            </tr>
            <tr id="trNewPatient2">
                <td class="header">
                    Cell Phone
                </td>
                <td>
                    <input type="text" id="txtCellPhone" style="width: 100%;" maxlength="20"  tabindex="104" />
                </td>
                <td class="header">
                    Address
                </td>
                <td>
                    <input type="text" id="txtAddress" style="width: 100%;" maxlength="500"  tabindex="105" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="subtitle">
                    Content
                </td>
            </tr>
            <tr>
                <td class="header">
                    Select Content
                </td>
                <td>
                    <select id="cboContent" style="width: 95%;"  tabindex="106">
                    </select>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="header">
                    Note
                </td>
                <td colspan="3">
                    <textarea id="txtNote" cols="10" rows="2" style="width: 95%;"  tabindex="107"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="subtitle">
                    Time
                </td>
            </tr>
            <tr>
                <td class="header" width="100">
                    From Time
                </td>
                <td width="220">
                    <select id="cboFromHour" style="float: left;"  tabindex="108">
                    </select><span id="loadingFromHour" class="loading" style="float: left;"></span>
                    <span id="spanFromDate" style="float: left;">
                        <input type="text" id="txtFromDate" class="datePicker" readonly="readonly" style="padding: 4px 2px 4px 2px;"  tabindex="109" /></span>
                </td>
                <td class="header" width="70">
                    To Time
                </td>
                <td>
                    <select id="cboToHour" style="float: left;" tabindex="110">
                    </select><span id="loadingToHour" class="loading" style="float: left;"></span> <span
                        id="spanToDate" style="float: left;">
                        <input type="text" id="txtToDate" class="datePicker" readonly="readonly" style="padding: 4px 2px 4px 2px;" tabindex="111" /></span>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="subtitle">
                    Doctor
                </td>
            </tr>
            <tr>
                <td class="header">
                    Select Doctor
                </td>
                <td>
                    <select id="cboStaff" style="width: 95%;" tabindex="112">
                    </select><span id="spanStaff" class="loading" style="float: left;"></span>
                </td>
                <td class="header">
                    Room
                </td>
                <td>
                    <select id="cboRoom" style="width: 95%;" tabindex="113">
                    </select><span id="loadingRoom" class="loading" style="float: left;"></span>
                </td>
            </tr>
            <tr>
                <td class="subtitle" colspan="4">
                    <input type="submit" value="Save" id="btnSave" style="float: left;" tabindex="114" />
                    <input type="submit" value="Cancel" id="btnCancel" style="float: left;" tabindex="115" />
                    <input type="submit" value="Delete" id="btnDelete" style="float: right;" tabindex="116" />
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
            <div class="dhx_minical_icon" id="dhx_minical_icon" onclick="show_minical()">
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
