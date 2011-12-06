<%@ Control Language="C#" ClassName="AppointmentFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataId" Text='<%# Bind("Id") %>' MaxLength="20"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataId" runat="server" Display="Dynamic" ControlToValidate="dataId" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCustomerId" runat="server" Text="Customer Id:" AssociatedControlID="dataCustomerId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataCustomerId" DataSourceID="CustomerIdCustomerDataSource" DataTextField="FirstName" DataValueField="Id" SelectedValue='<%# Bind("CustomerId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:CustomerDataSource ID="CustomerIdCustomerDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCustomerName" runat="server" Text="Customer Name:" AssociatedControlID="dataCustomerName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCustomerName" Text='<%# Bind("CustomerName") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataContentId" runat="server" Text="Content Id:" AssociatedControlID="dataContentId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataContentId" DataSourceID="ContentIdContentDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("ContentId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ContentDataSource ID="ContentIdContentDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataContentTitle" runat="server" Text="Content Title:" AssociatedControlID="dataContentTitle" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataContentTitle" Text='<%# Bind("ContentTitle") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDoctorUsername" runat="server" Text="Doctor Username:" AssociatedControlID="dataDoctorUsername" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataDoctorUsername" DataSourceID="DoctorUsernameStaffDataSource" DataTextField="FirstName" DataValueField="UserName" SelectedValue='<%# Bind("DoctorUsername") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:StaffDataSource ID="DoctorUsernameStaffDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDoctorShortName" runat="server" Text="Doctor Short Name:" AssociatedControlID="dataDoctorShortName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDoctorShortName" Text='<%# Bind("DoctorShortName") %>' MaxLength="50"></asp:TextBox>
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
        <td class="literal"><asp:Label ID="lbldataRoomTitle" runat="server" Text="Room Title:" AssociatedControlID="dataRoomTitle" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataRoomTitle" Text='<%# Bind("RoomTitle") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNurseUsername" runat="server" Text="Nurse Username:" AssociatedControlID="dataNurseUsername" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataNurseUsername" DataSourceID="NurseUsernameStaffDataSource" DataTextField="FirstName" DataValueField="UserName" SelectedValue='<%# Bind("NurseUsername") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:StaffDataSource ID="NurseUsernameStaffDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNurseShortName" runat="server" Text="Nurse Short Name:" AssociatedControlID="dataNurseShortName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNurseShortName" Text='<%# Bind("NurseShortName") %>' MaxLength="50"></asp:TextBox>
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
        <td class="literal"><asp:Label ID="lbldataStatusTitle" runat="server" Text="Status Title:" AssociatedControlID="dataStatusTitle" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataStatusTitle" Text='<%# Bind("StatusTitle") %>' MaxLength="200"></asp:TextBox>
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
        <td class="literal"><asp:Label ID="lbldataColorCode" runat="server" Text="Color Code:" AssociatedControlID="dataColorCode" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataColorCode" Text='<%# Bind("ColorCode") %>' MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataColorCode" runat="server" Display="Dynamic" ControlToValidate="dataColorCode" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIsComplete" runat="server" Text="Is Complete:" AssociatedControlID="dataIsComplete" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataIsComplete" SelectedValue='<%# Bind("IsComplete") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem><asp:ListItem Value="" Text="Pick ..." Enabled="False"></asp:ListItem></asp:RadioButtonList>
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


