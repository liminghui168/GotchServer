using System;
using System.Collections.Generic;
using System.IO;
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

        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/")); } //Path should! always end with '/'
        }

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <returns></returns>
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
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult AddStockDetail()
        {
            string json = Request["stockJson"];
            Stock stock = Newtonsoft.Json.JsonConvert.DeserializeObject<Stock>(json);
            var file = Request.Files[0];
            if(file!=null)
            {
                string fullPath = StorageRoot + Path.GetFileName(file.FileName);
                stock.picfile = file.FileName;
                file.SaveAs(fullPath);
            }

            int result = stockRepostory.AddStock(stock);
            if (result > 0)
                return Content("ok");
            return Content("error");
        } 
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public ActionResult UpdateStockDetail()
        {
            string json = Request["stockJson"];
            Stock stock = new Stock();
            stock = Newtonsoft.Json.JsonConvert.DeserializeObject<Stock>(json);
            var file = Request.Files[0];
            if (file != null)
            {
                string fullPath = StorageRoot + Path.GetFileName(file.FileName);
                stock.picfile = file.FileName;
                file.SaveAs(fullPath);
            }
            int result = stockRepostory.UpdateStock(stock);
            if (result > 0)
                return Content("ok");
            return Content("error");
        } 
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteStockDetail(int id)
        {
            int result = 0;
            var stockModel = stockRepostory.LoadStocks(s => s.Id == id).FirstOrDefault();
            if (stockModel != null)
            {
                result = stockRepostory.DeleteStock(stockModel);
                if (result > 0)
                {
                    return Content("ok");
                }
                return Content("error");
            }

            return Content("error");
        } 
        #endregion

        

        #region 获取第一条数据
        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadStockDetailFirst()
        {
            var stockModel = stockRepostory.LoadStocks(s => true).FirstOrDefault();
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
                stockModel.active,
                stockModel.picfile
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取上一条数据
        /// <summary>
        ///  获取上一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LoadStockDetailPre(int id)
        {
            var stockModel = stockRepostory.LoadStocks(s => s.Id < id).LastOrDefault();
            if (stockModel != null)
            {
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
                    stockModel.active,
                    stockModel.picfile
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            //已经是第一条数据
            return Content("isFirst");
        } 
        #endregion

        #region 获取下一条数据
        /// <summary>
        /// 获取下一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LoadStockDetailNext(int id)
        {
            var stockModel = stockRepostory.LoadStocks(s => s.Id > id).FirstOrDefault();
            if (stockModel != null)
            {
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
                    stockModel.active,
                    stockModel.picfile
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            //已经是最后一条数据了
            return Content("isLast");
        } 
        #endregion

        #region 获取最后一条数据
        /// <summary>
        /// 获取最后一条数据
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadStockDetailLast()
        {
            var stockModel = stockRepostory.LoadStocks(s => true).LastOrDefault();
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
                stockModel.active,
                stockModel.picfile
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取详细信息
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadStockDetail(int id)
        {
            var stockModel = stockRepostory.LoadStocks(s => s.Id == id).FirstOrDefault();
            if (stockModel == null)
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
                               stockModel.active,
                               stockModel.picfile
                           };

            return Json(data, JsonRequestBehavior.AllowGet);
        } 
        #endregion


        public ActionResult UpdataLoadImg()
        {
            try
            {
                var file = Request.Files[0];
                int stockId = Convert.ToInt32(Request["stockId"]);
                //string fileName = Request["fileName"];
                string fullPath = string.Empty;
                fullPath = StorageRoot + Path.GetFileName(file.FileName);

                file.SaveAs(fullPath);
                /*if(string.IsNullOrEmpty(fileName))
                {
                    file.SaveAs(fullPath + fileName);
                }
                else
                {
                    file.SaveAs(fullPath);
                }*/
                

                var model = stockRepostory.LoadStocks(s => s.Id == stockId).FirstOrDefault();
                if(model!=null)
                {
                    model.picfile = file.FileName;
                    stockRepostory.UpdateStockNoAttach(model);
                    return Content("up_success");
                }
                return Content("up_error");
            }
            catch (Exception)
            {
                return Content("up_error");
            }
        }

    }
}
