<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dhtmlxCombo/Combo.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ComboContentPlaceholder" runat="server">
	<script type="text/javascript">
	    var z = new dhtmlXCombo("combo_zone4", "alfa4", 200);
	    z.loadXML("02_sql_connector.ashx");

	    var z = new dhtmlXCombo("combo_zone5", "alfa4", 200);
	    z.enableFilteringMode(true, "02_sql_connector.ashx", true);

	    var z = new dhtmlXCombo("combo_zone6", "alfa4", 200);
	    z.enableFilteringMode(true, "02_sql_connector.ashx?dynamic=true", true, true);
	</script>
</asp:Content>
