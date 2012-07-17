<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Appointment_Default" %>

<%@ Import Namespace="AppointmentSystem.Settings.BusinessLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Appointment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script src="<%= Page.ResolveClientUrl("~/resources/scripts/date.format.js") %>"
        type="text/javascript"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/scripts/json2.js") %>" type="text/javascript"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/dhtmlxscheduler.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_timeline.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_treetimeline.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_minical.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_readonly.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_tooltip.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/resources/components/tokeninput/jquery.tokeninput.js") %>"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/resources/scripts/maxZIndex.js") %>"></script>

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/tokeninput/styles/token-input.css") %>"
        type="text/css" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/dhtmlxscheduler.css") %>"
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
        var floors = eval(<%=StrFloors%>);
        
        var minuteStep = eval(<%=ServiceFacade.SettingsHelper.MinuteStep%>);
        var maxHour = eval(<%=ServiceFacade.SettingsHelper.MaxHour%>);
        var maxMinute = eval(<%=ServiceFacade.SettingsHelper.MaxMinute%>);

    </script>

    <script src="GRNEditt.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Appointment</h5>
        </div>
        <div id="box-other">
            <div id="dialog-form" title="Create new patient" class="dialog-form">
                <p class="validateTips">
                </p>
                <form id="frm">
                <label for="name" class="title">
                    Sex</label>
                <input type="radio" name="radSex" id="radMale" value="false" checked="checked" />
                <label for="radMale">
                    Male</label>
                <input type="radio" name="radSex" id="radFemale" value="true" />
                <label for="radFemale">
                    Female</label>
                <div class="clear">
                </div>
                <label for="name" class="title">
                    Firstname</label>
                <input type="text" name="txtFirstName" id="txtFirstName" class="content-input ui-widget-content ui-corner-all" />
                <div class="clear">
                </div>
                <label for="email" class="title">
                    Lastname</label>
                <input type="text" name="txtLastName" id="txtLastName" value="" class="content-input ui-widget-content ui-corner-all" />
                <div class="clear">
                </div>
                <label for="txtCellPhone" class="title">
                    Cell Phone</label>
                <input type="text" name="txtCellPhone" id="txtCellPhone" value="" class="content-input ui-widget-content ui-corner-all" />
                <div class="clear">
                </div>
                <label for="txtAddress" class="title">
                    Address</label>
                <input type="text" name="txtAddress" id="txtAddress" value="" class="content-input ui-widget-content ui-corner-all" />
                <div class="clear">
                </div>
                </form>
            </div>
            <div style="padding: 10px;">
                <div class="appt-info">
                    <h3>
                        Patient's Information</h3>
                    <div class="title-info">
                        Firstname</div>
                    <div class="content-info" id="divFirstname" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        Lastname</div>
                    <div class="content-info" id="divLastname">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                    <div class="title-info">
                        Cell phone</div>
                    <div class="content-info" id="divCellPhone" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        Birthday</div>
                    <div class="content-info" id="divBirthday">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                    <div class="title-info">
                        Address</div>
                    <div class="content-info" id="divAddress" style="width: 435px;">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div style="float: right; padding: 10px 20px 10px 30px;">
                    <ul id="clock">
                        <li id="sec"></li>
                        <li id="hour"></li>
                        <li id="min"></li>
                    </ul>
                </div>
                <div style="float: right; width: 251px; height: 205px;" id="datepicker">
                    <div class="dhx_cal_today_button" style="margin-left: 90px; font-family: Tahoma;
                        font-size: 11px;">
                        Today
                    </div>
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
            <div id="RosterForm" class="schedulerForm" style="width: 650px;">
                <input type="hidden" id="hdId" value="" />
                <table cellpadding="3" width="100%" id="tblContent">
                    <tr>
                        <td colspan="4" class="title" id="tdTitle">
                        </td>
                    </tr>
                    <tr>
                        <td class="header">
                            Status
                        </td>
                        <td colspan="3">
                            <asp:DropDownList runat="server" ID="cboStatus">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="header">
                            Patient
                        </td>
                        <td colspan="3">
                            <input type="text" id="txtPatient" style="width: 80%;" />
                            <input type="button" id="create-user" style="width: 80%;" value="Create new patient" />
                        </td>
                    </tr>
                    <tr>
                        <td class="header">
                            Note
                        </td>
                        <td colspan="3">
                            <textarea id="txtNote" cols="10" rows="2" style="width: 95%;" tabindex="108"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="header" width="100">
                            From Time
                        </td>
                        <td width="220">
                            <select id="cboFromHour" style="float: left;" tabindex="109">
                                <option value="">Set status to Deleted</option>
                            </select><span id="loadingFromHour" class="loading" style="float: left;"></span>
                            <span id="spanFromDate" style="float: left;">
                                <input type="text" id="txtFromDate" class="datePicker" readonly="readonly" style="padding: 4px 2px 4px 2px;"
                                    tabindex="110" /></span>
                        </td>
                        <td class="header" width="70">
                            To Time
                        </td>
                        <td>
                            <select id="cboToHour" style="float: left;" tabindex="111">
                            </select><span id="loadingToHour" class="loading" style="float: left;"></span> <span
                                id="spanToDate" style="float: left;">
                                <input type="text" id="txtToDate" class="datePicker" readonly="readonly" style="padding: 4px 2px 4px 2px;"
                                    tabindex="112" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="header">
                            Doctor
                        </td>
                        <td colspan="3">
                            <input type="text" id="txtDoctor" style="width: 80%;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="header">
                            Room
                        </td>
                        <td colspan="3">
                            <input type="text" id="txtRoom" style="width: 80%;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input type="submit" value="Save" id="btnSave" style="float: left;" tabindex="115" />
                            <input type="submit" value="Cancel" id="btnCancel" style="float: left;" tabindex="116" />
                            <input type="submit" value="Delete" id="btnDelete" style="float: right;" tabindex="117" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="scheduler_here" class="dhx_cal_container" style='width: 100%; height: 330px;'>
                <div class="dhx_cal_navline">
                    <div class="dhx_cal_today_button" id="btnToday" style="display: none; float: right;">
                    </div>
                    <div class="dhx_cal_date" style="right: 10px; left: auto;">
                    </div>
                    <asp:Repeater ID="rptFloor" runat="server">
                        <ItemTemplate>
                            <div class="dhx_cal_tab" name="<%# Eval("Id") %>_tab" style="left: <%# (Container.ItemIndex * 70) + 10 %>px;">
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="dhx_cal_header">
                </div>
                <div class="dhx_cal_data">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
