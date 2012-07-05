<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="UserGroupEdit.aspx.cs" Inherits="UserGroupEdit" Title="UserGroup Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">User Group - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="UserGroupDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/UserGroupFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/UserGroupFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>UserGroup not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:UserGroupDataSource ID="UserGroupDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:UserGroupDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewGroupRole1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewGroupRole1_SelectedIndexChanged"			 			 
			DataSourceID="GroupRoleDataSource1"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_GroupRole.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Group Id" DataNavigateUrlFormatString="UserGroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="GroupIdSource" DataTextField="Title" />
				<data:HyperLinkField HeaderText="Role Id" DataNavigateUrlFormatString="RoleEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoleIdSource" DataTextField="Title" />
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Group Role Found! </b>
				<asp:HyperLink runat="server" ID="hypGroupRole" NavigateUrl="~/admin/GroupRoleEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:GroupRoleDataSource ID="GroupRoleDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:GroupRoleProperty Name="Role"/> 
					<data:GroupRoleProperty Name="UserGroup"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:GroupRoleFilter  Column="GroupId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:GroupRoleDataSource>		
		
		<br />
		

</asp:Content>

