<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Status_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/resources/components/colorpicker/css/colorpicker.css") %>"
        type="text/css" />
    <link rel="stylesheet" media="screen" type="text/css" href="<%= ResolveUrl("~/resources/components/colorpicker/css/layout.css") %>" />

    <script src="<%= Page.ResolveClientUrl("~/resources/scripts/json2.js") %>" type="text/javascript"></script>

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/components/colorpicker/js/colorpicker.js") %>"></script>

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/components/colorpicker/js/eye.js") %>"></script>

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/components/colorpicker/js/utils.js") %>"></script>

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/components/colorpicker/js/layout.js?ver=1.0.2") %>"></script>

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div class="title">
        <h5>
            Status</h5>
    </div>
    <div id="box-other">
        <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1"
            KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowUpdating="grid_RowUpdating"
            OnCustomButtonCallback="grid_CustomButtonCallback">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0">
                    <EditButton Visible="true" />
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                <dx:GridViewDataColumn FieldName="ColorCode" VisibleIndex="2" />
                <dx:GridViewDataMemoColumn FieldName="Note" VisibleIndex="3">
                    <Settings AllowAutoFilter="False" />
                </dx:GridViewDataMemoColumn>
            </Columns>
            <ClientSideEvents EndCallback="function(s, e) { BuildColorPicker(); RefreshGrid();}"
                BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
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
                                        <td class="content-row">
                                            <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' CssClass="text-form">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="title-row">
                                            Color Code
                                        </td>
                                        <td class="content-row">
                                            <dx:ASPxTextBox runat="server" ID="txtColorCode" Text='<%# Bind("ColorCode") %>'
                                                CssClass="text-form float-left" Width="80">
                                            </dx:ASPxTextBox>
                                            <span id="customWidget">
                                                <div id="colorSelector">
                                                    <div style="background-color: <%# Eval("ColorCode") %>">
                                                    </div>
                                                </div>
                                                <div id="colorpickerHolder" style="z-index: 9999">
                                                </div>
                                                <input type="hidden" value="input[id*=txtColorCode]" id="colorCode" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="title-row">
                                            Note
                                        </td>
                                        <td class="content-row" colspan="3">
                                            <dx:ASPxMemo runat="server" ID="txtNote" Text='<%# Bind("Note")%>' CssClass="area-form">
                                            </dx:ASPxMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dx:ContentControl>
                    </div>
                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </div>
                </EditForm>
            </Templates>
            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
            <SettingsPager Mode="ShowPager" PageSize="10" Position="Bottom">
            </SettingsPager>
            <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
        </dx:ASPxGridView>
        <data:StatusDataSource ID="AccessDataSource1" runat="server" SelectMethod="GetPaged"
            EnablePaging="True" EnableSorting="True">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:StatusDataSource>
    </div>
</asp:Content>
