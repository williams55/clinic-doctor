<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Patient_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Patient
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Manage Patient</h5>
        </div>
        <dx:ASPxGridView ID="gridPatient" ClientInstanceName="grid" runat="server" DataSourceID="PatientDatas"
            Width="100%" KeyFieldName="PatientCode" OnCustomButtonCallback="gridPatient_CustomButtonCallback"
            OnInitNewRow="gridPatient_InitNewRow" OnRowInserting="gridPatient_RowInserting"
            OnRowUpdating="gridPatient_RowUpdating" OnBeforePerformDataSelect="gridPatient_BeforePerformDataSelect">
            <Columns>
                <dx:GridViewDataColumn FieldName="PatientCode" VisibleIndex="1" />
                <dx:GridViewDataColumn FieldName="FirstName" VisibleIndex="2" />
                <dx:GridViewDataColumn FieldName="LastName" VisibleIndex="3" />
                <dx:GridViewDataColumn FieldName="MemberType" VisibleIndex="4" />
                <dx:GridViewDataColumn FieldName="Title" VisibleIndex="5" Visible="False" />
                <dx:GridViewDataCheckColumn FieldName="IsFemale" VisibleIndex="6" Caption="Female">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataDateColumn FieldName="Birthdate" VisibleIndex="7" Caption="DOB">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataColumn FieldName="HomePhone" VisibleIndex="8" />
                <dx:GridViewDataColumn FieldName="WorkPhone" VisibleIndex="9" />
                <dx:GridViewDataColumn FieldName="CellPhone" VisibleIndex="10" />
                <dx:GridViewDataColumn FieldName="CompanyCode" VisibleIndex="11" Visible="False" />
                <dx:GridViewCommandColumn VisibleIndex="12" Name="btnCommand">
                    <EditButton Visible="true">
                    </EditButton>
                    <NewButton Visible="true">
                    </NewButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            <Image Url="../../resources/images/icons/cross.png" ToolTip="Delete patient">
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div id="devexpress-form">
                        <table class="edit-form">
                            <tbody>
                                <tr>
                                    <td class="title-row">
                                        Patient Code
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="true" ID="txtTitle" Text='<%# Bind("PatientCode") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        First Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("FirstName") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Last Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox2" Text='<%# Bind("LastName") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Is Female
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxCheckBox TextAlign="Left" runat="server" Value='<%#Bind("IsFemale")%>' ID="ckcR">
                                        </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Member Type
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox3" Text='<%# Bind("MemberType") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Title
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtTitles" Text='<%# Bind("Title") %>' CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        DOB
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxGridViewTemplateReplacement runat="server" ColumnID="6" ReplacementType="EditFormCellEditor" />
                                    </td>
                                    <td class="title-row">
                                        Company Code
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox5" Text='<%# Bind("CompanyCode") %>'
                                            CssClass="text-form" Width="100%">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Home Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox7" Text='<%# Bind("HomePhone") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Work Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox8" Text='<%# Bind("WorkPhone") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Cell Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox4" Text='<%# Bind("CellPhone") %>'
                                            CssClass="text-form">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <%if (!gridPatient.IsNewRowEditing)
                                      { %>
                                    <td class="title-row">
                                        Avatar
                                    </td>
                                    <td class="content-row">
                                    </td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Note
                                    </td>
                                    <td class="content-row" colspan="4">
                                        <dx:ASPxMemo runat="server" ID="ASPxTextBox6" Text='<%# Bind("Note")%>' CssClass="text-form">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormUpdateButton"
                            runat="server">
                        </dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                            runat="server">
                        </dx:ASPxGridViewTemplateReplacement>
                    </div>
                </EditForm>
                <DetailRow>
                    <div style="padding: 3px 3px 2px 3px">
                        <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                            <TabPages>
                                <dx:TabPage Text="Appointtment" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <dx:ASPxGridView ID="GridAppointment" runat="server" DataSourceID="AppointmentDatas"
                                                KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="GridAppointment_BeforePerformDataSelect">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" />
                                                    <dx:GridViewDataColumn FieldName="Username" VisibleIndex="1" Caption="Doctor" />
                                                    <dx:GridViewDataColumn FieldName="RoomId" VisibleIndex="3" />
                                                    <dx:GridViewDataColumn FieldName="ServicesId" VisibleIndex="4" />
                                                    <dx:GridViewDataColumn FieldName="StatusId" VisibleIndex="5" />
                                                    <dx:GridViewDataColumn FieldName="AppointmentGroupId" VisibleIndex="6" />
                                                    <dx:GridViewDataColumn FieldName="StartTime" VisibleIndex="7" />
                                                    <dx:GridViewDataColumn FieldName="EndTime" VisibleIndex="8" />
                                                </Columns>
                                                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                                                    BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
                                                <Settings ShowFooter="True" />
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Group" Visible="false">
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </DetailRow>
            </Templates>
            <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }"
                BeginCallback="function(s, e) {command = e.command; gridObject = s;}"></ClientSideEvents>
            <ClientSideEvents CustomButtonClick="function(s, e) {   if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
            <SettingsDetail ShowDetailRow="true" />
            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
        </dx:ASPxGridView>
        <data:PatientDataSource ID="PatientDatas" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="PageIndex" ControlID="gridPatient" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:PatientDataSource>
        <data:AppointmentDataSource ID="AppointmentDatas" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <asp:ControlParameter Name="PageIndex" ControlID="gridPatient" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:AppointmentDataSource>
    </div>
</asp:Content>
