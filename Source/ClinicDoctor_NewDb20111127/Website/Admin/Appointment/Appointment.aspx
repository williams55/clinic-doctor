<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="Appointment.aspx.cs" Inherits="Admin_Appointment_Appointment" Title="Manage Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <iframe src="AppointmentIframe.aspx" style="width: 100%; height: 550px;"></iframe>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('iframe').focus();
        });
    </script>

</asp:Content>
