using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.MaterialBL
{
    public interface IMaterialBL: IBaseBL<Material>
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="textSearch"> chuỗi search where </param>
        /// <param name="pageSize"> kích thước 1 trang </param>
        /// <param name="pageNumber"> vị trí trang </param>
        /// <param name="sort"> loại sắp xếp </param>
        /// <returns></returns>
        public PagingData<Material> GetPaging(string? textSearch, long pageSize, long pageNumber, string? sort);

        /// <summary>
        /// Lấy mã max 
        /// </summary>
        /// <param name="startCode">Bắt đầu mã</param>
        /// <returns> số lớn nhất bắt đầu với mã tương ứng </returns>
        public string GetNewCode(string code);

        /// <summary>
        /// Thêm mới nguyên vật liệu
        /// </summary>
        /// <param name="material"></param>
        /// <param name="listConversionUnit"></param>
        /// <returns></returns>
        public int InsertMaterial(InforMaterial inforMaterial);

        /// <summary>
        /// Cập nhập nguyên vật liệu
        /// </summary>
        /// <param name="inforMaterial"></param>
        /// <returns></returns>
        public int UpdateMaterial(Guid materialID , InforMaterial updateMaterial);

        /// <summary>
        /// Lấy nguyên liệu theo id
        /// </summary>
        /// <param name="materialID"></param>
        /// <returns></returns>
        public InforMaterial GetMaterialByID(Guid materialID);

    }

}
