<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Screen_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
<script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
 <div id="box-tabs" class="box">
        <div class="title">
            <h5>Room</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridScreen" ClientInstanceName="grid" runat="server" DataSourceID="ScreenDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridScreen_RowInserting"
                OnCustomButtonCallback="gridScreen_CustomButtonCallback" 
                onrowupdating="gridScreen_RowUpdating">
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="3">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" ReadOnly="true" />
                    <dx:GridViewDataColumn FieldName="ScreenCode" VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="ScreenName" VisibleIndex="2" />                   
                </Columns>
                <Templates>
                    <EditForm>
                         <div id="devexpress-form">
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table class="edit-form">
                                    <tbody>
                                        <tr>
                                            
                                            <td class="title-row">
                                                Title
                                            </td>
                                            <td class="content-row" rowspan="2">
                                                 <dx:ASPxTextBox runat="server" ID="txtScreenCode" Text='<%# Bind("ScreenCode") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                                Note
                                            </td>
                                            <td class="content-row" rowspan="2">
                                               <dx:ASPxTextBox runat="server" ID="txtNote" Text='<%# Bind("ScreenName")%>' CssClass="text-form">
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
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:ScreenDataSource ID="ScreenDataSource" runat="server" SelectMethod="GetPaged" EnablePaging="True"
                EnableSorting="True">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridScreen" PropertyName="PageIndex"
                        Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:ScreenDataSource>
        </div>
    </div>

</asp:Content>

