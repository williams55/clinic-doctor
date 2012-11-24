
<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Users.aspx.cs" Inherits="Users" Title="Users List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Users List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="UsersDataSource"
				DataKeyNames="Username"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Users.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Password" HeaderText="Password" SortExpression="[Password]"  />
				<data:HyperLinkField HeaderText="Services Id" DataNavigateUrlFormatString="ServicesEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ServicesIdSource" DataTextField="Title" />
				<asp:BoundField DataField="Username" HeaderText="Username" SortExpression="[Username]" ReadOnly="True" />
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]"  />
				<asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="[Firstname]"  />
				<asp:BoundField DataField="Lastname" HeaderText="Lastname" SortExpression="[Lastname]"  />
				<asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="[DisplayName]"  />
				<asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" SortExpression="[CellPhone]"  />
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]"  />
				<asp:BoundField DataField="Avatar" HeaderText="Avatar" SortExpression="[Avatar]"  />
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]"  />
				<data:HyperLinkField HeaderText="User Group Id" DataNavigateUrlFormatString="UserGroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="UserGroupIdSource" DataTextField="Title" />
				<data:BoundRadioButtonField DataField="IsFemale" HeaderText="Is Female" SortExpression="[IsFemale]"  />
				<data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]"  />
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]"  />
				<asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Create Date" SortExpression="[CreateDate]"  />
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]"  />
				<asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Update Date" SortExpression="[UpdateDate]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Users Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnUsers" OnClientClick="javascript:location.href='UsersEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:UsersDataSource ID="UsersDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:UsersProperty Name="Services"/> 
					<data:UsersProperty Name="UserGroup"/> 
					<%--<data:UsersProperty Name="UserRoleCollection" />--%>
					<%--<data:UsersProperty Name="DoctorRoomCollection" />--%>
					<%--<data:UsersProperty Name="AppointmentCollection" />--%>
					<%--<data:UsersProperty Name="RosterCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:UsersDataSource>
	    		
</asp:Content>



