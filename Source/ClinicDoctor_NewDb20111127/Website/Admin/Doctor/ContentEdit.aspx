<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="ContentEdit.aspx.cs" Inherits="ContentEdit" Title="Content Edit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    Content - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="3" cellspacing="1">
        <tr>
            <td class="literal">
                <asp:Label ID="lbldataTitle" runat="server" Text="Title:" AssociatedControlID="dataTitle" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="dataTitle" MaxLength="200" Width="250px" CssClass="text-input"></asp:TextBox><asp:RequiredFieldValidator
                    ID="ReqVal_dataTitle" runat="server" Display="Dynamic" ControlToValidate="dataTitle"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="literal">
                <asp:Label ID="lbldataFuncId" runat="server" Text="Func Id:"  />
            </td>
            <td>
            
               <telerik:RadComboBox ID="RaddataFuncId" runat="server" Height="180px" Width="270px"
                    DropDownWidth="310px" EmptyMessage="Select Patient" HighlightTemplatedItems="true"
                    MarkFirstMatch="true" EnableLoadOnDemand="true" OnItemsRequested="RaddataFuncId_ItemsRequested">
                    <HeaderTemplate>
                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 50px;">
                                    Function ID
                                </td>
                                <td style="width: 250px;">
                                    Function Title
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 50px;">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td style="width: 250px;">
                                    <%# DataBinder.Eval(Container, "Attributes['FuncTitle']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <%-- <data:EntityDropDownList runat="server" ID="dataFuncId" DataSourceID="FuncIdFunctionalityDataSource"
                        DataTextField="Title" DataValueField="Id" 
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        Width="250px" onselectedindexchanged="dataFuncId_SelectedIndexChanged"  />
                    <data:FunctionalityDataSource ID="FuncIdFunctionalityDataSource" runat="server" SelectMethod="GetAll" />--%>
            </td>
        </tr>
        <tr>
            <td class="literal">
                <asp:Label ID="lbldataFuncTitle" runat="server" Text="Func Title:" AssociatedControlID="dataFuncTitle" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="dataFuncTitle" MaxLength="200" CssClass="text-input"
                    Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="literal">
                <asp:Label ID="lbldataNote" runat="server" Text="Note:" AssociatedControlID="dataNote" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="dataNote" TextMode="MultiLine" Width="250px" Rows="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="literal">
                <asp:Label ID="lbldataIsDisabled" runat="server" Text="Is Disabled:" AssociatedControlID="dataIsDisabled" />
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="dataIsDisabled" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="HDcreatedate" />
    <asp:HiddenField runat="server" ID="HDUpdateDate" />
    <asp:HiddenField runat="server" ID="hdCreateUser" />
    <asp:HiddenField runat="server" ID="hdUpdateUser" />
    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
        Text="Insert" />
    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
        Text="Update" />
    <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
        Text="Cancel" />
    <br />
</asp:Content>
