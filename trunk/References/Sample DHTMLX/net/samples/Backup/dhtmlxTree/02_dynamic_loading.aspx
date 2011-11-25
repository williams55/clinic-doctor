<%@ Page Language="C#" MasterPageFile="~/dhtmlxTree/tree.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="TreeContentPlaceholder">
    <script>
        tree.setXMLAutoLoading("DynamicFoldersTree.ashx")
        tree.loadXML("DynamicFoldersTree.ashx");   
    </script>
</asp:Content>    
