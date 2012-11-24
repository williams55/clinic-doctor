<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ServicesEdit.aspx.cs" Inherits="ServicesEdit" Title="Services Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Services - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="ServicesDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ServicesFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ServicesFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Services not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ServicesDataSource ID="ServicesDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:ServicesDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewUsers1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewUsers1_SelectedIndexChanged"			 			 
			DataSourceID="UsersDataSource1"
			DataKeyNames="Username"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Users.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Password" HeaderText="Password" SortExpression="[Password]" />				
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]" />				
				<asp:BoundField DataField="Firstname" HeaderText="Firstname" SortExpression="[Firstname]" />				
				<asp:BoundField DataField="Lastname" HeaderText="Lastname" SortExpression="[Lastname]" />				
				<asp:BoundField DataField="DisplayName" HeaderText="Display Name" SortExpression="[DisplayName]" />				
				<asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" SortExpression="[CellPhone]" />				
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]" />				
				<asp:BoundField DataField="Avatar" HeaderText="Avatar" SortExpression="[Avatar]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<data:HyperLinkField HeaderText="User Group Id" DataNavigateUrlFormatString="UserGroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="UserGroupIdSource" DataTextField="Title" />
				<data:HyperLinkField HeaderText="Services Id" DataNavigateUrlFormatString="ServicesEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ServicesIdSource" DataTextField="Title" />
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
		
		<data:UsersDataSource ID="UsersDataSource1" runat="server" SelectMethod="Find"
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
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:UsersFilter  Column="ServicesId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:UsersDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewAppointment2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewAppointment2_SelectedIndexChanged"			 			 
			DataSourceID="AppointmentDataSource2"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Appointment.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="PatientCode" HeaderText="Patient Code" SortExpression="[PatientCode]" />				
				<data:HyperLinkField HeaderText="Username" DataNavigateUrlFormatString="UsersEdit.aspx?Username={0}" DataNavigateUrlFields="Username" DataContainer="UsernameSource" DataTextField="Password" />
				<data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
				<data:HyperLinkField HeaderText="Services Id" DataNavigateUrlFormatString="ServicesEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ServicesIdSource" DataTextField="Title" />
				<data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
				<data:HyperLinkField HeaderText="Appointment Group Id" DataNavigateUrlFormatString="AppointmentGroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="AppointmentGroupIdSource" DataTextField="Title" />
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="[StartTime]" />				
				<asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="[EndTime]" />				
				<data:HyperLinkField HeaderText="Roster Id" DataNavigateUrlFormatString="RosterEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RosterIdSource" DataTextField="StartTime" />
				<asp:BoundField DataField="IsComplete" HeaderText="Is Complete" SortExpression="[IsComplete]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Appointment Found! </b>
				<asp:HyperLink runat="server" ID="hypAppointment" NavigateUrl="~/admin/AppointmentEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:AppointmentDataSource ID="AppointmentDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:AppointmentProperty Name="AppointmentGroup"/> 
					<data:AppointmentProperty Name="Services"/> 
					<data:AppointmentProperty Name="Room"/> 
					<data:AppointmentProperty Name="Roster"/> 
					<data:AppointmentProperty Name="Status"/> 
					<data:AppointmentProperty Name="Users"/> 
					<%--<data:AppointmentProperty Name="AppointmentHistoryCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:AppointmentFilter  Column="ServicesId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:AppointmentDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewRoom3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewRoom3_SelectedIndexChanged"			 			 
			DataSourceID="RoomDataSource3"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Room.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<data:HyperLinkField HeaderText="Services Id" DataNavigateUrlFormatString="ServicesEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ServicesIdSource" DataTextField="Title" />
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Room Found! </b>
				<asp:HyperLink runat="server" ID="hypRoom" NavigateUrl="~/admin/RoomEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:RoomDataSource ID="RoomDataSource3" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:RoomProperty Name="Services"/> 
					<%--<data:RoomProperty Name="DoctorRoomCollection" />--%>
					<%--<data:RoomProperty Name="AppointmentCollection" />--%>
					<%--<data:RoomProperty Name="RosterCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:RoomFilter  Column="ServicesId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:RoomDataSource>		
		
		<br />
		

</asp:Content>

