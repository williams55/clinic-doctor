<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserGroup.aspx.cs" Inherits="Admin_Users_UserGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
<div id="box-tabs" class="box">
        <div class="title">
            <h5>User Group</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView runat="server" ID="GridUserGroup" 
                DataSourceID="UserGroupDatas" KeyFieldName="Id" 
                oncustombuttoncallback="GridUserGroup_CustomButtonCallback" 
                onrowinserting="GridUserGroup_RowInserting" 
                onrowupdating="GridUserGroup_RowUpdating">
                <Columns>
                <dx:GridViewCommandColumn Caption="Control">
                    <NewButton Visible="true"></NewButton>
                    <EditButton Visible="true"></EditButton>
                </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="1"><EditFormSettings Visible="False"/></dx:GridViewDataColumn>                    
                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Note" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Roles" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataCheckColumn FieldName="IsLocked" VisibleIndex="1"></dx:GridViewDataCheckColumn>
                </Columns>
         
                        <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                        </ClientSideEvents>
                        <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                        </SettingsPager>
                        <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:UserGroupDataSource runat="server" ID="UserGroupDatas" SelectMethod="GetPaged">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter  Name="OrderByClause" Value="" ConvertEmptyStringToNull="false"/>
                    <asp:ControlParameter Name="PageIndex" ConvertEmptyStringToNull="false" ControlId="GridUserGroup" PropertyName="PageIndex"  />
                </Parameters>
            </data:UserGroupDataSource>
           
    </div>
    </div>
</asp:Content>

