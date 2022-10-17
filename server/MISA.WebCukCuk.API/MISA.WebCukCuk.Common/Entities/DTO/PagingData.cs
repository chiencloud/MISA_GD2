using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Entities.DTO
{
    /// <summary>
    /// Dữ liệu trả về khi lọc phân trang
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
    public class PagingData<T>
    {
        #region Field

        /// <summary>
        /// Danh sách các dữ liệu trả về 
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public long TotalCount { get; set; } = 0;

        #endregion
    }
}
