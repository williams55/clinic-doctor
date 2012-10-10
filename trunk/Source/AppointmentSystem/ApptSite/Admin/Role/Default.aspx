<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Role_Default" %>

<%@ Import Namespace="Appt.Common.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Role
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <script type="text/javascript">
        function OnClickButtonDel() {
            var currentGrid = grid;
            if ($('[id$=gridRoleDetail]').html()) currentGrid = gridDetail;

            if (currentGrid.GetSelectedRowCount() <= 0) {
                ShowDialog(null, null, "Please select item first.");
                return;
            }
            if (confirm("Are you sure to delete these items")) {
                currentGrid.PerformCallback('Delete');
            }
        }
        function OnClickButtonNew() {
            try {
                if ($('[id$=gridRoleDetail]').html()) {
                    gridDetail.AddNewRow();
                } else {
                    grid.AddNewRow();
                }
            } catch (e) {
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div class="color">
        <a class="add" title="New" onclick="OnClickButtonNew()" id="btnAdd" runat="server">
        </a><a class="delete" title="Delete selected items" onclick="OnClickButtonDel()"
            id="btnGeneralDelete" runat="server"></a>
    </div>
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Manager Role</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRole" ClientInstanceName="grid" runat="server" DataSourceID="RoleDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRole_RowInserting"
                OnCustomButtonCallback="gridRole_CustomButtonCallback" OnRowUpdating="gridRole_RowUpdating"
                OnCustomCallback="gridRole_CustomCallback">
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
                    <dx:GridViewCommandColumn Name="btnCommand" ButtonType="Image" Width="60" Caption="Operation">
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
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn Caption="#" ShowSelectCheckbox="True" Width="15">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                        <CellStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn>
                </Columns>
                <Templates>
                    <DetailRow>
                        <dx:ASPxGridView runat="server" ClientInstanceName="gridDetail" ID="gridRoleDetail"
                            DataSourceID="RoleDetailDataSource" KeyFieldName="Id" Width="100%"
                            OnRowInserting="gridRoleDetail_RowInserting" OnRowUpdating="gridRoleDetail_RowUpdating"
                            OnCustomButtonCallback="gridRoleDetail_OnCustomButtonCallback" OnInit="gridRoleDetail_Init"
                            OnCustomCallback="gridRoleDetail_CustomCallback">
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
                                <dx:GridViewCommandColumn Name="btnCommand" ButtonType="Image" Width="60" Caption="Operation">
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
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn Caption="#" ShowSelectCheckbox="True" Width="15">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                            ClientSideEvents-CheckedChanged="function(s, e) { gridDetail.SelectAllRowsOnPage(s.GetChecked()); }" />
                                    </HeaderTemplate>
                                    <CellStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <ClientSideEvents EndCallback="function(s, e) { AlertMessage(); RefreshGrid(); }"
                                BeginCallback="function(s, e) {command = e.command; gridObject = s;}" 
                                CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                            <SettingsEditing Mode="EditForm" />
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
                <ClientSideEvents EndCallback="function(s, e) { AlertMessage(); RefreshGrid(); }"
                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}" CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
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
