<%@ Control Language="C#" ClassName="RoomFuncFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataRoomId" runat="server" Text="Room Id:" AssociatedControlID="dataRoomId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="dataRoomId" DataSourceID="RoomIdRoomDataSource"
                        DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("RoomId") %>'
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        Width="250px" CssClass="text-input"  />
                    <data:RoomDataSource ID="RoomIdRoomDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataRoomTitle" runat="server" Text="Room Title:" AssociatedControlID="dataRoomTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataRoomTitle" Text='<%# Bind("RoomTitle") %>' MaxLength="200" CssClass="text-input" Width="240px" ReadOnly="true"></asp:TextBox><asp:RequiredFieldValidator
                        ID="ReqVal_dataRoomTitle" runat="server" Display="Dynamic" ControlToValidate="dataRoomTitle"
                        ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataFuncId" runat="server" Text="Func Id:" AssociatedControlID="dataFuncId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="dataFuncId" DataSourceID="FuncIdFunctionalityDataSource"
                        DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("FuncId") %>'
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        Width="250px" CssClass="text-input" />
                    <data:FunctionalityDataSource ID="FuncIdFunctionalityDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataFuncTitle" runat="server" Text="Func Title:" AssociatedControlID="dataFuncTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataFuncTitle" Text='<%# Bind("FuncTitle") %>' MaxLength="200" CssClass="text-input" Width="240px" ReadOnly="true"></asp:TextBox><asp:RequiredFieldValidator
                        ID="ReqVal_dataFuncTitle" runat="server" Display="Dynamic" ControlToValidate="dataFuncTitle"
                        ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
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
