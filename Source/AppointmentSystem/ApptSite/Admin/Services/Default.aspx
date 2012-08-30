<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Services_Default" %>

<%@ Import Namespace="AppointmentSystem.Settings.BusinessLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Service
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Service</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridServices" ClientInstanceName="grid" runat="server" DataSourceID="ServicesDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridServices_RowInserting"
                OnCustomButtonCallback="gridServices_CustomButtonCallback" OnRowUpdating="gridServices_RowUpdating">
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" ReadOnly="true" Visible="False">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="No." Width="50">
                        <DataItemTemplate>
                            <%# Container.ItemIndex + 1%>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Title" />
                    <dx:GridViewDataColumn FieldName="ShortTitle" />
                    <dx:GridViewDataColumn FieldName="Note">
                        <Settings AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PriorityIndex" Caption="Index" Width="70">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <Settings AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn Caption="Operation" Width="100">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
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
                                            <td class="title-row" style="width: 100px;">
                                                Title
                                            </td>
                                            <td class="content-row" style="width: 220px;">
                                                <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' CssClass="text-form"
                                                    MaxLength="100" Width="100%">
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Title is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row" style="width: 80px; text-align: right;">
                                                Note
                                            </td>
                                            <td class="title-row" rowspan="3">
                                                <dx:ASPxMemo runat="server" ID="txtNote" Text='<%# Bind("Note") %>' CssClass="text-form"
                                                    MaxLength="500" Width="100%" Rows="5">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Short Title
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox runat="server" ID="txtShortTitle" Text='<%# Bind("ShortTitle") %>'
                                                    CssClass="text-form" MaxLength="100" Width="100%">
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Short Title is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row" style="width: 60px;">
                                                Index
                                            </td>
                                            <td class="content-row" style="width: 220px;">
                                                <dx:ASPxSpinEdit ID="index" runat="server" Number='<%# Convert.ToInt32(Eval("PriorityIndex"))%>'
                                                    NumberType="Integer" MinValue="1" MaxValue='<%#ServiceFacade.SettingsHelper.MaxPriorityIndex %>'
                                                    CssClass="text-form" MaxLength="5">
                                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                        ErrorText="Error">
                                                        <RequiredField IsRequired="True" ErrorText="Index is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxSpinEdit>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">
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
                                  BeginCallback="function(s, e) {command = e.command; gridObject = s;}"
                                  CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
                <SettingsPager Mode="ShowPager" Position="Bottom">
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
            </dx:ASPxGridView>
            <data:ServicesDataSource ID="ServicesDataSource" runat="server" SelectMethod="GetPaged"
                EnablePaging="True" EnableSorting="True">
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
