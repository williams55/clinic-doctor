<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Grid.aspx.cs" Inherits="Admin_Appointment_Grid" %>

<%@ Import Namespace="AppointmentSystem.Settings.BusinessLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Appointment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <script type="text/javascript">
        var minuteStep = <%=ServiceFacade.SettingsHelper.MinuteStep %>;
        function OnClickButtonDel() {
            if (grid.GetSelectedRowCount() <= 0) {
                ShowDialog(null, null, "Please select item first.");
                return;
            }
            if (confirm("Are you sure to delete these items")) {
                grid.PerformCallback('Delete');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>Appointment</h5>
            <ul class="links">
                <li><a href="Default.aspx">Scheduler</a></li>
                <li class="ui-tabs-selected"><a href="javascript:return false;">Grid</a></li>
            </ul>
        </div>
        <table cellpadding="0" cellspacing="0" style="margin: 16px 0">
            <tr>
                <td style="padding-right: 4px; padding-left: 4px;">
                    <dx:ASPxButton ID="btnSelectAll" runat="server" Text="Select All" UseSubmitBehavior="False"
                        AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e) { grid.SelectRows(); }" />
                    </dx:ASPxButton>
                </td>
                <td style="padding-right: 4px">
                    <dx:ASPxButton ID="btnUnselectAll" runat="server" Text="Unselect All" UseSubmitBehavior="False"
                        AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e) { grid.UnselectRows(); }" />
                    </dx:ASPxButton>
                </td>
                <td style="padding-right: 4px">
                    <dx:ASPxButton ID="btnSelectAllOnPage" runat="server" Text="Select all on the page"
                        UseSubmitBehavior="False" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e) { grid.SelectAllRowsOnPage(); }" />
                    </dx:ASPxButton>
                </td>
                <td style="padding-right: 4px">
                    <dx:ASPxButton ID="btnUnselectAllOnPage" runat="server" Text="Unselect all on the page"
                        UseSubmitBehavior="False" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e) { grid.UnselectAllRowsOnPage(); }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to Excel" UseSubmitBehavior="False"
                        OnClick="btnXlsExport_Click" />
                </td>
            </tr>
        </table>
        <div id="box-order">
            <dx:ASPxGridView ID="gridAppointment" ClientInstanceName="grid" runat="server" DataSourceID="AppointmentDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="Id" Width="100" ReadOnly="True" SortOrder="Descending" VisibleIndex="1">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="ServicesId" Caption="Service" Width="100" VisibleIndex="2" Visible="False">
                        <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="ServicesDataSource">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="Username" Caption="Doctor" VisibleIndex="3" Width="120">
                        <PropertiesComboBox TextField="DisplayName" ValueField="Username" DataSourceID="UsersDataSource">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="PatientCode" Caption="Patient Code" Width="80" VisibleIndex="4">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="PatientCode" Caption="Patient Name" Width="150" VisibleIndex="5">
                        <PropertiesComboBox TextFormatString="{1} {3}" ValueField="PatientCode" DataSourceID="VcsPatient">
                            <Columns>
                                <dx:ListBoxColumn FieldName="PatientCode" Width="80px" />
                                <dx:ListBoxColumn FieldName="FirstName" Width="120px" />
                                <dx:ListBoxColumn FieldName="MiddleName" Width="80px" />
                                <dx:ListBoxColumn FieldName="LastName" Width="120px" />
                            </Columns>
                        </PropertiesComboBox>
                        <Settings AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="RoomId" Caption="Room" Width="100" VisibleIndex="6" Visible="False">
                        <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="RoomDataSource">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn FieldName="StartTime" Width="120" Caption="From" SortIndex="0" VisibleIndex="7">
                        <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                            EnableAnimation="False">
                        </PropertiesDateEdit>
                        <Settings AutoFilterCondition="GreaterOrEqual" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="To" FieldName="EndTime" Width="120px" VisibleIndex="8">
                        <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                            EnableAnimation="False">
                        </PropertiesDateEdit>
                        <Settings AutoFilterCondition="LessOrEqual" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataColumn FieldName="Note" Width="130" VisibleIndex="9">
                        <Settings AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="StatusId" Caption="Status" Width="50" VisibleIndex="10">
                        <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="StatusDataSource">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                </Columns>
                <Styles>
                    <AlternatingRow Enabled="true" />
                    <Table Wrap="True">
                    </Table>
                </Styles>
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gridAppointment" ExportedRowType="Selected" />
        </div>
        <data:AppointmentDataSource ID="AppointmentDataSource" runat="server" SelectMethod="GetPaged"
            EnableCaching="False" EnablePaging="True" EnableSorting="True" InsertMethod="Insert"
            UpdateMethod="Update">
            <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            </DeepLoadProperties>
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="PageIndex" ControlID="gridAppointment" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:AppointmentDataSource>
        <data:UsersDataSource SelectMethod="GetPaged" runat="server" ID="UsersDataSource">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UsersDataSource>
        <data:VcsPatientDataSource ID="VcsPatient" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:VcsPatientDataSource>
        <data:RoomDataSource SelectMethod="GetPaged" runat="server" ID="RoomDataSource">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:RoomDataSource>
        <data:ServicesDataSource ID="ServicesDataSource" runat="server" SelectMethod="GetPaged"
            EnablePaging="True" EnableSorting="True">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:ServicesDataSource>
        <data:StatusDataSource ID="StatusDataSource" runat="server" SelectMethod="GetPaged"
            EnablePaging="True" EnableSorting="True">
            <Parameters>
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:StatusDataSource>
    </div>
</asp:Content>
