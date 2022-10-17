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
    [Table("stock")]
    public class Stock
    {
        /// <summary>
        /// Id kho ngầm định
        /// </summary>
        [Key]
        [PropName("ID kho ngầm định")]
        [NotEmpty]
        public Guid StockID { get; set; }

        /// <summary>
        /// Mã kho ngầm định
        /// </summary>
        [PropName("Mã kho ngầm định")]
        [NotEmpty]
        public string StockCode { get; set; }

        /// <summary>
        /// Tên kho ngầm định
        /// </summary>
        [PropName("Tên kho ngầm định")]
        [NotEmpty]
        public string StockName { get; set; }

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
