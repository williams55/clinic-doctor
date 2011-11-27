<%@ Control Language="C#" ClassName="RosterTypeFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataTitle" runat="server" Text="Title:" AssociatedControlID="dataTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataTitle" Text='<%# Bind("Title") %>' MaxLength="100" CssClass="text-input"></asp:TextBox>
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
                        <asp:ListItem Value="True" Text="Yes" ></asp:ListItem>
                        <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                       
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataIsBooked" runat="server" Text="Is Booked:" AssociatedControlID="dataIsBooked" />
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="dataIsBooked" SelectedValue='<%# Bind("IsBooked") %>'
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Text="Yes" ></asp:ListItem>
                        <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                     
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
         <asp:HiddenField runat="server" ID="HDcreatedate" Value='<%# Bind("CreateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="HDUpdateDate" Value='<%# Bind("UpdateDate", "{0:d}") %>' />
    </ItemTemplate>
</asp:FormView>
