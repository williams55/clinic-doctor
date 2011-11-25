<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/dhtmlxGrid/grid.Master" %>
<asp:Content ContentPlaceHolderID="GridContentPlaceholder" ID="Grid" runat="server">			
	<div id="gridbox" width="650px" height="550px" style="background-color:white;overflow:hidden"></div>
    <script>
        //init grid and set its parameters (this part as always)
        mygrid = new dhtmlXGridObject('gridbox');
        mygrid.setImagePath('<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>imgs/');
        mygrid.setHeader("Sales,Book Title,Author,Price,In Store,Shipping,Bestseller,Date of Publication");
        mygrid.setInitWidths("50,150,120,80,80,80,80,200")
        mygrid.setColAlign("right,left,left,right,center,left,center,center")
        mygrid.setColTypes("dyn,ed,txt,price,ch,coro,ch,ro");
        mygrid.setSkin("light");
        mygrid.setColSorting("int,str,str,int,str,str,str,date")
        mygrid.init();
        mygrid.loadXML("gridRenderingConnector.ashx");
        //============================================================================================
        myDataProcessor = new dataProcessor("gridRenderingConnector.ashx"); //lock feed url
        myDataProcessor.init(mygrid); //link dataprocessor to the grid
        //============================================================================================
    </script>
    <input type="button" name="add" value="add row" onclick="var id=mygrid.uid(); mygrid.addRow(id,'',0); mygrid.showRow(id);">
    <input type="button" name="delete" value="delete row" onclick="mygrid.deleteSelectedRows()">
</asp:Content>