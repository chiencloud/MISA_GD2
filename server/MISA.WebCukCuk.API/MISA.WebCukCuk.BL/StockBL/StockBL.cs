using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.DL.BaseDL;
using MISA.WebCukCuk.DL.StockDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.StockBL
{
    public class StockBL : BaseBL<Stock>, IStockBL
    {

        #region Field

        private IStockDL _stockDL;

        #endregion

        public StockBL(IStockDL stockDL) : base(stockDL)
        {
            stockDL = new StockDL();
            _stockDL = stockDL;
        }
    }
}
