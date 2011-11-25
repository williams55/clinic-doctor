<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dhtmlxGrid/Grid.Master" %>

<asp:Content ContentPlaceHolderID="GridContentPlaceholder" ID="Grid" runat="server">
    <div id="gridbox" width="350px" height="550px" style="background-color:white;overflow:hidden"></div>
    <script>
	    mygrid = new dhtmlXGridObject('gridbox');
	    mygrid.setImagePath('<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>imgs/');
	    mygrid.setHeader("ISO, Name");
	    mygrid.attachHeader("#connector_text_filter,#connector_text_filter")
	    mygrid.setInitWidths("100,*")
	    mygrid.setColTypes("edtxt,edtxt");
	    mygrid.setColSorting("connector,connector")
	    mygrid.enableSmartRendering(true)
	    mygrid.enableMultiselect(true)
	    mygrid.init();
	    mygrid.loadXML("baseGridConnector.ashx?dynamic=50");
	    var dp = new dataProcessor("baseGridConnector.ashx?dynamic=50");
	    dp.init(mygrid);
    </script>
</asp:Content>