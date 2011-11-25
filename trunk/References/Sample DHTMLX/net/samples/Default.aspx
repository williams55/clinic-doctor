<%@ Page Language="C#" MasterPageFile="~/Layout.master" %>

<asp:Content ContentPlaceHolderID="SampleContent" ID="Content" runat="server">
    <asp:TreeView ID="NavigationTree" runat="server" DataSourceID="SiteMapData" 
        ImageSet="Simple">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
            HorizontalPadding="0px" VerticalPadding="0px" />
        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
            HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
    </asp:TreeView>
    <asp:SiteMapDataSource ID="SiteMapData" runat="server" />
</asp:Content>