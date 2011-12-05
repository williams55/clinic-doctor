<%@ Control Language="C#" ClassName="DoctorRoomFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataDoctorUserName" runat="server" Text="Doctor User Name:" AssociatedControlID="dataDoctorUserName" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDoctorUserName" Text='<%# Bind("DoctorUserName") %>' MaxLength="200"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDoctorUserName" runat="server" Display="Dynamic" ControlToValidate="dataDoctorUserName" ErrorMessage="Required"></asp:RequiredFieldValidator>
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
					<data:EntityDropDownList runat="server" ID="dataRoomId" DataSourceID="RoomIdRoomDataSource" DataTextField="Title" DataValueField="Id" SelectedValue='<%# Bind("RoomId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
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
        <td class="literal"><asp:Label ID="lbldataPriorityIndex" runat="server" Text="Priority Index:" AssociatedControlID="dataPriorityIndex" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPriorityIndex" Text='<%# Bind("PriorityIndex") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPriorityIndex" runat="server" Display="Dynamic" ControlToValidate="dataPriorityIndex" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataPriorityIndex" runat="server" Display="Dynamic" ControlToValidate="dataPriorityIndex" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
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


