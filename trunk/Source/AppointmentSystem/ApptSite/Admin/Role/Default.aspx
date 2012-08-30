<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Role_Default" %>

<%@ Import Namespace="Appt.Common.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Role
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Manager Role</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRole" ClientInstanceName="grid" runat="server" DataSourceID="RoleDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRole_RowInserting"
                OnCustomButtonCallback="gridRole_CustomButtonCallback" 
                OnRowUpdating="gridRole_RowUpdating">
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" Visible="False" />
                    <dx:GridViewDataColumn Caption="No." Width="50">
                        <DataItemTemplate>
                            <%# Container.ItemIndex + 1%>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Title">
                        <EditItemTemplate>
                            <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' CssClass="text-form"
                                MaxLength="100" Width="100%">
                                <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                    ErrorText="Error">
                                    <RequiredField IsRequired="True" ErrorText="Title is required" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Note">
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
                <Templates>
                    <DetailRow>
                        <dx:ASPxGridView runat="server" ClientInstanceName="gridDetail" ID="gridRoleDetail" DataSourceID="RoleDetailDataSource"
                            KeyFieldName="Id" oninit="gridRoleDetail_Init"
                            OnRowInserting="gridRoleDetail_RowInserting" OnRowUpdating="gridRoleDetail_RowUpdating"
                            OnRowValidating="gridRoleDetail_OnRowValidating" OnCustomButtonCallback="gridRoleDetail_OnCustomButtonCallback"
                            Width="100%">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="Id" Visible="False">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="No." Width="50">
                                    <DataItemTemplate>
                                        <%# Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="ScreenCode" Caption="Screen">
                                    <PropertiesComboBox TextField="ScreenName" ValueField="ScreenCode" DataSourceID="ScreenDataSource">
                                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                            ErrorText="Error">
                                            <RequiredField IsRequired="True" ErrorText="Screen is required" />
                                        </ValidationSettings>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataColumn FieldName="Crud" ReadOnly="true" Caption="Right">
                                    <EditItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxCheckBox runat="server" ID="ckcC" Text='<%#OperationConstant.Create.Value %>'
                                                        Checked='<%# GetCheckbox(Eval("Crud"), OperationConstant.Create.Key)%>'>
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxCheckBox runat="server" ID="ckcR" Text='<%#OperationConstant.Read.Value %>'
                                                        Checked='<%# GetCheckbox(Eval("Crud"), OperationConstant.Read.Key)%>'>
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxCheckBox runat="server" ID="ckcU" Text='<%#OperationConstant.Update.Value %>'
                                                        Checked='<%# GetCheckbox(Eval("Crud"), OperationConstant.Update.Key)%>'>
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxCheckBox runat="server" ID="CkcD" Text='<%#OperationConstant.Delete.Value %>'
                                                        Checked='<%# GetCheckbox(Eval("Crud"), OperationConstant.Delete.Key)%>'>
                                                    </dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn Caption="Operation" Width="100">
                                    <EditButton Visible="true">
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
                                BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
                            <ClientSideEvents CustomButtonClick="function(s, e) {   if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                            <SettingsEditing Mode="EditForm" />
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
            </dx:ASPxGridView>
            <data:RoleDataSource ID="RoleDataSource" runat="server" SelectMethod="GetPaged" EnablePaging="True"
                EnableSorting="True" InsertMethod="Insert" UpdateMethod="Update">
                <DeepLoadProperties Method="IncludeChildren" Recursive="False">
                </DeepLoadProperties>
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridRole" PropertyName="PageIndex"
                        Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:RoleDataSource>
            <data:RoleDetailDataSource runat="server" ID="RoleDetailDataSource" SelectMethod="GetPaged">
                <Parameters>
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ConvertEmptyStringToNull="false" ControlID="gridRole"
                        PropertyName="PageIndex" />
                </Parameters>
            </data:RoleDetailDataSource>
            <data:ScreenDataSource runat="server" ID="ScreenDataSource">
                <Parameters>
                    <data:CustomParameter Name="whereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                </Parameters>
            </data:ScreenDataSource>
        </div>
    </div>
</asp:Content>
