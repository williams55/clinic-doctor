<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="StatusEdit.aspx.cs" Inherits="StatusEdit" Title="Status Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Status - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="StatusDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/StatusFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/StatusFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Status not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:StatusDataSource ID="StatusDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:StatusDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewAppointment1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewAppointment1_SelectedIndexChanged"			 			 
			DataSourceID="AppointmentDataSource1"
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
				<data:HyperLinkField HeaderText="Username" DataNavigateUrlFormatString="UsersEdit.aspx?Username={0}" DataNavigateUrlFields="Username" DataContainer="UsernameSource" DataTextField="Title" />
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
		
		<data:AppointmentDataSource ID="AppointmentDataSource1" runat="server" SelectMethod="Find"
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
						<data:AppointmentFilter  Column="StatusId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:AppointmentDataSource>		
		
		<br />
		

</asp:Content>

