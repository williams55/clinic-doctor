
<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Status.aspx.cs" Inherits="Admin_Status" Title="Status List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Status List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="StatusDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Status.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]"  />
				<asp:BoundField DataField="ColorCode" HeaderText="Color Code" SortExpression="[ColorCode]"  />
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]"  />
				<data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]"  />
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]"  />
				<asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Create Date" SortExpression="[CreateDate]"  />
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]"  />
				<asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Update Date" SortExpression="[UpdateDate]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Status Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnStatus" OnClientClick="javascript:location.href='StatusEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:StatusDataSource ID="StatusDataSource" runat="server"
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
		</data:StatusDataSource>
	    		
</asp:Content>



