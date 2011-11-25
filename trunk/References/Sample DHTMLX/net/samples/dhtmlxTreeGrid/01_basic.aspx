<%@ Page Language="C#" MasterPageFile="~/dhtmlxTreeGrid/treeGrid.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="LoadGridXMLPlaceholder" >
    <script>
        mygrid.loadXML("Folders.ashx");
        var dp = new dataProcessor("Folders.ashx");
        dp.init(mygrid);
    </script>
</asp:Content>