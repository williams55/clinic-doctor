<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Sms_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    SMS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">
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
            <h5>SMS</h5>
        </div>
        <dx:ASPxGridView ID="gridSms" ClientInstanceName="grid" runat="server" DataSourceID="SmsDataSource"
            KeyFieldName="Id" Width="100%" EnableRowsCache="False"
            OnCustomButtonCallback="gridSms_CustomButtonCallback"
            OnAutoFilterCellEditorInitialize="gridSms_AutoFilterCellEditorInitialize"
            OnCustomCallback="gridSms_CustomCallback">
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
                <dx:GridViewDataComboBoxColumn FieldName="SmsType" Caption="Type" Width="100">
                    <PropertiesComboBox TextField="Value" ValueField="Key">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="Type is required" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataMemoColumn FieldName="Message">
                    <PropertiesMemoEdit Width="400" Height="100"></PropertiesMemoEdit>
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataDateColumn FieldName="SendTime" Width="120" SortIndex="0" SortOrder="Descending">
                    <PropertiesDateEdit DisplayFormatString="HH:mm MM/dd/yyyy">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataColumn FieldName="AppointmentId" Width="100">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="IsSent" Width="50">
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn Name="btnCommand" ButtonType="Image" Width="60" Caption="Operation">
                    <EditButton Visible="False">
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

            <Templates>

                <EditForm>

                    <div style="padding-top: 10px">
                        <div style="float: left; width: 150px">Message</div>
                        <div style="float: left; width: 600px">
                            <dx:ASPxMemo runat="server" Width="100%" ID="txtMessage" Height="50px" Text='<%# Eval("Message") %>'>
                            </dx:ASPxMemo>
                        </div>
                        <div style="clear: left"></div>
                    </div>

                    <div style="padding-top: 10px">
                        <div style="float: left; width: 150px">Send Time</div>
                        <div style="float: left; width: 170px;">
                            <dx:ASPxTimeEdit ID="dtSendTime" runat="server"
                                DateTime='<%# Eval("SendTime") == null? DateTime.Now : DateTime.Parse(Eval("SendTime").ToString()) %>'
                                EditFormatString="HH:mm" DisplayFormatString="HH:mm" Width="100%">
                            </dx:ASPxTimeEdit>
                        </div>
                        <div style="float: left;">
                            <dx:ASPxDateEdit ID="dtSendDate" Width="170" runat="server" EditFormatString="MM/dd/yyyy" DisplayFormatString="MM/dd/yyyy"
                                Date='<%# Eval("SendTime") == null? DateTime.Now : DateTime.Parse(Eval("SendTime").ToString()) %>'>
                            </dx:ASPxDateEdit>
                        </div>
                        <div style="clear: left"></div>
                    </div>

                    <div style="padding-top: 10px">
                        <div style="float: left; width: 150px">Is Send Now</div>
                        <div style="float: left; width: 170px;">
                            <dx:ASPxCheckBox ID="chkIsSendNow" runat="server">
                            </dx:ASPxCheckBox>
                        </div>
                        <div style="float: left;">
                        </div>
                        <div style="clear: left"></div>
                    </div>

                    <div style="padding-top: 10px">
                        <div style="float: left; width: 150px">Input mobile phone<br />
                            Seperate by ";"</div>
                        <div style="float: left; width: 600px">
                            <dx:ASPxMemo runat="server" Width="100%" ID="txtPhones" Height="50px">
                            </dx:ASPxMemo>
                        </div>
                        <div style="clear: left"></div>
                    </div>

                    <div style="padding-top: 10px">
                        <div style="float: left; width: 150px">&nbsp;</div>
                        <div style="float: left">

                            <div style="float: left; width: 50px">
                                <a onclick="javascript:grid.PerformCallback('Save');">Update</a>
                            </div>

                            <div style="float: left">
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                            </div>
                            <div style="clear: left"></div>
                        </div>
                        <div style="clear: left"></div>
                    </div>

                </EditForm>
            </Templates>

            <Styles>
                <AlternatingRow Enabled="true" />
                <Table Wrap="True">
                </Table>
            </Styles>
            <ClientSideEvents EndCallback="function(s, e) { AlertMessage(); RefreshGrid(); }"
                BeginCallback="function(s, e) {command = e.command; gridObject = s;}"
                CustomButtonClick="function(s, e) { if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
            <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
            </SettingsPager>
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings ShowGroupPanel="False" ShowFilterRow="True" ShowFilterRowMenu="True" />
        </dx:ASPxGridView>
        <data:SmsDataSource SelectMethod="GetPaged" runat="server" ID="SmsDataSource" EnablePaging="True"
            EnableSorting="True">
            <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            </DeepLoadProperties>
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="PageIndex" ControlID="gridSms" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:SmsDataSource>
    </div>
</asp:Content>

