/**
 * Mã lỗi sử lý bên frontend 
 * CreatedBy: NDCHIEN (18/8/2022)
 */
const ERROR_CODE = [
    // Mã lỗi không xác định phải để đầu tiên 
    {
        noDefine: "Lỗi không xác định. Vui lòng liên hệ MISA để được hỗ trợ",
    },
    {
        e001: 'Mã nhân viên đã tồn tại',
        fieldValidate: "employeeCode"
    },
    {
        e002: 'Mã không được bỏ trống',
        fieldValidate: "employeeCode"
    },
    {
        e003: 'Tên không được bỏ trống',
        fieldValidate: "employeeName"
    },
    {
        e004: 'Đơn vị không được bỏ trống',
        fieldValidate: "departmentID"
    },
]

export default ERROR_CODE;