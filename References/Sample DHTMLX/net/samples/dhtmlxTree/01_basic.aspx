<%@ Page Language="C#" MasterPageFile="~/dhtmlxTree/tree.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="TreeContentPlaceholder">
    <script>
        tree.loadXML("FoldersTree.ashx");
    </script>
</asp:Content>