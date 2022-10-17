using MISA.WebCukCuk.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Entities
{
    [Table("unit")]
    public class Unit
    {
        /// <summary>
        /// ID đơn vị chuyển đổi
        /// </summary>
        [Key]
        [PropName("ID đơn vị chuyển đổi")]
        [NotEmpty]
        public Guid ConversionUnitID { get; set; }

        /// <summary>
        /// Tên đơn vị chuyển đổi
        /// </summary>
        [PropName("Tên đơn vị chuyển đổi")]
        [NotEmpty]
        public string ConversionUnitName { get; set; }

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

    }
}
