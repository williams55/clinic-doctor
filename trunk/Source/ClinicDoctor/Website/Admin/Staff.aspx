
<%@ Page Language="C#"  MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"  CodeFile="Staff.aspx.cs" Inherits="Admin_Staff" Title="Staff List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Staff List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="StaffDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Staff.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True"  />				
				<asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="[FirstName]"  />
				<asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="[LastName]"  />
				<asp:BoundField DataField="ShortName" HeaderText="Short Name" SortExpression="[ShortName]"  />
				<data:HyperLinkField HeaderText="Group Id" DataNavigateUrlFormatString="GroupEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="GroupIdSource" DataTextField="Title" />
				<asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="[UserName]"  />
				<asp:BoundField DataField="Address" HeaderText="Address" SortExpression="[Address]"  />
				<asp:BoundField DataField="WorkPhone" HeaderText="Work Phone" SortExpression="[WorkPhone]"  />
				<asp:BoundField DataField="Birthdate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Birthdate" SortExpression="[Birthdate]"  />
				<data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]"  />
				<asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Create Date" SortExpression="[CreateDate]"  />
				<asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Update Date" SortExpression="[UpdateDate]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Staff Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnStaff" OnClientClick="javascript:location.href='StaffEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:StaffDataSource ID="StaffDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:StaffProperty Name="Group"/> 
					<%--<data:StaffProperty Name="NurseAppointmentCollection" />--%>
					<%--<data:StaffProperty Name="DoctorRoomCollection" />--%>
					<%--<data:StaffProperty Name="DoctorRosterCollection" />--%>
					<%--<data:StaffProperty Name="AppointmentCollection" />--%>
					<%--<data:StaffProperty Name="StaffRolesCollection" />--%>
					<%--<data:StaffProperty Name="DoctorFuncCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:StaffDataSource>
	    		
</asp:Content>



