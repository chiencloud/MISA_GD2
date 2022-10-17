using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Entities.DTO
{
    public class UpdateConversionUnit
    {
        /// <summary>
        /// 
        /// </summary>
        public ConversionUnit ConversionUnit { get; set; }

        public Guid OldIdConversionUnit { get; set; }
    }
}
