<%@ Page Language="C#" MasterPageFile="~/dhtmlxTree/tree.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="TreeContentPlaceholder">
    <script>
        tree.loadXML("SQLFoldersTree.ashx");
    </script>
</asp:Content>