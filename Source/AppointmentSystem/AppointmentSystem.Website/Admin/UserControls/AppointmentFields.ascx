<%@ Control Language="C#" ClassName="AppointmentFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataUsername" runat="server" Text="Username:" AssociatedControlID="dataUsername" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataUsername" DataSourceID="UsernameUsersDataSource" DataTextField="Password" DataValueField="Username" SelectedValue='<%# Bind("Username") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:UsersDataSource ID="UsernameUsersDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataRosterId" runat="server" Text="Roster Id:" AssociatedControlID="dataRosterId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataRosterId" DataSourceID="RosterIdRosterDataSource" DataTextField="StartTime" DataValueField="Id" SelectedValue='<%# Bind("RosterId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:RosterDataSource ID="RosterIdRosterDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataId" Text='<%# Bind("Id") %>' MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataId" runat="server" Display="Dynamic" ControlToValidate="dataId" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPatientCode" runat="server" Text="Patient Code:" AssociatedControlID="dataPatientCode" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPatientCode" Text='<%# Bind("PatientCode") %>' MaxLength="11"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPatientCode" runat="server" Display="Dynamic" ControlToValidate="dataPatientCode" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataRoomId" runat="server" Text="Room Id:" AssociatedControlID="dataRoomId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataRoomId" DataSourceID="RoomIdRoomDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("RoomId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:RoomDataSource ID="RoomIdRoomDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataServicesId" runat="server" Text="Services Id:" AssociatedControlID="dataServicesId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataServicesId" DataSourceID="ServicesIdServicesDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("ServicesId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ServicesDataSource ID="ServicesIdServicesDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataStatusId" runat="server" Text="Status Id:" AssociatedControlID="dataStatusId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataStatusId" DataSourceID="StatusIdStatusDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("StatusId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:StatusDataSource ID="StatusIdStatusDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAppointmentGroupId" runat="server" Text="Appointment Group Id:" AssociatedControlID="dataAppointmentGroupId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataAppointmentGroupId" DataSourceID="AppointmentGroupIdAppointmentGroupDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("AppointmentGroupId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:AppointmentGroupDataSource ID="AppointmentGroupIdAppointmentGroupDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNote" runat="server" Text="Note:" AssociatedControlID="dataNote" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNote" Text='<%# Bind("Note") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataStartTime" runat="server" Text="Start Time:" AssociatedControlID="dataStartTime" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataStartTime" Text='<%# Bind("StartTime", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataStartTime" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEndTime" runat="server" Text="End Time:" AssociatedControlID="dataEndTime" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEndTime" Text='<%# Bind("EndTime", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataEndTime" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIsComplete" runat="server" Text="Is Complete:" AssociatedControlID="dataIsComplete" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataIsComplete" SelectedValue='<%# Bind("IsComplete") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIsDisabled" runat="server" Text="Is Disabled:" AssociatedControlID="dataIsDisabled" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataIsDisabled" SelectedValue='<%# Bind("IsDisabled") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
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


