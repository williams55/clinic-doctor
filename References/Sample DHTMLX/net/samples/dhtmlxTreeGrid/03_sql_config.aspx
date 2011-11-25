<%@ Page Language="C#" MasterPageFile="~/dhtmlxTreeGrid/treeGrid.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="LoadGridXMLPlaceholder" >
    <script>
        mygrid.loadXML("SQLConfiguredFolders.ashx");
        var dp = new dataProcessor("SQLConfiguredFolders.ashx");
        dp.init(mygrid);
    </script>
</asp:Content>