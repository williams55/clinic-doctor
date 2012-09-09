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
        function grid_SelectionChanged(s, e) {
            s.GetSelectedFieldValues("Id", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            selList.BeginUpdate();
            try {
                selList.ClearItems();
                for(var i=0;i<values.length;i++) {
                    selList.AddItem(values[i]);
                }
            } finally {
                selList.EndUpdate();
            }
            document.getElementById("selCount").innerHTML=grid.GetSelectedRowCount();
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Roster</h5>
        </div>
        <div id="box-order">
            <div style="float: left; width: 20%">
                <div>
                    Selected values:
                </div>
                <dx:ASPxListBox ID="selList" ClientInstanceName="selList" runat="server" Height="250px"
                    Width="100%" />
                <div>
                    Selected count: <span id="selCount" style="font-weight: bold">0</span>
                </div>
            </div>
            <div style="float: right; width: 78%">
                <dx:ASPxGridView ID="gridRoster" ClientInstanceName="grid" runat="server" DataSourceID="RosterDataSource"
                    KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRoster_RowInserting"
                    OnCustomButtonCallback="gridRoster_CustomButtonCallback" OnRowUpdating="gridRoster_RowUpdating"
                    OnInitNewRow="gridRoster_InitNewRow" AutoGenerateColumns="False">
                    <Columns>
                        <dx:GridViewCommandColumn Width="40" ShowSelectCheckbox="True">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderTemplate>
                                <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn Caption="No." Width="50">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Id" Width="100" ReadOnly="True" />
                        <dx:GridViewDataComboBoxColumn FieldName="Username" Caption="Doctor">
                            <PropertiesComboBox TextField="DisplayName" ValueField="Username" DataSourceID="UsersDataSource">
                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                    ErrorText="Error">
                                    <RequiredField IsRequired="True" ErrorText="Doctor is required" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="RosterTypeId" Caption="Roster Type" Width="120">
                            <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="RosterTypeDataSource">
                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                    ErrorText="Error">
                                    <RequiredField IsRequired="True" ErrorText="Roster Type is required" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataDateColumn FieldName="StartTime" Width="120" Caption="From">
                            <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                                EnableAnimation="False">
                            </PropertiesDateEdit>
                            <Settings AutoFilterCondition="GreaterOrEqual" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="To" FieldName="EndTime" VisibleIndex="6" Width="120px">
                            <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                                EnableAnimation="False">
                            </PropertiesDateEdit>
                            <Settings AutoFilterCondition="LessOrEqual" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn FieldName="Note" Width="130">
                            <Settings AllowAutoFilter="False"></Settings>
                        </dx:GridViewDataColumn>
                        <dx:GridViewCommandColumn Caption="Operation" Width="100">
                            <EditButton Visible="True">
                            </EditButton>
                            <NewButton Visible="true">
                            </NewButton>
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                        BeginCallback="function(s, e) {command = e.command; gridObject = s;}" CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}"
                        SelectionChanged="grid_SelectionChanged" />
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
                                            <tr>
                                                <td class="title-row" style="width: 100px;">
                                                    Id
                                                </td>
                                                <td class="content-row" style="width: 220px;">
                                                    <dx:ASPxTextBox runat="server" ID="txtId" Text='<%# Eval("Id") %>' CssClass="text-form"
                                                        MaxLength="100" Width="100%" ReadOnly="True">
                                                    </dx:ASPxTextBox>
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
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="title-row" style="width: 100px;">
                                                    Doctor
                                                </td>
                                                <td class="content-row" style="width: 220px;">
                                                    <dx:ASPxTextBox runat="server" ID="txtUsername" Text='<%# Eval("Username") %>' CssClass="text-form"
                                                        MaxLength="100" Width="100%">
                                                    </dx:ASPxTextBox>
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
                                                <% if (!gridRoster.IsNewRowEditing)
                                                   {%>
                                                <td class="title-row" colspan="2">
                                                    Change similar rosters
                                                    <dx:ASPxCheckBox runat="server" ID="chkSimilar">
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <% }
                                                   else
                                                   { %>
                                                <td class="title-row">
                                                    Is Repeat
                                                </td>
                                                <td class="content-row">
                                                    <dx:ASPxCheckBox runat="server" ID="chkIsRepeat">
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <% } %>
                                            </tr>
                                            <tr>
                                                <td class="title-row">
                                                    Note
                                                </td>
                                                <td class="content-row">
                                                    <dx:ASPxMemo runat="server" ID="txtNote" Text='<%# Bind("Note") %>' CssClass="text-form"
                                                        MaxLength="500" Width="100%" Height="50px">
                                                    </dx:ASPxMemo>
                                                </td>
                                                <% if (gridRoster.IsNewRowEditing)
                                                   { %>
                                                <td class="title-row">
                                                    Weekday
                                                </td>
                                                <td class="content-row">
                                                    <asp:CheckBoxList ID="chkWeekday" runat="server" RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </td>
                                                <% }
                                                   else
                                                   { %>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <% } %>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: right;">
                                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
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
                EnablePaging="True" EnableSorting="True" InsertMethod="Insert" UpdateMethod="Update">
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
    </div>
</asp:Content>
