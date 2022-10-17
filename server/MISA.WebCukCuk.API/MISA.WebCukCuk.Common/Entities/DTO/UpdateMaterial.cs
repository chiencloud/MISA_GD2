using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Entities.DTO
{
    public class UpdateMaterial
    {
        /// <summary>
        /// Thông tin nguyên vật liệu cần cập nhập
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// Thông tin đơn vị chuyển đổi cần thêm
        /// </summary>
        public List<ConversionUnit>? AddUnit { get; set; }

        /// <summary>
        /// Thông tin đơn vị chuyển đổi cần update
        /// </summary>
        public List<UpdateConversionUnit>? UpdateUnit { get; set; }

        /// <summary>
        /// Thông tin đơn vị chuyển đổi cần xóa
        /// </summary>
        public List<Guid>? DeleteUnit { get; set; }

    }
}
