using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebCukCuk.API.NTier.BaseControllers;
using MISA.WebCukCuk.BL.MaterialBL;
using MISA.WebCukCuk.Common.Entities;
using MISA.WebCukCuk.Common.Entities.DTO;
using MISA.WebCukCuk.Common.Errors;
using MISA.WebCukCuk.Common.Exceptions;
using MySqlConnector;

namespace MISA.WebCukCuk.API.NTier.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        #region Field

        private IMaterialBL _materialBL;

        #endregion

        #region Constructor
        
        public MaterialsController(IMaterialBL materialBL)
        {
            _materialBL = materialBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult GetPaging(string? filter, long pageSize = 20, long pageNumber = 1, string? sort = "ModifiedDate DESC")
        {
            try
            {
                var listMaterialFilter = _materialBL.GetPaging(filter, pageSize, pageNumber, sort); // danh sách bản ghi lọc phân trang
                return StatusCode(StatusCodes.Status200OK, listMaterialFilter);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<Material>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Lấy mã nguyên vật liệu lớn nhất
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetNewCode")]       
        public IActionResult GetNewCode(string code)
        {
            try
            {
                var newCode = _materialBL.GetNewCode(code); // mã code mới
                return StatusCode(StatusCodes.Status200OK, newCode);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<Material>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Thêm mới một nguyên vật liệu
        /// </summary>
        /// <param name="inforMaterial"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertOneRecord([FromBody] InforMaterial inforMaterial)
        {
            try
            {
                var insert = _materialBL.InsertMaterial(inforMaterial); // số bản ghi được insert
                return StatusCode(StatusCodes.Status200OK, insert);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<Material>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Xóa nguyên vật liệu
        /// </summary>
        /// <param name="idRecord"></param>
        /// <returns></returns>
        [HttpDelete("{idRecord}")]
        public IActionResult DeleteOne([FromRoute] Guid idRecord)
        {
            try
            {
                int countDelete = _materialBL.DeleteOnRecord(idRecord); // số bản ghi bị xóa
                return StatusCode(StatusCodes.Status200OK, countDelete);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<Material>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Cập nhập thông tin 1 bản ghi
        /// </summary>
        /// <param name="materialID"></param>
        /// <param name="updateMaterial"></param>
        /// <returns></returns>
        [HttpPut("{materialID}")]
        public IActionResult UpdateOne([FromRoute] Guid materialID, InforMaterial updateMaterial)
        {
            try
            {
                int countUpdate = _materialBL.UpdateMaterial(materialID, updateMaterial); // số bản ghi được update
                return StatusCode(StatusCodes.Status200OK, countUpdate);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<Material>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }

        /// <summary>
        /// Lấy 1 nguyên vật liệu theo id
        /// </summary>
        /// <param name="materialID"></param>
        /// <returns></returns>
        [HttpGet("{materialID}")]
        public IActionResult GetByID([FromRoute] Guid materialID)
        {
            try
            {
                var inforMaterial = _materialBL.GetMaterialByID(materialID);
                return StatusCode(StatusCodes.Status200OK, inforMaterial);
            }
            catch (MISAException misaEx)
            {
                return StatusCode(StatusCodes.Status400BadRequest, misaEx.ErrorResult);
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult<Material>(mySqlException));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, HandleError.GenerateExceptionResult(exception));
            }
        }


        #endregion
    }
}
