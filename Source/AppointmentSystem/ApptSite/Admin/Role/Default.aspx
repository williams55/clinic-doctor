<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Role_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
<script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>Manager Role</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRole" ClientInstanceName="grid" runat="server" DataSourceID="RoleDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRole_RowInserting"
                OnCustomButtonCallback="gridRole_CustomButtonCallback" 
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
                           <dx:ASPxGridView runat="server" ID="Gridroledetail" 
                                DataSourceID="RoledetailDataS" KeyFieldName="Id"                               
                                OnBeforePerformDataSelect="Gridroledetail_OnBeforePerformDataSelect" 
                                OnRowInserting="Gridroledetail_RowInserting" 
                                OnRowUpdating="Gridroledetail_RowUpdating"
                                OnRowValidating="Gridroledetail_OnRowValidating"
                                OnCustomButtonCallback="Gridroledetail_OnCustomButtonCallback"
                                >
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="#" VisibleIndex="4" Name="Controlcommand">
                                        <EditButton Visible="true"></EditButton>
                                        <NewButton Visible="true"></NewButton>
                                        <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                                            </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>                                     
                                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0"  ReadOnly="true">                                     
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataComboBoxColumn FieldName="ScreenCode" VisibleIndex="1">
                                        <PropertiesComboBox TextField="ScreenName" ValueField="ScreenCode" DataSourceID="ScreenDatas"></PropertiesComboBox>                                       
                                    </dx:GridViewDataComboBoxColumn>
                                   <dx:GridViewDataColumn FieldName="Crud" VisibleIndex="2"  ReadOnly="true">                                     
                                    </dx:GridViewDataColumn>
                              </Columns>
                                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                                </ClientSideEvents>
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
                                                                        Title
                                                                    </td>
                                                                    <td class="content-row">
                                                                         <dx:ASPxComboBox runat="server" ID="chkscreen" Value='<%#Bind("ScreenCode")%>' TextField="Screenname" ValueField="ScreenCode" DataSourceID="ScreenDatas" >
                                                                         
                                                                         </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td class="title-row">
                                                                       Crud
                                                                    </td>
                                                                    <td class="content-row" colspan="2">                                                           
                                                                        <table>
                                                                            <tr>
                                                                                <td><dx:ASPxCheckBox runat="server" ID="ckcR" Text="READ"   Checked='<%# Getcheckbox( (string) (Eval("Crud")),"R")%>'></dx:ASPxCheckBox></td>
                                                                                <td><dx:ASPxCheckBox runat="server" ID="ckcC" Text="CREATE" Checked='<%# Getcheckbox( (string) (Eval("Crud")),"C")%>'></dx:ASPxCheckBox></td>
                                                                                <td><dx:ASPxCheckBox runat="server" ID="ckcU" Text="UPDATE" Checked='<%# Getcheckbox( (string) (Eval("Crud")),"U")%>'></dx:ASPxCheckBox></td>
                                                                                <td><dx:ASPxCheckBox runat="server" ID="CkcD" Text="DELETE" Checked='<%# Getcheckbox( (string) (Eval("Crud")),"D")%>'></dx:ASPxCheckBox></td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="title-row">
                                                                        Note
                                                                    </td>
                                                                    <td class="content-row"
                                                                      <dx:ASPxCheckBoxList runat="server" ID="chkRight">
                                                                       
                                                                      </dx:ASPxCheckBoxList>
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                </div> 
                                            </EditForm>
                                </Templates>
                          </dx:ASPxGridView>
                    </DetailRow>
                    <EditForm>
                         <div id="devexpress-form">
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table class="edit-form">
                                    <tbody>
                                        <tr>
                                            <td "title-row">
                                                Title
                                            </td>
                                            <td class="content-row" rowspan="2">
                                                 <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Title") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                                Note
                                            </td>
                                            <td class="content-row" rowspan="2">
                                               <dx:ASPxTextBox runat="server" ID="txtNote" Text='<%# Bind("Note")%>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                           
                                        </tr>
                                       
                                    </tbody>
                                </table>
                            </dx:ContentControl>
                        </div>
                        <div style="text-align: right; padding: 2px 50px 2px 2px">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </EditForm>
                </Templates>
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:RoleDataSource ID="RoleDataSource" runat="server" SelectMethod="GetPaged" EnablePaging="True"
                EnableSorting="True" InsertMethod="Insert" UpdateMethod="Update">
                    <DeepLoadProperties Method="IncludeChildren" Recursive="False"></DeepLoadProperties>
                    <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridRole" PropertyName="PageIndex"
                        Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:RoleDataSource>
            <data:RoleDetailDataSource runat="server" ID="RoledetailDataS" SelectMethod="GetPaged">
                <Parameters>
                    <data:CustomParameter  Name="OrderByClause" Value="" ConvertEmptyStringToNull="false"/>
                    <asp:ControlParameter Name="PageIndex" ConvertEmptyStringToNull="false" ControlId="gridRole" PropertyName="PageIndex"  />
                </Parameters>
            </data:RoleDetailDataSource>
             <data:ScreenDataSource runat="server" ID="ScreenDatas">
                 <Parameters>
                    <data:CustomParameter Name="whereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                </Parameters>
            </data:ScreenDataSource>
        </div>
    </div>
</asp:Content>

