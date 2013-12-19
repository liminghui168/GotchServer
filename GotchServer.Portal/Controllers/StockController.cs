﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using GoDutchServer.Model;

namespace GotchServer.Portal.Controllers
{
    public class StockController : Controller
    {

        private readonly StockRepostory stockRepostory = new StockRepostory();


        public ActionResult LoadList()
        {
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);
            int total = 0;

            var list = stockRepostory.LoadStocksByPage(pageIndex, pageSize, out total, s => true, s => s.Id, true);

            var data = new
                           {
                               total = total,
                               rows = from stock in list  
                                      select new
                                                 {
                                                     stock.Id,
                                                     stock.brand,
                                                     stock.stock1,
                                                     stock.vref,
                                                     stock.partno
                                                 }
                           };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddStockDetail()
        {
            string json = Request["stockJson"];
            Stock stock = Newtonsoft.Json.JsonConvert.DeserializeObject<Stock>(json);

            int result = stockRepostory.AddStock(stock);
            if (result > 0)
                return Content("ok");
            return Content("error");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult UpdateStockDetail()
        {
            string json = Request["stockJson"];
            Stock stock = new Stock();
            stock = Newtonsoft.Json.JsonConvert.DeserializeObject<Stock>(json);
            int result =  stockRepostory.UpdateStock(stock);
            if (result > 0)
                return Content("ok");
            return Content("error");
        }

        public ActionResult DeleteStockDetail(int id)
        {
            int result = 0;
            var stockModel = stockRepostory.LoadStocks(s => s.Id == id).FirstOrDefault();
            if(stockModel!=null)
            {
                result = stockRepostory.DeleteStock(stockModel);
                if(result>0)
                {
                    return Content("ok");
                }
                return Content("error");
            }

            return Content("error");
        }


        public ActionResult LoadStockDetail(int id)
        {
            var stockModel = stockRepostory.LoadStocks(s => s.Id == id).FirstOrDefault();
            if(stockModel==null)
            {
                return Content("null");
            }
            var data = new
                           {
                               stockModel.Id,
                               stockModel.stock1,
                               stockModel.vendor,
                               stockModel.vendorname,
                               stockModel.vref,
                               stockModel.des,
                               stockModel.shortdes,
                               stockModel.power,
                               stockModel.opowerrms,
                               stockModel.opowerpmpo,
                               stockModel.partno,
                               stockModel.unit,
                               stockModel.pricecurr,
                               stockModel.price,
                               stockModel.costcurr,
                               stockModel.cost,
                               createdata = stockModel.createdate.Value.ToString("yyyy-MM-dd"),
                               stockModel.brand,
                               stockModel.category,
                               stockModel.gbbarcode,
                               stockModel.ocbarcode,
                               stockModel.pbarcode,
                               stockModel.sclass,
                               stockModel.subclass,
                               stockModel.active
                           };

            return Json(data,JsonRequestBehavior.AllowGet);
        }

        //public IEnumerable<Stock> Get()
        //{
        //    IQueryable<Stock> list = null;
        //    int pageIndex = 1;
        //    int pageSize = 10;

        //    int total = 0;

        //    list = stockRepostory.LoadStocksByPage(pageIndex, pageSize, out total, s => true, s => s.Id, true);

        //    return list;
        //}

        //public IEnumerable<Stock> Post(int pageIndex)
        //{
        //    return null;
        //}

        //[HttpPost]
        //[ActionName("comlpx")]
        //public IEnumerable<Stock> LoadStocks([FromBody] int pageIndex, [FromBody] int pageSize)
        //{
        //    return null;
        //}

    }
}
