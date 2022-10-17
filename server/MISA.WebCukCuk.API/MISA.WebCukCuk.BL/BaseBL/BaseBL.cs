using MISA.WebCukCuk.Common.Attributes;
using MISA.WebCukCuk.Common.Entities.DTO;
using MISA.WebCukCuk.Common.Exceptions;
using MISA.WebCukCuk.Common.Resources;
using MISA.WebCukCuk.Common.Utilities;
using MISA.WebCukCuk.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {

        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi <typeparamref name="T"/>
        /// </summary>
        /// <returns>Danh sách các bản ghi <typeparamref name="T"/></returns>
        public List<T> GetAllRecords()
        {
            return _baseDL.GetAllRecord();
        }

        /// <summary>
        /// Lấy 1 bản ghi <typeparamref name="T"/>
        /// </summary>
        /// <param name="idRecord"> id bản ghi cần lấy </param>
        /// <returns> Bản ghi <typeparamref name="T"/> </returns>
        public T GetRecordById(Guid idRecord)
        {
            var record = _baseDL.GetRecordById(idRecord);
            // trả về lỗi nếu không lấy được bản ghi
            if (record == null)
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
            return record;
        }

        /// <summary>
        /// Thêm mới một bản ghi <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"> <typeparamref name="T"/> </param>
        /// <returns> Số bản ghi bị tác động </returns>
        public virtual int InsertOnRecord(T record)
        {
            var validate = ValidateData(record); // validate dữ liệu
            if(validate != null) // Có trường chưa được validate sẽ thông báo lỗi dữ liệu không hợp lệ
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
            return _baseDL.InsertOneRecord(record);
        }

        /// <summary>
        /// Cập nhập 1 bản ghi <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"> Thông tin bản ghi cần cập nhập <typeparamref name="T"/> </param>
        /// <param name="idRecord"> id bản ghi cần cập nhập </param>
        /// <returns> Số bản ghi bị tác động </returns>
        public int UpdateOnRecord(T record, Guid idRecord)
        {
            var validate = ValidateData(record); // validate dữ liệu update
            if(validate != null) // Có trường chưa được validate sẽ thông báo lỗi dữ liệu không hợp lệ
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
            int affected = _baseDL.UpdateOneRecord(record, idRecord); // Số bản ghi được update
            if (affected == 0) // Nếu = 0 thông báo bản ghi bị xóa hoặc không tồn tại
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
            return affected;
        }

        /// <summary>
        /// Xóa 1 bản ghi <typeparamref name="T"/>
        /// </summary>
        /// <param name="idRecord"> id bản ghi cần xóa </param>
        /// <returns> Số bản ghi bị tác động </returns>
        public int DeleteOnRecord(Guid idRecord)
        {
            int result = _baseDL.DeleteOneRecord(idRecord); // Số bản ghi được update
            if (result == 0)  // Nếu = 0 thông báo bản ghi bị xóa hoặc không tồn tại
            {
                string tableName = EntityUtilities.GetTableName<T>(); // Lây tên bảng
                
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
            return result;
        }

        /// <summary>
        /// Validate chung
        /// </summary>
        /// <param name="record"></param>
        /// <exception cref="MISAException"></exception>
        public List<string> ValidateData(T record)
        {
            bool isValid = true;
            List<string> errorUser = new List<string>();

            // Lấy các thuộc tính của record
            var props = record.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(record); // Value của thuộc tính
                // Lấy thuộc tính propName có trong
                var propName = prop.GetCustomAttributes(typeof(PropName), true).FirstOrDefault();
                var propNameValue = prop.Name;

                if (propName != null)
                    propNameValue = (propName as PropName).propName == string.Empty ? propNameValue : (propName as PropName).propName;
                var attrCustoms = prop.GetCustomAttributes(true); // Lấy danh sách các trường cần validate
                foreach (var attrCustom in attrCustoms)
                {
                    var typeofAttr = attrCustom.GetType();

                    // Validate bỏ trống
                    if (typeofAttr == typeof(NotEmpty))
                    {
                        if (string.IsNullOrEmpty(propValue.ToString()))
                        {
                            isValid = false;
                            errorUser.Add($"{propNameValue} \n");
                        }
                    }

                    // Validate ngày lớn hơn ngày hiện tại
                    if (typeofAttr == typeof(DateLongerNow) && propValue != null)
                    {
                        if ((DateTime)propValue > DateTime.Now)
                        {
                            isValid = false;
                            errorUser.Add($"{propNameValue} \n");
                        }
                    }

                    //Validate định dạng email
                    if (typeofAttr == typeof(ValiEmail) && propValue != null)
                    {
                        string regex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
                        var rx = new Regex(regex);
                        if (!rx.IsMatch(propValue.ToString()))
                        {
                            isValid = false;
                            errorUser.Add($"{propNameValue} \n");
                        }
                    }

                }
            }

            if (!isValid)
            {
                return errorUser;
            }
            return null;
        }

        /// <summary>
        /// Validate custom riêng
        /// </summary>
        /// <param name="record"></param>
        protected virtual void ValidateCustom(T record)
        {

        }
        #endregion
    }
}
