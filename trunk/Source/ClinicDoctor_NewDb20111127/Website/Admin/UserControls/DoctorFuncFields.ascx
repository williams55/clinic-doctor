<%@ Control Language="C#" ClassName="DoctorFuncFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataDoctorId" runat="server" Text="Doctor Id:" AssociatedControlID="dataDoctorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataDoctorId" DataSourceID="DoctorIdStaffDataSource" DataTextField="FirstName" DataValueField="Id" SelectedValue='<%# Bind("DoctorId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:StaffDataSource ID="DoctorIdStaffDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFuncId" runat="server" Text="Func Id:" AssociatedControlID="dataFuncId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataFuncId" DataSourceID="FuncIdFunctionalityDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("FuncId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:FunctionalityDataSource ID="FuncIdFunctionalityDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIsDisabled" runat="server" Text="Is Disabled:" AssociatedControlID="dataIsDisabled" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataIsDisabled" SelectedValue='<%# Bind("IsDisabled") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem><asp:ListItem Value="" Text="Pick ..." Enabled="False"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCreateUser" runat="server" Text="Create User:" AssociatedControlID="dataCreateUser" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCreateUser" Text='<%# Bind("CreateUser") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCreateDate" runat="server" Text="Create Date:" AssociatedControlID="dataCreateDate" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCreateDate" Text='<%# Bind("CreateDate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataCreateDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataUpdateUser" runat="server" Text="Update User:" AssociatedControlID="dataUpdateUser" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataUpdateUser" Text='<%# Bind("UpdateUser") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataUpdateDate" runat="server" Text="Update Date:" AssociatedControlID="dataUpdateDate" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataUpdateDate" Text='<%# Bind("UpdateDate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataUpdateDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


