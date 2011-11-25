<%@ Page Language="C#" MasterPageFile="~/dhtmlxGrid/grid.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="GridContentPlaceholder">
				
	<p><a href="javascript:void(0)" onclick="mygrid.addRow((new Date()).valueOf(),[0,'','','',false,'na',false,''],mygrid.getRowIndex(mygrid.getSelectedId()))">Add row</a></p>
	<p><a href="javascript:void(0)" onclick="mygrid.deleteSelectedItem()">Remove Selected Row</a></p>
	<input type="button" name="some_name" value="update" onclick="myDataProcessor.sendData();" />
	<div id="gridbox" width="600px" height="250px" style="overflow:hidden"></div>
    <script>
	    //init grid and set its parameters (this part as always)
        mygrid = new dhtmlXGridObject('gridbox');
        mygrid.setImagePath('<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>imgs/');
        mygrid.setColumnIds("sales,book,author,price,store,shipping,best,date");
	    mygrid.setHeader("Sales,Book Title,Author,Price,In Store,Shipping,Bestseller,Date of Publication");
	    mygrid.setInitWidths("50,150,120,80,80,80,80,200")
	    mygrid.setColAlign("right,left,left,right,center,left,center,center")
	    mygrid.setColTypes("dyn,ed,txt,price,ch,coro,ch,ro");
	    mygrid.setSkin("light");
	    mygrid.setColSorting("int,str,str,int,str,str,str,date")
	    mygrid.init();
	    mygrid.loadXML("gridConnector.ashx"); //used just for demo purposes	
    //============================================================================================
	    myDataProcessor = new dataProcessor("gridConnector.ashx"); //lock feed url
	    myDataProcessor.setDataColumns([false,true,true,false]); //only second and third columns will trigger data update
	    myDataProcessor.init(mygrid); //link dataprocessor to the grid
    //============================================================================================

    </script>
</asp:Content>