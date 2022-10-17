using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.DL.BaseDL;
using MISA.WebCukCuk.DL.UnitDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.UnitBL
{
    public class UnitBL : BaseBL<Unit>, IUnitBL
    {
        #region Field

        private IUnitDL _unitDL;

        #endregion

        public UnitBL(IUnitDL unitDL) : base(unitDL)
        {
            unitDL = new UnitDL();
            _unitDL = unitDL;
        }
    }
}
