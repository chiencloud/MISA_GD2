using Dapper;
using MISA.WebCukCuk.Common.Utilities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field

        // Khởi tạo chuỗi connect database
        protected const string CONNECT_STRING = "Server=localhost;Database=misa_cukcuk_ndchien;Uid=root;Pwd=260801;";

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi của một bảng</returns>
        public List<T> GetAllRecord()
        {
            string tableName = EntityUtilities.GetTableName<T>(); // Lấy tên bảng cần lấy bản ghi
            string getStoredProcedureName = $"Proc_{tableName}_GetAll"; // Tên procedure trong database để lấy tất cả bản ghi

            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                // Query
                var employeeGet = mySqlConnection.Query<T>(getStoredProcedureName, commandType: System.Data.CommandType.StoredProcedure).ToList();

                return employeeGet;
            }

        }

        /// <summary>
        /// Lấy một bản ghi theo mã
        /// </summary>
        /// <param name="idRecord"> id bản ghi cần lấy </param>
        /// <returns> Bản ghi cần lấy </returns>
        public T GetRecordById(Guid idRecord)
        {
            string tableName = EntityUtilities.GetTableName<T>(); // Lấy tên bảng
            string getStoredProcedureName = $"Proc_{tableName}_GetByID"; // Tên procedure trong database 

            //Truyền tham số cho procedure
            var parameters = new DynamicParameters();

            // lấy tên primary key của table 
            var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);

            var propertyName = $"v_{primaryKeyProperty.Name}";
            parameters.Add(propertyName, idRecord);

            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                // Query
                return mySqlConnection.QueryFirstOrDefault<T>(getStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record"> Thông tin bản ghi cần thêm mới</param>
        /// <returns>Số bản ghi được thêm mới</returns>
        public virtual int InsertOneRecord(T record)
        {
            string tableName = EntityUtilities.GetTableName<T>(); // Lây tên bảng
            string insertStoredProcedureName = $"Proc_{tableName}_Insert"; // Lấy tên procedure

            var properties = typeof(T).GetProperties(); // Lấy các thuộc tính của bảng
            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = $"v_{property.Name}"; // Tạo biến cho đầu vào cho store procedure
                var propertyValue = property.GetValue(record); // Lấy giá có thuộc tính propertyName của record
                parameters.Add(propertyName, propertyValue);
            }

            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                numberOfAffectedRow = mySqlConnection.Execute(insertStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return numberOfAffectedRow;
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record"> Thông tin bản ghi cần thêm mới</param>
        /// <returns>Số bản ghi được thêm mới</returns>
        public int InsertOneRecord(T record, MySqlTransaction transaction)
        {
            string tableName = EntityUtilities.GetTableName<T>(); // Lây tên bảng
            string insertStoredProcedureName = $"Proc_{tableName}_Insert"; // Lấy tên procedure

            var properties = typeof(T).GetProperties(); // Lấy các thuộc tính của bảng
            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = $"v_{property.Name}"; // Tạo biến cho đầu vào cho store procedure
                var propertyValue = property.GetValue(record); // Lấy giá có thuộc tính propertyName của record
                parameters.Add(propertyName, propertyValue);
            }

            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                numberOfAffectedRow = mySqlConnection.Execute(insertStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
            }
            return numberOfAffectedRow;
        }

        /// <summary>
        /// cập nhập thông tin 1 bản ghi
        /// </summary>
        /// <param name="record"> thông tin cần cập nhập</param>
        /// <param name="idRecord"> id bản ghi cần cập nhập</param>
        /// <returns> Số bản ghi bị tác động </returns>
        public int UpdateOneRecord(T record, Guid idRecord)
        {
            string tableName = EntityUtilities.GetTableName<T>(); // tên bảng
            string updateStoredProcedureName = $"Proc_{tableName}_Update"; // tên procedure

            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = $"v_{property.Name}"; // Tạo biến cho đầu vào cho store procedure
                var propertyValue = property.GetValue(record); // Lấy giá có thuộc tính propertyName của record
                parameters.Add(propertyName, propertyValue);
            }

            // set Id của bản ghi cập nhập
            //var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
            //parameters.Add($"v_{primaryKeyProperty[0].Name}", idRecord);

            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                // Số bản ghi bị tác động
                numberOfAffectedRow = mySqlConnection.Execute(updateStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return numberOfAffectedRow;
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="idRecord"> Id bản ghi cần xóa</param>
        /// <returns> Số bản ghi bị tác động</returns>
        public int DeleteOneRecord(Guid idRecord)
        {
            string tableName = EntityUtilities.GetTableName<T>(); // tên bảng
            string updateStoredProcedureName = $"Proc_{tableName}_Delete"; // tên procedure

            // lấy khóa chính của bảng
            var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
            var parameters = new DynamicParameters();

            var propertyName = $"v_{primaryKeyProperty.Name}";
            parameters.Add(propertyName, idRecord);

            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(CONNECT_STRING))
            {
                // Số bản ghi bị tác động
                numberOfAffectedRow = mySqlConnection.Execute(updateStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return numberOfAffectedRow;
        }

        #endregion
    }
}
