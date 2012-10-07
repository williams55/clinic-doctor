<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Room_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Room
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

    <script type="text/javascript">
        function OnClickButtonDel() {
            if (grid.GetSelectedRowCount() <= 0) {
                ShowDialog(null, null, "Please select item first.");
                return;
            }
            if (confirm("Are you sure to delete these items")) {
                grid.PerformCallback('Delete');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div class="color">
        <asp:HyperLink runat="server" ID="btnAdd" NavigateUrl="javascript:grid.AddNewRow()"
            ToolTip="New" CssClass="add"></asp:HyperLink>
        <a class="delete" title="Delete selected items" onclick="OnClickButtonDel()" id="btnGeneralDelete"
            runat="server"></a>
    </div>
    <div class="box">
        <div class="title">
            <h5>
                Room</h5>
        </div>
        <dx:ASPxGridView ID="gridRoom" ClientInstanceName="grid" runat="server" DataSourceID="RoomDatas"
            KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRoom_RowInserting"
            OnCustomButtonCallback="gridRoom_CustomButtonCallback" OnRowUpdating="gridRoom_RowUpdating"
            OnAutoFilterCellEditorInitialize="gridRoom_AutoFilterCellEditorInitialize" OnCustomCallback="gridRoom_CustomCallback"
            OnHtmlRowCreated="gridRoom_OnHtmlRowCreated">
            <Columns>
                <dx:GridViewDataColumn Visible="False" FieldName="Id">
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
                <dx:GridViewDataComboBoxColumn FieldName="ServicesId" Caption="Service">
                    <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="ServicesDatas">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Service is required" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="Title">
                    <EditItemTemplate>
                        <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' CssClass="text-form"
                            MaxLength="100" Width="100%">
                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                ErrorText="Error">
                                <RequiredField IsRequired="True" ErrorText="Title is required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Note">
                    <Settings AllowAutoFilter="False"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn Name="btnCommand" ButtonType="Image" Width="60" Caption="Operation">
                    <EditButton>
                        <Image Url="../../resources/images/icons/edit.png" ToolTip="Edit" AlternateText="Edit"
                            Height="15" Width="15">
                        </Image>
                    </EditButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnDelete">
                            <Image Url="../../resources/images/icons/del.png" ToolTip="Delete" AlternateText="Delete"
                                Height="15" Width="15">
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn Caption="#" ShowSelectCheckbox="True" Width="15">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                            ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                    </HeaderTemplate>
                    <CellStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
            </Columns>
            <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                BeginCallback="function(s, e) {command = e.command; gridObject = s;}" CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
            <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
            </SettingsPager>
            <SettingsEditing Mode="EditForm" />
            <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
        </dx:ASPxGridView>
        <data:RoomDataSource SelectMethod="GetPaged" runat="server" ID="RoomDatas" EnablePaging="True"
            EnableSorting="True">
            <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            </DeepLoadProperties>
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="PageIndex" ControlID="gridRoom" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:RoomDataSource>
        <data:ServicesDataSource SelectMethod="GetPaged" runat="server" ID="ServicesDatas">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:ServicesDataSource>
    </div>
</asp:Content>
