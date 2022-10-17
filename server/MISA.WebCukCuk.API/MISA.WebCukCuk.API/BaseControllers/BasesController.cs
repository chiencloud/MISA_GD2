using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.Common.Errors;
using MISA.WebCukCuk.Common.Exceptions;
using MySqlConnector;
using System;

namespace MISA.WebCukCuk.API.NTier.BaseControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Danh sách các bản ghi </returns>
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var listRecord = _baseBL.GetAllRecords(); // Danh sách tất cả bản ghi
                return
                    StatusCode(StatusCodes.Status200OK, listRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(ex));

            }
        }

        /// <summary>
        /// Lấy 1 bản ghi 
        /// </summary>
        /// <param name="idRecord"> id bản ghi cần lấy </param>
        /// <returns> Bản ghi </returns>
        [HttpGet("{idRecord}")]
        public IActionResult GetRecordById(Guid idRecord)
        {
            try
            {
                var reordGetByID = _baseBL.GetRecordById(idRecord); // thông tin bản ghi cần lấy
                return StatusCode(StatusCodes.Status200OK, reordGetByID);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest,misaEx.ErrorResult);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"> <typeparamref name="T"/> </param>
        /// <returns> Số bản ghi bị tác động </returns>
        [HttpPost]
        public virtual IActionResult InsertOneRecord([FromBody] T record)
        {
            try
            {
                int insertRecord = _baseBL.InsertOnRecord(record); // số bản ghi được insert
                return StatusCode(StatusCodes.Status201Created, insertRecord);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest,misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<T>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Cập nhập 1 bản ghi 
        /// </summary>
        /// <param name="record"> Thông tin bản ghi cần cập nhập <typeparamref name="T"/> </param>
        /// <param name="idRecord"> id bản ghi cần cập nhập </param>
        /// <returns> Số bản ghi bị tác động </returns>
        [HttpPut("{idRecord}")]
        public IActionResult UpdateOnRecord([FromBody] T record, [FromRoute] Guid idRecord)
        {
            try
            {
                int countUpdate = _baseBL.UpdateOnRecord(record, idRecord); // số bản ghi được cập nhập
                return StatusCode(StatusCodes.Status200OK, countUpdate);

            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<T>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="idRecord"> id bản ghi cần xóa </param>
        /// <returns> Số bản ghi bị tác động </returns>
        [HttpDelete("{idRecord}")]
        public IActionResult DeleteOneRecord([FromRoute] Guid idRecord)
        {
            try
            {
                int countDelete = _baseBL.DeleteOnRecord(idRecord); // số bản ghi được xóa
                return StatusCode(StatusCodes.Status200OK, countDelete);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        #endregion

    }
}
