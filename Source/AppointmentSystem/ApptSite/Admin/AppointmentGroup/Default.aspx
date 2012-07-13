<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_AppointmentGroup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Appointment Group Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Appointment Group</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRoom" ClientInstanceName="grid" runat="server" DataSourceID="UnitsDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRoom_RowInserting"
                OnCustomButtonCallback="gridRoom_CustomButtonCallback">
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="Note" VisibleIndex="2" />
                </Columns>
                <Templates>
                    <DetailRow>
                        These ones will appear at column in scheduler
                        <br />
                        <br />
                        <dx:ASPxGridView ID="detailGrid" runat="server" DataSourceID="AppointmentGroupDataSource"
                            KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="detailGrid_DataSelect"
                            OnRowInserting="detailGrid_RowInserting">
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0">
                                    <EditButton Visible="true" />
                                    <NewButton Visible="true" />
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="Note" VisibleIndex="2" />
                            </Columns>
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="True" />
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>
                <SettingsEditing PopupEditFormWidth="600px" Mode="EditFormAndDisplayRow" />
            </dx:ASPxGridView>
            <data:UnitsDataSource ID="UnitsDataSource" runat="server" SelectMethod="GetPaged"
                EnablePaging="True" EnableSorting="True">
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridRoom" PropertyName="PageIndex"
                        Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:UnitsDataSource>
            <data:AppointmentGroupDataSource ID="AppointmentGroupDataSource" runat="server" SelectMethod="GetPaged"
                EnablePaging="True" EnableSorting="True">
                <Parameters>
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridRoom" PropertyName="PageIndex"
                        Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
                </Parameters>
            </data:AppointmentGroupDataSource>
        </div>
    </div>
</asp:Content>
