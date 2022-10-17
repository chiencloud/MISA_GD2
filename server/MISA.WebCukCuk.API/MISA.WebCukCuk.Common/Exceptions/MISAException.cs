using MISA.WebCukCuk.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Exceptions
{
    public class MISAException : Exception
    {
        #region Field
        public ErrorResult ErrorResult { get; set; }
        #endregion

        #region Constructor

        public MISAException(ErrorResult errorResult)
        {
            ErrorResult = errorResult;
        }

        #endregion
    }
}
