using System;
using System.Data.Entity;
using System.Linq;
using EntityState = System.Data.EntityState;

namespace GoDutchServer.Model
{
    public class StockRepostory
    {
        private static Sanwai_CustEntities dbContext;

        public StockRepostory()
        {
            dbContext = new Sanwai_CustEntities();
        }


        public IQueryable<Stock> LoadStocks(Func<Stock, bool> whereLambda)
        {
            return dbContext.Stocks.Where<Stock>(whereLambda).AsQueryable();
        }

        public IQueryable<Stock> LoadStocksByPage<S>(int pageIndex, int pageSize, out int total,
            Func<Stock, bool> whereLambda, Func<Stock, S> orderByLambad, bool isAsc)
        {
            var temp = dbContext.Stocks.Where<Stock>(whereLambda);
            total = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<Stock, S>(orderByLambad)
                    .Skip<Stock>((pageIndex - 1)*pageSize)
                    .Take<Stock>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<Stock, S>(orderByLambad)
                    .Skip<Stock>((pageIndex - 1) * pageSize)
                    .Take<Stock>(pageSize);
            }
            return temp.AsQueryable();
        }

        public int AddStock(Stock stock)
        {
            dbContext.Stocks.AddObject(stock);

            return SaveChanges();
        }

        public int UpdateStock(Stock stock)
        {
            dbContext.Stocks.Attach(stock);
            dbContext.ObjectStateManager.ChangeObjectState(stock, EntityState.Modified);
            return SaveChanges();
        }

        public int DeleteStock(Stock stock)
        {

            dbContext.Stocks.AddObject(stock);
            dbContext.ObjectStateManager.ChangeObjectState(stock, EntityState.Deleted);
            return SaveChanges();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}