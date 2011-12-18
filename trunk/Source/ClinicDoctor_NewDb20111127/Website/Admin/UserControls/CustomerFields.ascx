<%@ Control Language="C#" ClassName="CustomerFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <table border="0" width="100%" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataId" Text='<%# Bind("Id") %>' MaxLength="20" CssClass="text-input"
                        Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataId" runat="server"
                            Display="Dynamic" ControlToValidate="dataId" ErrorMessage="Required"></asp:RequiredFieldValidator>
               
               
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataFirstName" runat="server" Text="First Name:" AssociatedControlID="dataFirstName" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataFirstName" Text='<%# Bind("FirstName") %>' MaxLength="200"
                        CssClass="text-input" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqVal_dataFirstName" runat="server" Display="Dynamic"
                        ControlToValidate="dataFirstName" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
                <td class="literal">
                    <asp:Label ID="lbldataLastName" runat="server" Text="Last Name:" AssociatedControlID="dataLastName" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataLastName" Text='<%# Bind("LastName") %>' MaxLength="200"
                        CssClass="text-input" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                        ControlToValidate="dataLastName" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataIsFemale" runat="server" Text="Is Female:" AssociatedControlID="dataIsFemale" />
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="dataIsFemale" SelectedValue='<%# Bind("IsFemale") %>'
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="literal">
                    <asp:Label ID="lbldataBirthdate" runat="server" Text="Birthdate:" AssociatedControlID="dataBirthdate" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataBirthdate" Text='<%# Bind("Birthdate", "{0:d}") %>'
                        MaxLength="10" CssClass="datepicker text-input " Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataHomePhone" runat="server" Text="Home Phone:" AssociatedControlID="dataHomePhone" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataHomePhone" Text='<%# Bind("HomePhone") %>' MaxLength="20"
                        CssClass="text-input" Width="250px"></asp:TextBox>
                </td>
                <td class="literal">
                    <asp:Label ID="lbldataWorkPhone" runat="server" Text="Work Phone:" AssociatedControlID="dataWorkPhone" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataWorkPhone" Text='<%# Bind("WorkPhone") %>' MaxLength="20"
                        CssClass="text-input" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataCellPhone" runat="server" Text="Cell Phone:" AssociatedControlID="dataCellPhone" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataCellPhone" Text='<%# Bind("CellPhone") %>' MaxLength="20"
                        CssClass="text-input" Width="250px"></asp:TextBox>
                </td>
                <td class="literal">
                    <asp:Label ID="lbldataAddress" runat="server" Text="Address:" AssociatedControlID="dataAddress" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataAddress" Text='<%# Bind("Address") %>' TextMode="MultiLine"
                        Width="250px" Rows="2" CssClass="text-input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataTitle" runat="server" Text="Title:" AssociatedControlID="dataTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataTitle" Text='<%# Bind("Title") %>' MaxLength="10"
                        CssClass="text-input" Width="250px"></asp:TextBox>
                </td>
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
                    <asp:Label ID="lbldataIsDisabled" runat="server" Text="Is Disabled:" AssociatedControlID="dataIsDisabled" />
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="dataIsDisabled" SelectedValue='<%# Bind("IsDisabled") %>'
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HDcreatedate" Value='<%# Bind("CreateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="HDUpdateDate" Value='<%# Bind("UpdateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="hdCreateUser" Value='<%# Bind("CreateUser") %>' />
        <asp:HiddenField runat="server" ID="hdUpdateUser" Value='<%# Bind("UpdateUser") %>' />
    </ItemTemplate>
</asp:FormView>
