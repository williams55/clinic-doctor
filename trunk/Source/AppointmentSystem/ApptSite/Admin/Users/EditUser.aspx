<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditUser.aspx.cs" Inherits="Admin_Users_EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/function.js") %>"></script>

    <script type="text/javascript">
        function OnClickButtonDel() {
            var currentGrid = grid;
            if ($('[id$=gridUserRole]').html()) currentGrid = gridDetail;

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
                if ($('[id$=gridUserRole]').html()) {
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
                Manage User</h5>
        </div>
        <dx:ASPxGridView ID="gridUser" ClientInstanceName="grid" runat="server" DataSourceID="UserDatas"
            Width="100%" KeyFieldName="Username" OnRowInserting="gridUser_RowInserting" OnRowUpdating="gridUser_RowUpdating"
            OnCustomButtonCallback="gridUser_CustomButtonCallback" OnCustomCallback="gridUser_OnCustomCallback"
            EnableViewState="False">
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
                <dx:GridViewDataColumn FieldName="Username" />
                <dx:GridViewDataColumn FieldName="Title" Visible="False" />
                <dx:GridViewDataColumn FieldName="Firstname" />
                <dx:GridViewDataColumn FieldName="Lastname" />
                <dx:GridViewDataColumn FieldName="DisplayName" />
                <dx:GridViewDataColumn FieldName="CellPhone" />
                <dx:GridViewDataComboBoxColumn Caption="Service" FieldName="ServicesId">
                    <PropertiesComboBox DropDownStyle="DropDown" ValueField="Id" TextField="Title" DataSourceID="ServicesDatas">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="Email" />
                <dx:GridViewDataComboBoxColumn Caption="Group" FieldName="UserGroupId">
                    <PropertiesComboBox DropDownStyle="DropDown" ValueField="Id" TextField="Title" DataSourceID="UserGroupDatas">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
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
                    <dx:ASPxGridView runat="server" ClientInstanceName="gridDetail" ID="gridUserRole"
                        DataSourceID="UserRoleDatas" KeyFieldName="Username" Width="100%" OnRowInserting="gridUserRole_RowInserting"
                        OnCustomButtonCallback="gridUserRole_CustomButtonCallback" OnInit="gridUserRole_OnInit"
                        OnCustomCallback="gridUserRole_OnCustomCallback" OnInitNewRow="gridUserRole_OnInitNewRow">
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
                                                    <dx:ASPxComboBox Width="200" runat="server" ID="cboUserRole" TextField="Title" ValueType="System.Int32"
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
                <EditForm>
                    <div id="devexpress-form">
                        <table class="edit-form users">
                            <tbody>
                                <tr>
                                    <td class="title-row">
                                        Username
                                    </td>
                                    <td class="content-row">
                                        <%if (!gridUser.IsNewRowEditing)
                                          { %>
                                        <%# Eval("Username") %>
                                        <%}
                                          else
                                          { %>
                                        <dx:ASPxTextBox runat="server" ID="txtUsernamme" MaxLength="50" TabIndex="1" Text=""
                                            CssClass="text-form" Width="100%" Value='<%# Bind("Username") %>'>
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Username is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                        <%} %>
                                    </td>
                                    <td class="title-row">
                                        First Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtFirstname" MaxLength="50" TabIndex="4" Text='<%# Bind("Firstname") %>'
                                            CssClass="text-form" Width="100%">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="First Name is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Sex
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxRadioButton runat="server" Checked='<%# !(Eval("IsFemale") != null && Boolean.Parse(Eval("IsFemale").ToString())) %>'
                                            Text="Male" ID="radMale" GroupName="radSex" Layout="Flow" TabIndex="7" />
                                        <dx:ASPxRadioButton runat="server" Checked='<%# Eval("IsFemale") != null && Boolean.Parse(Eval("IsFemale").ToString()) %>'
                                            Text="Female" ID="radFemale" GroupName="radSex" Layout="Flow" TabIndex="8" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Service
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxComboBox runat="server" ID="ASPxComboBox2" TabIndex="2" TextField="Title"
                                            ValueField="Id" Value='<%# Bind("ServicesId") %>' DataSourceID="ServicesDatas"
                                            ValueType="System.Int32" Width="100%">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Service is required" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="title-row">
                                        Last Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtLastname" MaxLength="50" TabIndex="5" Text='<%# Bind("Lastname") %>'
                                            CssClass="text-form" Width="100%">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Last Name is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Email
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtEmail" MaxLength="50" TabIndex="9" Text='<%# Bind("Email") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        User Group
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxComboBox runat="server" ID="ASPxComboBox1" TabIndex="3" TextField="Title"
                                            ValueField="Id" Value='<%# Bind("UserGroupId") %>' DataSourceID="UserGroupDatas"
                                            ValueType="System.String" Width="100%">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Group is required" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="title-row">
                                        Display Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtDisplayname" MaxLength="50" TabIndex="6" Text='<%# Bind("DisplayName") %>'
                                            CssClass="text-form" Width="100%">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Display Name is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Cell Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" MaxLength="20" TabIndex="10" ID="txtCellphone"
                                            Text='<%# Bind("CellPhone") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Note
                                    </td>
                                    <td class="content-row" colspan="6">
                                        <dx:ASPxMemo runat="server" ID="ASPxTextBox6" MaxLength="500" TabIndex="11" Text='<%# Bind("Note")%>'
                                            CssClass="text-form">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                        <dx:ASPxHyperLink runat="server" Text="Update" ID="ASPxHyperLink1">
                            <ClientSideEvents Click="function(s, e) { grid.UpdateEdit(); }" />
                        </dx:ASPxHyperLink>
                        <dx:ASPxHyperLink runat="server" Text="Cancel" ID="ASPxHyperLink2">
                            <ClientSideEvents Click="function(s, e) { grid.CancelEdit(); }" />
                        </dx:ASPxHyperLink>
                    </div>
                </EditForm>
            </Templates>
            <Styles>
                <AlternatingRow Enabled="true" />
                <Table Wrap="True">
                </Table>
            </Styles>
            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
            <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                BeginCallback="function(s, e) {command = e.command; gridObject = s;}" CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}">
            </ClientSideEvents>
            <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
            </SettingsPager>
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
        </dx:ASPxGridView>
        <data:UsersDataSource ID="UserDatas" runat="server" SelectMethod="GetPaged" EnablePaging="True"
            EnableSorting="True" InsertMethod="Insert" UpdateMethod="Update">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" />
                <data:CustomParameter Name="OrderByClause" Value="Username" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UsersDataSource>
        <data:UserRoleDataSource runat="server" ID="UserRoleDatas" SelectMethod="GetPaged"
            EnableDeepLoad="true" EnablePaging="True" EnableSorting="True" InsertMethod="Insert"
            UpdateMethod="Update">
            <DeepLoadProperties Method="IncludeChildren" Recursive="False">
                <Types>
                    <data:RoleDetailProperty Name="Role" />
                    <data:RoleDetailProperty Name="Screen" />
                </Types>
            </DeepLoadProperties>
            <Parameters>
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UserRoleDataSource>
        <data:UserGroupDataSource runat="server" ID="UserGroupDatas" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UserGroupDataSource>
        <data:RoleDataSource ID="RoleDatas" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:RoleDataSource>
        <data:ServicesDataSource ID="ServicesDatas" runat="server" SelectMethod="GetPaged"
            EnablePaging="True" EnableSorting="True">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:ServicesDataSource>
    </div>
</asp:Content>
