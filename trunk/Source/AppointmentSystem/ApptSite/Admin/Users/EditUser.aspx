<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="Admin_Users_EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">

  <dx:ASPxGridView ID="gridUser" ClientInstanceName="grid" runat="server" DataSourceID="UserDatas"
        Width="100%" KeyFieldName="Id" >
        <Columns>
            <dx:GridViewDataColumn FieldName="Id" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="Username" VisibleIndex="2" />
            <dx:GridViewDataColumn FieldName="Title" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="Firstname" Visible="false" VisibleIndex="4" />
            <dx:GridViewDataColumn FieldName="DisplayName" VisibleIndex="5" />
            <dx:GridViewDataColumn FieldName="CellPhone" VisibleIndex="4" />
            <dx:GridViewDataColumn FieldName="Email" VisibleIndex="6" />
            <dx:GridViewDataColumn FieldName="UserGroupId" VisibleIndex="8" />
            <dx:GridViewCommandColumn VisibleIndex="9">
                <EditButton Visible="true"></EditButton>
                <NewButton Visible="true"></NewButton>
                <CustomButtons>                   
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Templates>
           <DetailRow>
                <div style="padding: 3px 3px 2px 3px">
                    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                        <TabPages>
                            <dx:TabPage Text="UserRole" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server">
                                        <dx:ASPxGridView ID="UserRoleGrid" runat="server" DataSourceID="UserRoleDatas"
                                            KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="UserRoleGrid_DataSelect">
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" />
                                                <dx:GridViewDataColumn FieldName="UserId" VisibleIndex="1" />
                                                <dx:GridViewDataColumn FieldName="RoleId" VisibleIndex="2" />
                                            </Columns>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <Settings ShowFooter="True" />
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Group" Visible="true">
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
                                            <Settings ShowFooter="True" />
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                    </ContentCollection>
                            </dx:TabPage>
                        
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>
            </DetailRow> 
            <EditForm>
                <table width="100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <% if(!gridUser.IsNewRowEditing) { %>
                        <td rowspan="4">
                            <div style="border: solid 1px #C2D4DA; padding: 2px;">
                                <dx:ASPxImage Height="100" Width="100" ID="ImageUser" runat="server" ImageUrl='http://localhost:18015/ApptSite/Admin/Images/<%#Eval("Avatar")%>'>
                                    
                                </dx:ASPxImage>
                            </div>
                        </td>
                        <% } %>
                        <td style="white-space: nowrap">
                            First Name
                        </td>
                        <td style="width: 50%">
                            <dx:ASPxTextBox runat="server" ID="edFirst" Text='<%# Bind("Firstname") %>' Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="white-space: nowrap">
                            Last Name
                        </td>
                        <td style="width: 50%">
                            <dx:ASPxTextBox runat="server" ID="edLast" Text='<%# Bind("Lastname") %>' Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                     <td>
                           User Name
                        </td>
                        <td style="width: 50%">
                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox3" Text='<%# Bind("Username") %>' Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            Cell Phone
                        </td>
                        <td style="width: 50%">
                            <dx:ASPxTextBox runat="server" ID="edTitle" Text='<%# Bind("CellPhone") %>' Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap">
                            Email
                        </td>
                        <td style="width: 50%">
                            <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Email") %>' Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="white-space: nowrap">
                            Group ID
                        </td>
                        <td style="width: 50%">
                             <dx:ASPxTextBox runat="server" ID="ASPxTextBox2" Text='<%# Bind("UserGroupId") %>' Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td> Note</td>
                        <td colspan="4">
                            <dx:ASPxMemo runat="server" ID="edNotes" Text='<%# Bind("Note")%>' Width="100%"
                                Height="100px">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                </table>
                <div style="text-align: right; padding: 2px">
                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                </div> 
                
            </EditForm>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
        <Settings ShowGroupPanel="True" />
    </dx:ASPxGridView>
    <data:UsersDataSource ID="UserDatas" runat="server" SelectMethod="GetPaged" EnablePaging="true">
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="PageIndex" ControlID="gridUser" PropertyName="PageIndex"  Type="Int32" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />  
        </Parameters>
    </data:UsersDataSource>
    <data:UserRoleDataSource runat="server" ID="UserRoleDatas" SelectMethod="GetPaged" >
       <Parameters>
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
       </Parameters>
    </data:UserRoleDataSource>
    <data:UserGroupDataSource runat="server" ID="UserGroupDatas" SelectMethod="GetPaged">
         <Parameters>
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
       </Parameters>
    </data:UserGroupDataSource>
</asp:Content>

