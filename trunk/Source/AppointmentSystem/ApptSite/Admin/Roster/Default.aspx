<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Roster_Default" %>

<%@ Import Namespace="AppointmentSystem.Settings.BusinessLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Roster
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

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_treetimeline.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_minical.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_readonly.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <script src="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/ext/dhtmlxscheduler_tooltip.js") %>"
        type="text/javascript" charset="utf-8"></script>

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/dhtmlxscheduler_dhx_terrace.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/components/dhtmlxScheduler/blocksection.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/css/scheduler.css") %>"
        type="text/css" media="screen" title="no title" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/resources/css/dialog-form.css") %>"
        type="text/css" media="screen" title="no title" />

    <script type="text/javascript" charset="utf-8">
        var weekday = <%=Constants.Weekdays %>;
        var stepTime = <%=ServiceFacade.SettingsHelper.MinuteStep %>;
        var html = function (id) { return document.getElementById(id); }; //just a helper
        
        var minuteStep = eval(<%=ServiceFacade.SettingsHelper.RosterMinuteStep%>);
        var maxHour = eval(<%=ServiceFacade.SettingsHelper.MaxHour%>);
        var maxMinute = eval(<%=ServiceFacade.SettingsHelper.MaxMinute%>);

        var listTree = <%=TreeList%>;
    </script>

    <style media="screen">
        /* enabling marked timespans for month view */.dhx_scheduler_month .dhx_marked_timespan
        {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Roster</h5>
            <ul class="links">
                <li class="ui-tabs-selected"><a href="javascript:return false;">Scheduler</a>
                    <div style="position: absolute; top: 20px; right: 70px;">
                        <dx:ASPxComboBox runat="server" TextField="Text" ValueField="Value" ID="cboSelector"
                            ValueType="System.String" ClientInstanceName="cboSelector" EncodeHtml="False" 
                            DropDownStyle="DropDownList" IncrementalFilteringMode="Contains">
                            <ClientSideEvents ValueChanged="function(s, e) { CallDoctorTree(); }"></ClientSideEvents>
                        </dx:ASPxComboBox>
                    </div>
                </li>
                <li><a href="Grid.aspx">Grid</a></li>
            </ul>
        </div>
        <div id="box-other">
            <div id="scheduler_here" class="dhx_cal_container" style='width: 100%; height: 600px;'>
                <div class="dhx_cal_navline">
                    <div class="dhx_cal_prev_button">
                        &nbsp;</div>
                    <div class="dhx_cal_next_button">
                        &nbsp;</div>
                    <div class="dhx_cal_today_button">
                    </div>
                    <div class="dhx_cal_date">
                    </div>
                    <div class="dhx_minical_icon" id="dhx_minical_icon" onclick="ShowMinical()" style="left: auto;
                        right: 220px;">
                        &nbsp;</div>
                    <div class="dhx_cal_tab" name="week_tab">
                    </div>
                    <div class="dhx_cal_tab" name="day_tab">
                    </div>
                    <div class="dhx_cal_tab" name="timeline_tab">
                    </div>
                    <div class="dhx_cal_tab" name="month_tab">
                    </div>
                </div>
                <div class="dhx_cal_header">
                </div>
                <div class="dhx_cal_data">
                </div>
            </div>
        </div>
    </div>
    <div class="dhx_cal_light dhx_cal_light_wide" id="form-dhtmlx" style="height: 320px;
        display: none;">
        <input type="hidden" id="hdId" value="" />
        <div class="dhx_cal_ltitle" id="drag-title">
            <span class="dhx_mark">&nbsp;</span><span class="dhx_time"></span><span class="dhx_title"></span><div
                class="dhx_close_icon" onclick="CancelRoster();" title="Close form without save (Esc)">
            </div>
        </div>
        <div class="dhx_cal_larea" style="height: 220px;">
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required">
                    Doctor</div>
                <div class="dhx_cal_ltext">
                    <dx:ASPxComboBox ID="cboDoctor" ClientInstanceName="cboDoctor" runat="server" Width="550"
                        DropDownWidth="550" DropDownStyle="DropDownList" DataSourceID="UsersDataSource"
                        ValueField="Username" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                        IncrementalFilteringMode="StartsWith">
                        <Columns>
                            <dx:ListBoxColumn FieldName="Username" Width="130px" />
                            <dx:ListBoxColumn FieldName="DisplayName" Width="200px" />
                        </Columns>
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Doctor is required" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required">
                    Roster Type</div>
                <div class="dhx_cal_ltext">
                    <dx:ASPxComboBox runat="server" DataSourceID="RosterTypeDataSource" Width="550" TextField="Title"
                        ValueField="Id" ID="cboRosterType" ValueType="System.Int32" ClientInstanceName="cboRosterType"
                        Value='<%# ServiceFacade.SettingsHelper.DefaultRosterType %>' Enabled="False">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Roster Type is required" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div class="dhx_wrap_section" id="repeater-section">
                <div class="dhx_cal_lsection">
                    <div class="dhx_custom_button" index="1" id="btnRepeat">
                        <div class="dhx_custom_button_recurring">
                        </div>
                        <div>
                            Disabled</div>
                    </div>
                    Repeat event</div>
                <div class="dhx_cal_ltext" style="display: none;">
                    <input id="chkRepeat" type="checkbox" value="repeated" style="display: none;" />
                    <table border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <input id="chkWeekday_Sunday" type="checkbox" value="0" /><label for="chkWeekday_Sunday">Sunday</label>
                                </td>
                                <td>
                                    <input id="chkWeekday_Monday" type="checkbox" value="1" /><label for="chkWeekday_Monday">Monday</label>
                                </td>
                                <td>
                                    <input id="chkWeekday_Tuesday" type="checkbox" value="2" /><label for="chkWeekday_Tuesday">Tuesday</label>
                                </td>
                                <td>
                                    <input id="chkWeekday_Wednesday" type="checkbox" value="3" /><label for="chkWeekday_Wednesday">Wednesday</label>
                                </td>
                                <td>
                                    <input id="chkWeekday_Thursday" type="checkbox" value="4" /><label for="chkWeekday_Thursday">Thursday</label>
                                </td>
                                <td>
                                    <input id="chkWeekday_Friday" type="checkbox" value="5" /><label for="chkWeekday_Friday">Friday</label>
                                </td>
                                <td>
                                    <input id="chkWeekday_Saturday" type="checkbox" value="6" /><label for="chkWeekday_Saturday">Saturday</label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection required">
                    Time period</div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxTimeEdit ID="startTime" runat="server" ClientInstanceName="startTime" EditFormatString="HH:mm"
                        DisplayFormatString="HH:mm" Width="70px" EditFormat="Time">
                    </dx:ASPxTimeEdit>
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxDateEdit ID="startDate" ClientInstanceName="startDate" runat="server" EditFormatString="MM/dd/yyyy"
                        DisplayFormatString="MM/dd/yyyy" Width="150px">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="From Date is required" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </div>
                <div class="dhx_cal_ltext" style="float: left; text-align: center;">
                    -
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxTimeEdit ID="endTime" ClientInstanceName="endTime" runat="server" EditFormatString="HH:mm"
                        DisplayFormatString="HH:mm" Width="70px" EditFormat="Time">
                    </dx:ASPxTimeEdit>
                </div>
                <div class="dhx_cal_ltext" style="float: left;">
                    <dx:ASPxDateEdit ID="endDate" ClientInstanceName="endDate" runat="server" EditFormatString="MM/dd/yyyy"
                        DisplayFormatString="MM/dd/yyyy" Width="150px">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="To Date is required" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div class="dhx_form_row">
                <div class="dhx_cal_lsection">
                    Note</div>
                <div class="dhx_cal_ltext">
                    <textarea id="txtNote" style="width: 550px; font-family: Arial;" rows="3"></textarea>
                </div>
            </div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_save_btn_set" id="divSave" runat="server">
            <div dhx_button="1" class="dhx_save_btn">
            </div>
            <div title="Save roster (Enter)" onclick="NewRoster();" id="btnSave" runat="server">
                Save</div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_save_btn_set" id="divUpdate" runat="server">
            <div title="Update roster (Enter)" onclick="UpdateRoster();" id="btnUpdate" runat="server">
                Update</div>
        </div>
        <div class="dhx_btn_set dhx_left_btn_set dhx_cancel_btn_set">
            <div dhx_button="1" class="dhx_cancel_btn">
            </div>
            <div title="Cancel editing (Esc)" onclick="CancelRoster();">
                Cancel</div>
        </div>
        <div class="dhx_btn_set dhx_right_btn_set dhx_delete_btn_set" style="float: right;"
            id="divDelete" runat="server">
            <div dhx_button="1" class="dhx_delete_btn">
            </div>
            <div title="Delete current roster" id="delete-form-roster" onclick="DeleteRoster();">
                Delete</div>
        </div>
    </div>
    <data:UsersDataSource SelectMethod="GetPaged" runat="server" ID="UsersDataSource">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:UsersDataSource>
    <data:RosterTypeDataSource SelectMethod="GetPaged" runat="server" ID="RosterTypeDataSource">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="Title ASC" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:RosterTypeDataSource>
</asp:Content>
