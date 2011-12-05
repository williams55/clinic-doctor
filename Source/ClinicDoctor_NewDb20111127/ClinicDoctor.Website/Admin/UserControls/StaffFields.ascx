<%@ Control Language="C#" ClassName="StaffFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataId" Text='<%# Bind("Id") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataId" runat="server" Display="Dynamic" ControlToValidate="dataId" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataId" runat="server" Display="Dynamic" ControlToValidate="dataId" ErrorMessage="Invalid value" MaximumValue="9223372036854775807" MinimumValue="-9223372036854775808" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFirstName" runat="server" Text="First Name:" AssociatedControlID="dataFirstName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFirstName" Text='<%# Bind("FirstName") %>' MaxLength="200"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataFirstName" runat="server" Display="Dynamic" ControlToValidate="dataFirstName" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLastName" runat="server" Text="Last Name:" AssociatedControlID="dataLastName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataLastName" Text='<%# Bind("LastName") %>' MaxLength="200"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataLastName" runat="server" Display="Dynamic" ControlToValidate="dataLastName" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataShortName" runat="server" Text="Short Name:" AssociatedControlID="dataShortName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataShortName" Text='<%# Bind("ShortName") %>' MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataShortName" runat="server" Display="Dynamic" ControlToValidate="dataShortName" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataGroupId" runat="server" Text="Group Id:" AssociatedControlID="dataGroupId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataGroupId" DataSourceID="GroupIdGroupDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("GroupId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:GroupDataSource ID="GroupIdGroupDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataUserName" runat="server" Text="User Name:" AssociatedControlID="dataUserName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataUserName" Text='<%# Bind("UserName") %>' MaxLength="200"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataUserName" runat="server" Display="Dynamic" ControlToValidate="dataUserName" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAddress" runat="server" Text="Address:" AssociatedControlID="dataAddress" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAddress" Text='<%# Bind("Address") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHomePhone" runat="server" Text="Home Phone:" AssociatedControlID="dataHomePhone" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHomePhone" Text='<%# Bind("HomePhone") %>' MaxLength="20"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataWorkPhone" runat="server" Text="Work Phone:" AssociatedControlID="dataWorkPhone" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataWorkPhone" Text='<%# Bind("WorkPhone") %>' MaxLength="20"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCellPhone" runat="server" Text="Cell Phone:" AssociatedControlID="dataCellPhone" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCellPhone" Text='<%# Bind("CellPhone") %>' MaxLength="20"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataBirthdate" runat="server" Text="Birthdate:" AssociatedControlID="dataBirthdate" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataBirthdate" Text='<%# Bind("Birthdate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataBirthdate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIsFemale" runat="server" Text="Is Female:" AssociatedControlID="dataIsFemale" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataIsFemale" SelectedValue='<%# Bind("IsFemale") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem><asp:ListItem Value="" Text="Pick ..." Enabled="False"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTitle" runat="server" Text="Title:" AssociatedControlID="dataTitle" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTitle" Text='<%# Bind("Title") %>' MaxLength="10"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNote" runat="server" Text="Note:" AssociatedControlID="dataNote" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNote" Text='<%# Bind("Note") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataRoles" runat="server" Text="Roles:" AssociatedControlID="dataRoles" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataRoles" Text='<%# Bind("Roles") %>' MaxLength="200"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataRoles" runat="server" Display="Dynamic" ControlToValidate="dataRoles" ErrorMessage="Required"></asp:RequiredFieldValidator>
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
					<asp:TextBox runat="server" ID="dataCreateDate" Text='<%# Bind("CreateDate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataCreateDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataCreateDate" runat="server" Display="Dynamic" ControlToValidate="dataCreateDate" ErrorMessage="Required"></asp:RequiredFieldValidator>
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
					<asp:TextBox runat="server" ID="dataUpdateDate" Text='<%# Bind("UpdateDate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataUpdateDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataUpdateDate" runat="server" Display="Dynamic" ControlToValidate="dataUpdateDate" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


