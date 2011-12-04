<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"  CodeFile="GroupEdit.aspx.cs" Inherits="GroupEdit" Title="Group Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    Group - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" 
            DataSourceID="GroupDataSource" onload="FormView1_Load">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/GroupFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/GroupFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Group not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:GroupDataSource ID="GroupDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:GroupDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewStaff1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewStaff1_SelectedIndexChanged"			 			 
			DataSourceID="StaffDataSource1"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Staff.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="[FirstName]" />				
				<asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="[LastName]" />				
				<asp:BoundField DataField="ShortName" HeaderText="Short Name" SortExpression="[ShortName]" />				
				<data:HyperLinkField HeaderText="Group Id" DataNavigateUrlFormatString="GroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="GroupIdSource" DataTextField="Title" />
				<asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="[UserName]" />				
				<asp:BoundField DataField="Address" HeaderText="Address" SortExpression="[Address]" />				
				<asp:BoundField DataField="HomePhone" HeaderText="Home Phone" SortExpression="[HomePhone]" />				
				<asp:BoundField DataField="WorkPhone" HeaderText="Work Phone" SortExpression="[WorkPhone]" />				
				<asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" SortExpression="[CellPhone]" />				
				<asp:BoundField DataField="Birthdate" HeaderText="Birthdate" SortExpression="[Birthdate]" />				
				<asp:BoundField DataField="IsFemale" HeaderText="Is Female" SortExpression="[IsFemale]" />				
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<asp:BoundField DataField="Roles" HeaderText="Roles" SortExpression="[Roles]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Staff Found! </b>
				<asp:HyperLink runat="server" ID="hypStaff" NavigateUrl="~/admin/StaffEdit.aspx">Add 
                New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:StaffDataSource ID="StaffDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:StaffProperty Name="Group"/> 
					<%--<data:StaffProperty Name="DoctorRoomCollection" />--%>
					<%--<data:StaffProperty Name="AppointmentCollectionGetByNurseId" />--%>
					<%--<data:StaffProperty Name="AppointmentCollectionGetByDoctorId" />--%>
					<%--<data:StaffProperty Name="DoctorRosterCollection" />--%>
					<%--<data:StaffProperty Name="DoctorFuncCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:StaffFilter  Column="GroupId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:StaffDataSource>		
		
		<br />
		

</asp:Content>

