<%@ Control Language="C#" ClassName="DoctorRoomFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>

        <script src='<%=ResolveUrl("../myscript/GetDoctor.js") %>' type="text/javascript"></script>

        <script src='<%=ResolveUrl("../myscript/GetRoom.js") %>' type="text/javascript"></script>

        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataDoctorUserName" runat="server" Text="Doctor User Name:" AssociatedControlID="cboDoctorUserName" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="cboDoctorUserName" DataSourceID="DoctorUserNameStaffDataSource"
                        DataTextField="UserName" DataValueField="UserName" SelectedValue='<%# Bind("DoctorUserName") %>'
                        AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required"
                        Width="250px" onchange="GetDoctor();" CssClass="text-input"  />
                    <data:StaffDataSource ID="DoctorUserNameStaffDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataDoctorShortName" runat="server" Text="Doctor Short Name:" AssociatedControlID="dataDoctorShortName" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataDoctorShortName" Text='<%# Bind("DoctorShortName") %>'
                        MaxLength="50" Width="250px" CssClass="text-input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataRoomId" runat="server" Text="Room Id:" AssociatedControlID="cbRoomId" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="cbRoomId" DataSourceID="RoomIdRoomDataSource"
                        DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("RoomId") %>'
                        Width="250px" AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>"
                        ErrorText="Required" onchange="GetRoom();" />
                    <data:RoomDataSource ID="RoomIdRoomDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
         <%-- <tr>
                <td class="literal">
                    <asp:Label ID="lbldataRoomTitle" runat="server" Text="Room Title:" AssociatedControlID="dataRoomTitle" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataRoomTitle" Text='<%# Bind("RoomTitle") %>' MaxLength="200"
                        Width="250px" CssClass="text-input"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataPriorityIndex" runat="server" Text="Priority Index:" AssociatedControlID="dataPriorityIndex" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataPriorityIndex" Text='<%# Bind("PriorityIndex") %>'
                        Width="250px" CssClass="text-input"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPriorityIndex"
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
        <asp:HiddenField runat="server" ID="HdRoomTitle" Value='<%# Bind("RoomTitle")%>' />
        <asp:HiddenField runat="server" ID="HDcreatedate" Value='<%# Bind("CreateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="HDUpdateDate" Value='<%# Bind("UpdateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="hdCreateUser" Value='<%# Bind("CreateUser") %>' />
        <asp:HiddenField runat="server" ID="hdUpdateUser" Value='<%# Bind("UpdateUser") %>' />
    </ItemTemplate>
</asp:FormView>
