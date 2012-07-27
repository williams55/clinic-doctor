<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Screen_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
<script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
 <div class="box">
        <div class="title">
            <h5>Screen</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridScreen" ClientInstanceName="grid"  runat="server" DataSourceID="ScreenDataSource"
                KeyFieldName="ScreenCode" Width="100%" EnableRowsCache="False" OnRowInserting="gridScreen_RowInserting"
                OnCustomButtonCallback="gridScreen_CustomButtonCallback" 
                onrowupdating="gridScreen_RowUpdating">
                <Columns>                    
                    <dx:GridViewDataColumn FieldName="ScreenCode"  VisibleIndex="1">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ScreenName" VisibleIndex="2" />  
                    <dx:GridViewDataColumn FieldName="PriorityIndex" VisibleIndex="3" />      
                    <dx:GridViewCommandColumn VisibleIndex="4">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>           
                </Columns>
              
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();AlertMessage();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <ClientSideEvents CustomButtonClick="function(s, e) {   if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
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

