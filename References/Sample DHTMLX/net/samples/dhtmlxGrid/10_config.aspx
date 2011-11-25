<%@ Page Language="C#" MasterPageFile="~/dhtmlxGrid/grid.Master" %>

<asp:Content ID="Content" ContentPlaceHolderID="GridContentPlaceholder" runat="server">
	<div id="gridbox" width="350px" height="550px" style="background-color:white;overflow:hidden"></div>
    <script>
        mygrid = new dhtmlXGridObject('gridbox');
        mygrid.setImagePath('<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>imgs/');
        mygrid.enableSmartRendering(true)
	    mygrid.loadXML("configConnector.ashx");
	    var dp = new dataProcessor("configConnector.ashx");
	    dp.init(mygrid);
    </script>
    <input type="button" name="add" value="add row" onclick="var id=mygrid.uid(); mygrid.addRow(id,'',0); mygrid.showRow(id);">
    <input type="button" name="delete" value="delete row" onclick="mygrid.deleteSelectedRows()">
</asp:Content>