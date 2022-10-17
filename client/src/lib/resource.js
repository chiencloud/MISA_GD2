// Loại form
const TYPE_FORM = {
    ADD: 'add',
    UPDATE: 'update',
    DUPLICATE: 'duplicate',
}

// Kiểu form khi update
const TYPE_UPDATE = {
    ADD: 1,
    UPDATE: 2,
    DELETE: 3
}

// kiểu http request
const HTTP_METHOD = {
    GET: 'GET',
    PUT: 'PUT',
    POST: 'POST',
    DELETE: 'DELETE'
}

const ALERT_POPUP = {
    DataChange: 'Dữ liệu đã bị thay đổi. Bạn có muốn cất không?',
    AddSuccessMaterial: 'Thêm mới thành công nguyên vật liệu',
    AddUnitSuccess: 'Thêm mới thành công đơn vị tính',
    AddStockSuccess: 'Thêm mới thành công kho ngầm định',
    UpdateSuccessMaterial: 'Cập nhập thành công nguyên vật liệu',
    DeleteSuccess: 'Xóa thành công',
    DeleteFail: 'Xóa thất bại',
}

const ERROR_BE = {
    e000: 'Lỗi không xác định. Vui lòng liên hệ MISA',
    e001: 'Trùng mã nguyên vật liệu',
    e002: 'Trùng mã kho ngầm định',
    e003: 'Trùng tên đơn vị tính',
    e004: 'Trùng đơn vị chuyển đổi',
    e005: 'Bản ghi đã bị xóa hoặc không tồn tại'
}

// Danh sách lỗi validate frontend
const ERROR_VALIDATE_FE = {
    UnitNameNotEmpty: "Tên đợn vị tính không được bỏ trống",
    StockCodeNotEmpty: 'Mã kho không được bỏ trống',
    StockNameNotEmpty: 'Tên kho không được bỏ trống',
    MaterialNameNotEmpty: 'Tên nguyên vật liệu không được bỏ trống',
    MaterialCodeNotEmpty: 'Mã nguyên vật liệu không được bỏ trống',
    UnitIDNotEmpty: 'Đơn vị tính không được bỏ trống',
    DuplicateUnit: 'Đơn vị chuyển đổi không được trùng với đơn vị chính',
    DuplicateConversionUnitMaterial: 'Đơn vị tính không được trùng với đơn vị chuyển đổi',
    DuplicateConversionUnit: 'Đơn vị chuyển đổi không được trùng nhau',
    ConversionUnitNotEmpty: 'Thông tin đơn vị chuyển đổi không được bỏ trống',
    SelectMaterialDelete: 'Vui lòng chọn nguyên vật liệu muốn xóa',
    QuestionDeleteMaterial: 'Bạn có chắc chắn muốn xóa nguyên vật liệu',
    DeleteSuccess: 'Xóa thành công',
    DeleteFail: 'Xóa thất bại',
    RequireChoiceMaterial: 'Vui lòng chọn nguyên vật liệu để thực hiện chức năng'
}

// Các trường của material
const FIELD_MATERIAL = {
    tableName: 'material',
    materialID: 'materialID',
    materialCode: 'materialCode',
    materialName: 'materialName',
    feature: 'feature',
    unitID: 'unitID',
    unitName: 'unitName',
    stockID: 'stockID',
    stockName: 'stockName',
    categoryName: 'categoryName',
    valueExp: 'valueExp',
    typeExp: 'typeExp',
    inventoryNumber: 'inventoryNumber',
    description: 'description',
    status: 'status',
    createdBy: 'createdBy',
    createdDate: 'createdDate',
    modifiedBy: 'modifiedBy',
    modifiedDate: 'modifiedDate'
}

const FIELD_NAME_MATERIAL = {
    tableName: 'Nguyên vật liệu',
    materialID: 'Id nguyên vật liệu',
    materialCode: 'Mã nguyên vật liệu',
    materialName: 'Tên nguyên vật liệu',
    feature: 'Tính chất',
    unitID: 'Id đơn vị tính',
    unitName: 'Tên đơn vị tính',
    stockID: 'Id kho ngầm định',
    stockName: 'Tên kho ngầm định',
    categoryName: 'Nhóm nguyên vật liệu',
    valueExp: 'Giá trị hạn sử dụng',
    typeExp: 'Loại hạn sử dụng',
    inventoryNumber: 'Số lượng tồn tối thiểu',
    description: 'Ghi chú',
    status: 'Ngừng theo dõi',
    createdBy: 'Người tạo',
    createdDate: 'Ngày tạo',
    modifiedBy: 'Người sửa',
    modifiedDate: 'Ngày sửa'
}

// Các trường của đơn vị chuyển đổi
const FIELD_CONVERSION_UNIT = {
    tableName: 'conversionunit',
    stt: 'stt',
    conversionUnitID: 'conversionUnitID',
    materialID: 'materialID',
    conversionRate: 'conversionRate',
    calculation: 'calculation',
    conversionUnitIDOld: 'conversionUnitIDOld',
    createdBy: 'createdBy',
    createdDate: 'createdDate',
    modifiedBy: 'modifiedBy',
    modifiedDate: 'modifiedDate'
}

// Các trường của đơn vị tính
const FIELD_UNIT = {
    tableName: 'unit',
    conversionUnitID: 'conversionUnitID',
    conversionUnitName: 'conversionUnitName',
    createdBy: 'createdBy',
    createdDate: 'createdDate',
    modifiedBy: 'modifiedBy',
    modifiedDate: 'modifiedDate'
}

// Các trường của kho ngầm định
const FIELD_STOCK = {
    tableName: 'stock',
    stockID: 'stockID',
    stockCode: 'stockCode',
    stockName: 'stockName',
    createdBy: 'createdBy',
    createdDate: 'createdDate',
    modifiedBy: 'modifiedBy',
    modifiedDate: 'modifiedDate'
}

const ICON_TOAST_MESSAGE = {
    success: 'success',
    warning: 'warning',
    error: 'error',
}



export { 
    TYPE_FORM, 
    TYPE_UPDATE, 
    HTTP_METHOD,
    ALERT_POPUP,
    ERROR_BE,
    ERROR_VALIDATE_FE,
    FIELD_MATERIAL,
    FIELD_NAME_MATERIAL,
    FIELD_CONVERSION_UNIT,
    FIELD_UNIT,
    FIELD_STOCK,
    ICON_TOAST_MESSAGE
}