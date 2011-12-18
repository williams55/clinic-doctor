<%@ Control Language="C#" ClassName="RosterTypeFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
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
                    <asp:Label ID="lbldataNote" runat="server" Text="Note:" AssociatedControlID="dataNote" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataNote" Text='<%# Bind("Note") %>' TextMode="MultiLine"
                        Width="250px" Rows="2" CssClass="text-input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataColorCode" runat="server" Text="Color Code:" AssociatedControlID="dataColorCode" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataColorCode" Text='<%# Bind("ColorCode") %>' MaxLength="10"
                        CssClass="Multiple" Width="230px" Height="25px"></asp:TextBox>
                  
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataIsBooked" runat="server" Text="Is Booked:" AssociatedControlID="dataIsBooked" />
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="dataIsBooked" SelectedValue='<%# Bind("IsBooked") %>'
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
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
        <asp:HiddenField runat="server" ID="HDcreatedate" Value='<%# Bind("CreateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="HDUpdateDate" Value='<%# Bind("UpdateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="hdCreateUser" Value='<%# Bind("CreateUser") %>' />
        <asp:HiddenField runat="server" ID="hdUpdateUser" Value='<%# Bind("UpdateUser") %>' />
    </ItemTemplate>
</asp:FormView>
