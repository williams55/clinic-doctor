﻿<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="CustomerEdit.aspx.cs" Inherits="CustomerEdit" Title="Customer Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Customer - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" 
        DataSourceID="CustomerDataSource" onload="FormView1_Load">
        <EditItemTemplatePaths>
            <data:TemplatePath Path="~/Admin/UserControls/CustomerFields.ascx" />
        </EditItemTemplatePaths>
        <InsertItemTemplatePaths>
            <data:TemplatePath Path="~/Admin/UserControls/CustomerFields.ascx" />
        </InsertItemTemplatePaths>
        <EmptyDataTemplate>
            <b>Customer not found!</b>
        </EmptyDataTemplate>
        <FooterTemplate>
            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" CssClass="button"
                Text="Insert" />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" CssClass="button"
                Text="Update" />
            <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="button"
                Text="Cancel" />
        </FooterTemplate>
    </data:MultiFormView>
    <data:CustomerDataSource ID="CustomerDataSource" runat="server" SelectMethod="GetById">
        <Parameters>
            <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />
        </Parameters>
    </data:CustomerDataSource>
    <br />
    <data:EntityGridView ID="GridViewAppointment1" runat="server" AutoGenerateColumns="False"
        OnSelectedIndexChanged="GridViewAppointment1_SelectedIndexChanged" DataSourceID="AppointmentDataSource1"
        DataKeyNames="Id" AllowMultiColumnSorting="false" DefaultSortColumnName="" DefaultSortDirection="Ascending"
        ExcelExportFileName="Export_Appointment.xls" Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'>
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <data:HyperLinkField HeaderText="Customer Id" DataNavigateUrlFormatString="CustomerEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="CustomerIdSource" DataTextField="FirstName" />
            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="[CustomerName]" />
            <data:HyperLinkField HeaderText="Content Id" DataNavigateUrlFormatString="ContentEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="ContentIdSource" DataTextField="Title" />
            <asp:BoundField DataField="ContentTitle" HeaderText="Content Title" SortExpression="[ContentTitle]" />
            <data:HyperLinkField HeaderText="Doctor Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}"
                DataNavigateUrlFields="UserName" DataContainer="DoctorUsernameSource" DataTextField="FirstName" />
            <asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />
            <data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
            <asp:BoundField DataField="RoomTitle" HeaderText="Room Title" SortExpression="[RoomTitle]" />
            <data:HyperLinkField HeaderText="Nurse Username" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}"
                DataNavigateUrlFields="UserName" DataContainer="NurseUsernameSource" DataTextField="FirstName" />
            <asp:BoundField DataField="NurseShortName" HeaderText="Nurse Short Name" SortExpression="[NurseShortName]" />
            <data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
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
            <asp:HyperLink runat="server" ID="hypAppointment" NavigateUrl="~/admin/AppointmentEdit.aspx">Add 
            New</asp:HyperLink>
        </EmptyDataTemplate>
    </data:EntityGridView>
    <data:AppointmentDataSource ID="AppointmentDataSource1" runat="server" SelectMethod="Find"
        EnableDeepLoad="True">
        <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            <Types>
                <data:AppointmentProperty Name="Content" />
                <data:AppointmentProperty Name="Customer" />
                <data:AppointmentProperty Name="Room" />
                <data:AppointmentProperty Name="Staff" />
                <data:AppointmentProperty Name="Status" />
            </Types>
        </DeepLoadProperties>
        <Parameters>
            <data:SqlParameter Name="Parameters">
                <Filters>
                    <data:AppointmentFilter Column="CustomerId" QueryStringField="Id" />
                </Filters>
            </data:SqlParameter>
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
        </Parameters>
    </data:AppointmentDataSource>
    <br />
</asp:Content>