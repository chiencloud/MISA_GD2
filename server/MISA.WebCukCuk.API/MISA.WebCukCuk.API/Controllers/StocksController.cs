using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebCukCuk.API.NTier.BaseControllers;
using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.BL.StockBL;
using MISA.WebCukCuk.Common.Entities;

namespace MISA.WebCukCuk.API.NTier.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StocksController : BasesController<Stock>
    {

        #region Field

        private IStockBL _stockBL;

        #endregion

        public StocksController(IStockBL stockBL) : base(stockBL)
        {
            _stockBL = stockBL;
        }
    }
}
