﻿<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"  CodeFile="RoomFuncEdit.aspx.cs" Inherits="RoomFuncEdit" Title="RoomFunc Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    Room Func - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" 
            DataSourceID="RoomFuncDataSource" onload="FormView1_Load">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/RoomFuncFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/RoomFuncFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>RoomFunc not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" CssClass="button" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" CssClass="button" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" CssClass="button" runat="server" CausesValidation="False" CommandName="Cancel" Text="Finish" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:RoomFuncDataSource ID="RoomFuncDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:RoomFuncDataSource>
		
		<br />

		

</asp:Content>
