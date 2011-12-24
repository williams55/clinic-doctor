<%@ Control Language="C#" ClassName="DoctorFuncFields" %>
<asp:FormView ID="FormView1" runat="server">
    <ItemTemplate>
        <script src='<%=ResolveUrl("../myscript/GetDoctor.js")%>' type="text/javascript"></script>
         <script src='<%=ResolveUrl("../myscript/GetFunc.js")%>' type="text/javascript"></script>
        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataDoctorUserName" runat="server" Text="Doctor User Name:" AssociatedControlID="cboDoctorUserName" />
                </td>
                <td>
                    <data:EntityDropDownList runat="server" ID="cboDoctorUserName" DataSourceID="DoctorUserNameStaffDataSource"
                        DataTextField="FirstName" DataValueField="UserName" SelectedValue='<%# Bind("DoctorUserName") %>'
                        AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>"  Width="250px"  onchange="GetDoctor();" />
                    <data:StaffDataSource ID="DoctorUserNameStaffDataSource" runat="server" SelectMethod="GetAll" />
                </td>
            </tr>
            <tr>
                <td class="literal">
                    <asp:Label ID="lbldataDoctorShortName" runat="server" Text="Doctor Short Name:" AssociatedControlID="dataDoctorShortName" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dataDoctorShortName" Text='<%# Bind("DoctorShortName") %>'
                        MaxLength="50"  Width="250px" CssClass="text-input"></asp:TextBox>
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
                        Width="250px" onchange="GetFunc();" />
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
        <asp:HiddenField runat="server" ID="hdFuncTitle" Value='<%# Bind("FuncTitle") %>' />
        <asp:HiddenField runat="server" ID="HDcreatedate" Value='<%# Bind("CreateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="HDUpdateDate" Value='<%# Bind("UpdateDate", "{0:d}") %>' />
        <asp:HiddenField runat="server" ID="hdCreateUser" Value='<%# Bind("CreateUser") %>' />
        <asp:HiddenField runat="server" ID="hdUpdateUser" Value='<%# Bind("UpdateUser") %>' />
    </ItemTemplate>
</asp:FormView>
