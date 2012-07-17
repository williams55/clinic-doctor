<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GroupRole.aspx.cs" Inherits="Admin_Role_GroupRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
<div id="box-tabs" class="box">
        <div class="title">
            <h5>Manager Role</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridGroupRole" ClientInstanceName="grid" runat="server" DataSourceID="UserGruopDatas"
                KeyFieldName="Id" Width="100%" 
                     >
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="3" Caption="#">
                            <EditButton Visible="true" />
                            <NewButton Visible="true" />
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" ReadOnly="true" />
                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="Note" VisibleIndex="2" />                   
                </Columns>
                <Templates>
                    <DetailRow>
                           <dx:ASPxGridView runat="server" ID="Gridrole" 
                                DataSourceID="GroupRoleDatas" KeyFieldName="Id" 
                                OnBeforePerformDataSelect="Gridrolegroup_OnBeforePerformDataSelect"  OnRowInserting="Gridrole_OnRowInserting"
                                >
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="Control" VisibleIndex="4">
                                        <EditButton Visible="true"></EditButton>
                                        <NewButton Visible="true"></NewButton>
                                    </dx:GridViewCommandColumn>                                     
                                                                       
                                    <dx:GridViewDataColumn FieldName="RoleId" VisibleIndex="2"></dx:GridViewDataColumn>  
                                    <dx:GridViewDataColumn FieldName="RoleIdSource.Title" VisibleIndex="3">                                     
                                    </dx:GridViewDataColumn>                                                        
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
                                                                <td class="title-row">
                                                                   Room Id
                                                                </td>
                                                                <td class="content-row">
                                                                   <dx:ASPxComboBox runat="server" ID="cboroleid" DataSourceID="RoleDataSource" Value="<%#Bind('RoleId') %>"  ValueField="Id" TextField="Title">
                                                                    
                                                                   </dx:ASPxComboBox>
                                                                </td>                                                                  
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                            </div> 
                                        </EditForm>
                                      </Templates> 
                                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                                </ClientSideEvents>
                          </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:GroupRoleDataSource ID="GroupRoleDatas" runat="server" SelectMethod="GetPaged" EnableDeepLoad="true">
                  <DeepLoadProperties  >
                    <Types >
                       <data:RoleDetailProperty Name="Role" />
                    </Types>
                  </DeepLoadProperties>
            </data:GroupRoleDataSource>
            <data:RoleDataSource ID="RoleDataSource" runat="server" SelectMethod="GetPaged" EnablePaging="True"
                EnableSorting="True" InsertMethod="Insert" UpdateMethod="Update">
                    <DeepLoadProperties Method="IncludeChildren" Recursive="False"></DeepLoadProperties>
                    <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridGroupRole" PropertyName="PageIndex" Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:RoleDataSource>
            <data:UserGroupDataSource runat="server" ID="UserGruopDatas" SelectMethod="GetPaged">
                <Parameters>
                    <asp:ControlParameter Name="PageIndex" ControlID="gridGroupRole" PropertyName="PageIndex" Type="Int32" />
                </Parameters>
            </data:UserGroupDataSource>
        </div>
    </div>
</asp:Content>

