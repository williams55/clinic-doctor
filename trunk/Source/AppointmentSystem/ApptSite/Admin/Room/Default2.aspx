<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default2.aspx.cs" Inherits="Admin_Room_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript" src="<%= ResolveUrl("~/resources/scripts/cst/devexpress.js") %>"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="Server">
    <div id="box-tabs" class="box">
        <div class="title">
            <h5>Room</h5>
        </div>
        <div id="box-order">
            <dx:ASPxGridView ID="gridRoom" ClientInstanceName="grid" runat="server" DataSourceID="RoomDataSource"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnRowInserting="gridRoom_RowInserting"
                OnCustomButtonCallback="gridRoom_CustomButtonCallback" 
                onrowupdating="gridRoom_RowUpdating">
                <Columns>                   
                    <dx:GridViewDataColumn FieldName="Id" VisibleIndex="0" />
                    <dx:GridViewDataColumn FieldName="Title" VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="Note" VisibleIndex="2" />
                    <dx:GridViewDataComboBoxColumn FieldName="ServicesId" VisibleIndex="4">
                        <PropertiesComboBox TextField="Title" ValueField="Id" DataSourceID="ServicesDatas">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>                     
                    <dx:GridViewCommandColumn VisibleIndex="5">
                        <EditButton Visible="true" />
                        <NewButton Visible="true" />
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Delete">
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            <%--    <Templates>
                    <EditForm>                 
                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormUpdateButton"
                                runat="server">
                            </dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                                runat="server">
                            </dx:ASPxGridViewTemplateReplacement>
                        </div>
                        <div id="devexpress-form">
                                <table class="edit-form">
                                    <tbody>
                                        <tr>
                                            <td class="title-row">
                                               Room Id
                                            </td>
                                            <td class="content-row">
                                                <dx:ASPxTextBox runat="server" ID="txtTitle" Text='<%# Bind("Id") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                                Title
                                            </td>
                                            <td class="content-row">
                                                 <dx:ASPxTextBox runat="server" ID="ASPxTextBox1" Text='<%# Bind("Title") %>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title-row">
                                                Note
                                            </td>
                                            <td class="content-row">
                                               <dx:ASPxTextBox runat="server" ID="txtNote" Text='<%# Bind("Note")%>' CssClass="text-form">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td class="title-row">
                                               Services
                                            </td>
                                            <td class="content-row">                                            
                                               <dx:ASPxComboBox  runat="server" ID="ListServicesId"  TextField="Title" ValueField="Id"   Value='<%# Bind("ServicesId") %>' DataSourceID="ServicesDataSource">
                                                     
                                               </dx:ASPxComboBox> 
                                                
                                               <data:ServicesDataSource runat="server" ID="ServicesDataSource" SelectMethod="GetAll"></data:ServicesDataSource>                                    
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                        </div> 
                    </EditForm>
                  </Templates>--%>
                <ClientSideEvents EndCallback="function(s, e) { RefreshGrid();}" BeginCallback="function(s, e) {command = e.command; gridObject = s;}">
                </ClientSideEvents>
                <SettingsPager Mode="ShowPager" PageSize="5" Position="Bottom">
                </SettingsPager>                
              
            </dx:ASPxGridView>
            <data:RoomDataSource ID="RoomDataSource" EnableDeepLoad="true" runat="server" SelectMethod="GetPaged" EnablePaging="True"  EnableSorting="True" UpdateMethod="DeepSave">
                <DeepLoadProperties Method="IncludeChildren" Recursive="False">
	                <Types>					 
					    <data:RoomProperty Name="Services"/>
					    <data:ServicesProperty Name="RoomCollection" />					   
				    </Types>				
			    </DeepLoadProperties>
                <Parameters>
                    <data:CustomParameter Name="WhereClause" Value="IsDisabled = 'false'" ConvertEmptyStringToNull="false" />
                    <data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="PageIndex" ControlID="gridRoom" PropertyName="PageIndex"  Type="Int32" />
                    <data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />                   
                </Parameters>     
            </data:RoomDataSource>
             <data:ServicesDataSource SelectMethod="GetAll" runat="server" ID="ServicesDatas" >
            </data:ServicesDataSource>
        </div>
    </div>
</asp:Content>
