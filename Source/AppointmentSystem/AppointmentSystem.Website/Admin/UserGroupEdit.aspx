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
		<data:EntityGridView ID="GridViewUsers2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewUsers2_SelectedIndexChanged"			 			 
			DataSourceID="UsersDataSource2"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Users.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Username" HeaderText="Username" SortExpression="[Username]" />				
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]" />				
				<asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="[Firstname]" />				
				<asp:BoundField DataField="Lastname" HeaderText="Lastname" SortExpression="[Lastname]" />				
				<asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="[DisplayName]" />				
				<asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" SortExpression="[CellPhone]" />				
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]" />				
				<asp:BoundField DataField="Avatar" HeaderText="Avatar" SortExpression="[Avatar]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<data:HyperLinkField HeaderText="User Group Id" DataNavigateUrlFormatString="UserGroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="UserGroupIdSource" DataTextField="Title" />
				<asp:BoundField DataField="IsFemale" HeaderText="Is Female" SortExpression="[IsFemale]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Users Found! </b>
				<asp:HyperLink runat="server" ID="hypUsers" NavigateUrl="~/admin/UsersEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:UsersDataSource ID="UsersDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:UsersProperty Name="UserGroup"/> 
					<%--<data:UsersProperty Name="UserRoleCollection" />--%>
					<%--<data:UsersProperty Name="DoctorRoomCollection" />--%>
					<%--<data:UsersProperty Name="AppointmentCollection" />--%>
					<%--<data:UsersProperty Name="RosterCollection" />--%>
					<%--<data:UsersProperty Name="DoctorServiceCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:UsersFilter  Column="UserGroupId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:UsersDataSource>		
		
		<br />
		

</asp:Content>

