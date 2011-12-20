
function GetRoomTitle(obId) {
    var requestdata = JSON.stringify({ Id: obId });
    $.ajax({
        type: "POST",
        url:"../DoctorServices.asmx/GetFromRoom",
        data: requestdata,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(response) {
            var room = response.d;
          $("[id$=HdRoomTitle]").val(room.Title);
       
            
        }
    });
}


function GetRoom() {

    var id=$("[id$=cbRoomId]").val();
    GetRoomTitle(id);
  
}
//function calculatetotal(quanti)
//{
////calculate total
//     var price=parseFloat($("[id$=CostPriceResult]").val());
//     var quantity=parseFloat(quanti);
//     var textTotal = $("#sptotal"); // lay nguyen doi tuong
//     var total=parseFloat(quantity*price);
//     textTotal.text(CommaFormatted(total, 0));
//}
//function GetDrug()
//{
//    var drugId=$("[id$=dataDrugId]").val();
//    var poId=$("[id$=dataPoId]").val();
//    GetDrugDetail(drugId);
//    GetPo(drugId,poId);
//    
//    
//     
//}
//function GetPo(drugId,poId) {
//    var requestdata = JSON.stringify({ DrugId: drugId,PoId: poId });
//    $.ajax({
//        type: "POST",
//        url: "DrugServices.asmx/GetList",
//        data: requestdata,
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function(response) {    
//           var poDetail = response.d;
//          $("[id$=HdPoQuantity]").val(poDetail.RemainQuantity);
//          $("[id$=tbQuantity]").val(poDetail.RemainQuantity);
//          calculatetotal(poDetail.RemainQuantity);
//        }
//    });
//}




//   

//function DeleteRow(obj, detailId) {
//var poId=$("[id$=dataPoId]").val();
//    var requestdata = JSON.stringify({ GrnDetailId: detailId,PoId:poId });
//    $.ajax({
//            type: "POST",
//            url: "GrnEdit.aspx/DeleteGrnDetail",
//            data: requestdata,
//            dataType: "json",
//            contentType: "application/json; charset=utf-8",
//            success: function() {
//                $(obj).closest("tr").fadeOut(1000);
//            }
//        });
//}

//function pageLoad() {
//    $(function() {
//        $('[id$=dataDrugId]').keypress(function(e) {
//            var code = null;
//            code = (e.keyCode ? e.keyCode : e.which);
//            if (code == 13 || code == 9) {
//                $("[id$=tbQuantity]").focus();
//                $("[id$=tbQuantity]").select();
//                return false;
//            }
//            return true;
//        });
//    });
//}