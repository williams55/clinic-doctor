<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>
                Home</h5>
        </div>
        <div id="box-other" style="min-height: 500px;">
        </div>
    </div>
</asp:Content>
