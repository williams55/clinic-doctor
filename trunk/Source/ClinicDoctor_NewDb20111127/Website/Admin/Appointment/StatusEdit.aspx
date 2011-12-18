﻿<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="StatusEdit.aspx.cs" Inherits="StatusEdit" Title="Status Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Status - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="StatusDataSource"
        OnLoad="FormView1_Load">
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
            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert" />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" />
            <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" />
        </FooterTemplate>
    </data:MultiFormView>
    <data:StatusDataSource ID="StatusDataSource" runat="server" SelectMethod="GetById">
        <Parameters>
            <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />
        </Parameters>
    </data:StatusDataSource>
    <br />
    <data:EntityGridView ID="GridViewAppointment1" runat="server" AutoGenerateColumns="False"
        OnSelectedIndexChanged="GridViewAppointment1_SelectedIndexChanged" DataSourceID="AppointmentDataSource1"
        DataKeyNames="Id" AllowMultiColumnSorting="false" DefaultSortColumnName="" DefaultSortDirection="Ascending"
        ExcelExportFileName="Export_Appointment.xls" Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'>
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <data:HyperLinkField HeaderText="Customer Id" DataNavigateUrlFormatString="CustomerEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="CustomerIdSource" DataTextField="FirstName" />
            <data:HyperLinkField HeaderText="Content Id" DataNavigateUrlFormatString="ContentEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="ContentIdSource" DataTextField="Title" />
            <data:HyperLinkField HeaderText="Doctor Id" DataNavigateUrlFormatString="StaffEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="DoctorIdSource" DataTextField="FirstName" />
            <data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
            <data:HyperLinkField HeaderText="Nurse Id" DataNavigateUrlFormatString="StaffEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="NurseIdSource" DataTextField="FirstName" />
            <data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />
            <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="[StartTime]" />
            <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="[EndTime]" />
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
                    <data:AppointmentFilter Column="StatusId" QueryStringField="Id" />
                </Filters>
            </data:SqlParameter>
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
        </Parameters>
    </data:AppointmentDataSource>
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:TextBox ID="color123" runat="server"></asp:TextBox>
        <ajaxToolkit:ColorPickerExtender ID="color12" runat="server" TargetControlID="color123">
        </ajaxToolkit:ColorPickerExtender>
    </div>
</asp:Content>
