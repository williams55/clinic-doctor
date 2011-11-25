<%@ Page Language="C#" MasterPageFile="~/dhtmlxTreeGrid/treeGrid.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="LoadGridXMLPlaceholder" >
    <script>
        mygrid.kidsXmlFile = "DynamicFolders.ashx";
        mygrid.loadXML("DynamicFolders.ashx");
        var dp = new dataProcessor("DynamicFolders.ashx");
        dp.init(mygrid);

    </script>
</asp:Content>