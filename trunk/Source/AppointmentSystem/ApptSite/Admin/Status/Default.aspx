<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Status_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Status</h5>
        </div>
        <div id="box-other">
            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="AccessDataSource1"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowUpdating="grid_RowUpdating">
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="1" Visible="False" />
                    <dx:GridViewDataColumn VisibleIndex="1" Caption="No." Width="50">
                        <DataItemTemplate>
                            <%# Container.ItemIndex + 1%>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="ColorCode" VisibleIndex="3" Width="70" />
                    <dx:GridViewDataColumn FieldName="PriorityIndex" Caption="Index" VisibleIndex="4"
                        Width="70">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn VisibleIndex="5" Caption="Operation" Width="70">
                        <EditButton Visible="true" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </dx:GridViewCommandColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <div id="devexpress-form">
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table class="edit-form">
                                    <tbody>
                                        <tr>
                                            <td class="title-row" style="width: 60px;">
                                                Title
                                            </td>
                                            <td class="content-row" style="width: 220px;">
                                                <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' CssClass="text-form"
                                                    MaxLength="50" Width="190px">
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Title is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="title-row" style="width: 80px;">
                                                Color Code
                                            </td>
                                            <td class="content-row" style="width: 150px;">
                                                <dx:ASPxColorEdit runat="server" ID="ColorEditHeaderBackColor" Color='<%# System.Drawing.ColorTranslator.FromHtml(Eval("ColorCode").ToString()) %>'
                                                    CssClass="text-form" Width="120px">
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Color Code is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxColorEdit>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="title-row" style="width: 60px;">
                                                Index
                                            </td>
                                            <td class="content-row" style="width: 100px;">
                                                <dx:ASPxSpinEdit ID="index" runat="server" Number='<%# Convert.ToInt32(Eval("PriorityIndex"))%>'
                                                    NumberType="Integer" MinValue="1" MaxValue="99999" CssClass="text-form" MaxLength="5"
                                                    Width="70px">
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Index is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right;">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server">
                                                </dx:ASPxGridViewTemplateReplacement>
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server">
                                                </dx:ASPxGridViewTemplateReplacement>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dx:ContentControl>
                        </div>
                    </EditForm>
                </Templates>
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
                <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:StatusDataSource ID="AccessDataSource1" runat="server" SelectMethod="GetPaged"
                EnablePaging="True" EnableSorting="True">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderBy" Value="PriorityIndex ASC" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:StatusDataSource>
        </div>
    </div>
</asp:Content>
