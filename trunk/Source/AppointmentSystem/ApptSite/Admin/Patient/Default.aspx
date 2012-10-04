<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Patient_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Patient
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div class="color">
        <asp:HyperLink runat="server" ID="btnAdd" NavigateUrl="javascript:grid.AddNewRow()"
            ToolTip="New" CssClass="add"></asp:HyperLink>
    </div>
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Manage Patient</h5>
        </div>
        <dx:ASPxGridView ID="gridPatient" ClientInstanceName="grid" runat="server" DataSourceID="VcsPatient"
            Width="100%" KeyFieldName="PatientCode" OnCustomButtonCallback="gridPatient_CustomButtonCallback"
            OnInitNewRow="gridPatient_InitNewRow" OnRowInserting="gridPatient_RowInserting"
            OnRowUpdating="gridPatient_RowUpdating">
            <Columns>
                <dx:GridViewDataColumn Caption="No." Width="50">
                    <DataItemTemplate>
                        <%# Container.ItemIndex + 1%>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PatientCode" />
                <dx:GridViewDataColumn FieldName="FirstName" />
                <dx:GridViewDataColumn FieldName="LastName" />
                <dx:GridViewDataComboBoxColumn FieldName="MemberType" Width="120">
                    <PropertiesComboBox TextField="MemberType" ValueField="MemberType" DataSourceID="VcsMemberTypeDataSource"
                        Width="100%">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="EmailAddress" Visible="False" />
                <dx:GridViewDataColumn FieldName="Sex" Caption="Sex">
                </dx:GridViewDataColumn>
                <dx:GridViewDataDateColumn FieldName="DateOfBirth" Width="120" Caption="DOB" Visible="False">
                    <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" EditFormat="Custom" EditFormatString="MM/dd/yyyy"
                        EnableAnimation="False" Width="100%">
                        <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                            ErrorText="Error">
                            <RequiredField IsRequired="True" ErrorText="DOB is required" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <Settings AutoFilterCondition="GreaterOrEqual" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataColumn FieldName="HomePhone" Visible="False" />
                <dx:GridViewDataColumn FieldName="CompanyPhone" Visible="False" />
                <dx:GridViewDataColumn FieldName="MobilePhone" />
                <dx:GridViewDataComboBoxColumn FieldName="CompanyCode">
                    <PropertiesComboBox TextField="CompanyName" ValueField="CompanyCode" DataSourceID="VcsCompanyDataSource"
                        Width="100%">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="Remark" Width="150" />
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
            </Columns>
            <Templates>
                <EditForm>
                    <div id="devexpress-form">
                        <table class="edit-form">
                            <tbody>
                                <tr>
                                    <td class="title-row" style="width: 100px;">
                                        Patient Code
                                    </td>
                                    <td class="content-row" style="width: 220px;">
                                        <dx:ASPxTextBox runat="server" ReadOnly="true" ID="txtTitle" Text='<%# Bind("PatientCode") %>'
                                            CssClass="text-form" TabIndex="1">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row" style="width: 100px;">
                                        Home Street
                                    </td>
                                    <td class="content-row" style="width: 220px;">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox3" Text='<%# Bind("HomeStreet") %>'
                                            CssClass="text-form" TabIndex="10">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row" style="width: 100px;">
                                        Work Street
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox9" Text='<%# Bind("WorkStreet") %>'
                                            CssClass="text-form" TabIndex="18">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        First Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtFirstName" Text='<%# Bind("FirstName") %>'
                                            CssClass="text-form" MaxLength="50" Width="100%" TabIndex="2">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error" ValidateOnLeave="True">
                                                <RequiredField IsRequired="True" ErrorText="First Name is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Home Ward
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox10" Text='<%# Bind("HomeWard") %>'
                                            CssClass="text-form" TabIndex="11">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Work Ward
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox11" Text='<%# Bind("WorkWard") %>'
                                            CssClass="text-form" TabIndex="19">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Middle Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("MiddleName") %>'
                                            CssClass="text-form" MaxLength="50" Width="100%" TabIndex="3">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Home District
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox12" Text='<%# Bind("HomeDistrict") %>'
                                            CssClass="text-form" TabIndex="12">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Work District
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox13" Text='<%# Bind("WorkDistrict") %>'
                                            CssClass="text-form" TabIndex="20">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Last Name
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="txtLastName" Text='<%# Bind("LastName") %>' CssClass="text-form"
                                            MaxLength="50" Width="100%" TabIndex="4">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Last Name is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Home City
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox15" Text='<%# Bind("HomeCity") %>'
                                            CssClass="text-form" TabIndex="13">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Work City
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox14" Text='<%# Bind("WorkCity") %>'
                                            CssClass="text-form" TabIndex="21">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Sex
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxRadioButton runat="server" Checked='<%# Eval("Sex") != null && Eval("Sex").ToString() == "M" %>'
                                            Text="Male" ID="radMale" GroupName="radSex" Layout="Flow" TabIndex="5" />
                                        <dx:ASPxRadioButton runat="server" Checked='<%# Eval("Sex") != null && Eval("Sex").ToString() == "F" %>'
                                            Text="Female" ID="radFemale" GroupName="radSex" Layout="Flow" TabIndex="6" />
                                    </td>
                                    <td class="title-row">
                                        Home Country
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox16" Text='<%# Bind("HomeCountry") %>'
                                            CssClass="text-form" TabIndex="14">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Work Country
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox17" Text='<%# Bind("WorkCountry") %>'
                                            CssClass="text-form" TabIndex="22">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Member Type
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement3" runat="server"
                                            ColumnID="4" ReplacementType="EditFormCellEditor">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                    <td class="title-row">
                                        Home Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox7" Text='<%# Bind("HomePhone") %>'
                                            CssClass="text-form" TabIndex="15">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Company Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ID="ASPxTextBox8" Text='<%# Bind("CompanyPhone") %>'
                                            CssClass="text-form" TabIndex="23">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        DOB
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxGridViewTemplateReplacement runat="server" ColumnID="7" ReplacementType="EditFormCellEditor" />
                                    </td>
                                    <td class="title-row">
                                        Mobile Phone
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox4" Text='<%# Bind("MobilePhone") %>'
                                            CssClass="text-form" TabIndex="16">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Company Code
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" runat="server"
                                            ColumnID="11" ReplacementType="EditFormCellEditor">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Nationality
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox2" Text='<%# Bind("Nationality") %>'
                                            CssClass="text-form" TabIndex="9" Width="100%">
                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"
                                                ErrorText="Error">
                                                <RequiredField IsRequired="True" ErrorText="Nationality is required" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Billing Address
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox19" Text='<%# Bind("BillingAddress") %>'
                                            CssClass="text-form" TabIndex="17">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="title-row">
                                        Fax
                                    </td>
                                    <td class="content-row">
                                        <dx:ASPxTextBox runat="server" ReadOnly="false" ID="ASPxTextBox18" Text='<%# Bind("Fax") %>'
                                            CssClass="text-form" TabIndex="25">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title-row">
                                        Remark
                                    </td>
                                    <td class="content-row" colspan="6">
                                        <dx:ASPxMemo runat="server" ID="ASPxTextBox6" Text='<%# Bind("Remark")%>' CssClass="text-form"
                                            TabIndex="26">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                        <dx:ASPxHyperLink runat="server" Text="Update" ID="ASPxHyperLink1">
                            <ClientSideEvents Click="function(s, e) { grid.UpdateEdit(); }" />
                        </dx:ASPxHyperLink>
                        <dx:ASPxHyperLink runat="server" Text="Cancel" ID="ASPxHyperLink2">
                            <ClientSideEvents Click="function(s, e) { grid.CancelEdit(); }" />
                        </dx:ASPxHyperLink>
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
        <data:VcsPatientDataSource ID="VcsPatient" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:VcsPatientDataSource>
        <data:VcsMemberTypeDataSource ID="VcsMemberTypeDataSource" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:VcsMemberTypeDataSource>
        <data:VcsCompanyDataSource ID="VcsCompanyDataSource" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:VcsCompanyDataSource>
        <data:AppointmentDataSource ID="AppointmentDatas" runat="server" SelectMethod="GetPaged">
            <Parameters>
                <asp:ControlParameter Name="PageIndex" ControlID="gridPatient" PropertyName="PageIndex"
                    Type="Int32" />
                <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
            </Parameters>
        </data:AppointmentDataSource>
    </div>
</asp:Content>
