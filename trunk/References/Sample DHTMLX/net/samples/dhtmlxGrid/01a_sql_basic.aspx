<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dhtmlxGrid/grid.Master"  %>

<asp:Content ContentPlaceHolderID="GridContentPlaceholder" ID="Grid" runat="server">			
	<div id="gridbox" width="350px" height="550px" style="background-color:white;overflow:hidden"></div>
<script>
	mygrid = new dhtmlXGridObject('gridbox');
	mygrid.setImagePath('<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>imgs/');
	mygrid.setHeader("Column A, Column B");
	mygrid.attachHeader("#connector_text_filter,#connector_text_filter")
	mygrid.setInitWidths("100,*")
	mygrid.setColTypes("edtxt,ed");
	mygrid.setColSorting("connector,connector")
	mygrid.enableSmartRendering(true)
	mygrid.enableMultiselect(true)
	mygrid.init();
	mygrid.loadXML("gridBasicSQLConnector.ashx");
	var dp = new dataProcessor("gridBasicSQLConnector.ashx");
	dp.init(mygrid);
</script>
<input type="button" name="add" value="add row" onclick="var id=mygrid.uid(); mygrid.addRow(id,'',0); mygrid.showRow(id);">
<input type="button" name="delete" value="delete row" onclick="mygrid.deleteSelectedRows()">
</asp:Content>