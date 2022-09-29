/**
 * Chuỗi call API employee
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
function stringApiEmployee() {
    let urlMainEmployee = `${process.env.VUE_APP_URL_API}Employees`

    /**
     * Tạo chuỗi call API thêm mới nhân viên, lấy toàn bộ danh sách nhân viên
     */
    String.prototype.addEmployee = function () {
        return `${urlMainEmployee}/`;
    };

    /**
     * Tạo chuỗi call API cập nhập thông tin 1 nhân viên
     * @param {string} idEmployee
     */
    String.prototype.updateEmployee = function (idEmployee) {
        return `${urlMainEmployee}/${idEmployee}`;
    };

    /**
     * Tạo chuỗi call API chuỗi filter employee
     * @param {int} pageSize
     * @param {int} pageNumber
     * @param {string} textSearch
     */
    String.prototype.filterEployeeAPI = function (pageSize, pageNumber, textSearch = '') {
        return `${urlMainEmployee}/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&textSearch=${textSearch}`;
    };

    /**
     * Tạo chuỗi call API tạo mới mã nhân viên
     */
    String.prototype.newEmployeeCode = function () {
        return `${urlMainEmployee}/NewEmployeeCode`;
    };

    /**
     * Tạo chuỗi call API xuất file excel danh sách nhân viên
     */
    String.prototype.getFileListEmployee = function () {
        return `${urlMainEmployee}/FileListEmployee`;
    };

    /**
     * Tạo chuỗi call API xóa 1 nhân viên
     * @param {string} idEmployeeDel
     */
    String.prototype.deleteOneEmployee = function (idEmployeeDel) {
        return `${urlMainEmployee}/${idEmployeeDel}`;
    };

    /**
     * Tạo chuỗi call API xóa nhiều nhân viên
     */
    String.prototype.deleteMultipleEmployee = function () {
        return `${urlMainEmployee}/DeleteMultipleEmployee`;
    };
}

/**
 * Chuỗi call API department
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
function stringApiDepartment() {
    let urlMainDepartment = `${process.env.VUE_APP_URL_API}Departments`

    /**
     * Tạo chuỗi call API thêm mới đơn vị, lấy toàn bộ danh sách đơn vị
     */
     String.prototype.getAllDepartment = function () {
        return `${urlMainDepartment}`;
    };
}
export { stringApiEmployee, stringApiDepartment}