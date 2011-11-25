<%@ Page Language="C#" MasterPageFile="~/dhtmlxGrid/grid.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="GridContentPlaceholder">
	<div id="gridbox" width="650px" height="550px" style="background-color:white;overflow:hidden"></div>
    <script>
        mygrid = new dhtmlXGridObject('gridbox');
        mygrid.setImagePath('<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>imgs/');
        mygrid.setColumnIds("sales,book,author,price,store,shipping,best,date");
        mygrid.setHeader("Sales,Book Title,Author,Price,In Store,Shipping,Bestseller,Date of Publication");
        mygrid.setInitWidths("50,150,120,80,80,80,80,200")
        mygrid.setColAlign("right,left,left,right,center,left,center,center")
        mygrid.setColTypes("dyn,ed,ed,price,ch,co,ch,ro");
        mygrid.setSkin("light");
        mygrid.setColSorting("int,str,str,int,str,connector,str,date")
        mygrid.init();
	    
	    mygrid.loadXML("explicitOptionsConnector.ashx");

	    var dp = new dataProcessor("explicitOptionsConnector.ashx");
	    dp.setUpdateMode("off")
	    dp.init(mygrid);
    </script>
    <input type="button" name="add" value="add row" onclick="var id=mygrid.uid(); mygrid.addRow(id,'',0); mygrid.showRow(id);">
    <input type="button" name="delete" value="delete row" onclick="mygrid.deleteSelectedRows()">
    <input type="button" name="update" value="update row" onclick="dp.sendData()">
</asp:Content>