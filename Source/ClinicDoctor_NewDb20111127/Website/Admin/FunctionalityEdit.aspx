<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"  CodeFile="FunctionalityEdit.aspx.cs" Inherits="FunctionalityEdit" Title="Functionality Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Functionality - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="FunctionalityDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/FunctionalityFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/FunctionalityFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Functionality not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:FunctionalityDataSource ID="FunctionalityDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:FunctionalityDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewDoctorFunc1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewDoctorFunc1_SelectedIndexChanged"			 			 
			DataSourceID="DoctorFuncDataSource1"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_DoctorFunc.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Doctor Id" DataNavigateUrlFormatString="StaffEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="DoctorIdSource" DataTextField="FirstName" />
				<data:HyperLinkField HeaderText="Func Id" DataNavigateUrlFormatString="FunctionalityEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="FuncIdSource" DataTextField="Title" />
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
		
		<data:DoctorFuncDataSource ID="DoctorFuncDataSource1" runat="server" SelectMethod="Find"
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
						<data:DoctorFuncFilter  Column="FuncId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:DoctorFuncDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewContent2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewContent2_SelectedIndexChanged"			 			 
			DataSourceID="ContentDataSource2"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Content.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]" />				
				<data:HyperLinkField HeaderText="Func Id" DataNavigateUrlFormatString="FunctionalityEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="FuncIdSource" DataTextField="Title" />
				<asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />				
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Content Found! </b>
				<asp:HyperLink runat="server" ID="hypContent" NavigateUrl="~/admin/ContentEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ContentDataSource ID="ContentDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ContentProperty Name="Functionality"/> 
					<%--<data:ContentProperty Name="AppointmentCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ContentFilter  Column="FuncId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ContentDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewRoomFunc3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewRoomFunc3_SelectedIndexChanged"			 			 
			DataSourceID="RoomFuncDataSource3"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_RoomFunc.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
				<data:HyperLinkField HeaderText="Func Id" DataNavigateUrlFormatString="FunctionalityEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="FuncIdSource" DataTextField="Title" />
				<asp:BoundField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />				
				<asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />				
				<asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="[CreateDate]" />				
				<asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />				
				<asp:BoundField DataField="UpdateDate" HeaderText="Update Date" SortExpression="[UpdateDate]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Room Func Found! </b>
				<asp:HyperLink runat="server" ID="hypRoomFunc" NavigateUrl="~/admin/RoomFuncEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:RoomFuncDataSource ID="RoomFuncDataSource3" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:RoomFuncProperty Name="Functionality"/> 
					<data:RoomFuncProperty Name="Room"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:RoomFuncFilter  Column="FuncId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:RoomFuncDataSource>		
		
		<br />
		

</asp:Content>

