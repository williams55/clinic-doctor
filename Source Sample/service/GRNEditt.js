/// <reference path="jquery-1.4.2.min.js" />
/// <reference path="CommonFunction.js" />

function GetDrugDetail(objDrugId) {
    var requestdata = JSON.stringify({ drugId: objDrugId });
    $.ajax({
        type: "POST",
        url: "DrugServices.asmx/GetFullMasterList",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var drug = response.d;
            $("#UnitResult").text(drug.Unit);
            $("[id$=CostPriceResult]").val(drug.CostPrice);
            $("[id$=HFoldCost]").val(drug.CostPrice);
            $("#MarkupPercent").text(CommaFormatted(drug.MarkupPercent, 0));
            $("#SellPriceOld").text(CommaFormatted(drug.SellPrice, 0));
            $("[id$=SellPriceNew]").val(CommaFormatted(drug.SellPrice, 0));
           
            
        }
    });
}
function calculatetotal(quanti)
{
//calculate total
     var price=parseFloat($("[id$=CostPriceResult]").val());
     var quantity=parseFloat(quanti);
     var textTotal = $("#sptotal"); // lay nguyen doi tuong
     var total=parseFloat(quantity*price);
     textTotal.text(CommaFormatted(total, 0));
}
function GetDrug()
{
    var drugId=$("[id$=dataDrugId]").val();
    var poId=$("[id$=dataPoId]").val();
    GetDrugDetail(drugId);
    GetPo(drugId,poId);
    
    
     
}
function GetPo(drugId,poId) {
    var requestdata = JSON.stringify({ DrugId: drugId,PoId: poId });
    $.ajax({
        type: "POST",
        url: "DrugServices.asmx/GetList",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {    
           var poDetail = response.d;
          $("[id$=HdPoQuantity]").val(poDetail.RemainQuantity);
          $("[id$=tbQuantity]").val(poDetail.RemainQuantity);
          calculatetotal(poDetail.RemainQuantity);
        }
    });
}




   

function DeleteRow(obj, detailId) {
var poId=$("[id$=dataPoId]").val();
    var requestdata = JSON.stringify({ GrnDetailId: detailId,PoId:poId });
    $.ajax({
            type: "POST",
            url: "GrnEdit.aspx/DeleteGrnDetail",
            data: requestdata,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function() {
                $(obj).closest("tr").fadeOut(1000);
            }
        });
}