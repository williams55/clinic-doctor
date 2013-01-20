<%@ Control Language="C#" ClassName="MessageConfigFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescription" runat="server" Text="Description:" AssociatedControlID="dataDescription" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescription" Text='<%# Bind("Description") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMessageValue" runat="server" Text="Message Value:" AssociatedControlID="dataMessageValue" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMessageValue" Text='<%# Bind("MessageValue") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMessageKey" runat="server" Text="Message Key:" AssociatedControlID="dataMessageKey" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMessageKey" Text='<%# Bind("MessageKey") %>' MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataMessageKey" runat="server" Display="Dynamic" ControlToValidate="dataMessageKey" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


