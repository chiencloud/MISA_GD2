/**
 * Chuỗi call API employee
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
function stringApiMaterial() {
    let urlMainMaterial = `${process.env.VUE_APP_URL_API}Materials`

    /**
     * Tạo chuỗi call API thêm mới nhân viên, lấy toàn bộ danh sách nhân viên
     */
    String.prototype.addMaterial = function () {
        return `${urlMainMaterial}/`;
    };

    /**
     * Tạo chuỗi call API cập nhập thông tin 1 nhân viên
     * @param {string} idMaterial
     */
    String.prototype.updateMaterial = function (idMaterial) {
        return `${urlMainMaterial}/${idMaterial}`;
    };

    /**
     * Tạo chuỗi call API chuỗi filter Material
     * @param {int} pageSize
     * @param {int} pageNumber
     * @param {string} textSearch
     */
    String.prototype.filterMaterialAPI = function (pageSize, pageNumber, textSearch = '', sort = "") {
        return `${urlMainMaterial}/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&filter=${textSearch}&sort=${sort}`;
    };

    /**
     * Tạo chuỗi call API tạo mới mã nhân viên
     */
    String.prototype.newMaterialCode = function (code) {
        return `${urlMainMaterial}/GetNewCode?code=${code}`;
    };

    /**
     * Tạo chuỗi call API xuất file excel danh sách nhân viên
     */
    String.prototype.getFileListMaterial = function () {
        return `${urlMainMaterial}/FileListMaterial`;
    };

    /**
     * Tạo chuỗi call API xóa 1 nhân viên
     * @param {string} idMaterialDel
     */
    String.prototype.deleteOneMaterial = function (idMaterialDel) {
        return `${urlMainMaterial}/${idMaterialDel}`;
    };

    /**
     * Tạo chuỗi call API xóa nhiều nhân viên
     */
    String.prototype.deleteMultipleMaterial = function () {
        return `${urlMainMaterial}/DeleteMultipleMaterial`;
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

/**
 * Chuỗi call API unit
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
function stringApiUnit() {
    let urlMainUnit = `${process.env.VUE_APP_URL_API}Units`

    /**
     * Tạo chuỗi call API thêm mới đơn vị, lấy toàn bộ danh sách đơn vị
     */
    String.prototype.getAllUnit = function () {
        return `${urlMainUnit}`
    }

}

/**
 * Chuỗi call API Stock
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
 function stringApiStock() {
    let urlMainStock = `${process.env.VUE_APP_URL_API}Stocks`

    /**
     * Tạo chuỗi call API thêm mới đơn vị, lấy toàn bộ danh sách đơn vị
     */
    String.prototype.getAllStock = function () {
        return `${urlMainStock}`
    }

}


export { stringApiMaterial, stringApiDepartment, stringApiUnit, stringApiStock}