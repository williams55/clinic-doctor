﻿<%@ Control Language="C#" ClassName="ContentFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>

        <script src='<%=ResolveUrl("../myscript/GetFunc.js")%>' type="text/javascript"></script>

        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataTitle" runat="server" Text="Title:" AssociatedControlID="dataTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataTitle" Text='<%# Bind("Title") %>' MaxLength="200"
                        CssClass="text-input" Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTitle"
                            runat="server" Display="Dynamic" ControlToValidate="dataTitle" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataFuncId" runat="server" Text="Func Id:" AssociatedControlID="cbFuncId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="cbFuncId" DataSourceID="FuncIdFunctionalityDataSource"
                        DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("FuncId") %>'
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        CssClass="text-input" Width="250px" onchange="GetFunc();" />
                    <data:FunctionalityDataSource ID="FuncIdFunctionalityDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
          
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataColorCode" runat="server" Text="Color Code:" AssociatedControlID="dataColorCode" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataColorCode" Text='<%# Bind("ColorCode") %>' MaxLength="10"
                        CssClass="Multiple" Width="230px" Height="25px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqVal_dataColorCode" runat="server" Display="Dynamic"
                        ControlToValidate="dataColorCode" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataNote" runat="server" Text="Note:" AssociatedControlID="dataNote" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataNote" Text='<%# Bind("Note") %>' TextMode="MultiLine"
                        Width="250px" Rows="5" CssClass="text-input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataIsDisabled" runat="server" Text="Is Disabled:" AssociatedControlID="dataIsDisabled" />
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="dataIsDisabled" SelectedValue='<%# Bind("IsDisabled") %>'
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hdFuncTitle" Value='<%# Bind("FuncTitle") %>' />
        <asp:HiddenField runat="server" ID="HDcreatedate" Value='<%# Bind("CreateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="HDUpdateDate" Value='<%# Bind("UpdateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="hdCreateUser" Value='<%# Bind("CreateUser") %>' />
        <asp:HiddenField runat="server" ID="hdUpdateUser" Value='<%# Bind("UpdateUser") %>' />
    </ItemTemplate>
</asp:FormView>
