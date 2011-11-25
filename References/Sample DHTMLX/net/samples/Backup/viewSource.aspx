<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="viewSource.aspx.cs" Inherits="dhtmlxConnector.Net_Samples.viewSource" %>

<asp:Content ContentPlaceHolderID="SampleContent" runat="server" ID="Content">
    <div style="width:100%;height:100%;padding:5px 5px 5px 5px;">
        <asp:TextBox TextMode="MultiLine" ID="Source" runat="server" style='width:900px;height:600px;background-color:White;border:1px solid;border-color:#ccc #aaa #aaa #ccc' ReadOnly="true"></asp:TextBox>
    </div>
</asp:Content>