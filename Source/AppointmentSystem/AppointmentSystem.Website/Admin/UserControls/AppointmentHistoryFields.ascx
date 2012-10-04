<%@ Control Language="C#" ClassName="AppointmentHistoryFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
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
        <td class="literal"><asp:Label ID="lbldataNote" runat="server" Text="Note:" AssociatedControlID="dataNote" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNote" Text='<%# Bind("Note") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataGuid" runat="server" Text="Guid:" AssociatedControlID="dataGuid" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataGuid" Value='<%# Bind("Guid") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAppointmentId" runat="server" Text="Appointment Id:" AssociatedControlID="dataAppointmentId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataAppointmentId" DataSourceID="AppointmentIdAppointmentDataSource" DataTextField="PatientCode" DataValueField="Id" SelectedValue='<%# Bind("AppointmentId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:AppointmentDataSource ID="AppointmentIdAppointmentDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


