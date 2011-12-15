﻿<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="StaffEdit.aspx.cs" Inherits="StaffEdit" Title="Staff Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Staff - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="StaffDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/StaffFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/StaffFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Staff not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:StaffDataSource ID="StaffDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:StaffDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewDoctorRoom1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewDoctorRoom1_SelectedIndexChanged"			 			 
			DataSourceID="DoctorRoomDataSource1"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_DoctorRoom.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Doctor User Name" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="DoctorUserNameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />				
				<data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
				<asp:BoundField DataField="RoomTitle" HeaderText="Room Title" SortExpression="[RoomTitle]" />				
				<asp:BoundField DataField="PriorityIndex" HeaderText="Priority Index" SortExpression="[PriorityIndex]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Doctor Room Found! </b>
				<asp:HyperLink runat="server" ID="hypDoctorRoom" NavigateUrl="~/admin/DoctorRoomEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:DoctorRoomDataSource ID="DoctorRoomDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:DoctorRoomProperty Name="Room"/> 
					<data:DoctorRoomProperty Name="Staff"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:DoctorRoomFilter  Column="DoctorUserName" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:DoctorRoomDataSource>		
		
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
				<data:HyperLinkField HeaderText="Customer Id" DataNavigateUrlFormatString="CustomerEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="CustomerIdSource" DataTextField="FirstName" />
				<asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="[CustomerName]" />				
				<data:HyperLinkField HeaderText="Content Id" DataNavigateUrlFormatString="ContentEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ContentIdSource" DataTextField="Title" />
				<asp:BoundField DataField="ContentTitle" HeaderText="Content Title" SortExpression="[ContentTitle]" />				
				<data:HyperLinkField HeaderText="Doctor Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="DoctorUsernameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />				
				<data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
				<asp:BoundField DataField="RoomTitle" HeaderText="Room Title" SortExpression="[RoomTitle]" />				
				<data:HyperLinkField HeaderText="Nurse Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="NurseUsernameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="NurseShortName" HeaderText="Nurse Short Name" SortExpression="[NurseShortName]" />				
				<data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
				<asp:BoundField DataField="StatusTitle" HeaderText="Status Title" SortExpression="[StatusTitle]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="[StartTime]" />				
				<asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="[EndTime]" />				
				<asp:BoundField DataField="ColorCode" HeaderText="Color Code" SortExpression="[ColorCode]" />				
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
					<data:AppointmentProperty Name="Content"/> 
					<data:AppointmentProperty Name="Customer"/> 
					<data:AppointmentProperty Name="Room"/> 
					<data:AppointmentProperty Name="Staff"/> 
					<data:AppointmentProperty Name="Status"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:AppointmentFilter  Column="NurseUsername" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:AppointmentDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewAppointment3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewAppointment3_SelectedIndexChanged"			 			 
			DataSourceID="AppointmentDataSource3"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Appointment.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Customer Id" DataNavigateUrlFormatString="CustomerEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="CustomerIdSource" DataTextField="FirstName" />
				<asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="[CustomerName]" />				
				<data:HyperLinkField HeaderText="Content Id" DataNavigateUrlFormatString="ContentEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ContentIdSource" DataTextField="Title" />
				<asp:BoundField DataField="ContentTitle" HeaderText="Content Title" SortExpression="[ContentTitle]" />				
				<data:HyperLinkField HeaderText="Doctor Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="DoctorUsernameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />				
				<data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
				<asp:BoundField DataField="RoomTitle" HeaderText="Room Title" SortExpression="[RoomTitle]" />				
				<data:HyperLinkField HeaderText="Nurse Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="NurseUsernameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="NurseShortName" HeaderText="Nurse Short Name" SortExpression="[NurseShortName]" />				
				<data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
				<asp:BoundField DataField="StatusTitle" HeaderText="Status Title" SortExpression="[StatusTitle]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="[StartTime]" />				
				<asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="[EndTime]" />				
				<asp:BoundField DataField="ColorCode" HeaderText="Color Code" SortExpression="[ColorCode]" />				
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
		
		<data:AppointmentDataSource ID="AppointmentDataSource3" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:AppointmentProperty Name="Content"/> 
					<data:AppointmentProperty Name="Customer"/> 
					<data:AppointmentProperty Name="Room"/> 
					<data:AppointmentProperty Name="Staff"/> 
					<data:AppointmentProperty Name="Status"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:AppointmentFilter  Column="DoctorUsername" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:AppointmentDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewDoctorRoster4" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewDoctorRoster4_SelectedIndexChanged"			 			 
			DataSourceID="DoctorRosterDataSource4"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_DoctorRoster.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Doctor User Name" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="DoctorUserNameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />				
				<data:HyperLinkField HeaderText="Roster Type Id" DataNavigateUrlFormatString="RosterTypeEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RosterTypeIdSource" DataTextField="Title" />
				<asp:BoundField DataField="RosterTypeTitle" HeaderText="Roster Type Title" SortExpression="[RosterTypeTitle]" />				
				<asp:BoundField DataField="ColorCode" HeaderText="Color Code" SortExpression="[ColorCode]" />				
				<asp:BoundField DataField="IsBooked" HeaderText="Is Booked" SortExpression="[IsBooked]" />				
				<asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="[StartTime]" />				
				<asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="[EndTime]" />				
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<asp:BoundField DataField="IsComplete" HeaderText="Is Complete" SortExpression="[IsComplete]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Doctor Roster Found! </b>
				<asp:HyperLink runat="server" ID="hypDoctorRoster" NavigateUrl="~/admin/DoctorRosterEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:DoctorRosterDataSource ID="DoctorRosterDataSource4" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:DoctorRosterProperty Name="RosterType"/> 
					<data:DoctorRosterProperty Name="Staff"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:DoctorRosterFilter  Column="DoctorUserName" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:DoctorRosterDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewDoctorFunc5" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewDoctorFunc5_SelectedIndexChanged"			 			 
			DataSourceID="DoctorFuncDataSource5"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_DoctorFunc.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Doctor User Name" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="DoctorUserNameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />				
				<data:HyperLinkField HeaderText="Func Id" DataNavigateUrlFormatString="FunctionalityEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="FuncIdSource" DataTextField="Title" />
				<asp:BoundField DataField="FuncTitle" HeaderText="Func Title" SortExpression="[FuncTitle]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Doctor Func Found! </b>
				<asp:HyperLink runat="server" ID="hypDoctorFunc" NavigateUrl="~/admin/DoctorFuncEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:DoctorFuncDataSource ID="DoctorFuncDataSource5" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:DoctorFuncProperty Name="Functionality"/> 
					<data:DoctorFuncProperty Name="Staff"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:DoctorFuncFilter  Column="DoctorUserName" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:DoctorFuncDataSource>		
		
		<br />
		

</asp:Content>
