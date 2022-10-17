using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.DL.ConversionUnitDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.ConversionUnitBL
{
    public class ConversionUnitBL: BaseBL<ConversionUnit>, IConversionUnitBL
    {
        #region Field

        private IConversionUnitDL _conversionUnitlDL;

        #endregion

        #region Constructor

        public ConversionUnitBL(IConversionUnitDL conversionUnitlDL) : base(conversionUnitlDL)
        {
            conversionUnitlDL = new ConversionUnitDL();
            _conversionUnitlDL = conversionUnitlDL;
        }



        #endregion
    }
}
