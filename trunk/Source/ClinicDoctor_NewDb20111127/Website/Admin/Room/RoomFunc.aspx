<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="RoomFunc.aspx.cs" Inherits="Admin_RoomFunc" Title="RoomFunc List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Room Func List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1"
        PersistenceMethod="Session" />
    <br />
    <data:EntityGridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        DataSourceID="RoomFuncDataSource" DataKeyNames="Id" AllowMultiColumnSorting="false"
        DefaultSortColumnName="" DefaultSortDirection="Ascending" 
        ExcelExportFileName="Export_RoomFunc.xls" onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <data:HyperLinkField HeaderText="Room Id" DataNavigateUrlFormatString="RoomEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="RoomIdSource" DataTextField="Title" />
            <data:HyperLinkField HeaderText="Func Id" DataNavigateUrlFormatString="FunctionalityEdit.aspx?Id={0}"
                DataNavigateUrlFields="Id" DataContainer="FuncIdSource" DataTextField="Title" />
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
            <b>No RoomFunc Found!</b>
        </EmptyDataTemplate>
    </data:EntityGridView>
    <br />
    <asp:Button runat="server" ID="btnRoomFunc" CssClass="button" OnClientClick="javascript:location.href='RoomFuncEdit.aspx'; return false;"
        Text="Add New"></asp:Button>
    <data:RoomFuncDataSource ID="RoomFuncDataSource" runat="server" SelectMethod="GetPaged"
        EnablePaging="True" EnableSorting="True" EnableDeepLoad="True">
        <DeepLoadProperties Method="IncludeChildren" Recursive="False">
            <Types>
                <data:RoomFuncProperty Name="Functionality" />
                <data:RoomFuncProperty Name="Room" />
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
    </data:RoomFuncDataSource>
</asp:Content>
