<%@ Control Language="C#" ClassName="DoctorRoomFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataDoctorId" runat="server" Text="Doctor Id:" AssociatedControlID="dataDoctorId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="dataDoctorId" DataSourceID="DoctorIdStaffDataSource"
                        DataTextField="FirstName" DataValueField="Id" SelectedValue='<%# Bind("DoctorId") %>'
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        Width="250px" />
                    <data:StaffDataSource ID="DoctorIdStaffDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataRoomId" runat="server" Text="Room Id:" AssociatedControlID="dataRoomId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="dataRoomId" DataSourceID="RoomIdRoomDataSource"
                        DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("RoomId") %>'
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        Width="250px" />
                    <data:RoomDataSource ID="RoomIdRoomDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataPriorityIndex" runat="server" Text="Priority Index:" AssociatedControlID="dataPriorityIndex" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataPriorityIndex" Text='<%# Bind("PriorityIndex") %>'
                        CssClass="text-input" Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPriorityIndex"
                            runat="server" Display="Dynamic" ControlToValidate="dataPriorityIndex" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="RangeVal_dataPriorityIndex" runat="server" Display="Dynamic" ControlToValidate="dataPriorityIndex"
                                ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648"
                                Type="Integer"></asp:RangeValidator>
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
