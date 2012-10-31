<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Appointment_Default" %>

<%@ Import Namespace="AppointmentSystem.Settings.BusinessLayer" %>
<%@ Import Namespace="Appt.Common.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Appointment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script src="<%= Page.ResolveClientUrl("~/resources/scripts/date.format.js") %>"
        type="text/javascript"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/scripts/json2.js") %>" type="text/javascript"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/dhtmlxscheduler.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_dhx_terrace.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_active_links.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_limit.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_timeline.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_units.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_minical.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_readonly.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_tooltip.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_collision.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/resources/scripts/maxZIndex.js") %>"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/resources/scripts/jquery.scrollTo-1.4.2-min.js") %>"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/resources/components/analogClock/jqueryRotate.js") %>"></script>

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/dhtmlxscheduler_dhx_terrace.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/blocksection.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/css/scheduler.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/css/dialog-form.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/analogClock/analogclock.css") %>"
        type="text/css" media="screen" title="no title" />

    <script type="text/javascript" charset="utf-8">
        // Load weekday
        var weekday = <%=Constants.Weekdays %>;
        var stepTime = <%=ServiceFacade.SettingsHelper.MinuteStep %>;
        var html = function (id) { return document.getElementById(id); }; //just a helper
        var floors = eval(<%=ListServices%>);
        
        var minuteStep = eval(<%=ServiceFacade.SettingsHelper.MinuteStep%>);
        var maxHour = eval(<%=ServiceFacade.SettingsHelper.MaxHour%>);
        var maxMinute = eval(<%=ServiceFacade.SettingsHelper.MaxMinute%>);

        var angleSec = 0;
        var angleMin = 0;
        var angleHour = 0;

        $(document).ready(function() {
            $("#sec").rotate(angleSec);
            $("#min").rotate(angleMin);
            $("#hour").rotate(angleHour);
        });

        setInterval(function() {
            var d = new Date;

            angleSec = (d.getSeconds() * 6);
            $("#sec").rotate(angleSec);

            angleMin = (d.getMinutes() * 6);
            $("#min").rotate(angleMin);

            angleHour = ((d.getHours() * 5 + d.getMinutes() / 12) * 6);
            $("#hour").rotate(angleHour);

        }, 1000);
    </script>

    <style media="screen">
        /* enabling marked timespans for month view */.dhx_scheduler_month .dhx_marked_timespan
        {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="pleaseWait-darkBackground" style="width: 1366px; height: 2021px; display: none;
        opacity: 0.5; visibility: visible;">
    </div>
    <div id="dialog-form" title="Patient" class="dialog-form">
        <table class="table-form" style="width: 100%">
            <tr>
                <td class="header" style="width: 80px;">
                    First Name
                </td>
                <td>
                    <input tabindex="1" type="text" name="txtFirstName" id="txtFirstName" style="width: 100%"
                        class="content-input ui-widget-content ui-corner-all" />
                </td>
                <td style="width: 10px;">
                </td>
                <td class="header">
                    DOB (m/d/yyyy)
                </td>
                <td>
                    <input type="text" id="txtDob" class="datePicker content-input ui-widget-content ui-corner-all"
                        readonly="readonly" tabindex="6" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    Middle Name
                </td>
                <td>
                    <input type="text" tabindex="2" name="txtMiddileName" id="txtMiddileName" value=""
                        style="width: 100%" class="content-input ui-widget-content ui-corner-all" />
                </td>
                <td>
                </td>
                <td class="header">
                    Mobile Phone
                </td>
                <td>
                    <input type="text" name="txtCellPhone" tabindex="7" id="txtMobilePhone" value=""
                        style="width: 100%" class="content-input ui-widget-content ui-corner-all" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    Last Name
                </td>
                <td>
                    <input type="text" tabindex="3" name="txtLastName" id="txtLastName" value="" style="width: 100%"
                        class="content-input ui-widget-content ui-corner-all" />
                </td>
                <td>
                </td>
                <td class="header">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="header">
                    Sex
                </td>
                <td>
                    <input type="radio" name="radSex" id="radMale" value="<%= SexConstant.Male.Key %>"
                        checked="checked" tabindex="4" />
                    <label for="radMale">
                        <%= SexConstant.Male.Value %></label>
                    <input type="radio" name="radSex" id="radFemale" value="<%= SexConstant.Female.Key %>" />
                    <label for="radFemale" tabindex="5">
                        <%= SexConstant.Female.Value %></label>
                </td>
                <td>
                </td>
                <td class="header">
                    Nationality
                </td>
                <td>
                    <input type="text" tabindex="9" name="txtNationality" id="txtNationality" value=""
                        style="width: 100%" class="content-input ui-widget-content ui-corner-all" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    Remark
                </td>
                <td colspan="4">
                    <textarea id="txtRemark" tabindex="10" style="width: 100%; height: 40px;" class="content-input ui-widget-content ui-corner-all"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="RosterForm" class="dialog-form" title="Appointment" style="display: none;">
        <input type="hidden" id="hdId" value="" />
        <div class="title" id="dialog-modal" style="width: 100%; text-align: center;">
            <span class="loading"></span>
        </div>
        <table class="table-form" id="tblContent">
            <tr>
                <td class="header">
                    Status
                </td>
                <td colspan="2">
                    <asp:DropDownList runat="server" ID="cboStatus">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="header">
                    Doctor
                </td>
                <td colspan="2">
                    <span id="lblDoctor" style="font-weight: bold;"></span>
                    <input type="hidden" id="hdfDoctor" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    Patient
                </td>
                <td>
                    <input type="text" id="txtPatient" />
                </td>
                <td>
                    <input type="button" id="createUser" value="New" style="width: 50px;" runat="server" />
                    <input type="button" id="changeUser" value="Change" style="width: 50px;" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    From
                </td>
                <td colspan="2">
                    <select id="cboFromHour">
                    </select><input type="text" id="txtFromDate" class="datePicker" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    To
                </td>
                <td colspan="2">
                    <select id="cboToHour">
                    </select><input type="text" id="txtToDate" class="datePicker" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="header">
                    Room
                </td>
                <td colspan="2">
                    <select id="cboRoom">
                    </select>
                    <input type="hidden" id="hdfRoom" />
                    <span id="loadingRoom" class="loading"></span>
                </td>
            </tr>
            <tr>
                <td class="header">
                    Note
                </td>
                <td colspan="2">
                    <textarea id="txtNote" cols="10" rows="3" style="width: 100%;"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div class="box">
        <div class="title">
            <h5>
                Appointment</h5>
        </div>
        <div>
            <div style="padding: 10px;">
                <div class="appt-info">
                    <h5 style="padding: 0 0 5px 0; margin: 0;">
                        Patient's Information</h5>
                    <div class="title-info">
                        First Name</div>
                    <div class="content-info" id="divFirstname" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        Last Name</div>
                    <div class="content-info" id="divLastname">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                    <div class="title-info">
                        Mobile phone</div>
                    <div class="content-info" id="divCellPhone" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        DOB</div>
                    <div class="content-info" id="divBirthday">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                    <div class="title-info">
                        Remark</div>
                    <div class="content-info" id="divNote" style="width: 435px;">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div id="clockHolder">
                    <div class="rotatingWrapper">
                        <img id="sec" src="<%= Page.ResolveClientUrl("~/resources/components/analogClock/images/second.png") %>"
                            style="-webkit-transform: rotate(658.5deg);"></div>
                    <div class="rotatingWrapper">
                        <img id="hour" src="<%= Page.ResolveClientUrl("~/resources/components/analogClock/images/hour.png") %>"
                            style="-webkit-transform: rotate(342deg);"></div>
                    <div class="rotatingWrapper">
                        <img id="min" src="<%= Page.ResolveClientUrl("~/resources/components/analogClock/images/minute.png") %>"
                            style="-webkit-transform: rotate(228deg);"></div>
                    <img id="clock" src="<%= Page.ResolveClientUrl("~/resources/components/analogClock/images/clockface.jpg") %>">
                </div>
                <div style="float: right; width: 251px; height: 180px;" id="datepicker">
                </div>
                <div class="status-info">
                    <asp:Repeater ID="rptStatus" runat="server">
                        <ItemTemplate>
                            <div class="tinybox" style="background-color: <%# Eval("ColorCode") %>">
                            </div>
                            <span class="tinybox-info">
                                <%# Eval("Title") %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div id="box-tabs" class="box">
        <div class="title">
            <ul class="links" id="service-tabs" style="float: left;">
            </ul>
        </div>
        <div>
            <div id="scheduler_here" class="dhx_cal_container" style='width: 100%; height: 530px;'>
                <div class="dhx_cal_navline">
                    <div class="dhx_cal_prev_button">
                        &nbsp;</div>
                    <div class="dhx_cal_next_button">
                        &nbsp;</div>
                    <div class="dhx_cal_today_button">
                    </div>
                    <div class="dhx_cal_date">
                    </div>
                    <div class="dhx_cal_tab" name="week_tab">
                    </div>
                    <div class="dhx_cal_tab" name="day_tab">
                    </div>
                    <div class="dhx_cal_tab" name="timeline_tab">
                    </div>
                    <div class="dhx_cal_tab" name="unit_tab">
                    </div>
                    <div class="dhx_cal_tab" name="month_tab">
                    </div>
                </div>
                <div class="dhx_cal_header">
                </div>
                <div class="dhx_cal_data" id="main-dhx">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
