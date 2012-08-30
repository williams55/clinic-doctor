<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Grid.aspx.cs" Inherits="Admin_Roster_Grid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Roster
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Roster</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRoster" ClientInstanceName="grid" runat="server" DataSourceID="RosterDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRole_RowInserting"
                OnCustomButtonCallback="gridRole_CustomButtonCallback" 
                OnRowUpdating="gridRole_RowUpdating" AutoGenerateColumns="False">
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
                        <PropertiesDateEdit DisplayFormatString="hh:mm MM/dd/yyyy" 
                            EditFormatString="MM/dd/yyyy" DisplayFormatInEditMode="True">
                        </PropertiesDateEdit>
                        <Settings AutoFilterCondition="GreaterOrEqual" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="To" FieldName="EndTime" VisibleIndex="6" 
                        Width="120px">
                        <PropertiesDateEdit DisplayFormatString="hh:mm MM/dd/yyyy" 
                            EditFormat="Custom" EditFormatString="MM/dd/yyyy" EnableAnimation="False">
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
                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}" CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
            </dx:ASPxGridView>
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
