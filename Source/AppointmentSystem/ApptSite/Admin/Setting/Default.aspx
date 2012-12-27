<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Setting_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Settings
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript">
        // <![CDATA[
        function OnNameValidation(s, e) {
            var name = e.value;
            if (name == null)
                return;
            if (name.length < 2)
                e.isValid = false;
        }
        function OnMinuteStepValidation(s, e) {
            var age = e.value;
            if (age == null || age == "")
                return;
            var digits = "0123456789";
            for (var i = 0; i < age.length; i++) {
                if (digits.indexOf(age.charAt(i)) == -1) {
                    e.isValid = false;
                    break;
                }
            }
            if (e.isValid && age.charAt(0) == '0') {
                age = age.replace(/^0+/, "");
                if (age.length == 0)
                    age = "0";
                e.value = age;
            }
            if (age < 5 || age > 60)
                e.isValid = false;
        }
        function OnArrivalDateValidation(s, e) {
            var selectedDate = s.date;
            if (selectedDate == null || selectedDate == false)
                return;
            var currentDate = new Date();
            if (currentDate.getFullYear() != selectedDate.getFullYear() || currentDate.getMonth() != selectedDate.getMonth())
                e.isValid = false;
        }
        // ]]>
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <input type="reset" id="resetTmp" style="display: none;" />
    <div id="box-left-tabs" class="box box-left box-padding">
        <!-- box / title -->
        <div class="title">
            <h5>
                Prefix Settings</h5>
        </div>
        <!-- end box / title -->
        <div id="box-left-forms">
            <div class="form">
                <div class="fields">
                    <div class="field field-first">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="AppointmentLabel" AssociatedControlID="AppointmentTextBox"
                                Text="Appointment:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="AppointmentTextBox"
                            MaxLength="5" Width="200px">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="Appointment Prefix is required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="field">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="RosterLabel" AssociatedControlID="RosterTextBox"
                                Text="Roster:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="RosterTextBox" MaxLength="5"
                            Width="200px">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="Roster Prefix is required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="field">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="PatientLabel" AssociatedControlID="PatientTextBox"
                                Text="Patient:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="PatientTextBox" MaxLength="5"
                            Width="200px">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="Patient Prefix is required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="field">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="UserLabel" AssociatedControlID="UserTextBox" Text="User:"
                                EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="UserTextBox" MaxLength="5"
                            Width="200px">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="User Prefix is required" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <div class="buttons">
                        <table style="width: auto;">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="SubmitPrefix" runat="server" Text="Save" Width="80px" OnClick="SubmitPrefix_Click" />
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
    <div class="box box-right">
        <!-- box / title -->
        <div class="title">
            <h5>
                Appointment Settings</h5>
        </div>
        <div>
            <div class="form">
                <div class="fields">
                    <div class="field field-first">
                        <div class="label">
                            <dx:ASPxLabel runat="server" ID="MinuteStepLabel" AssociatedControlID="MinuteStepTextBox"
                                Text="Minute Step:" EnableViewState="False" />
                        </div>
                        <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" ID="MinuteStepTextBox"
                            ClientInstanceName="MinuteStep" OnValidation="MinuteStepTextBox_Validation" MaxLength="2"
                            Width="200px">
                            <ValidationSettings ErrorText="Minute Step must be between 5 and 60">
                                <RequiredField IsRequired="True" ErrorText="Minute Step is required" />
                                <RegularExpression ErrorText="Invalid number" ValidationExpression="[0-9]*" />
                            </ValidationSettings>
                            <ClientSideEvents Validation="OnMinuteStepValidation" />
                        </dx:ASPxTextBox>
                    </div>
                    <div class="buttons">
                        <table style="width: auto;">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="SubmitAppointment" runat="server" Text="Save" Width="80px" OnClick="SubmitAppointment_Click" />
                                </td>
                                <td style="padding-left: 8px">
                                    <dx:ASPxButton ID="ResetAppointment" runat="server" AutoPostBack="False" Text="Reset"
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
