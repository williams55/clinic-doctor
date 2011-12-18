<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="RosterTypeEdit.aspx.cs" Inherits="RosterTypeEdit" Title="RosterType Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Roster Type - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" 
        DataSourceID="RosterTypeDataSource" onload="FormView1_Load">
        <EditItemTemplatePaths>
            <data:TemplatePath Path="~/Admin/UserControls/RosterTypeFields.ascx" />
        </EditItemTemplatePaths>
        <InsertItemTemplatePaths>
            <data:TemplatePath Path="~/Admin/UserControls/RosterTypeFields.ascx" />
        </InsertItemTemplatePaths>
        <EmptyDataTemplate>
            <b>RosterType not found!</b>
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
    <data:RosterTypeDataSource ID="RosterTypeDataSource" runat="server" SelectMethod="GetById">
        <Parameters>
            <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />
        </Parameters>
    </data:RosterTypeDataSource>
    <br />
    <data:EntityGridView ID="GridViewDoctorRoster1" runat="server" AutoGenerateColumns="False"
        OnSelectedIndexChanged="GridViewDoctorRoster1_SelectedIndexChanged" DataSourceID="DoctorRosterDataSource1"
        DataKeyNames="Id" AllowMultiColumnSorting="false" DefaultSortColumnName="" DefaultSortDirection="Ascending"
        ExcelExportFileName="Export_DoctorRoster.xls" Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'>
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <data:HyperLinkField HeaderText="Doctor User Name" DataNavigateUrlFormatString="StaffEdit.aspx?UserName={0}"
                DataNavigateUrlFields="UserName" DataContainer="DoctorUserNameSource" DataTextField="FirstName" />
            <asp:BoundField DataField="DoctorShortName" HeaderText="Doctor Short Name" SortExpression="[DoctorShortName]" />
            <data:HyperLinkField HeaderText="Roster Type Id" DataNavigateUrlFormatString="RosterTypeEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="RosterTypeIdSource" DataTextField="Title" />
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
            <asp:HyperLink runat="server" ID="hypDoctorRoster" NavigateUrl="~/admin/DoctorRosterEdit.aspx">Add 
            New</asp:HyperLink>
        </EmptyDataTemplate>
    </data:EntityGridView>
    <data:DoctorRosterDataSource ID="DoctorRosterDataSource1" runat="server" SelectMethod="Find"
        EnableDeepLoad="True">
        <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            <Types>
                <data:DoctorRosterProperty Name="RosterType" />
                <data:DoctorRosterProperty Name="Staff" />
            </Types>
        </DeepLoadProperties>
        <Parameters>
            <data:SqlParameter Name="Parameters">
                <Filters>
                    <data:DoctorRosterFilter Column="RosterTypeId" QueryStringField="Id" />
                </Filters>
            </data:SqlParameter>
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
        </Parameters>
    </data:DoctorRosterDataSource>
    <br />
 
</asp:Content>
