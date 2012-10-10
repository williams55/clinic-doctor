<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserGroup.aspx.cs" Inherits="Admin_Users_UserGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Group
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <script type="text/javascript">
        function OnClickButtonDel() {
            var currentGrid = grid;
            if ($('[id$=gridGroupRole]').html()) currentGrid = gridDetail;

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
                if ($('[id$=gridGroupRole]').html()) {
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
                User Group</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView runat="server" ClientInstanceName="grid" ID="gridGroup" Width="100%"
                DataSourceID="UserGroupDataSource" KeyFieldName="Id" EnableRowsCache="False"
                OnCustomButtonCallback="gridGroup_CustomButtonCallback" OnRowInserting="gridGroup_RowInserting"
                OnRowUpdating="gridGroup_RowUpdating" OnCustomCallback="gridGroup_OnCustomCallback">
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
                    <dx:GridViewDataColumn FieldName="Title">
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
                        <dx:ASPxGridView runat="server" ClientInstanceName="gridDetail" ID="gridGroupRole"
                            DataSourceID="GroupRoleDataSource" KeyFieldName="Id" Width="100%" OnRowInserting="gridGroupRole_OnRowInserting"
                            OnCustomButtonCallback="gridGroupRole_OnCustomButtonCallback" OnInit="gridGroupRole_OnInit"
                            OnCustomCallback="gridGroupRole_OnCustomCallback" OnInitNewRow="gridGroupRole_OnInitNewRow">
                            <Columns>
                                <dx:GridViewDataComboBoxColumn FieldName="RoleId" Caption="Role" Visible="False">
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataColumn Caption="No." Width="50">
                                    <DataItemTemplate>
                                        <%# Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="RoleIdSource.Title" Caption="Role">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="RoleIdSource.Note" Caption="Note">
                                    <EditFormSettings Visible="False" />
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
                            <Templates>
                                <EditForm>
                                    <div id="devexpress-form">
                                        <table class="edit-form">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        Role
                                                    </td>
                                                    <td colspan="3" style="text-align: left;">
                                                        <asp:HiddenField runat="server" Value='<%# Eval("RoleId") %>' ID="hdfRole" />
                                                        <dx:ASPxComboBox Width="200" runat="server" ID="cboGroupRole" TextField="Title" ValueType="System.Int32"
                                                            ValueField="Id" Value='<%# Bind("RoleId") %>'>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement3" ReplacementType="EditFormUpdateButton"
                                            runat="server">
                                        </dx:ASPxGridViewTemplateReplacement>
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement4" ReplacementType="EditFormCancelButton"
                                            runat="server">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </div>
                                </EditForm>
                            </Templates>
                            <Styles>
                                <AlternatingRow Enabled="true" />
                                <Table Wrap="True">
                                </Table>
                            </Styles>
                            <ClientSideEvents EndCallback="function(s, e) { AlertMessage(); RefreshGrid(); }"
                                BeginCallback="function(s, e) {command = e.command; gridObject = s;}" CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                            <SettingsEditing Mode="EditFormAndDisplayRow" />
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                <Styles>
                    <AlternatingRow Enabled="true" />
                    <Table Wrap="True">
                    </Table>
                </Styles>
                <ClientSideEvents EndCallback="function(s, e) { AlertMessage(); RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}"
                    CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
            </dx:ASPxGridView>
            <data:UserGroupDataSource runat="server" ID="UserGroupDataSource" SelectMethod="GetPaged"
                EnablePaging="True" EnableSorting="True" InsertMethod="Insert" UpdateMethod="Update">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ConvertEmptyStringToNull="false" ControlID="gridGroup"
                        PropertyName="PageIndex" />
                </Parameters>
            </data:UserGroupDataSource>
            <data:GroupRoleDataSource runat="server" ID="GroupRoleDataSource" SelectMethod="GetPaged"
                EnableDeepLoad="true" EnablePaging="True" EnableSorting="True" InsertMethod="Insert"
                UpdateMethod="Update">
                <DeepLoadProperties Method="IncludeChildren" Recursive="False">
                    <Types>
                        <data:RoleDetailProperty Name="Role" />
                    </Types>
                </DeepLoadProperties>
                <Parameters>
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:GroupRoleDataSource>
            <data:RoleDataSource ID="RoleDataSource" runat="server" SelectMethod="GetPaged">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:RoleDataSource>
        </div>
    </div>
</asp:Content>
