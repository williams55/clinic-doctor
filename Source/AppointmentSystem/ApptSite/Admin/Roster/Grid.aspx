<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Grid.aspx.cs" Inherits="Admin_Roster_Grid" %>

<%@ Import Namespace="AppointmentSystem.Settings.BusinessLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Roster
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <script type="text/javascript">
        var minuteStep = <%=ServiceFacade.SettingsHelper.RosterMinuteStep %>;
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
    <div class="color">
        <asp:HyperLink runat="server" ID="btnAdd" NavigateUrl="javascript:grid.AddNewRow()"
            ToolTip="New" CssClass="add"></asp:HyperLink>
        <a class="delete" title="Delete selected items" onclick="OnClickButtonDel()"></a>
    </div>
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Roster</h5>
            <ul class="links">
                <li><a href="Default.aspx">Messages</a></li>
                <li class="ui-tabs-selected"><a href="javascript:return false;">Grid</a></li>
            </ul>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRoster" ClientInstanceName="grid" runat="server" DataSourceID="RosterDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRoster_RowInserting"
                OnCustomButtonCallback="gridRoster_CustomButtonCallback" AutoGenerateColumns="False"
                OnRowUpdating="gridRoster_RowUpdating" OnCommandButtonInitialize="gridRoster_CommandButtonInitialize"
                OnCustomButtonInitialize="gridRoster_CustomButtonInitialize" OnCustomCallback="gridRoster_CustomCallback"
                OnHtmlRowCreated="gridRoster_OnHtmlRowCreated">
                <Columns>
                    <dx:GridViewDataColumn Caption="No." Width="50">
                        <DataItemTemplate>
                            <%# Container.ItemIndex + 1%>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Id" Width="100" ReadOnly="True" SortOrder="Descending" />
                    <dx:GridViewDataComboBoxColumn FieldName="Username" Caption="Doctor">
                        <PropertiesComboBox TextField="DisplayName" ValueField="Username" DataSourceID="UsersDataSource">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="RosterTypeId" Caption="Roster Type" Width="120">
                        <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="RosterTypeDataSource">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDateColumn FieldName="StartTime" Width="120" Caption="From" SortIndex="0">
                        <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                            EnableAnimation="False">
                        </PropertiesDateEdit>
                        <Settings AutoFilterCondition="GreaterOrEqual" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="To" FieldName="EndTime" Width="120px">
                        <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                            EnableAnimation="False">
                        </PropertiesDateEdit>
                        <Settings AutoFilterCondition="LessOrEqual" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataColumn FieldName="Note" Width="130">
                        <Settings AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn Caption="Operation" ButtonType="Image" Width="60">
                        <EditButton>
                            <Image Url="../../resources/images/icons/edit.png" ToolTip="Edit" AlternateText="Edit"
                                Height="15" Width="15">
                            </Image>
                        </EditButton>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete">
                                <Image Url="../../resources/images/icons/del.png" ToolTip="Delete" AlternateText="Delete"
                                    Height="15" Width="15">
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <CellStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="#">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init">
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbCheck" runat="server" OnInit="cbCheck_Init">
                            </dx:ASPxCheckBox>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Styles>
                    <AlternatingRow Enabled="true" />
                    <Table Wrap="True">
                    </Table>
                </Styles>
                <ClientSideEvents EndCallback="function(s, e) { AlertMessage(); RefreshGrid(); }"
                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}" 
                    CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}"
                    />
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
                <Templates>
                    <EditForm>
                        <div id="devexpress-form">
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table class="edit-form">
                                    <tbody>
                                        <% if (gridRoster.IsNewRowEditing)
                                           { %>
                                        <tr>
                                            <td class="title-row" style="width: 100px;">
                                                Id
                                            </td>
                                            <td class="content-row" style="width: 220px;">
                                                Auto Id
                                            </td>
                                            <td class="title-row" style="width: 80px;">
                                                From
                                            </td>
                                            <td class="content-row">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 80px; padding-right: 10px;">
                                                            <dx:ASPxTimeEdit ID="fromTime" runat="server" DateTime='<%# getDate(Eval("StartTime"), false) %>'
                                                                EditFormatString="HH:mm" DisplayFormatString="HH:mm" Width="100%">
                                                                <ClientSideEvents ButtonClick='function(s, e) {
                                                                    if (e.buttonIndex == -2) //increment button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes + minuteStep);
                                                                        s.SetDate(date);
                                                                    }
                                                                    else if (e.buttonIndex == -3) //down button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes - minuteStep); 
                                                                        s.SetDate(date);
                                                                    }}' />
                                                            </dx:ASPxTimeEdit>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxDateEdit ID="fromDate" runat="server" EditFormatString="MM/dd/yyyy" DisplayFormatString="MM/dd/yyyy"
                                                                Date='<%# Eval("StartTime") == null? DateTime.Now : DateTime.Parse(Eval("StartTime").ToString()) %>'
                                                                Width="100%">
                                                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                                    ErrorText="Error">
                                                                    <RequiredField IsRequired="True" ErrorText="From Date is required" />
                                                                </ValidationSettings>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Doctor
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxComboBox ID="cboDoctor" runat="server" Width="100%" DropDownWidth="550" DropDownStyle="DropDownList"
                                                    DataSourceID="UsersDataSource" ValueField="Username" ValueType="System.String"
                                                    TextFormatString="{0}" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                                                    Value='<%# Eval("Username") %>'>
                                                    <Columns>
                                                        <dx:ListBoxColumn FieldName="Username" Width="130px" />
                                                        <dx:ListBoxColumn FieldName="DisplayName" Width="200px" />
                                                    </Columns>
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Doctor is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td class="title-row">
                                                To
                                            </td>
                                            <td class="content-row">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 80px; padding-right: 10px;">
                                                            <dx:ASPxTimeEdit ID="endTime" runat="server" DateTime='<%# getDate(Eval("EndTime"), true) %>'
                                                                EditFormatString="HH:mm" DisplayFormatString="HH:mm" Width="100%">
                                                                <ClientSideEvents ButtonClick='function(s, e) {
                                                                    if (e.buttonIndex == -2) //increment button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes + minuteStep);
                                                                        s.SetDate(date);
                                                                    }
                                                                    else if (e.buttonIndex == -3) //down button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes - minuteStep); 
                                                                        s.SetDate(date);
                                                                    }}' />
                                                            </dx:ASPxTimeEdit>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxDateEdit ID="endDate" runat="server" EditFormatString="MM/dd/yyyy" DisplayFormatString="MM/dd/yyyy"
                                                                Date='<%# Eval("EndTime") == null? DateTime.Now : DateTime.Parse(Eval("EndTime").ToString()) %>'
                                                                Width="100%">
                                                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                                    ErrorText="Error">
                                                                    <RequiredField IsRequired="True" ErrorText="To Date is required" />
                                                                </ValidationSettings>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Roster Type
                                            </td>
                                            <td class="content-row">
                                                <asp:HiddenField runat="server" ID="hdfRosterType" Value='<%# Eval("RosterTypeId") %>'
                                                    Visible="False"></asp:HiddenField>
                                                <dx:ASPxComboBox runat="server" DataSourceID="RosterTypeDataSource" Width="100%"
                                                    TextField="Title" ValueField="Id" ID="cboRosterType" ValueType="System.Int32"
                                                    Value='<%# Eval("RosterTypeId") %>'>
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Roster Type is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td class="title-row">
                                                Is Repeat
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxCheckBox runat="server" ID="chkIsRepeat">
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Note
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxMemo runat="server" ID="txtNote" Text='<%# Bind("Note") %>' CssClass="text-form"
                                                    Width="100%" Height="50px">
                                                </dx:ASPxMemo>
                                            </td>
                                            <td class="title-row">
                                                Weekday
                                            </td>
                                            <td class="content-row">
                                                <asp:CheckBoxList ID="chkWeekday" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                                    <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                                    <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                                    <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                                    <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                                    <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                                    <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <% }
                                           else
                                           { %>
                                        <tr>
                                            <td class="title-row" style="width: 100px;">
                                                Id
                                            </td>
                                            <td class="content-row" style="width: 220px;">
                                                <dx:ASPxTextBox runat="server" ID="txtId" Text='<%# Eval("Id") %>' CssClass="text-form"
                                                    MaxLength="100" Width="100%" ReadOnly="True">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row" style="width: 100px;">
                                                Edit Mode
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxRadioButton runat="server" GroupName="EditRosterMode" ID="radSingle" Checked="True"
                                                    Text="Single" Layout="Flow" />
                                                <dx:ASPxRadioButton ID="radAllSimilar" runat="server" GroupName="EditRosterMode"
                                                    Text="All Similar Rosters" Layout="Flow" />
                                                <dx:ASPxRadioButton ID="radEnabledSimilar" runat="server" GroupName="EditRosterMode"
                                                    Text="Enabled Similar Rosters" Layout="Flow" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Doctor
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxComboBox ID="choDoctorEdit" runat="server" Width="100%" DropDownWidth="550"
                                                    DropDownStyle="DropDownList" DataSourceID="UsersDataSource" ValueField="Username"
                                                    ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                                                    Value='<%# Eval("Username") %>'>
                                                    <Columns>
                                                        <dx:ListBoxColumn FieldName="Username" Width="130px" />
                                                        <dx:ListBoxColumn FieldName="DisplayName" Width="200px" />
                                                    </Columns>
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Doctor is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td class="title-row">
                                                Similar rosters
                                            </td>
                                            <td class="title-row" rowspan="5" style="vertical-align: top">
                                                <asp:HiddenField runat="server" Value="<%# Eval(&quot;RepeatId&quot;) == null 
                                                    ? &quot;0 > 1&quot; : Eval(&quot;RepeatId&quot;, &quot;RepeatId = '{0}'&quot;)
                                                    + &quot; AND &quot; + Eval(&quot;Id&quot;, &quot;Id <> '{0}'&quot;)
                                                    + &quot; AND &quot; + &quot;IsDisabled = 'False' &quot; %>" ID="hdfRepeatId" />
                                                <dx:ASPxGridView ID="gridSimilarRoster" runat="server" DataSourceID="UpdateRosterDataSource"
                                                    KeyFieldName="Id" Width="100%" EnableRowsCache="False">
                                                    <Columns>
                                                        <dx:GridViewDataColumn Caption="No." Width="70">
                                                            <DataItemTemplate>
                                                                <%# Container.ItemIndex + 1%>
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn FieldName="Id" Width="120" ReadOnly="True" />
                                                        <dx:GridViewDataDateColumn FieldName="StartTime" Caption="Day" SortIndex="0" SortOrder="Descending">
                                                            <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" EnableAnimation="False">
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataColumn Width="40">
                                                            <DataItemTemplate>
                                                                <img src="../../resources/images/icons/cancel_sign.png" style="display: <%# DateTime.Parse(Eval("StartTime").ToString()) < DateTime.Now ? "block" : "none" %>"
                                                                    alt="Disabled" title="Cannot change this" class="icon-row" />
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Settings AllowSort="False"></Settings>
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Styles>
                                                        <AlternatingRow Enabled="true" />
                                                    </Styles>
                                                    <Settings ShowGroupPanel="False" ShowFilterRow="False" ShowFilterRowMenu="False"
                                                        ShowVerticalScrollBar="True" />
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                                <data:RosterDataSource ID="UpdateRosterDataSource" runat="server" SelectMethod="GetPaged"
                                                    EnablePaging="True" EnableSorting="True">
                                                    <DeepLoadProperties Method="IncludeChildren" Recursive="False">
                                                    </DeepLoadProperties>
                                                    <Parameters>
                                                        <asp:ControlParameter Name="WhereClause" ControlID="hdfRepeatId" Type="String" PropertyName="Value" />
                                                        <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                                                        <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                                                    </Parameters>
                                                </data:RosterDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Roster Type
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxComboBox runat="server" DataSourceID="RosterTypeDataSource" Width="100%"
                                                    TextField="Title" ValueField="Id" ID="cboRosterTypeEdit" ValueType="System.Int32"
                                                    Value='<%# Eval("RosterTypeId") %>'>
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Roster Type is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                From
                                            </td>
                                            <td class="content-row">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 80px; padding-right: 10px;">
                                                            <dx:ASPxTimeEdit ID="fromTimeEdit" runat="server" DateTime='<%# getDate(Eval("StartTime"), false) %>'
                                                                EditFormatString="HH:mm" DisplayFormatString="HH:mm" Width="100%">
                                                                <ClientSideEvents ButtonClick='function(s, e) {
                                                                    if (e.buttonIndex == -2) //increment button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes + minuteStep);
                                                                        s.SetDate(date);
                                                                    }
                                                                    else if (e.buttonIndex == -3) //down button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes - minuteStep); 
                                                                        s.SetDate(date);
                                                                    }}' />
                                                            </dx:ASPxTimeEdit>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxDateEdit ID="fromDateEdit" runat="server" EditFormatString="MM/dd/yyyy" DisplayFormatString="MM/dd/yyyy"
                                                                Date='<%# Eval("StartTime") == null? DateTime.Now : DateTime.Parse(Eval("StartTime").ToString()) %>'
                                                                Width="100%">
                                                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                                    ErrorText="Error">
                                                                    <RequiredField IsRequired="True" ErrorText="From Date is required" />
                                                                </ValidationSettings>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                To
                                            </td>
                                            <td class="content-row">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 80px; padding-right: 10px;">
                                                            <dx:ASPxTimeEdit ID="endTimeEdit" runat="server" DateTime='<%# getDate(Eval("EndTime"), true) %>'
                                                                EditFormatString="HH:mm" DisplayFormatString="HH:mm" Width="100%">
                                                                <ClientSideEvents ButtonClick='function(s, e) {
                                                                    if (e.buttonIndex == -2) //increment button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes + minuteStep);
                                                                        s.SetDate(date);
                                                                    }
                                                                    else if (e.buttonIndex == -3) //down button
                                                                    {
                                                                        var date = s.GetDate();
                                                                        var minutes = date.getMinutes();
                                                                        date.setMinutes(minutes - minuteStep); 
                                                                        s.SetDate(date);
                                                                    }}' />
                                                            </dx:ASPxTimeEdit>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxDateEdit ID="endDateEdit" runat="server" EditFormatString="MM/dd/yyyy" DisplayFormatString="MM/dd/yyyy"
                                                                Date='<%# Eval("EndTime") == null? DateTime.Now : DateTime.Parse(Eval("EndTime").ToString()) %>'
                                                                Width="100%">
                                                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                                    ErrorText="Error">
                                                                    <RequiredField IsRequired="True" ErrorText="To Date is required" />
                                                                </ValidationSettings>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row" style="vertical-align: top">
                                                Note
                                            </td>
                                            <td class="content-row" style="vertical-align: top">
                                                <dx:ASPxMemo runat="server" ID="txtNoteEdit" Text='<%# Bind("Note") %>' CssClass="text-form"
                                                    Width="100%" Height="120px">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>
                                        <% } %>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">
                                                <dx:ASPxHyperLink runat="server" Text="Update" ID="ASPxHyperLink1">
                                                    <ClientSideEvents Click="function(s, e) { grid.UpdateEdit(); }" />
                                                </dx:ASPxHyperLink>
                                                <dx:ASPxHyperLink runat="server" Text="Cancel" ID="ASPxHyperLink2">
                                                    <ClientSideEvents Click="function(s, e) { grid.CancelEdit(); }" />
                                                </dx:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dx:ContentControl>
                        </div>
                    </EditForm>
                </Templates>
            </dx:ASPxGridView>
        </div>
        <data:RosterDataSource ID="RosterDataSource" runat="server" SelectMethod="GetPaged"
            EnableCaching="False" EnablePaging="True" EnableSorting="True" InsertMethod="Insert"
            UpdateMethod="Update">
            <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            </DeepLoadProperties>
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="PageIndex" ControlID="gridRoster" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:RosterDataSource>
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
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:RosterTypeDataSource>
    </div>
</asp:Content>
