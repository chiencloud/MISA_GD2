using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns> Danh sách tất cả bản ghi </returns>
        public List<T> GetAllRecords();

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="record"> Thông tin bản ghi cần thêm mới </param>
        /// <returns> Số bản ghi bị tác động</returns>
        public int InsertOnRecord(T record);

        /// <summary>
        /// Cập nhập thông tin bản ghi
        /// </summary>
        /// <param name="record"> Thông tin bản ghi cần cập nhập </param>
        /// <param name="idRecord"> Id bản ghi cần cập nhập </param>
        /// <returns>Số bản ghi bị tác động</returns>
        public int UpdateOnRecord(T record, Guid idRecord);

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="idRecord"> id bản cần xóa </param>
        /// <returns>Số bản ghi bị tác động</returns>
        public int DeleteOnRecord(Guid idRecord);

        /// <summary>
        /// Lấy 1 bản ghi
        /// </summary>
        /// <param name="idRecord"> Id bản ghi cần lấy</param>
        /// <returns> Bản ghi <typeparamref name="T"/></returns>
        public T GetRecordById(Guid idRecord);
    }
}
