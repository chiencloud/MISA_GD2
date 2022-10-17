using MISA.WebCukCuk.Common.Attributes;
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
    [Table("material")]
    public class Material
    {
        /// <summary>
        /// Id nguyên vật liệu
        /// </summary>
        [Key]
        [PropName("ID nguyên vật liệu")]
        [NotEmpty]
        public Guid MaterialID { get; set; }

        /// <summary>
        /// Mã nguyên vật liệu
        /// </summary>
        [PropName("Mã nguyên vật liệu")]
        [NotEmpty]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Tên nguyên vật liệu
        /// </summary>
        [PropName("Tên nguyên vật liệu")]
        [NotEmpty]
        public string MaterialName { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public string? ExpiryDate { get; set; }

        /// <summary>
        /// Mã đơn vị tính
        /// </summary>
        [PropName("ID đơn vị tính")]
        [NotEmpty]
        public Guid UnitID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [PropName("Tên đơn vị tính")]
        public string UnitName { get; set; }

        /// <summary>
        /// Id nhóm nguyên vật liệu
        /// </summary>
        public Guid? CategoryID { get; set; }

        /// <summary>
        /// Tên nhóm nguyên vật liệu
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// ID kho ngầm định
        /// </summary>
        public Guid? StockID { get; set; }

        /// <summary>
        /// Tên kho nhầm định
        /// </summary>
        public string? StockName { get; set; }

        /// <summary>
        /// Tính chất
        /// </summary>
        public string? Feature { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// </summary>
        public double? InventoryNumber { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Trạng thái theo dõi
        /// </summary>
        public StatusFollow? Status { get; set; }

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
