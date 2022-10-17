using MISA.WebCukCuk.Common.Resources;
using MISA.WebCukCuk.Common.Utilities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Errors
{
    public static class HandleError
    {
        #region Method

        /// <summary>
        /// Sinh ra đối tượng lỗi trả về khi gặp exception
        /// </summary>
        /// <param name="exception">Đối tượng exception gặp phải</param>
        /// <param name="httpContext">Context khi gọi API sử dụng để lấy được traceId</param>
        /// <returns>Đối tượng chứa thông tin lỗi trả về cho client</returns>
        /// Created by: NDCHIEN (14/09/2022)
        public static ErrorResult? GenerateExceptionResult(Exception exception)
        {
            return new ErrorResult(
                Error.NoDefine,
                exception.Message,
                exception.Message,
                "https://openapi.misa.com.vn/errorcode/e002",
                "");
        }

        /// <summary>
        /// Sinh ra đối tượng lỗi trả về khi gặp lỗi trùng mã
        /// </summary>
        /// <param name="exception">Đối tượng exception gặp phải</param>
        /// <param name="httpContext">Context khi gọi API sử dụng để lấy được traceId</param>
        /// <returns>Đối tượng chứa thông tin lỗi trả về cho client</returns>
        /// Created by: NDCHIEN (14/09/2022)
        public static ErrorResult? GenerateDuplicateCodeErrorResult<T>(MySqlException mySqlException)
        {
            if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                var tableName = EntityUtilities.GetTableName<T>();
                var errorCode = Error.NoDefine;

                // Trùng mã nguyên vật liệu
                if (tableName == TableName.Material)
                {
                    errorCode = Error.DuplicateMaterialCode;
                }
                // Trùng mã kho ngầm định
                else if (tableName == TableName.Stock)
                {
                    errorCode = Error.DuplicateStockCode;
                }    
                // Trùng tên đơn vị tính
                else if(tableName == TableName.Unit)
                {
                    errorCode = Error.DuplicateUnitName;
                }
                // Trùng tên đơn vị chuyển đổi
                else if(tableName == TableName.ConversionUnit)
                {
                    errorCode = Error.DuplicateConversionUnit;
                }


                return new ErrorResult(
                    errorCode,
                    Error.DuplicateCode,
                    Error.DuplicateCode,
                    "https://openapi.misa.com.vn/errorcode/e003",
                    "");
            }

            return new ErrorResult(
                Error.NoDefine,
                Error.AnUnknownError,
                Error.AnUnknownError,
                "https://openapi.misa.com.vn/errorcode/e002",
                "");
        }

        #endregion
    }
}
