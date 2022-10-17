using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebCukCuk.API.NTier.BaseControllers;
using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.BL.UnitBL;
using MISA.WebCukCuk.Common.Entities;

namespace MISA.WebCukCuk.API.NTier.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitsController : BasesController<Unit>
    {

        #region Field

        private IUnitBL _unitBL;

        #endregion

        #region Constructor
        
        public UnitsController(IUnitBL unitBL) : base(unitBL)
        {
            _unitBL = unitBL;
        } 

        #endregion
    }
}
