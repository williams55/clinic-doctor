<%@ WebService Language="C#" Class="DrugServices" %>

//using System.Web.Script.Services;
using System;
using System.Web.Script.Services;
using System.Web.Services;
using Pharmacy.Data;
using Pharmacy.Entities;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

public class DrugServices : WebService
{
    public class Drug
    {
        public string DrugId { get; set; }
        public string Unit { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal MarkupPercent { get; set; }
      
        
    }
    public class SPodetails
    {
        public int RemainQuantity { get; set; }
       
    }
    
    
    public class StockInfo
    {
        public string DrugId { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string batchLot { get; set; }
        public string expDate { get; set; }
        public int quantity { get; set; }
       
    }
    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Drug GetFromMasterList(string drugId)
    {
        DrugDispo obj = DataRepository.DrugDispoProvider.GetByDrugId(drugId);
        Drug objD = new Drug();
        if (obj != null)
            objD = new Drug { DrugId = obj.DrugId, Unit = obj.Unit, CostPrice = obj.CostPrice };
        return objD;
    }
    [WebMethod]
    public Drug GetFullMasterList(string drugId)
    {
        DrugDispo obj = DataRepository.DrugDispoProvider.GetByDrugId(drugId);
        DataRepository.DrugDispoProvider.DeepLoad(obj);
        Drug objD = new Drug();
        if (obj != null)
            objD = new Drug { DrugId = obj.DrugId, Unit = obj.Unit, CostPrice = obj.CostPrice, SellPrice = obj.SellPrice, MarkupPercent = obj.MarkUpTypeSource.MarkUpPercent };
        return objD;
      
    }
    [WebMethod]
    public SPodetails GetList(string DrugId,string PoId)
    {

        int count = 0;
        TList<PoDetails> listobj = DataRepository.PoDetailsProvider.GetPaged("DrugId='" + DrugId + "' And Poid='" + PoId + "'", "", 0, 1, out count);
        SPodetails objPD = new SPodetails();
        if (count>0)
            objPD = new SPodetails { RemainQuantity = listobj[0].RemainQuantity };
        return objPD;
        
     

    }
    [WebMethod]
    public StockInfo GetFromStock(string stockId)
    {
        Stock objstock=DataRepository.StockProvider.GetByStockId(long.Parse(stockId));
        DrugDispo obj = DataRepository.DrugDispoProvider.GetByDrugId(objstock.DrugId);
        string dtexp=null;
        //DateTime? dtexp; DateTime dt;
        //dtexp = DateTime.TryParse(objstock.ExpDate.ToString(), out dt) ;
        //expDate=DateTime.TryParse(GrnDetailItem.ExpDate.Value.ToString()
        // expDate = DateTime.TryParse(objstock.ExpDate.Value.ToString(), out dt) ? dt : (DateTime?)null;
         if (objstock.ExpDate != null) dtexp = objstock.ExpDate.Value.ToString("dd-MMM-yyyy");
         StockInfo objs=new StockInfo();
        if (objstock != null)
        {
            objs = new StockInfo()
             {
                 DrugId = objstock.DrugId,
                 Unit = obj.Unit,
                 UnitPrice = objstock.UnitPrice,
                 batchLot = objstock.BatchLot,
                 expDate = dtexp,
                 quantity = objstock.Quantity
             };
    }
        return objs;
    }
    
 
    
    
    
        
}
