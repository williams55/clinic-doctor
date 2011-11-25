<%@ Page Language="C#" MasterPageFile="~/dhtmlxTreeGrid/treeGrid.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="LoadGridXMLPlaceholder" >
    <script>
        mygrid.loadXML("CustomizedConfiguredFolders.ashx");
        var dp = new dataProcessor("CustomizedConfiguredFolders.ashx");
        dp.init(mygrid);
    </script>
</asp:Content>