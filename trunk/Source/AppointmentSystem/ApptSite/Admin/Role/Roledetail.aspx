<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Roledetail.aspx.cs" Inherits="Admin_Role_Roledetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
<div id="box-tabs" class="box">
        <div class="title">
            <h5>Role Detail</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView runat="server" ID="Gridroledetail" 
                DataSourceID="RoledetailDataS" KeyFieldName="Id" 
                onrowinserting="Gridroledetail_RowInserting" 
                onrowupdating="Gridroledetail_RowUpdating">
                <Columns>
                <dx:GridViewCommandColumn Caption="Control">
                    <NewButton Visible="true"></NewButton>
                    <EditButton Visible="true"></EditButton>
                </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="1">
                        <EditFormSettings Visible="False"/>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="RoleId" VisibleIndex="2">
                        <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="RoleDatas"></PropertiesComboBox>                        
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="ScreenId" VisibleIndex="2">
                        <PropertiesComboBox TextField="ScreenCode" ValueField="Id" DataSourceID="ScreenDatas"></PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataColumn FieldName="Crud" Visible="true" ></dx:GridViewDataColumn>
                </Columns>
          <%--         <Templates>
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
                                                         <dx:ASPxTextBox runat="server" ID="txtScreenCode" Text='<%# Bind("Id") %>' CssClass="text-form">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="title-row">
                                                       Screen 
                                                    </td>
                                                    <td class="content-row" rowspan="2">
                                                      <dx:ASPxComboBox ID="cborole" value> </dx:ASPxComboBo>
                                                    </td>     
                                                    <td class="title-row">
                                                        Crud
                                                    </td>
                                                    <td class="content-row" rowspan="2">
                                                       <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Crud")%>' CssClass="text-form">
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
                        </Templates>--%>
                        <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                        </ClientSideEvents>
                        <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                        </SettingsPager>
                        <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:RoleDetailDataSource runat="server" ID="RoledetailDataS" SelectMethod="GetPaged">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter  Name="OrderByClause" Value="" ConvertEmptyStringToNull="false"/>
                    <asp:ControlParameter Name="PageIndex" ConvertEmptyStringToNull="false" ControlId="GridRoledetail" PropertyName="PageIndex"  />
                </Parameters>
            </data:RoleDetailDataSource>
            <data:RoleDataSource ID="RoleDatas" runat="server" SelectMethod="GetPaged">
                <Parameters>
                    <data:CustomParameter Name="whereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                </Parameters>
            </data:RoleDataSource>
            <data:ScreenDataSource runat="server" ID="ScreenDatas">
                 <Parameters>
                    <data:CustomParameter Name="whereClause" Value="IsDisabled='false'" ConvertEmptyStringToNull="false" />
                </Parameters>
            </data:ScreenDataSource>
    </div>
    </div>
</asp:Content>

