using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebCukCuk.BL.ConversionUnitBL;
using MISA.WebCukCuk.API.NTier.BaseControllers;
using MISA.WebCukCuk.Common.Entities;

namespace MISA.WebCukCuk.API.NTier.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConversionUnitsController : BasesController<ConversionUnit>
    {
        #region Field

        private IConversionUnitBL _conversionUnitBL;

        #endregion

        #region Constructor

        public ConversionUnitsController(IConversionUnitBL conversionUnitBL) : base(conversionUnitBL)
        {
            _conversionUnitBL = conversionUnitBL;
        }

        #endregion
    }
}
