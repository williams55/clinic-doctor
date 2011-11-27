
<%@ Page Language="C#"  MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"  CodeFile="DoctorFunc.aspx.cs" Inherits="Admin_DoctorFunc" Title="DoctorFunc List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Doctor Func List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="DoctorFuncDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_DoctorFunc.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<data:HyperLinkField HeaderText="Doctor Id" DataNavigateUrlFormatString="StaffEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="DoctorIdSource" DataTextField="FirstName" />
				<data:HyperLinkField HeaderText="Func Id" DataNavigateUrlFormatString="FunctionalityEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="FuncIdSource" DataTextField="Title" />
				<data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]"  />
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]"  />
				<asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Create Date" SortExpression="[CreateDate]"  />
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]"  />
				<asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Update Date" SortExpression="[UpdateDate]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No DoctorFunc Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnDoctorFunc" OnClientClick="javascript:location.href='DoctorFuncEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:DoctorFuncDataSource ID="DoctorFuncDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:DoctorFuncProperty Name="Functionality"/> 
					<data:DoctorFuncProperty Name="Staff"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:DoctorFuncDataSource>
	    		
</asp:Content>



