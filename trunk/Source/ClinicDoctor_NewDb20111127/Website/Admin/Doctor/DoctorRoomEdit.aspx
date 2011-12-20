<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="DoctorRoomEdit.aspx.cs" Inherits="DoctorRoomEdit" Title="DoctorRoom Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Doctor Room - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="DoctorRoomDataSource"
        OnLoad="FormView1_Load">
        <EditItemTemplatePaths>
            <data:TemplatePath Path="~/Admin/UserControls/DoctorRoomFields.ascx" />
        </EditItemTemplatePaths>
        <InsertItemTemplatePaths>
            <data:TemplatePath Path="~/Admin/UserControls/DoctorRoomFields.ascx" />
        </InsertItemTemplatePaths>
        <EmptyDataTemplate>
            <b>DoctorRoom not found!</b>
        </EmptyDataTemplate>
        <FooterTemplate>
            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert" CssClass="button" />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" CssClass="button" />
            <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" CssClass="button" />
        </FooterTemplate>
    </data:MultiFormView>
    <data:DoctorRoomDataSource ID="DoctorRoomDataSource" runat="server" SelectMethod="GetById">
        <Parameters>
            <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />
        </Parameters>
    </data:DoctorRoomDataSource>
    <br />
   
</asp:Content>
