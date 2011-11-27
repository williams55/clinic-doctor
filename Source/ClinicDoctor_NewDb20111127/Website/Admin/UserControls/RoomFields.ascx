<%@ Control Language="C#" ClassName="RoomFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataTitle" runat="server" Text="Title:" AssociatedControlID="dataTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataTitle" Text='<%# Bind("Title") %>' MaxLength="200"
                        CssClass="text-input"></asp:TextBox>
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
                    <asp:Label ID="lbldataStatusId" runat="server" Text="Status Id:" AssociatedControlID="dataStatusId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="dataStatusId" DataSourceID="StatusIdStatusDataSource"
                        DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("StatusId") %>'
                        AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
                    <data:StatusDataSource ID="StatusIdStatusDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataCreateUser" runat="server" Text="Create User:" AssociatedControlID="dataCreateUser" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataCreateUser" Text='<%# Bind("CreateUser") %>'
                        MaxLength="200" CssClass="text-input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataUpdateUser" runat="server" Text="Update User:" AssociatedControlID="dataUpdateUser" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataUpdateUser" Text='<%# Bind("UpdateUser") %>'
                        MaxLength="200" CssClass="text-input"></asp:TextBox>
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
    </ItemTemplate>
</asp:FormView>
