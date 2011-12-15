﻿
<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Customer.aspx.cs" Inherits="Admin_Customer" Title="Customer List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Customer List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="CustomerDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Customer.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="[Id]" ReadOnly="True" />
				<asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="[FirstName]"  />
				<asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="[LastName]"  />
				<asp:BoundField DataField="Address" HeaderText="Address" SortExpression="[Address]"  />
				<asp:BoundField DataField="HomePhone" HeaderText="Home Phone" SortExpression="[HomePhone]"  />
				<asp:BoundField DataField="WorkPhone" HeaderText="Work Phone" SortExpression="[WorkPhone]"  />
				<asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" SortExpression="[CellPhone]"  />
				<asp:BoundField DataField="Birthdate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Birthdate" SortExpression="[Birthdate]"  />
				<data:BoundRadioButtonField DataField="IsFemale" HeaderText="Is Female" SortExpression="[IsFemale]"  />
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]"  />
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]"  />
				<data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]"  />
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]"  />
				<asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Create Date" SortExpression="[CreateDate]"  />
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]"  />
				<asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Update Date" SortExpression="[UpdateDate]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Customer Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnCustomer" OnClientClick="javascript:location.href='CustomerEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:CustomerDataSource ID="CustomerDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
		>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:CustomerDataSource>
	    		
</asp:Content>


