using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.Common.Entities.DTO;
using MISA.WebCukCuk.Common.Exceptions;
using MISA.WebCukCuk.Common.Resources;
using MISA.WebCukCuk.Common.Utilities;
using MISA.WebCukCuk.DL.BaseDL;
using MISA.WebCukCuk.DL.MaterialDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.MaterialBL
{
    public class MaterialBL : BaseBL<Material>, IMaterialBL
    {
        #region Field

        private IMaterialDL _materialDL;

        #endregion

        #region Constructor

        public MaterialBL(IMaterialDL materialDL) : base(materialDL)
        {
            materialDL = new MaterialDL();
            _materialDL = materialDL;
        }



        #endregion

        #region Method 

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="textSearch"> chuỗi search where </param>
        /// <param name="pageSize"> kích thước 1 trang </param>
        /// <param name="pageNumber"> vị trí trang </param>
        /// <param name="sort"> loại sắp xếp </param>
        /// <returns></returns>
        public PagingData<Material> GetPaging(string? filter, long pageSize, long pageNumber, string? sort)
        {
            string queryWhere = null;

            // Set sort theo ModifiedDate nếu k truyền
            if (string.IsNullOrEmpty(sort))
            {
                sort = $"{Resource.SortModifiedDateDESC}";
            }

            // Kiểm tra chuỗi filter rỗng
            if (!string.IsNullOrEmpty(filter))
            {
                // Danh sách các item cần query cho WHERE
                var listFilter = JsonSerializer.Deserialize<List<FilterPaging>>(filter);

                queryWhere = QueryCondition.GetQueryStringWhere(listFilter);
            }

            return _materialDL.GetPaging(queryWhere, pageSize, pageNumber, sort);
        }

        /// <summary>
        /// Lấy mã max 
        /// </summary>
        /// <param name="startCode">Bắt đầu mã</param>
        /// <returns> số lớn nhất bắt đầu với mã tương ứng </returns>
        public string GetNewCode(string code)
        {
            // số lớn nhất
            long numberMax = _materialDL.GetNewCode(code);

            return $"{code}{numberMax + 1}";
        }

        /// <summary>
        /// Thêm mới 1 nguyên vật liệu
        /// </summary>
        /// <param name="inforMaterial"></param>
        /// <returns></returns>
        public int InsertMaterial(InforMaterial inforMaterial)
        {
            var validateMaterial = ValidateData(inforMaterial.Material); // validate nguyên vật liệu
            List<string> validate = new List<string>();
            var baseConversionUnit  = new BaseBL<ConversionUnit>(new BaseDL<ConversionUnit>());
            // Validate nguyên vật liệu
            if (validateMaterial != null)
            {
                foreach (var item in validateMaterial)
                {
                    validate.Add(item);
                }
            }
            // Validate danh sách đơn vị chuyển đổi
            foreach (var item in inforMaterial.ListConversionUnits)
            {
                var validateConversionUnit = baseConversionUnit.ValidateData(item); // validate đơn vị chuyển đổi
                if(validateConversionUnit != null)
                {
                    foreach(var item2 in validateConversionUnit)
                    {
                        validate.Add(item2);
                    }    
                }
            }

            if(validate.Count > 0) // chưa validate
            { 
                throw new MISAException(
                    new ErrorResult(
                        Error.InvalidDataCode,
                        Error.InvalidData,
                        validate,
                        Error.InvalidData,
                        ""
                    )
                );
            }
           
            return _materialDL.InsertMaterial(inforMaterial);
        }

        /// <summary>
        /// Cập nhập thông tin 1 nguyên vật liệu
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="updateMaterial"></param>
        /// <returns></returns>
        public int UpdateMaterial(Guid materialID, InforMaterial updateMaterial)
        {
            return _materialDL.UpdateMaterial(materialID, updateMaterial);
        }

        /// <summary>
        /// Lấy một nguyên vật liệu theo id
        /// </summary>
        /// <param name="materialID"></param>
        /// <returns></returns>
        public InforMaterial GetMaterialByID(Guid materialID)
        {
            var data = _materialDL.GetMaterialByID(materialID); // bản ghi cần lấy 
            if(data == null) // Nếu null trả về lỗi bản ghi không tồn tại hoặc đã bị xóa
            {
                throw new MISAException(
                    new ErrorResult(
                        Error.NotExistOrDeleted,
                        Error.NotExistOrIsDeleted,
                        Error.NotExistOrIsDeleted,
                        Error.NotExistOrIsDeleted,
                        ""
                    )
                );
            }
            return data;
        }

        #endregion
    }
}
