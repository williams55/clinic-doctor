<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Users_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
    <dx:ASPxGridView ID="gridUser" ClientInstanceName="grid" runat="server" DataSourceID="UserDatas"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" 
         OnInitNewRow="gridUser_OnInitNewRow" OnRowInserting="gridUser_OnRowInserting">
        <Settings ShowGroupPanel="True" />
        <SettingsEditing Mode="Inline" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0">
                <EditButton Visible="True"></EditButton>
                <NewButton Visible="true"></NewButton> 
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>           
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn VisibleIndex="1" FieldName="Id"> </dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="2" FieldName="Username"> </dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="3" FieldName="DisplayName"> </dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="4" FieldName="Email"> </dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="5" FieldName="Note"> </dx:GridViewDataColumn> 
            <dx:GridViewDataColumn VisibleIndex="6" FieldName="UserGroupId"> </dx:GridViewDataColumn> 
        </Columns>
       <%-- <Templates>
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
                                               User Id
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox  runat="server" ReadOnly="false" ID="txtTitle" Text='<%# Bind("Id") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                                Title
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Title") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Note
                                            </td>
                                            <td class="content-row">
                                               <dx:ASPxTextBox runat="server" ID="txtNote" Text='<%# Bind("Note")%>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                               Services
                                            </td>
                                            <td class="content-row">                                            
                                               <dx:ASPxComboBox  runat="server" ID="ListServicesId"  TextField="Title" ValueField="Id"   Value='<%# Bind("ServicesId") %>' DataSourceID="ServicesDataSource">                                                   
                                               </dx:ASPxComboBox>                                                 
                                               <data:ServicesDataSource runat="server" ID="ServicesDataSource" SelectMethod="GetAll"></data:ServicesDataSource>                                    
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                        </div> 
                    </EditForm>
        </Templates>--%>
        <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
        </ClientSideEvents>
        <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
        </SettingsPager>                
        <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
        
</dx:ASPxGridView>
    <data:UsersDataSource SelectMethod="GetPaged" runat="server" ID="UserDatas"  EnablePaging="True"  EnableSorting="True" > 
    <DeepLoadProperties>
        <Types>
          <data:UserGroupProperty Name="GroupRoleCollection"/>
        </Types>
    </DeepLoadProperties>   
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="PageIndex" ControlID="gridUser" PropertyName="PageIndex"  Type="Int32" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />                   
        </Parameters>    
    </data:UsersDataSource>
</asp:Content>

