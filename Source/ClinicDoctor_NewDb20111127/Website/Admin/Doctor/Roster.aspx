﻿<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="Roster.aspx.cs" Inherits="Admin_Doctor_Roster" Title="Roster Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <iframe src="RosterIframe.aspx" style="width: 100%; height: 550px;"></iframe>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('iframe').focus();
        });
    </script>
</asp:Content>
