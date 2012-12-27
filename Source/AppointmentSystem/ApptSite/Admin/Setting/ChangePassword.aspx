<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Admin_Setting_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" Runat="Server">
    <input type="reset" id="resetTmp" style="display: none;" />
    <div id="box-left-tabs" class="box box-left box-padding">
        <!-- box / title -->
        <div class="title">
            <h5>
                Change Password</h5>
        </div>
        <!-- end box / title -->
        <div id="box-left-forms">
            <div class="form">
                <div class="fields">
                    <div class="field field-first">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="OldPasswordLabel" AssociatedControlID="AppointmentTextBox"
                                Text="Old password:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="OldPasswordTextBox"
                            MaxLength="50" Width="200px" Password="True" ClientInstanceName="OldPassword">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="Old password is required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="field">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="NewPasswordLabel" AssociatedControlID="RosterTextBox"
                                Text="New Password:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="NewPasswordTextBox"
                            MaxLength="50" Width="200px" Password="True" ClientInstanceName="NewPassword">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="New Password is required" />
                            </ValidationSettings>
                            <ClientSideEvents Validation="function(s, e) {
                                    if (NewPassword.GetText() != ''){
                                        if(NewPassword.GetText().length < 7){
                                            e.isValid = false;
                                            e.errorText = 'New Password must be at least 7 characters.';
                                        }
                                    }
                                }" />
                        </dx:ASPxTextBox>
                    </div>
                    <div class="field">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="ConfirmPasswordLabel" AssociatedControlID="PatientTextBox"
                                Text="Confirm New Password:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="ConfirmPasswordTextBox"
                            MaxLength="50" Width="200px" Password="True" ClientInstanceName="ConfirmPassword">
                            <ValidationSettings EnableCustomValidation="True" ErrorText="Confirm password is not match">
                                <RequiredField IsRequired="True" ErrorText="Confirm New Password is required" />
                            </ValidationSettings>
                            <ClientSideEvents Validation="function(s, e) {
                                    if (ConfirmPassword.GetText() != ''){
                                        if(ConfirmPassword.GetText().length < 7){
                                            e.isValid = false;
                                            e.errorText = 'Confirm password must be at least 7 characters.';
                                        }
                                        else if(NewPassword.GetText() != ConfirmPassword.GetText()){
                                            e.isValid = false;
                                            e.errorText = 'Confirm password is not match.';
                                        }
                                    }
                                }" />
                        </dx:ASPxTextBox>
                    </div>
                    <div class="buttons">
                        <table style="width: auto;">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="SubmitPrefix" runat="server" Text="Save" Width="80px" OnClick="SubmitAppointment_Click"/>
                                </td>
                                <td style="padding-left: 8px">
                                    <dx:ASPxButton ID="ResetPrefix" runat="server" AutoPostBack="False" Text="Reset"
                                        CausesValidation="False" Width="80px">
                                        <ClientSideEvents Click="function(s, e) { $('#resetTmp').click(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

