<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="Room.aspx.cs" Inherits="Admin_Room" Title="Room List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Room List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1"
        PersistenceMethod="Session" />
    <br />
    <data:EntityGridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        DataSourceID="RoomDataSource" DataKeyNames="Id" AllowMultiColumnSorting="false"
        DefaultSortColumnName="" DefaultSortDirection="Ascending" ExcelExportFileName="Export_Room.xls">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="[Title]" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="[Note]" />
            <data:HyperLinkField HeaderText="Status Id" DataNavigateUrlFormatString="StatusEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="StatusIdSource" DataTextField="Title" />
            <asp:BoundField DataField="CreateUser" HeaderText="Create User" SortExpression="[CreateUser]" />
            <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HtmlEncode="False"
                HeaderText="Create Date" SortExpression="[CreateDate]" />
            <asp:BoundField DataField="UpdateUser" HeaderText="Update User" SortExpression="[UpdateUser]" />
            <asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HtmlEncode="False"
                HeaderText="Update Date" SortExpression="[UpdateDate]" />
            <data:BoundRadioButtonField DataField="IsDisabled" HeaderText="Is Disabled" SortExpression="[IsDisabled]" />
        </Columns>
        <EmptyDataTemplate>
            <b>No Room Found!</b>
        </EmptyDataTemplate>
    </data:EntityGridView>
    <br />
    <asp:Button runat="server" ID="btnRoom" OnClientClick="javascript:location.href='RoomEdit.aspx'; return false;"
        Text="Add New"></asp:Button>
    <data:RoomDataSource ID="RoomDataSource" runat="server" SelectMethod="GetPaged" EnablePaging="True"
        EnableSorting="True" EnableDeepLoad="True">
        <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            <Types>
                <data:RoomProperty Name="Status" />
                <%--<data:RoomProperty Name="DoctorRoomCollection" />--%>
                <%--<data:RoomProperty Name="AppointmentCollection" />--%>
                <%--<data:RoomProperty Name="RoomFuncCollection" />--%>
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
    </data:RoomDataSource>
</asp:Content>
