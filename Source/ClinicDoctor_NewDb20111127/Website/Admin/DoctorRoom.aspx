<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="DoctorRoom.aspx.cs" Inherits="Admin_DoctorRoom" Title="DoctorRoom List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Doctor Room List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1"
        PersistenceMethod="Session" />
    <br />
    <data:EntityGridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        DataSourceID="DoctorRoomDataSource" DataKeyNames="Id" AllowMultiColumnSorting="false"
        DefaultSortColumnName="" DefaultSortDirection="Ascending" 
        ExcelExportFileName="Export_DoctorRoom.xls" onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <data:HyperLinkField HeaderText="Doctor Id" DataNavigateUrlFormatString="StaffEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="DoctorIdSource" DataTextField="FirstName" />
            <data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
            <asp:BoundField DataField="PriorityIndex" HeaderText="Priority Index" SortExpression="[PriorityIndex]" />
            <data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />
            <asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />
            <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False"
                HeaderText="Create Date" SortExpression="[CreateDate]" />
            <asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />
            <asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False"
                HeaderText="Update Date" SortExpression="[UpdateDate]" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btn_Delete" runat="server" CommandName="CustomDelete" CommandArgument='<%#Eval("Id") %>'
                        ImageUrl="~/Admin/resources/images/icons/cross_circle.png" OnClientClick="return confirm('Are you sure want to delete?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <b>No DoctorRoom Found!</b>
        </EmptyDataTemplate>
    </data:EntityGridView>
    <br />
    <asp:Button runat="server" ID="btnDoctorRoom" OnClientClick="javascript:location.href='DoctorRoomEdit.aspx'; return false;"
        Text="Add New"></asp:Button>
    <data:DoctorRoomDataSource ID="DoctorRoomDataSource" runat="server" SelectMethod="GetPaged"
        EnablePaging="True" EnableSorting="True" EnableDeepLoad="True">
        <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            <Types>
                <data:DoctorRoomProperty Name="Room" />
                <data:DoctorRoomProperty Name="Staff" />
            </Types>
        </DeepLoadProperties>
        <Parameters>
            <data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
            <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex"
                Type="Int32" />
            <asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize"
                Type="Int32" />
            <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
        </Parameters>
    </data:DoctorRoomDataSource>
</asp:Content>
