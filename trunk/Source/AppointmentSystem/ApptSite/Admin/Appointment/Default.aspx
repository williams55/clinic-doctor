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

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/resources/scripts/jquery.scrollTo-1.4.2-min.js") %>"></script>

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/dhtmlxscheduler_dhx_terrace.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/blocksection.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/css/scheduler.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/css/dialog-form.css") %>"
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
    </script>

    <style media="screen">
        /* enabling marked timespans for month view */ .dhx_scheduler_month .dhx_marked_timespan {
            display: block;
        }

        a.autocomplete {
            border-top: 1px solid gray;
            margin: 0;
            padding: 0;
        }

        a:hover.autocomplete {
            font-weight: normal;
        }

        a.autocomplete div.col {
            float: left;
            padding: 0;
            margin: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div class="box">
        <div class="title">
            <h5>Appointment</h5>
            <ul class="links">
                <li class="ui-tabs-selected">
                    <a href="javascript:return false;">Scheduler</a>
                </li>
                <li><a href="Grid.aspx">Grid</a></li>
            </ul>
        </div>
        <div>
            <div style="padding: 5px;">
                <div class="appt-info">
                    <h5 style="padding: 0 0 5px 0; margin: 0;">Patient's Information</h5>
                    <input type="hidden" id="hdPatient" value="" />
                    <div class="title-info">
                        First Name
                    </div>
                    <div class="content-info" id="divFirstname" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        Last Name
                    </div>
                    <div class="content-info" id="divLastname">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                    <div class="title-info">
                        Mobile phone
                    </div>
                    <div class="content-info" id="divCellPhone" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        DOB
                    </div>
                    <div class="content-info" id="divBirthday">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                    <div class="title-info">
                        Exp Date
                    </div>
                    <div class="content-info required" id="divExpDate" style="margin-right: 35px;">
                        &nbsp;
                    </div>
                    <div class="title-info">
                        Nationality
                    </div>
                    <div class="content-info" id="divNationality">
                        &nbsp;
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div style="float: right; width: 251px;" id="datepicker">
                </div>
                <div class="appt-info" style="float: right; width: 270px; height: 125px; margin-right: 20px;">
                    <h5 style="padding: 0 0 5px 0; margin: 0; border: 0px;">Patient Remark</h5>
                    <textarea id="apptRemark" rows="5" cols="20" style="width: 274px;" maxlength="200"></textarea>
                    <div class="dhx_btn_set dhx_left_btn_set dhx_save_btn_set" style="margin: 5px 0; float: right; height: 20px; line-height: 20px;">
                        <div title="Save appointment remark" onclick="SaveRemark();" style="height: 20px; line-height: 20px;">
                            Save
                        </div>
                    </div>
                </div>
                <div class="status-info">
                    <asp:Repeater ID="rptStatus" runat="server" DataSourceID="StatusDataSource">
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
                        &nbsp;
                    </div>
                    <div class="dhx_cal_next_button">
                        &nbsp;
                    </div>
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
    <div class="dhx_cal_light dhx_cal_light_wide" id="form-scheduler" style="height: 560px; width: 620px; display: none;">
        <asp:HiddenField runat="server" ID="hdId" Value="" />
        <div class="dhx_cal_ltitle" id="drag-title">
            <span class="dhx_mark">&nbsp;</span><span class="dhx_time"></span><span class="dhx_title"></span><div
                class="dhx_close_icon" onclick="CancelAppointment();" title="Close form without save (Esc)">
            </div>
        </div>
        <div class="dhx_cal_larea" style="height: 460px; width: 602px;">
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Status
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxComboBox runat="server" DataSourceID="StatusDataSource" Width="165px" TextField="Title"
                        ValueField="Id" ID="cboStatus" ValueType="System.String" ClientInstanceName="cboStatus">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Status is required" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </div>
                <div class="dhx_cal_lsection required" style="float: left; width: 80px;">
                    Service
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxComboBox runat="server" DataSourceID="ServicesDataSource" Width="165px" TextField="Title"
                        ValueField="Id" ID="cboService" ValueType="System.Int32" ClientInstanceName="cboService">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Service is required" />
                        </ValidationSettings>
                        <ClientSideEvents ValueChanged="function(s, e) { RefreshDoctorList(); }" />
                    </dx:ASPxComboBox>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Doctor
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <asp:HiddenField runat="server" ID="hdDocCon" Value="IsDisabled = 'False'" />
                    <dx:ASPxComboBox ID="cboDoctor" ClientInstanceName="cboDoctor" runat="server" Width="165px"
                        DropDownStyle="DropDownList" ValueField="Username" TextField="DisplayName" ValueType="System.String"
                        TextFormatString="{1}" EnableCallbackMode="true" OnCallback="cboDoctor_OnCallback"
                        IncrementalFilteringMode="StartsWith">
                        <Columns>
                            <dx:ListBoxColumn FieldName="Username" Width="130px" />
                            <dx:ListBoxColumn FieldName="DisplayName" Width="200px" />
                        </Columns>
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Doctor is required" />
                        </ValidationSettings>
                        <ClientSideEvents ValueChanged="function(s, e) { RefreshRoomList(); }" EndCallback="function(s, e) { 
                            if(doctor){ if(s.FindItemByValue(doctor)) s.SetSelectedItem(s.FindItemByValue(doctor)); s.Validate(); } }"
                            Init="function(s, e) { RefreshRoomList(); }" />
                    </dx:ASPxComboBox>
                </div>
                <div class="dhx_cal_lsection required" style="float: left; width: 80px;">
                    Room
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxComboBox runat="server" Width="165px" TextField="Title" ValueField="Id" ID="cboRoom"
                        ValueType="System.Int32" ClientInstanceName="cboRoom" OnCallback="cboRoom_OnCallback">
                        <ClientSideEvents EndCallback="function(s, e) { 
                            if(room){  if(s.FindItemByValue(room))s.SetSelectedItem(s.FindItemByValue(room)); } }" />
                    </dx:ASPxComboBox>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Patient
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <input type="text" name="patient" id="patient-search" maxlength="20" style="width: 200px;" class="form-input" />
                    <input type="hidden" name="patient" id="patient-name" />
                    <input type="hidden" name="patient" id="patient-code" />
                    <img class="error-icon" id="patient-error" title="Patient is required" />
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <input type="button" id="createUser" value="New" style="width: 50px;" runat="server"
                        onclick="OpenPatient();" />
                    <input type="button" id="changeUser" value="Change" style="width: 50px;" runat="server"
                        onclick="OpenPatient(true);" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required">
                    Time period
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxTimeEdit ID="startTime" runat="server" ClientInstanceName="startTime" EditFormatString="HH:mm"
                        DisplayFormatString="HH:mm" Width="55">
                    </dx:ASPxTimeEdit>
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxDateEdit ID="startDate" ClientInstanceName="startDate" runat="server" EditFormatString="MM/dd/yyyy"
                        DisplayFormatString="MM/dd/yyyy" Width="85">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Start time is required" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </div>
                <div class="dhx_cal_ltext" style="float: left; text-align: center;">
                    &ndash;
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxTimeEdit ID="endTime" ClientInstanceName="endTime" runat="server" EditFormatString="HH:mm"
                        DisplayFormatString="HH:mm" Width="55">
                    </dx:ASPxTimeEdit>
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxDateEdit ID="endDate" ClientInstanceName="endDate" runat="server" EditFormatString="MM/dd/yyyy"
                        DisplayFormatString="MM/dd/yyyy" Width="85">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="End time is required" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection">
                    Note
                </div>
                <div class="dhx_cal_ltext">
                    <textarea id="txtNote" style="width: 450px; font-family: Arial;" rows="2" maxlength="500"></textarea>
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_ltext">
                    <dx:ASPxGridView ID="gridHistory" ClientInstanceName="grid" runat="server" Width="100%"
                        KeyFieldName="Guid" OnCustomCallback="gridHistory_OnCustomCallback">
                        <Columns>
                            <dx:GridViewDataColumn Caption="No." Width="50">
                                <DataItemTemplate>
                                    <%# Container.ItemIndex + 1%>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <EditFormSettings Visible="False" />
                                <Settings AllowSort="False"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataDateColumn FieldName="UpdateDate" Width="140" Caption="Time">
                                <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy HH:mm:ss" EditFormat="Custom"
                                    EditFormatString="MM/dd/yyyy HH:mm:ss" EnableAnimation="False" Width="100%">
                                </PropertiesDateEdit>
                                <Settings AutoFilterCondition="GreaterOrEqual" />
                                <Settings AllowSort="False"></Settings>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="UpdateUser" Caption="User" Width="80">
                                <Settings AllowSort="False"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="LogMessage">
                                <Settings AllowSort="False"></Settings>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Styles>
                            <AlternatingRow Enabled="true" />
                            <Table Wrap="True">
                            </Table>
                        </Styles>
                        <Settings ShowVerticalScrollBar="True" />
                        <SettingsPager Visible="False">
                        </SettingsPager>
                    </dx:ASPxGridView>
                </div>
            </div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_save_btn_set" id="divSave" runat="server">
            <div dhx_button="1" class="dhx_save_btn">
            </div>
            <div title="Save roster (Enter)" onclick="NewAppointment();" id="btnSave" runat="server">
                Save
            </div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_save_btn_set" id="divUpdate" runat="server">
            <div title="Update roster (Enter)" onclick="UpdateAppointment();" id="btnUpdate" runat="server">
                Update
            </div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_cancel_btn_set">
            <div dhx_button="1" class="dhx_cancel_btn">
            </div>
            <div title="Cancel editing (Esc)" onclick="CancelAppointment();">
                Cancel
            </div>
        </div>
        <div class="dhx_btn_set dhx_right_btn_set dhx_delete_btn_set" style="float: right;"
            id="divDelete" runat="server">
            <div dhx_button="1" class="dhx_delete_btn">
            </div>
            <div title="Delete current roster" id="delete-form-roster" onclick="DeleteAppointment();">
                Delete
            </div>
        </div>
        <div class="dhx_btn_set dhx_right_btn_set dhx_delete_btn_set" style="float: right;"
            id="divManualSms" runat="server">
            <div dhx_button="1" class="dhx_delete_btn">
            </div>
            <div title="Send SMS" id="send-sms-form" onclick="SendSms();">
                Send SMS
            </div>
        </div>
    </div>
    <div class="dhx_cal_light dhx_cal_light_wide" id="form-patient" style="height: 410px; width: 620px; display: none;">
        <div class="dhx_cal_ltitle" id="drag-title2">
            <span class="dhx_mark">&nbsp;</span><span class="dhx_time"></span><span class="dhx_title"></span><div
                class="dhx_close_icon" onclick="ClosePatient();" title="Close form without save (Esc)">
            </div>
        </div>
        <div class="dhx_cal_larea" style="height: 310px; width: 602px;">
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Patient Code
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxTextBox runat="server" ReadOnly="True" ID="txtPatientCode" Text="" Width="130px"
                        CssClass="text-form" ClientInstanceName="txtPatientCode">
                    </dx:ASPxTextBox>
                </div>
                <div class="dhx_cal_lsection" style="float: left; width: 120px;">
                    Sex
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxRadioButton runat="server" Text="Male" ID="radMale" GroupName="radSex" Layout="Flow"
                        ClientInstanceName="radMale" />
                    <dx:ASPxRadioButton runat="server" Text="Female" ID="radFemale" GroupName="radSex"
                        Layout="Flow" ClientInstanceName="radFemale" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Last Name
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxTextBox runat="server" ID="txtLastName" Text="" CssClass="text-form" MaxLength="50"
                        Width="130px" ClientInstanceName="txtLastName">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Last Name is required" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <div class="dhx_cal_lsection" style="float: left; width: 120px;">
                    Middle Name
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxTextBox runat="server" ID="txtMiddleName" Text="" CssClass="text-form" MaxLength="50"
                        Width="130px" ClientInstanceName="txtMiddleName">
                    </dx:ASPxTextBox>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    First Name
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxTextBox runat="server" ID="txtFirstName" Text="" CssClass="text-form" MaxLength="50"
                        Width="130px" ClientInstanceName="txtFirstName">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="First Name is required" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <div class="dhx_cal_lsection required" style="float: left; width: 120px;">
                    DOB <span class="min-day">(MM/dd/yyyy)</span>
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxDateEdit ID="txtDob" ClientInstanceName="txtDob" runat="server" EditFormatString="MM/dd/yyyy"
                        DisplayFormatString="MM/dd/yyyy" Width="130px">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="DOB is required" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Nationality
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxComboBox ID="cboNationality" ClientInstanceName="cboNationality" runat="server"
                        Width="130px" DropDownStyle="DropDownList" DataSourceID="CountryDataSource" ValueField="CitizenName"
                        TextField="CitizenName" ValueType="System.String" EnableCallbackMode="False"
                        IncrementalFilteringMode="StartsWith">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Nationality is required" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </div>
                <div class="dhx_cal_lsection" style="float: left; width: 120px;">
                    Company
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxComboBox ID="cboCompany" ClientInstanceName="cboCompany" runat="server" Width="130px"
                        DropDownStyle="DropDownList" DropDownWidth="300px" DataSourceID="CompanyDataSource"
                        ValueField="CompanyCode" TextField="CompanyName" ValueType="System.String" TextFormatString="{1}"
                        EnableCallbackMode="False" IncrementalFilteringMode="StartsWith">
                        <Columns>
                            <dx:ListBoxColumn FieldName="CompanyCode" Width="100" />
                            <dx:ListBoxColumn FieldName="CompanyName" />
                        </Columns>
                    </dx:ASPxComboBox>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required" style="float: left;">
                    Mobile Phone
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxTextBox runat="server" ID="txtMobilePhone" Text="" CssClass="text-form" MaxLength="50"
                        Width="130px" ClientInstanceName="txtMobilePhone">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Mobile Phone is required" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <div class="dhx_cal_lsection" style="float: left; width: 120px;">
                    Home Phone
                </div>
                <div class="dhx_cal_ltext" style="float: left; width: 165px;">
                    <dx:ASPxTextBox runat="server" ID="txtHomePhone" Text="" CssClass="text-form" MaxLength="50"
                        Width="130px" ClientInstanceName="txtHomePhone">
                    </dx:ASPxTextBox>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection">
                    Remark
                </div>
                <div class="dhx_cal_ltext">
                    <textarea id="txtRemark" style="width: 435px; font-family: Arial;" rows="3"></textarea>
                </div>
            </div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_save_btn_set">
            <div dhx_button="1" class="dhx_save_btn">
            </div>
            <div title="Save patient (Enter)" onclick="SavePatient();" id="btnSavePatient">
                Save
            </div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_cancel_btn_set">
            <div dhx_button="1" class="dhx_cancel_btn">
            </div>
            <div title="Cancel editing (Esc)" onclick="ClosePatient();">
                Cancel
            </div>
        </div>
    </div>
    <data:UsersDataSource SelectMethod="GetPaged" runat="server" ID="UsersDataSource">
        <Parameters>
            <asp:ControlParameter Name="WhereClause" ControlID="hdDocCon" ConvertEmptyStringToNull="False" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:UsersDataSource>
    <data:StatusDataSource SelectMethod="GetPaged" runat="server" ID="StatusDataSource"
        EnableSorting="True">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderBy" Value="PriorityIndex ASC" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:StatusDataSource>
    <data:VcsPatientDataSource ID="PatientDataSource" runat="server" SelectMethod="GetPaged">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:VcsPatientDataSource>
    <data:VcsCompanyDataSource ID="CompanyDataSource" runat="server" SelectMethod="GetPaged">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:VcsCompanyDataSource>
    <data:VcsCountryDataSource ID="CountryDataSource" runat="server" SelectMethod="GetPaged">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:VcsCountryDataSource>
    <data:RoomDataSource SelectMethod="GetPaged" runat="server" ID="RoomDataSource" EnablePaging="True"
        EnableSorting="True">
        <DeepLoadProperties Method="IncludeChildren" Recursive="False">
        </DeepLoadProperties>
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:RoomDataSource>
    <data:ServicesDataSource SelectMethod="GetPaged" runat="server" ID="ServicesDataSource">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:ServicesDataSource>
</asp:Content>
