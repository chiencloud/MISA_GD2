using Dapper;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.Common.Entities.DTO;
using MISA.WebCukCuk.Common.Resources;
using MISA.WebCukCuk.Common.Utilities;
using MISA.WebCukCuk.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.WebCukCuk.DL.MaterialDL
{
    public class MaterialDL : BaseDL<Material>, IMaterialDL
    {

        #region Method
        /// <summary>
        /// Lấy mã max 
        /// </summary>
        /// <param name="startCode">Bắt đầu mã</param>
        /// <returns> số lớn nhất bắt đầu với mã tương ứng </returns>
        public long GetNewCode(string startCode)
        {
            string queryGetNewCode = $"{StoreProcedure.GetNewCodeMaterial}";

            var parameter = new DynamicParameters();
            parameter.Add("v_code", startCode);

            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                var resultsSQL = mySqlConnection.QueryFirstOrDefault(queryGetNewCode, parameter, commandType: System.Data.CommandType.StoredProcedure);
                var result = resultsSQL.MaterialCodeMax;
                if (result == null)
                    return 0;
                return result;
            }
        }

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="textSearch"> chuỗi search where </param>
        /// <param name="pageSize"> kích thước 1 trang </param>
        /// <param name="pageNumber"> vị trí trang </param>
        /// <param name="sort"> loại sắp xếp </param>
        /// <returns></returns>
        public PagingData<Material> GetPaging(string? textSearch, long pageSize, long pageNumber, string? sort)
      {
            string queryGetDepartments = $"{StoreProcedure.GetPagingMaterial}";

            var parameter = new DynamicParameters();
            parameter.Add("$Offset", (pageNumber - 1) * pageSize);
            parameter.Add("$Limit", pageSize);
            parameter.Add("$Sort", sort);
            parameter.Add("$Where", textSearch);
            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                var multiResults = mySqlConnection.QueryMultiple(queryGetDepartments, parameter, commandType: System.Data.CommandType.StoredProcedure);
                var materials = multiResults.Read<Material>().ToList();
                var totalCount = multiResults.Read<long>().Single();
                return new PagingData<Material>
                {
                    Data = materials,
                    TotalCount = totalCount
                };
            }
        }

        /// <summary>
        /// Thêm mới nguyên vật liệu
        /// </summary>
        /// <param name="inforMaterial"></param>
        /// <param name="listConversionUnit"></param>
        /// <returns></returns>
        public int InsertMaterial(InforMaterial inforMaterial)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
                {
                    mySqlConnection.Open();
                    int totalSuccess = 0;
                    var baseDLConversionUnit = new BaseDL<ConversionUnit>();
                    totalSuccess += InsertOneRecord(inforMaterial.Material);

                    if (inforMaterial.ListConversionUnits != null)
                    {
                        foreach (var conversionUnit in inforMaterial.ListConversionUnits)
                        {
                            totalSuccess += baseDLConversionUnit.InsertOneRecord(conversionUnit);
                        }
                    }
                    scope.Complete();
                    return totalSuccess;

                }
            }
        }

        /// <summary>
        /// Cập nhập thông tin 1 nguyên vật liệu
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="updateMaterial"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int UpdateMaterial(Guid materialID, InforMaterial updateMaterial)
        {
            int totalSuccess = 0;
            var baseDLConversionUnit = new BaseDL<ConversionUnit>();
            string deleteStoredProcedureName = $"{StoreProcedure.DeleteConversionUnit}";
            string updateStoredProcedureName = $"{StoreProcedure.UpdateConversionUnit}";
            string addStoredProcedureName = $"{StoreProcedure.UpdateConversionUnit}";

            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                mySqlConnection.Open();

                totalSuccess += UpdateOneRecord(updateMaterial.Material, materialID);

                    
                foreach (var conversionUnit in updateMaterial.ListConversionUnits)
                {

                    var properties = typeof(ConversionUnit).GetProperties();
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var propertyName = $"v_{property.Name}"; // Tạo biến cho đầu vào cho store procedure
                        var propertyValue = property.GetValue(conversionUnit); // Lấy giá có thuộc tính propertyName của record
                        parameters.Add(propertyName, propertyValue);
                    }

                    // Xóa đơn vị chuyển đổi
                    if (conversionUnit.Type == Common.Enums.Type.Delete)
                    {
                        totalSuccess += mySqlConnection.Execute(deleteStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    }
                    // Sửa đơn vị chuyển đổi
                    else if(conversionUnit.Type == Common.Enums.Type.Update)
                    {
                        totalSuccess += mySqlConnection.Query(updateStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList()[0].Count;
                    }
                    // Thêm mới đơn vị chuyển đổi
                    else if(conversionUnit.Type == Common.Enums.Type.Add)
                    {
                        totalSuccess += baseDLConversionUnit.InsertOneRecord(conversionUnit);
                    }
                }

                return totalSuccess;
            }
        }

        public InforMaterial GetMaterialByID(Guid materialID)
        {
            string getStoredProcedureName = $"{StoreProcedure.GetMaterialByID}";
            //string getStoredProcedureName = $"Proc_material_GetByID";

            //Truyền tham số cho procedure
            var parameters = new DynamicParameters();
            parameters.Add("v_MaterialID", materialID);

            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                var multiResults = mySqlConnection.QueryMultiple(getStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var materials = multiResults.Read<Material>().ToList();
                if(materials.Count > 0)
                {
                    var listConversionUnit = multiResults.Read<ConversionUnit>().ToList();
                    return new InforMaterial
                    {
                        Material = materials[0],
                        ListConversionUnits = listConversionUnit
                    };
                }
                return null;
            }

        }
        #endregion


    }
}
