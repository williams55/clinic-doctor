<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Patient_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
<script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
<div id="box-tabs" class="box">
<div class="title">
            <h5>Manager Patient</h5>
        </div>
<dx:ASPxGridView ID="gridPatient" ClientInstanceName="grid" runat="server" DataSourceID="PatientDatas"
        Width="100%" KeyFieldName="PatientCode" 
        oncustombuttoncallback="gridPatient_CustomButtonCallback" 
        oninitnewrow="gridPatient_InitNewRow" onrowinserting="gridPatient_RowInserting" onrowupdating="gridPatient_RowUpdating" 
        >
        <Columns>
            <dx:GridViewDataColumn FieldName="PatientCode" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="FirstName"  VisibleIndex="2" />
            <dx:GridViewDataColumn FieldName="LastName" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="MemberType" VisibleIndex="4" />
           <dx:GridViewDataColumn FieldName="Title" VisibleIndex="5" />
           <dx:GridViewDataCheckColumn FieldName="IsFemale" VisibleIndex="6" Caption="FeMale"></dx:GridViewDataCheckColumn>
           <dx:GridViewDataDateColumn FieldName="Birthdate" VisibleIndex="7"></dx:GridViewDataDateColumn>
            <dx:GridViewDataColumn FieldName="HomePhone" VisibleIndex="8" />
            <dx:GridViewDataColumn FieldName="WorkPhone" VisibleIndex="9" />
            <dx:GridViewDataColumn FieldName="CellPhone" VisibleIndex="10" />
            <dx:GridViewDataColumn FieldName="CompanyCode" VisibleIndex="11" />
            <dx:GridViewCommandColumn VisibleIndex="12" Name="btnCommand">
                <EditButton Visible="true"></EditButton>
                <NewButton Visible="true"></NewButton>
                <CustomButtons>  
                         <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete" >
                                <Image Url="../../resources/images/icons/cross.png" ToolTip="Delete patient"></Image>
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
                                                <dx:ASPxTextBox  runat="server" ReadOnly="true" ID="txtTitle" Text='<%# Bind("PatientCode") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                               First Name
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("FirstName") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                              Last Name
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox  runat="server"  ID="ASPxTextBox2" Text='<%# Bind("LastName") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>                                               
                                            </td>
                                            <td class="title-row">
                                               IsFemale
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxCheckBox runat="server" value='<%#Bind("IsFemale")%>' ID="ckcR">                                                 
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                              Member type
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox  runat="server"  ID="ASPxTextBox3" Text='<%# Bind("MemberType") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>                                               
                                            </td>
                                            <td class="title-row">
                                               Title
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxTextBox  runat="server"   ID="txtTitles" Text='<%# Bind("Title") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                               Birth date
                                            </td>
                                            <td class="content-row">                                             
                                                <dx:ASPxGridViewTemplateReplacement runat="server" ColumnID="6"   ReplacementType="EditFormCellEditor" />
                                            </td>
                                            <td class="title-row">
                                                Copany Code
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxTextBox runat="server" ID="ASPxTextBox5" Text='<%# Bind("CompanyCode") %>' CssClass="text-form" Width="100%">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                               Home phone
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox  runat="server" ReadOnly="false" ID="ASPxTextBox7" Text='<%# Bind("HomePhone") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                                WorkPhone
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxTextBox runat="server" ID="ASPxTextBox8" Text='<%# Bind("WorkPhone") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="title-row">
                                              Cell Phone
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox  runat="server" ReadOnly="false" ID="ASPxTextBox4" Text='<%# Bind("CellPhone") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <%if(!gridPatient.IsNewRowEditing){ %>
                                             <td class="title-row">
                                                Avarta
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
                                runat="server" CssClass="btn-update" >
                                
                            </dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                                runat="server" CssClass="btn-cancel">
                            </dx:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
        </Templates>
          <ClientSideEvents EndCallback="function(s, e) { RefreshGrid(); AlertMessage(); }" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                                </ClientSideEvents>
         <ClientSideEvents CustomButtonClick="function(s, e) {   if(e.buttonID == 'btnDelete'){ e.processOnServer = confirmDelete();}}" />
        <SettingsDetail ShowDetailRow="true" />
        <Settings ShowGroupPanel="True" />
    </dx:ASPxGridView>
<data:PatientDataSource ID="PatientDatas" runat="server" SelectMethod="GetPaged">
    <Parameters >
             <data:CustomParameter Name="WhereClause" Value="IsDisabled ='false'" ConvertEmptyStringToNull="false" />
             <asp:ControlParameter Name="PageIndex" ControlID="gridPatient" PropertyName="PageIndex"  Type="Int32" />
             <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
    </Parameters>
</data:PatientDataSource>
</div>
</asp:Content>

