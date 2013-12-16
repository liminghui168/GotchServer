using System;
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
            return null;
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
