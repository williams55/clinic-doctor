<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Room_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
<script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
    <dx:ASPxGridView ID="gridRoom" ClientInstanceName="grid" runat="server" DataSourceID="RoomDatas"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRoom_RowInserting"
                OnCustomButtonCallback="gridRoom_CustomButtonCallback"
                onrowupdating="gridRoom_RowUpdating">
        <Settings ShowGroupPanel="True" />
        <SettingsEditing Mode="Inline" />
        <Columns>            
            <dx:GridViewDataColumn VisibleIndex="0" FieldName="Id"><EditFormSettings Visible="False" /></dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="3" FieldName="Note"> </dx:GridViewDataColumn>
            <dx:GridViewDataColumn VisibleIndex="1" FieldName="Title"> </dx:GridViewDataColumn>
            <dx:GridViewDataComboBoxColumn FieldName="ServicesId" VisibleIndex="1">
                <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="ServicesDatas">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewCommandColumn VisibleIndex="4">
                <EditButton Visible="True"></EditButton>
                <NewButton Visible="true"></NewButton> 
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>           
            </dx:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
        </ClientSideEvents>
        <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
        </SettingsPager>                
        <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
</dx:ASPxGridView>
    <data:RoomDataSource SelectMethod="GetPaged" runat="server" ID="RoomDatas"  EnablePaging="True"  EnableSorting="True" >
    <DeepLoadProperties Method="IncludeChildren" Recursive="False"></DeepLoadProperties>
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="PageIndex" ControlID="gridRoom" PropertyName="PageIndex"  Type="Int32" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />                   
        </Parameters>    
    </data:RoomDataSource>
    <data:ServicesDataSource SelectMethod="GetPaged" runat="server" ID="ServicesDatas" >
         <Parameters>
            <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />                   
        </Parameters>    
    </data:ServicesDataSource>
</asp:Content>

