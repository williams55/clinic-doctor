<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditUser.aspx.cs" Inherits="Admin_Users_EditUser" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxUploadControl" Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <style type="text/css">
        #mainContainer td.buttonCell
        {
            padding-top: 15px;
        }
        #mainContainer td.caption
        {
            padding-right: 5px;
            padding-top: 4px;
            vertical-align: top;
        }
        #mainContainer td.content
        {
            padding-bottom: 20px;
        }
        #mainContainer td.imagePreviewCell
        {
            border: solid 2px gray;
            width: 110px;
            height: 115px; /*if IE*/
            height: expression("110px");
            text-align: center;
        }
        #mainContainer td.note
        {
            text-align: left;
            padding-top: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">

    <script type="text/javascript">
        // <![CDATA[
        function Uploader_OnUploadStart() {
            btnUpload.SetText("Uploading...");
            btnUpload.SetEnabled(false);
        }
        function Uploader_OnUploadComplete(args) {
            var imgSrc = $("[id$=previewImage]").attr("src");
            console.log(args);
            if (args.isValid) {
                var date = new Date();
                imgSrc = "../Images/" + args.callbackData + "?dx=" + date.getTime();
                $("[id$=previewImage]").attr("src", imgSrc);
                grid.UpdateEdit();
            } else {
                alert("a");
            }
            $("[id$=previewImage]").attr("src", imgSrc);
            btnUpload.SetEnabled(true);
            btnUpload.SetText("Upload");
        }
        // ]]> 
    </script>

    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Manage User</h5>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <dx:ASPxGridView ID="gridUser" ClientInstanceName="grid" runat="server" DataSourceID="UserDatas"
            Width="100%" KeyFieldName="Username" OnInitNewRow="gridUser_InitNewRow" OnRowInserting="gridUser_RowInserting"
            OnRowUpdating="gridUser_RowUpdating" OnHtmlRowPrepared="gridUser_HtmlRowPrepared"
            OnRowValidating="gridUser_RowValidating" OnCustomButtonCallback="gridUser_CustomButtonCallback"
            EnableViewState="False">
            <Columns>
                <dx:GridViewDataColumn FieldName="Username" VisibleIndex="2" />
                <dx:GridViewDataColumn FieldName="Title" VisibleIndex="3" />
                <dx:GridViewDataColumn FieldName="Firstname" Visible="false" VisibleIndex="4" />
                <dx:GridViewDataColumn FieldName="DisplayName" VisibleIndex="5" />
                <dx:GridViewDataColumn FieldName="CellPhone" VisibleIndex="4" />
                <dx:GridViewDataColumn FieldName="ServicesId" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataComboBoxColumn Caption="Services" FieldName="ServicesId" VisibleIndex="4">
                    <PropertiesComboBox DropDownStyle="DropDown" ValueField="Id" TextField="Title" DataSourceID="ServicesDatas">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="Email" VisibleIndex="6" />
                <dx:GridViewDataColumn FieldName="UserGroupId" VisibleIndex="8" />
                <dx:GridViewCommandColumn VisibleIndex="9">
                    <EditButton Visible="true">
                    </EditButton>
                    <NewButton Visible="true">
                    </NewButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <Settings ShowFilterRow="true" />
            <Templates>
                <DetailRow>
                    <div style="padding: 3px 3px 2px 3px">
                        <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                            <TabPages>
                                <dx:TabPage Text="UserRole" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <dx:ASPxGridView ID="UserRoleGrid" runat="server" DataSourceID="UserRoleDatas" KeyFieldName="UserId"
                                                Width="100%" OnBeforePerformDataSelect="UserRoleGrid_DataSelect" OnCustomButtonCallback="UserRoleGrid_CustomButtonCallback"
                                                OnRowInserting="UserRoleGrid_RowInserting">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="RoleId" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="RoleIdSource.Title" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="RoleIdSource.Note" VisibleIndex="3" />
                                                    <dx:GridViewCommandColumn VisibleIndex="4">
                                                        <NewButton Visible="true">
                                                        </NewButton>
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnDeleteRole" Text="Delete">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormUpdateButton"
                                                                runat="server">
                                                            </dx:ASPxGridViewTemplateReplacement>
                                                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                                                                runat="server">
                                                            </dx:ASPxGridViewTemplateReplacement>
                                                        </div>
                                                        <div id="devexpress-form">
                                                            <table class="edit-form">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            Role
                                                                        </td>
                                                                        <td colspan="3" style="text-align: left">
                                                                            <dx:ASPxComboBox Width="200" runat="server" ID="cboroleid" DataSourceID="RoleDatas"
                                                                                Value="<%#Bind('RoleId') %>" ValueField="Id" TextField="Title">
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </EditForm>
                                                </Templates>
                                                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                                                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
                                                <Settings ShowFooter="True" />
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Group" Visible="false">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <dx:ASPxGridView ID="UserGroupGrid" runat="server" DataSourceID="UserGroupDatas"
                                                KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="UserGroupGrid_DataSelect">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" />
                                                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="Note" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="Roles" VisibleIndex="2" />
                                                </Columns>
                                                <SettingsDetail ShowDetailRow="true" />
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </DetailRow>
                <EditForm>
                    <div id="devexpress-form">
                        <table class="edit-form">
                            <tbody>
                                <tr>
                                    <td class="title-row">
                                        Username
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtUsername" Text='<%# Bind("Username") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Female
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxCheckBox runat="server" Value='<%#Bind("IsFemale")%>' ID="ckcR">
                                        </dx:ASPxCheckBox>
                                    </td>
                                    <td class="title-row">
                                        Cell phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="txtCellphone" Text='<%# Bind("CellPhone") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td rowspan="4" style="text-align: center;">
                                        <dx:ASPxImage runat="server" Height="100" Width="100" AlternateText="Avatar" 
                                            ImageUrl='<%# Eval("Avatar") == null ? "../../resources/images/avatar.png" : "../Images/"+ Eval("Avatar")%>'
                                            ID="previewImage" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Title
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        First name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtFirstname" Text='<%# Bind("Firstname") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Email
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtEmail" Text='<%# Bind("Email") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        User Group
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxComboBox runat="server" ID="ListServicesId" SelectedIndex="0" TextField="Title"
                                            ValueField="Id" Value='<%# Bind("UserGroupId") %>' DataSourceID="UserGroupDatas">
                                        </dx:ASPxComboBox>
                                        <data:ServicesDataSource runat="server" ID="ServicesDataSource" SelectMethod="GetAll">
                                        </data:ServicesDataSource>
                                    </td>
                                    <td class="title-row">
                                        Last name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtLastname" Text='<%# Bind("Lastname") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row" rowspan="2">
                                        Avatar
                                    </td>
                                    <td class="content-row" rowspan="2">
                                        <dx:ASPxUploadControl ID="uplImage" runat="server" ClientInstanceName="uploader"
                                            Size="35" OnFileUploadComplete="uplImage_FileUploadComplete">
                                            <ClientSideEvents FileUploadComplete="function(s, e) { Uploader_OnUploadComplete(e); }"
                                                FileUploadStart="function(s, e) { Uploader_OnUploadStart(); }"></ClientSideEvents>
                                            <ValidationSettings MaxFileSize="4000000" AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif">
                                            </ValidationSettings>
                                        </dx:ASPxUploadControl>
                                        <dx:ASPxLabel ID="lblAllowebMimeType" runat="server" Text="Allowed image types: jpeg, gif"
                                            Font-Size="8pt">
                                        </dx:ASPxLabel>
                                        <br />
                                        <dx:ASPxLabel ID="lblMaxFileSize" runat="server" Text="Maximum file size: 4Mb" Font-Size="8pt">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td class="title-row">
                                        Display name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtDisplayname" Text='<%# Bind("DisplayName") %>'
                                            CssClass="text-form">
                                            <ValidationSettings ValidationGroup="editForm" Display="Dynamic" ErrorText="*">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Note
                                    </td>
                                    <td class="content-row" colspan="6">
                                        <dx:ASPxMemo runat="server" ID="ASPxTextBox6" Text='<%# Bind("Note")%>' CssClass="text-form">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                        <dx:ASPxHyperLink runat="server" Text="Update" ID="btnUpload" ClientInstanceName="btnUpload">
                            <ClientSideEvents Click="function(s, e) { uploader.UploadFile(); }" />
                        </dx:ASPxHyperLink>
                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                            runat="server">
                        </dx:ASPxGridViewTemplateReplacement>
                    </div>
                </EditForm>
            </Templates>
            <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
            <ClientSideEvents CustomButtonClick="function(s, e) {   if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
            <SettingsDetail ShowDetailRow="true" />
        </dx:ASPxGridView>
        <data:UsersDataSource ID="UserDatas" runat="server" SelectMethod="GetPaged" EnablePaging="true">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" />
                <data:CustomParameter Name="OrderByClause" Value="UserId" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UsersDataSource>
        <data:UserRoleDataSource runat="server" ID="UserRoleDatas" SelectMethod="GetPaged"
            EnableDeepLoad="true">
            <DeepLoadProperties>
                <Types>
                    <data:RoleDetailProperty Name="Role" />
                    <data:RoleDetailProperty Name="Screen" />
                </Types>
            </DeepLoadProperties>
            <Parameters>
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UserRoleDataSource>
        <data:UserGroupDataSource runat="server" ID="UserGroupDatas" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:UserGroupDataSource>
        <data:RoleDataSource ID="RoleDatas" runat="server">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:RoleDataSource>
        <data:ServicesDataSource ID="ServicesDatas" runat="server">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            </Parameters>
        </data:ServicesDataSource>
    </div>
</asp:Content>
