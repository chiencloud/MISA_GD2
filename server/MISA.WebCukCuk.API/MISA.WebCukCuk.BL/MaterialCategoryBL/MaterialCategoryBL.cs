using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.DL.BaseDL;
using MISA.WebCukCuk.DL.MaterialCategoryDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.MaterialCategoryBL
{
    public class MaterialCategoryBL : BaseBL<MaterialCategory>, IMaterialCategoryBL
    {
        #region Field

        private IMaterialCategoryDL _materialCategoryDL;

        #endregion

        #region Constructor

        public MaterialCategoryBL(IMaterialCategoryDL materialCategoryDL) : base(materialCategoryDL)
        {
            _materialCategoryDL = materialCategoryDL;
        } 

        #endregion
    }
}
