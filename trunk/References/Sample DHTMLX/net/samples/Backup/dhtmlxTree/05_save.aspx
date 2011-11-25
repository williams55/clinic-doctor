<%@ Page Language="C#" MasterPageFile="~/dhtmlxTree/tree.Master" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="TreeContentPlaceholder">
    <%--<script src='<%= System.Configuration.ConfigurationManager.AppSettings["srcRoot"] %>dhtmlxTree/codebase/ext/dhtmlxTree_ed.js'></script>	
    --%><script>
        if (tree.enableItemEditor) 
            tree.enableItemEditor(true);
        tree.enableDragAndDrop(true)
        tree.loadXML("UpdatableTree.ashx");
        var dp = new dataProcessor("UpdatableTree.ashx");
        dp.setUpdateMode("off");
        dp.init(tree);
    </script>
    <input type="button" name="some_name" value="delete item" onclick="tree.deleteItem(tree.getSelectedItemId())">
    <input type="button" name="some_name" value="add item" onclick="tree.insertNewItem((tree.getSelectedItemId()||'0'),(new Date()).valueOf(),'item')">
    <input type="button" name="some_name" value="update" onclick="dp.sendData();">
</asp:Content>