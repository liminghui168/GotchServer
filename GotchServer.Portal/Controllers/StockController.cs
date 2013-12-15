using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoDutchServer.Model;

namespace GotchServer.Portal.Controllers
{
    public class StockController : ApiController
    {

        

        public string Get()
        {

            using (Sanwai_CustEntities dao = new Sanwai_CustEntities())
            {
                Stock stock = dao.Stocks.Where(s => s.Id == 1).FirstOrDefault();
            }

            return "";
        }
    }
}
