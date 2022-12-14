using MISA.WebCukCuk.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = MISA.WebCukCuk.Common.Enums.Type;

namespace MISA.WebCukCuk.Common.Entities
{
    [Table("conversionunit")]
    public class ConversionUnit
    {
        /// <summary>
        /// ID đơn vị chuyển đổi
        /// </summary>
        [Key]
        public Guid ConversionUnitID { get; set; }

        /// <summary>
        /// Id nguyên vật liệu
        /// </summary>
        [Key]
        public Guid MaterialID { get; set; }

        /// <summary>
        /// Phép tính
        /// </summary>
        public Calculate Calculation { get; set; }

        /// <summary>
        /// Tỷ lệ chuyển đổi
        /// </summary>
        public double ConversionRate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Loại sửa đổi
        /// </summary>
        public Type? Type { get; set; }

        /// <summary>
        /// Id cũ của đơn vị chuyển đổi khi sửa
        /// </summary>  
        public Guid? ConversionUnitIDOld { get; set; }
    }
}
