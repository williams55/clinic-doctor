
<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Appointment.aspx.cs" Inherits="Admin_Appointment" Title="Appointment List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Appointment List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="AppointmentDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Appointment.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="[Id]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Customer Id" DataNavigateUrlFormatString="CustomerEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="CustomerIdSource" DataTextField="FirstName" />
				<asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="[CustomerName]"  />
				<data:HyperLinkField HeaderText="Content Id" DataNavigateUrlFormatString="ContentEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="ContentIdSource" DataTextField="Title" />
				<asp:BoundField DataField="ContentTitle" HeaderText="Content Title" SortExpression="[ContentTitle]"  />
				<data:HyperLinkField HeaderText="Doctor Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="DoctorUsernameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]"  />
				<asp:BoundField DataField="DoctorEmail" HeaderText="Doctor Email" SortExpression="[DoctorEmail]"  />
				<data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
				<asp:BoundField DataField="RoomTitle" HeaderText="Room Title" SortExpression="[RoomTitle]"  />
				<data:HyperLinkField HeaderText="Nurse Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}" DataNavigateUrlFields="UserName" DataContainer="NurseUsernameSource" DataTextField="FirstName" />
				<asp:BoundField DataField="NurseShortName" HeaderText="Nurse Short Name" SortExpression="[NurseShortName]"  />
				<data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
				<asp:BoundField DataField="StatusTitle" HeaderText="Status Title" SortExpression="[StatusTitle]"  />
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]"  />
				<asp:BoundField DataField="StartTime" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Start Time" SortExpression="[StartTime]"  />
				<asp:BoundField DataField="EndTime" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="End Time" SortExpression="[EndTime]"  />
				<asp:BoundField DataField="ColorCode" HeaderText="Color Code" SortExpression="[ColorCode]"  />
				<data:BoundRadioButtonField DataField="IsComplete" HeaderText="Is Complete" SortExpression="[IsComplete]"  />
				<data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]"  />
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]"  />
				<asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Create Date" SortExpression="[CreateDate]"  />
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]"  />
				<asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Update Date" SortExpression="[UpdateDate]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Appointment Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnAppointment" OnClientClick="javascript:location.href='AppointmentEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:AppointmentDataSource ID="AppointmentDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
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
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:AppointmentDataSource>
	    		
</asp:Content>



