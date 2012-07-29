<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Services_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Service
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>Service</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridServices" ClientInstanceName="grid" runat="server" DataSourceID="ServicesDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridServices_RowInserting"
                OnCustomButtonCallback="gridServices_CustomButtonCallback" 
                onrowupdating="gridServices_RowUpdating" 
                onrowvalidating="gridServices_RowValidating">
                <Columns>                    
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" ReadOnly="true" >
                        <EditFormSettings  Visible="False"/>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="ShortTitle" VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="PriorityIndex" VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="Note" VisibleIndex="4" />
                    <dx:GridViewCommandColumn VisibleIndex="5" Name="#">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>                   
                </Columns>
                <Templates>
                  
                </Templates>
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <ClientSideEvents CustomButtonClick="function(s, e) {   if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:ServicesDataSource ID="ServicesDataSource" runat="server" SelectMethod="GetPaged" EnablePaging="True"
                EnableSorting="True">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridServices" PropertyName="PageIndex"
                        Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:ServicesDataSource>
        </div>
    </div>

</asp:Content>

