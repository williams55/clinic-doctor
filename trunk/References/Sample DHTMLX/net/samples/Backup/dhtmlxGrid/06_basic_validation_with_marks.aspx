<%@ Page Language="C#" MasterPageFile="~/dhtmlxGrid/grid.Master" %>

<asp:Content ContentPlaceHolderID="GridContentPlaceholder" ID="Content" runat="server">
	<div id="gridbox" width="600px" height="250px" style="overflow:hidden"></div>		
	<p><a href="javascript:void(0)" onclick="mygrid.addRow((new Date()).valueOf(),[0,'','','',false,'na',false,''],mygrid.getRowIndex(mygrid.getSelectedId()))">Add row</a></p>
	<p><a href="javascript:void(0)" onclick="mygrid.deleteSelectedItem()">Remove Selected Row</a></p>
	<input type="button" name="some_name" value="update" onclick="myDataProcessor.sendData();" />
	
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
	    function not_empty(value,id,ind){
		    if (value=="") mygrid.setCellTextStyle(id,ind,"background-color:yellow;");
		    return value!="";
	    }
	    function greater_0(value,id,ind){
		    if (parseFloat(value)<=0) mygrid.setCellTextStyle(id,ind,"background-color:yellow;");
		    return parseFloat(value)>0;
	    }

	    myDataProcessor = new dataProcessor("gridConnector.ashx"); //lock feed url
	    myDataProcessor.setVerificator(0,greater_0);
	    myDataProcessor.setVerificator(3,greater_0);
	    myDataProcessor.setVerificator(1,not_empty);
	    myDataProcessor.setVerificator(2,not_empty);
	    //block native marking for invalid rows
	    myDataProcessor.attachEvent("onRowMark",function(id){
		    if (this.is_invalid(id)=="invalid") return false;
		    return true;
	    })
	    myDataProcessor.init(mygrid); //link dataprocessor to the grid
    //============================================================================================
    </script>
</asp:Content>