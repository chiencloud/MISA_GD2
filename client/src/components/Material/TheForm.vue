<template>
    <div class="form_material">
        <BaseLoading v-if="loadingForm" type="full" />
        <BasePopUp
            v-if="popup.isShowPopup"
            :message="popup.messagePopup"
            :type="popup.typePopup"
            @closePopup="customPopup"
            @processWhenClose="popup.callbackFun.callbackProcessWhenClose"
            @clickWarningDeleteYes="popup.callbackFun.callbackWarningDeleteYes"
            @confirmPopup="popup.callbackFun.callbackConfirmPopup"
            @clickQuestionNo="popup.callbackFun.callbackQuestionNo"
            @clickQuestionYes="popup.callbackFun.callbackQuestionYes"
        />
        <BaseNotice v-if="isShowFormUnit" @closeNotice="closeUnit">
            <template #title>
                Thêm đơn vị tính
            </template>
            <template #content>
                <div class="content_add_unit">
                    <div class="add_unit_item">
                        <div class="add_unit_name">
                            Đơn vị tính <span style="color: red">(*)</span>
                        </div>    
                        <div tabindex="0" @focus="focusItemForm(listRef.btnCancelPopupUnit)"></div>
                        <BaseInput 
                            style="width: 300px"
                            :fieldName="FIELD_UNIT.conversionUnitName"
                            :ref="listRef.addConversionUnitName"
                            :messError="ERROR_VALIDATE_FE.UnitNameNotEmpty"
                            required
                            @onInput="getDataAddUnit"
                        />
                    </div>
                    <div class="add_unit_func">
                        <div>
                            <BaseButtonIcon icon="fa-solid  fa-circle-question" colorIcon="#2281c1" val="Giúp"/>
                        </div>
                        <div>
                            <BaseButtonIcon icon="fa-solid fa-file-export" colorIcon="#2281c1" styleCss="margin-right: 5px" val="Cất" @click="addUnit"/>
                            <BaseButtonIcon icon="fa-regular fa-circle-xmark" :ref="listRef.btnCancelPopupUnit" colorIcon="red" val="Hủy bỏ"  @click="closeUnit"/>
                            <div tabindex="0" @focus="focusItemForm(listRef.addConversionUnitName)"></div>
                        </div>
                    </div>
                </div>
            </template>
        </BaseNotice>
        
        <BaseNotice v-show="isShowFormStock" @closeNotice="closeStock">
            <template #title>
                Kho ngầm định
            </template>
            <template #content>
                <div class="content_add_unit">
                    <div class="add_unit_list">
                        <div class="add_unit_item">
                            <div tabindex="0" @focus="focusItemForm(listRef.btnCancelPopupStock)"></div>
                            <div class="add_unit_name">
                                Mã kho <span style="color: red">(*)</span>
                            </div>    
                            <BaseInput 
                                style="width: 300px" 
                                :fieldName="FIELD_STOCK.stockCode"
                                :ref="listRef.addStockCode"
                                :messError="ERROR_VALIDATE_FE.StockCodeNotEmpty"
                                required
                                @onInput="getDataAddStock"
                            />
                        </div>
                        <div class="add_unit_item">
                            <div class="add_unit_name">
                                Tên kho <span style="color: red">(*)</span>
                            </div>    
                            <BaseInput 
                                style="width: 300px" 
                                :fieldName="FIELD_STOCK.stockName"
                                :ref="listRef.addStockName"
                                :messError="ERROR_VALIDATE_FE.StockNameNotEmpty"
                                required
                                @onInput="getDataAddStock"
                            />
                        </div>
                    </div>
                    <div class="add_unit_func">
                        <div>
                            <BaseButtonIcon icon="fa-solid  fa-circle-question" colorIcon="#2281c1" val="Giúp"/>
                        </div>
                        <div>
                            <BaseButtonIcon icon="fa-solid fa-file-export" colorIcon="#2281c1" styleCss="margin-right: 5px" val="Cất" @click="addStock"/>
                            <BaseButtonIcon icon="fa-regular fa-circle-xmark" :ref="listRef.btnCancelPopupStock" colorIcon="red" val="Hủy bỏ"  @click="closeStock"/>
                            <div tabindex="0" @focus="focusItemForm(listRef.addStockCode)"></div>
                        </div>
                    </div>
                </div>
            </template>
        </BaseNotice>
        <div class="form_detail" @keydown="onKeyDownFormMain">
            <div class="form_title">
                <div class="title_left">{{type != 'update' ? 'Thêm' : 'Sửa'}} nguyên vật liệu</div>
                <div class="title_right">
                    <div><i class="fa-solid fa-xmark" @click="closeFormMain"></i></div>
                </div>
            </div>
            <div class="detail">
                <div class="info">
                    <div class="info_container_1">
                        <div class="info_material">
                            <div :ref="listRef.tabFirstFormMain" tabindex="0" @focus="focusItemForm(listRef.btnCancel)"></div>
                            <div class="info_name">Tên<span><span style="color: red">(*)</span></span></div>
                            <BaseInput 
                                style="width: 300px" 
                                :fieldName="FIELD_MATERIAL.materialName"
                                :ref="listRef.materialName"
                                :messError="ERROR_VALIDATE_FE.MaterialNameNotEmpty"
                                required
                                v-model:value="dataFormDetail.material.materialName"
                                @onInput="getData"
                                @getValue="changeMaterialName"
                            />
                        </div>
                        <div class="info_material">
                            <div class="info_name">Mã<span><span style="color: red">(*)</span></span></div>
                            <BaseInput 
                                style="width: 300px" 
                                :fieldName="FIELD_MATERIAL.materialCode"
                                :ref="listRef.materialCode"
                                :messError="ERROR_VALIDATE_FE.MaterialCodeNotEmpty"
                                required
                                v-model:value="dataFormDetail.material.materialCode"
                                @onInput="getData"
                            />
                        </div>
                        <div class="info_material">
                            <div class="info_name" title="Đơn vị tính">ĐVT<span><span style="color: red">(*)</span></span></div>
                            
                            <BaseSelectBox
                                :data="listUnit"
                                v-model:setData="dataFormDetail.material.unitID"
                                addSelect
                                addSelectIcon="fa-solid fa-plus"
                                addSelectColor="#2281c1"
                                width="100px"
                                :ref="listRef.conversionUnitID"
                                :fieldNameShow="FIELD_UNIT.conversionUnitName"
                                :fieldNameValue="FIELD_UNIT.conversionUnitID"
                                :fieldName="FIELD_MATERIAL.unitID"
                                :messError="ERROR_VALIDATE_FE.UnitIDNotEmpty"
                                required
                                :fieldListHide="[
                                    { field: 'conversionUnitName' },
                                ]"
                                styleValHide="top: calc(100% + 5px)"
                                @showBoxAddSelect="showBoxAddUnit('conversionUnitID', dataFormDetail.material, 'unitID')"
                                @getValue="getData"
                            />
                        </div>
                        <div class="info_material">
                            <div class="info_name">Kho ngầm định</div>
                            <BaseSelectBox
                                :data="listStock"
                                addSelect
                                v-model:setData="dataFormDetail.material.stockID"
                                addSelectIcon="fa-solid fa-plus"
                                addSelectColor="#2281c1"
                                width="200px"
                                :ref="listRef.stockID"
                                :fieldNameShow="FIELD_STOCK.stockName"
                                :fieldNameValue="FIELD_STOCK.stockID"
                                :fieldName="FIELD_MATERIAL.stockID"
                                :fieldListHide="[
                                    { field: 'stockCode', title: 'Mã kho' },
                                    { field: 'stockName', title: 'Tên kho' },
                                ]"
                                focusFirstData
                                styleValHide="top: calc(100% + 5px)"
                                @showBoxAddSelect="showBoxAddStock"
                                @getValue="getData"
                            />
                        </div>
                        <div class="info_material">
                            <div class="info_name">Hạn sử dụng</div>
                            <BaseInput 
                                style="width: 100px" 
                                :fieldName="FIELD_MATERIAL.valueExp"
                                textRight
                                type="int"
                                :ref="listRef.valueExp"
                                v-model:value="dataFormDetail.material.valueExp"
                                @onInput="getData"
                            />
                            <div style="width: 7px"></div>
                            <BaseSelectBox
                                :data="[
                                    { id: 1, value: 'Ngày'},
                                    { id: 2, value: 'Tháng'},
                                    { id: 3, value: 'Năm'},
                                ]"
                                v-model:setData="dataFormDetail.material.typeExp"
                                fieldNameShow="value"
                                fieldNameValue="id"
                                fieldName="NumberWrite"
                                :fieldListHide="[
                                    { field: 'value' },
                                ]"
                                focusFirstData
                                styleValHide="top: calc(100% + 5px)"
                            />
                        </div>
                        <div class="info_material">
                            <div class="info_name" title="Số lượng tồn tối thiểu">SL tồn tối thiểu</div>
                            <BaseInput 
                                style="width: 205px" 
                                :fieldName="FIELD_MATERIAL.inventoryNumber"
                                type="float"
                                textRight
                                lengthAfterComma="2"
                                v-model:value="dataFormDetail.material.inventoryNumber"
                                @onInput="getData"
                            />
                        </div>
                    </div>
                    <div class="info_material info_w100">
                        <div class="info_name" title="Mô tả">Mô tả</div>
                        <BaseTextArea 
                            :fieldName="FIELD_MATERIAL.description"
                            v-model:value="dataFormDetail.material.description"
                            @onInput="getData"
                        />
                    </div>
                </div>
                <div class="info_w100 info_unit">
                    <div class="info_title_unit">
                        <span>Đơn vị chuyển đổi</span>
                    </div>
                    <div class="table_unit">
                        <BaseTable 
                            :dataTable="dataTable"
                            :inforTable="inforTable"
                            hideColFixed
                            class="table_unit_info"
                            @clickTrTable="getRowConversionUnit"
                        >
                            <template #stt="{index}">
                                {{index + 1}}
                            </template>
                            <template #selectBox="{data, index}">
                                <BaseSelectBox
                                    :data="listUnit"
                                    :id="data.stt"
                                    :ref="`${listRef.tableConversionUnit}${index}`"
                                    addSelect
                                    addSelectIcon="fa-solid fa-plus"
                                    addSelectColor="#2281c1"
                                    :style="`width: 120px;`"
                                    showBorder
                                    :setData="data.conversionUnitID"
                                    :fieldNameShow="FIELD_UNIT.conversionUnitName"
                                    :fieldNameValue="FIELD_UNIT.conversionUnitID"
                                    :fieldName="FIELD_CONVERSION_UNIT.conversionUnitID"
                                    :fieldListHide="[
                                        { field: 'conversionUnitName' },
                                    ]"
                                    styleValHide="top: calc(100% + 3px)"
                                    @showBoxAddSelect="showBoxAddUnit(`${listRef.tableConversionUnit}${index}`, data, 'conversionUnitID', true)"
                                    @getValue="getDataUnitConversion"
                                />
                            </template>
                            <template #inputUnit="{data, index}">
                                <BaseInput 
                                    style="width: 200px" 
                                    fieldName="conversionRate"
                                    type="float"
                                    :ref="`${listRef.tableConversionRate}${index}`"
                                    textRight
                                    :id="data.stt"
                                    lengthAfterComma="2"
                                    v-model:value="data.conversionRate"
                                    @onInput="getDataUnitConversion"
                                />
                            </template>
                            <template #selectBox2="{data, index}">
                                <BaseSelectBox
                                    :data="
                                        [
                                            { calculate: '*', value: 1 },
                                            { calculate: '/', value: 2 }
                                        ]
                                    "
                                    :id="data.stt"
                                    addSelect
                                    addSelectIcon="fa-solid fa-plus"
                                    addSelectColor="#2281c1"
                                    :setData="data.calculation"
                                    :ref="`${listRef.tableCalculation}${index}`"
                                    width="100px"
                                    showBorder
                                    fieldNameShow="calculate"
                                    fieldNameValue="value"
                                    :fieldName="FIELD_CONVERSION_UNIT.calculation"
                                    :fieldListHide="[
                                        { field: 'calculate' },
                                    ]"
                                    focusFirstData
                                    styleValHide="top: calc(100% + 3px)"
                                    @getValue="getDataUnitConversion"
                                />
                            </template>
                            <template #description="{data}">
                                {{descriptionConversionUnit(data.conversionUnitID, data.conversionRate, data.calculation)}}
                            </template>
                        </BaseTable>
                    </div>
                    <div class="func_unit">
                        <BaseButtonIcon icon="fa-solid fa-add" colorIcon="#2281c1" val="Thêm dòng" @click="addNewUnitMaterial" />
                        <BaseButtonIcon icon="fa-solid fa-close" colorIcon="red" styleCss="margin-left: 5px" val="Xóa dòng" @click="deleteNewUnitMaterial"/>
                    </div>
                </div>
                <div class="info_w100 func_form">
                    <div class="func_form_left">
                        <BaseButtonIcon icon="fa-solid fa-circle-question" colorIcon="#2281c1" val="Giúp"/>
                    </div>
                    <div class="func_form_right">
                        <BaseButtonIcon icon="fa-sharp fa-solid fa-floppy-disk" colorIcon="#2281c1" styleCss="margin-right: 5px" val="Cất" @click="saveAndClose"/>
                        <BaseButtonIcon icon="fa-solid fa-file-export" colorIcon="#2281c1" styleCss="margin-right: 5px" val="Cất và thêm" @click="saveAndAdd"/>
                        <BaseButtonIcon icon="fa-regular fa-circle-xmark" colorIcon="red" :ref="listRef.btnCancel" val="Hủy" @click="closeFormMain"/>
                        <div :ref="listRef.tabLastFormMain" tabindex="0" @focus="focusItemForm(listRef.materialName)"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseInput from '../Base/BaseInput.vue';
import BaseTable from '../Base/BaseTable.vue';
import BaseButtonIcon from '../Base/BaseButtonIcon.vue';
import BaseSelectBox from '../Base/BaseSelectBox.vue';
import BaseNotice from '../Base/BaseNotice.vue';
import BasePopUp from '../Base/BasePopUp.vue';

import {shallowEqual, fetchAPI, generateUUID, deleteReference, getErrorBE} from '@/lib/lib'
import {
    TYPE_FORM, 
    TYPE_UPDATE,
    HTTP_METHOD,
    ALERT_POPUP,
    ERROR_VALIDATE_FE,
    FIELD_MATERIAL,
    FIELD_CONVERSION_UNIT,
    FIELD_UNIT,
    FIELD_STOCK
} from '@/lib/resource'
import BaseLoading from '../Base/BaseLoading.vue';
import BaseTextArea from '../Base/BaseTextArea.vue';

const ICON_TOAST_MESSAGE = {
    success: 'success',
    warning: 'warning',
    error: 'error',
}

const EMIT = {
    updateDataTableMaterial: 'update:dataTableMaterial', // update lại dữ liệu bảng nvl 
    updateType: 'update:type', // update lại loại form
    hideForm: 'hideForm',  // Đóng form
    afterAdd: 'afterAdd', // Hàm xử lý sau khi thêm mới nguyên vật liệu
    addToastMessage: 'addToastMessage', // Hàm thêm toast message
}

// Format lại số 100.000,90 => 100000.90
const formatFloat = (data) => {
    return parseFloat(data.replace(/[.]/g,'').replace(',','.'))
}


export default {
    components: { BaseInput, BaseTable, BaseButtonIcon, BaseSelectBox, BaseNotice, BasePopUp, BaseLoading, BaseTextArea },
    props: {
        dataTableMaterial: Array, // Danh sách dữ liệu của bảng table
        dataForm: Object, // Dữ liệu truyền vào cho form
        type: String, // Loại form (update, add, ..)
    },
    data(){
        return {
            HTTP_METHOD, // kiểu request
            ALERT_POPUP, // Danh sách thông báo của popup
            ERROR_VALIDATE_FE, // Danh sách lỗi validate frontend
            FIELD_MATERIAL, // Các trường của material
            FIELD_CONVERSION_UNIT, // Các trường của đơn vị chuyển đổi
            FIELD_UNIT, // Các trường của đơn vị tính
            FIELD_STOCK, // Các trường của kho ngầm định
            dataFormDetail: {
                material: {}, // Thông tin nguyên vật liệu
                listUnit: [] // Danh sách đơn vị chuyển đổi tương ứng
            }, // Dữ liệu dataForm mới

            dataOrigin: {}, // Dữ liệu gốc

            loadingForm: false, // loading khi xử lý dữ liệu

            dataTable: {
                // Link get data
                url: ''.filterMaterialAPI(20, 1),
                //Id tương ứng cho mỗi hàng
                fieldId: 'stt',
                // Khai báo các trường hiện thị trong bảng ()
                field: [
                    {
                        field: 'stt', // Trường dữ liệu
                        fieldName: 'STT', // Tên cột
                        textAlign: 'left', // Căn lề
                        type: 'component', // loại
                        width: 35,
                        nameComponent: 'stt'
                    },
                    {
                        field: 'conversionUnitName',
                        fieldName: 'Đơn vị chuyển đổi',
                        textAlign: 'left',
                        type: 'component', // loại
                        width: 160,
                        nameComponent: 'selectBox'
                    },
                    {
                        field: 'conversionRate',
                        fieldName: 'Tỷ lệ chuyển đổi',
                        textAlign: 'left',
                        width: 180,
                        type: 'component', // loại
                        nameComponent: 'inputUnit'
                    },
                    {
                        field: 'calculation',
                        fieldName: 'Phép tính',
                        textAlign: 'left',
                        width: 160,
                        type: 'component', // loại
                        nameComponent: 'selectBox2'
                        // format: 'date', // Định dạng kiểu dữ liệu
                    },
                    {
                        field: 'description',
                        fieldName: 'Mô tả',
                        textAlign: 'left',
                        type: 'component',
                        nameComponent: 'description'
                    },
                ]
            },

            // Danh sách ref được dùng
            listRef: {
                addConversionUnitName: 'addConversionUnitName', // Input thêm mới đơn vị tính
                btnCancelPopupUnit: 'btnCancelPopupUnit', // Nút hủy bỏ thêm đơn vị tính
                addStockCode: 'addStockCode', // input mã kho ngầm định
                addStockName: 'addStockName', // input tên kho ngầm định
                btnCancelPopupStock: 'btnCancelPopupStock', // Nút hủy bỏ thêm mới kho ngầm định
                tabFirstFormMain: 'tabFirstFormMain', // div đầu tiên trong form chính, khi được focus sẽ focus vào nút hủy bỏ của form chính
                materialName: 'materialName', // Tên nguyên vật liệu form chính
                materialCode: 'materialCode', // Mã nguyên vật liệu form chính
                conversionUnitID: 'conversionUnitID', // selectbox đơn vị tính
                stockID: 'stockID', // kho ngầm định
                valueExp: 'valueExp', // hạn sử dụng
                tableConversionUnit: 'tableConversionUnit', // đơn vị chuyển đổi + index {danh sách đơn vị chuyển đổi }
                tableConversionRate: 'tableConversionRate', // tỷ lệ chuyển đổi + index {danh sách đơn vị chuyển đổi }
                tableCalculation: 'tableCalculation', // phép tính + index {danh sách đơn vị chuyển đổi }
                btnCancel: 'btnCancel', // Nút hủy bỏ form chính
                tabLastFormMain: 'tabLastFormMain', // div cuối trong form chính, khi được focus sẽ focus vào input đầu tiên
            },

            // Thông tin về field, data cho table
            inforTable: {},

            // Id hàng đơn vị chuyển đổi được chọn
            itemRowTableSelected: null,

            // hiển thị form thêm mới đơn vị tính
            isShowFormUnit: false,

            // Thông tin về form thêm mới đơn vị tính
            unitForm: {
                ref: null, // Trường cần foucs
                setData: null, // Data cần set
                field: null // Trường cần set
            },

            // hiển thị form thêm mới đơn vị tính
            isShowFormStock: false,

            /**
             * Danh sách đơn vị chuyển đổi cần xóa 
             */ 
            listConversionUnitDelete: [],

            // Danh sách đơn vị chuyển đổi
            listUnit: [],

            // Dữ liệu thêm mới đơn vị chuyển đổi
            dataAddUnit: {},

            //Danh sách kho ngầm định
            listStock: [],

            // Dữ liệu thêm mới kho ngầm định
            dataAddStock: {},

            //Popup
            popup: {
                isShowPopup: false, // Trạng thái ẩn hiện popup
                messagePopup: '', // tiêu đề popup
                typePopup: '', // loại popup
                callbackFun: {
                    callbackProcessWhenClose: () => {}, // Xử lý khi đóng popup
                    callbackWarningDeleteYes: () => {}, // xử lý khi click không popup question
                    callbackConfirmPopup: () => {}, // xử lý khi click đồng ý popup warning
                    callbackQuestionNo: () => {}, // xử lý khi click không popup question
                    callbackQuestionYes: () => {}, // xử lý khi click có popup question
                },
            },

            

        }
    },

    methods:{
        /**
         * Lấy dữ liệu từ input trong form
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getData(data) {
            // check null
            if (this.dataFormDetail && data && this.FIELD_MATERIAL) {
                // Khi gọi hàm component sẽ trả về
                //data{
                //    fieldName: trường dữ liệu mà tương ứng với val khi gửi lên server( đươc truyền từ component cha thông qua fieldName)
                //    data: dữ liệu của input/ selectbox
                //}
                this.dataFormDetail.material[data.fieldName] = data.val;
                
                if (data.fieldName == this.FIELD_MATERIAL.unitID && this.$refs[this.listRef.conversionUnitID]) {
                    // Kiểm tra dữ liệu trả về đã tồn tại trong danh sách đơn vị tính chưa
                    var checkDuplicateUnit = this.dataFormDetail.listUnit.some(item => {
                        return item.conversionUnitID == data.val
                    })
                    // Nếu đã tồn tại báo lỗi
                    if(checkDuplicateUnit){
                        this.dataFormDetail.material[data.fieldName] = null // set lại đơn vị tính về null
                        // Show poup báo lỗi 
                        this.customPopup(
                            true,
                            ERROR_VALIDATE_FE.DuplicateConversionUnitMaterial,
                            process.env.VUE_APP_POPUP_WARNING,
                        );
                        // Hàm xử lý khi đóng popup
                        this.popup.callbackFun.callbackConfirmPopup = () => {
                            // Đóng popup
                            this.customPopup()
                            // Focus vào đơn vị tính
                            this.$nextTick(() => {
                                this.$refs[this.listRef.conversionUnitID].focusFunc()
                            })
                        };
                    }
                    // Khôn tồn tại set tên đơn vị tính
                    else{
                        if(this.FIELD_MATERIAL.unitName && this.$refs[this.listRef.conversionUnitID]) // check null
                            this.dataFormDetail.material[this.FIELD_MATERIAL.unitName] = this.$refs[this.listRef.conversionUnitID].getValueSelectBox().show || '';
                    }
                }
            }
        },

        /**
         * Lấy dữ liệu từ input trong table đơn vị chuyển đổi
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getDataUnitConversion(data){
            // check null
            if(this.dataFormDetail.listUnit && data && this.dataFormDetail.material && this.ERROR_VALIDATE_FE && this.FIELD_CONVERSION_UNIT){
                let position = null, // vị trí của đơn vị chuyển đổi được chọn
                    dataListUnit = this.dataFormDetail.listUnit // Danh sách đơn vị chuyển đổi
                if(dataListUnit){
                    position = dataListUnit.indexOf(dataListUnit.find(item => item.stt == data.id)) // Lấy vị trí của đơn vị chuyển đổi được chọn
                }
                // Kiểm tra đơn vị chuyển đổi thêm vào đã tồn tại chưa
                var checkDuplicateUnit = this.dataFormDetail.listUnit.some(item => {
                    if(item.conversionUnitID && item.conversionRate && item.calculation){
                        return item.conversionUnitID == data.val
                    }
                    return false;
                })

                if(this.dataFormDetail.listUnit[position][data.fieldName] == data.val){
                    checkDuplicateUnit = false;
                }

                // hàm validate khi thêm đơn vị chuyển đổi
                const validateAddConversionUnit = (content) => {
                    // Show popup
                    this.customPopup(
                        true,
                        content,
                        process.env.VUE_APP_POPUP_WARNING,
                    );

                    // định nghĩa hàm khi click đồng ý popup
                    this.popup.callbackFun.callbackConfirmPopup = () => {
                        this.dataFormDetail.listUnit[position][data.fieldName] = null
                        this.customPopup() // đóng popup
                        this.$refs[`${this.listRef.tableConversionUnit}${position}`].focusFunc() // focus vào đơn vị chuyển đổi 
                    };

                    // định nghĩa hàm khi thực hiện click không cất
                    this.popup.callbackFun.callbackProcessWhenClose = () => {
                        this.dataFormDetail.listUnit[position][data.fieldName] = null
                        this.customPopup() // đóng popup
                        this.$refs[`${this.listRef.tableConversionUnit}${position}`].focusFunc() // focus vào đơn vị chuyển đổi
                    };
                }

                // Check có trùng với đơn vị tính ban đầu không
                if(data.val == this.dataFormDetail.material.unitID ){
                    validateAddConversionUnit(this.ERROR_VALIDATE_FE.DuplicateUnit);
                }
                // Check trùng với đơn vị đã có trong danh sách chuyển đổi
                else if(checkDuplicateUnit){
                    validateAddConversionUnit(this.ERROR_VALIDATE_FE.DuplicateConversionUnit);
                }
                else{
                    // Set lại type là cập nhập nếu type không phải add
                    if(this.dataFormDetail.listUnit[position].type != TYPE_UPDATE.ADD ){
                        this.dataFormDetail.listUnit[position].type = TYPE_UPDATE.UPDATE
                    }
                    this.dataFormDetail.listUnit[position][data.fieldName] = data.val
                    this.dataFormDetail.listUnit[position][this.FIELD_CONVERSION_UNIT.modifiedDate] = new Date()
                    this.unitForm = null 
                }
                

            }
        },

        /**
         * Lấy dữ liệu từ input set cho đơn vị chuyển đổi
         * @param {*} data 
         */
        getDataAddUnit(data){
            if(this.dataAddUnit && data){
                this.dataAddUnit[data.fieldName] = data.val
            }
        },

        /**
         * Lấy dữ liệu từ input set cho kho ngầm định
         * @param {*} data 
         */
        getDataAddStock(data){
            if(this.dataAddStock && data){
                this.dataAddStock[data.fieldName] = data.val
            }
        },

        /**
         * Lấy dòng được chọn trong đơn vị chuyển đổi
         */
        getRowConversionUnit(data){
            try {
                this.itemRowTableSelected = data[this.dataTable.fieldId]
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Xử lý đóng form
         * @param {*} dataOrigin // dữ liệu gốc
         * @param {*} dataChange // dữ liệu thay đổi
         * @param {*} funcYes // function click có
         * @param {*} funcNo // function click không
         * @param {*} funcClose // function khi đóng form
         * @param {*} funcNoChange // function khi dữ liệu không thay đổi 
         */
        closeFormCommon(dataOrigin, dataChange, funcYes, funcNo, funcClose, funcNoChange){
            // Hiển thị popup khi có dữ liệu thay đổi trc khi đóng form
            if(!shallowEqual(dataOrigin, dataChange) && this.popup && this.popup.callbackFun && this.ALERT_POPUP){
                // show popup hỏi trước khi đóng
                this.customPopup(
                    true,
                    this.ALERT_POPUP.DataChange,
                    process.env.VUE_APP_POPUP_QUESTION,
                );

                // định nghĩa hàm khi click đóng popup
                this.popup.callbackFun.callbackProcessWhenClose = () => {
                    funcClose()
                };

                // định nghĩa hàm khi thực hiện click không cất
                this.popup.callbackFun.callbackQuestionNo = () => {
                    funcNo()
                };

                // Thực hiện khi click có đồng ý
                this.popup.callbackFun.callbackQuestionYes = () => {
                    funcYes();
                };
            }else{
                // Hàm xử lý nếu không có j thay đổi
                funcNoChange();
            }
        },

        /**
         * Đóng form đơn vị tính
         */
        closeUnit(){
            this.closeFormCommon(
                {}, // data ban đầu
                this.dataAddUnit, // data thêm mới
                // Xử lý khi click có trong popup
                () => {
                    this.addUnit() // Thêm mói đơn vị tính
                    this.customPopup(); // Đóng popup 
                },
                // Xử lý khi click không trong popup
                () => { 
                    this.isShowFormUnit = false // Đóng form thêm đơn vị tính
                    this.customPopup(); // đóng popup
                    if(this.$refs[this.unitForm.ref]){
                        this.$refs[this.unitForm.ref].focusFunc(); // focus vào ô ĐVT
                    }
                },
                // Xử lý khi đóng popup
                () => {
                    this.customPopup(); // đóng popup
                    if(this.$refs[this.listRef.addConversionUnitName]){
                        this.$refs[this.listRef.addConversionUnitName].focusFunc(); // focus vào tên đơn vị tính trong form thêm mới đơn vị tính
                    }
                },
                // Xử lý khi không có dữ liệu thay đổi
                () => { 
                    this.isShowFormUnit = false 
                },
            )
        },

        /**
         * Đóng form đơn vị tính
         */
         closeStock(){
            this.closeFormCommon(
                {}, // data ban đầu
                this.dataAddStock, // dữ liệu thêm mới kho ngầm định
                // Xử lý click có popup
                () => {
                    this.addStock() // Lưu kho ngầm định
                },
                // Xử lý click không popup
                () => { 
                    this.isShowFormStock = false; // đóng form
                    this.customPopup() // đóng popup thông báo
                    if(this.$refs[this.listRef.stockID])
                        this.$refs[this.listRef.stockID].focusFunc(); // focus kho ngầm định
                },
                // Xử lý khi đóng popup
                () => {
                    this.customPopup() // đóng popup thông báo
                    if(this.$refs[this.listRef.addStockCode])
                        this.$refs[this.listRef.addStockCode].focusFunc(); // focus mã kho ngầm định trong popup thêm mới kho
                },
                // xử lý khi dữ liệu không thay đổi
                () => {
                    this.isShowFormStock = false;
                },
            )
        },

        /**
         * Đóng form đơn vị chuyển đổi
         */
        closeFormMain(){
            // Xóa tham chiếu lấy thông tin chi tiết form nguyên vật liệu
            let dataDetail = deleteReference(this.dataFormDetail)
            if(dataDetail.material && dataDetail.listUnit){
                // Format lại kho ngầm định về float
                if(dataDetail.material.inventoryNumber)
                    dataDetail.material.inventoryNumber = formatFloat(dataDetail.material.inventoryNumber)
                // format lại tỷ lệ chuyển đổi về float
                dataDetail.listUnit = dataDetail.listUnit.map(item => {
                    if(item.conversionRate)
                        item.conversionRate = formatFloat(item.conversionRate)
                    return item
                })
            }
            // gọi hàm khi đóng form nguyên vật liệu
            this.closeFormCommon(
                this.dataOrigin, // Dữ liệu ban đầu
                dataDetail, // dữ liệu sau 
                // Click có trong popup
                () => {
                    this.saveAndClose(); // lưu và đóng
                    this.customPopup() // đóng popup
                },
                // click không trong popup
                () => { 
                    // Kiểm nếu không là form sửa thông tin thì xóa phần tử vừa thêm vào bảng nguyên vật liệu
                    if (this.type != process.env.VUE_APP_PROCESS_UPDATE) {
                        var a = [...this.dataTableMaterial];
                        a.shift();
                        this.$emit(EMIT.updateDataTableMaterial, a);
                    }
                    // set lại dữ liệu bảng về dữ liệu ban đầu
                    else{
                        for (let key in this.dataOrigin.material) {
                            this.dataFormDetail.material[key] = this.dataOrigin.material[key];
                        }
                    }
                    this.$emit(EMIT.hideForm); // đóng form
                },
                // Hàm xử lý khi đóng form
                () => {},
                // Hàm xử lý khi không có dữ liệu thay đổi
                () => { 
                    // Kiểm nếu không là form sửa thông tin thì xóa phần tử vừa thêm vào bảng nguyên vật liệu
                    if (this.type != process.env.VUE_APP_PROCESS_UPDATE) {
                        var a = [...this.dataTableMaterial];
                        a.shift();
                        this.$emit(EMIT.updateDataTableMaterial, a);
                    }
                    this.$emit(EMIT.hideForm); // Ẩn form
                },
            )
        },

        /**
         * hiển thị form thêm mới đơn vị tính
         */
        showBoxAddUnit(ref, setData, field, isListConversionUnit = false) {
            try {
                this.isShowFormUnit = true; // show form thêm đơn vị tính
                this.unitForm = {}
                // Set thông tin cân thực hiện sau khi thêm đơn vị tính
                if(this.unitForm){
                    this.unitForm.ref = ref // trường cần focus sau khi thêm
                    this.unitForm.setData = setData // dữ liệu chứa trường cần set lại đơn vị tính sau khi thêm
                    this.unitForm.field = field // trường cần set lại dữ liệu
                    this.unitForm.isListConversionUnit = isListConversionUnit // xét xem có phải là đơn vị tính hay đơn vị chuyển đổi
                }
                // focus tên đợn vị tính
                this.$nextTick(() => {
                    this.$refs[this.listRef.addConversionUnitName].focusFunc();                
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * hiển thị form thêm mới đơn vị tính
         */
        showBoxAddStock() {
            try{
                this.isShowFormStock = true; // show form thêm kho ngầm định
                this.$nextTick(() => {
                    this.$refs[this.listRef.addStockCode].focusFunc(); // focus mã kho ngầm định trong form kho ngầm định
                })
            }catch (error) {
                console.log(error)
            }
        },

        /**
         * Lấy mã mới tự động
         * @param {*} data Tên nguyên vật liệu cần lấy mã mới
         */
        changeMaterialName(data){
            try {
                if(data && this.dataFormDetail){

                    // Cắt khoảng trống, lấy chứ cái đầu mỗi từ rồi nối chuỗi viết hoa chữ cái đầu
                    let string = data.val.split(" ").map(item => { // lấy chữ cái đầu và viết hoa
                        return item[0].toUpperCase()
                    })
                    .filter( item => {
                        return item != "" && !/[!@#$%^&*()+\=\[\]{};':"\\|,.<>\/?]/.test(item) // lọc ký tự trống và ký tự đặc biệt
                    })                    
                    .join(""); // ghép lại

                    // Call API lấy mã mới
                    if(string){
                        fetchAPI(
                        ''.newMaterialCode(string),
                        HTTP_METHOD.GET,
                        (res) => {
                            if(!res.userMsg && this.dataFormDetail.material){
                                // thành công set lại mã nguyên vật liệu bằng mã mới
                                this.dataFormDetail.material.materialCode = res
                            }
                        }
                    )
                    }
                }                
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Mô tả đơn vị chuyển đổi
         * @param {*} conversionUnit 
         * @param {*} conversionRate 
         * @param {*} calculation 
         */
        descriptionConversionUnit(conversionUnit, conversionRate, calculation){
            if(this.dataFormDetail.material && this.listUnit && conversionUnit && conversionRate && calculation){ // check null
                // Lấy tên đơn vị chuyển đổi gốc
                var nameUnitMaterial = this.dataFormDetail.material.unitName
                // K có tên đơn vị chuyển đổi trả về chuoixo rỗng
                if(!nameUnitMaterial)
                    return '';
                // Lọc trong danh sách đơn vị chuyển đổi để lấy tên đơn vị chuyển đổi qua id
                var nameConversionUnit = this.listUnit.filter(item => {
                    return item.conversionUnitID == conversionUnit
                })
                var showUnitName = ''
                // Check nếu tìm được đơn vị chuyển đổi lấy tên và show 
                if(nameConversionUnit.length > 0){
                    let description = '';
                    showUnitName = nameConversionUnit[0].conversionUnitName
                    if(calculation == 2){
                        description = `1 ${nameUnitMaterial} = 1/${conversionRate}  ${showUnitName} `; 
                    }
                    else if(calculation == 1){
                        description = `1 ${nameUnitMaterial} = ${conversionRate} * ${showUnitName} `;
                    }
                    return description;
                }
                
            }
        },

        /**
         * Thêm mới đơn vị chuyển đổi
         */
        addNewUnitMaterial(){
            try {
                if(this.dataFormDetail.listUnit && this.dataFormDetail.material){
                    // Thêm mới data để binding 
                    this.dataFormDetail.listUnit.push({
                        stt: generateUUID(),
                        type: TYPE_UPDATE.ADD,
                        materialID: this.dataFormDetail.material.materialID
                    })
                    this.$nextTick(() => {
                        // Focus vào đơn vị chuyển đổi cần thêm
                        this.$refs[`${this.listRef.tableConversionUnit}${this.dataFormDetail.listUnit.length - 1}`].focusFunc()
                    })
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xóa đơn vị chuyển đổi
         */
        deleteNewUnitMaterial(){
            try {
                if(this.inforTable.data){
                    let inforData = this.inforTable.data // dữ liệu của danh sách đơn vị chuyển đổi
                    // check xem đã có hàng nào trong đơn vị chuyển đôi được chọn chưa
                    let checkConversionUnit = inforData.some(item => item[this.dataTable.fieldId] == this.itemRowTableSelected)
                    // Xóa hàng được chọn
                    if(checkConversionUnit){
                        let position = null,
                            dataListUnit = this.inforTable.data
                        // vị trí của hàng được chọn 
                        position = dataListUnit.indexOf(dataListUnit.find(item => item[this.dataTable.fieldId] == this.itemRowTableSelected)) 
                        if(position || position == 0){
                            // Nếu hàng được xóa không phải hàng thêm mới thì thêm vào danh sách xóa đơn vị chuyển đổi
                            if(inforData[position].type != TYPE_UPDATE.ADD ){
                                this.listConversionUnitDelete.push({
                                    ...inforData[position],
                                    type: TYPE_UPDATE.DELETE
                                })
                            }
                            // Xóa khỏi dánh sach
                            this.inforTable.data.splice(position, 1)
                        }
                    }
                    else{
                        //Nếu không có hàng được chọn xóa phần tử cuối cùng
                        if(inforData[inforData.length - 1].type != TYPE_UPDATE.ADD )
                            this.listConversionUnitDelete.push({
                                ...inforData[inforData.length - 1],
                                type: TYPE_UPDATE.DELETE
                            })
                        this.inforTable.data.splice(this.inforTable.data.length - 1, 1)
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Validate dữ liệu
         */
        validateData(type){
            var errMess = ''; // Nội dung lỗi
            var status = true; // kiểm tra lỗi
            var errorList = [];

            // Hàm set lỗi
            const errEmpty = (errContent, fieldPush) => {
                errMess += `${errContent}\n`;
                status = false;
                errorList.push(fieldPush);
            };

            // Validate bỏ trống
            const validateEmpty = (dataValidate) => {
                dataValidate.forEach(element => {
                    let errorMsg = null
                     
                    if (!element.data && this.$refs[element.field].validate().messError) {
                        errorMsg = this.$refs[element.field].validate().messError // nội dung lỗi
                        errEmpty(errorMsg, element.field); // thêm lỗi vào danh sách
                    }
                });
            }
            switch (type) {
                case this.FIELD_MATERIAL.tableName:
                    // Kiểm tra trường bắt buộc phải nhập
                    if(this.dataFormDetail.material){
                        let materialCode = this.dataFormDetail.material[this.FIELD_MATERIAL.materialCode], // mã nguyên vật liệu
                            materialName = this.dataFormDetail.material[this.FIELD_MATERIAL.materialName], // tên nguyên vật liệu
                            conversionUnitID = this.dataFormDetail.material[this.FIELD_MATERIAL.unitID]; // đơn vị

                        validateEmpty([
                            {data: materialName, field: this.listRef.materialName}, // field là ref, data là dữ liệu cần check
                            {data: materialCode, field: this.listRef.materialCode},
                            {data: conversionUnitID, field: this.listRef.conversionUnitID}
                        ])
                    }

                    if(this.dataFormDetail.listUnit){
                        let refFocus = '';
                        // Kiểm tra đơn vị chuyển đổi không được bỏ trống
                        let checkNullItem = this.dataFormDetail.listUnit.some((item,index) => {
                            if(!item.conversionUnitID)
                                refFocus = `${this.listRef.tableConversionUnit}${index}` // ref đơn vị chuyển đổi
                            else if(!item.conversionRate)
                                refFocus = `${this.listRef.tableConversionRate}${index}` // ref tỷ lệ chuyển đổi
                            else if(!item.calculation)
                                refFocus = `${this.listRef.tableCalculation}${index}` // ref đơn vị tính

                            return refFocus ? true : false
                        })
                        if(checkNullItem){
                            errMess += `${this.ERROR_VALIDATE_FE.ConversionUnitNotEmpty}\n`;
                            status = false;
                            errorList.push(refFocus);
                        }
                    }
                    break;
                case this.FIELD_UNIT.tableName:
                    if(this.dataAddUnit){
                        let unitName = this.dataAddUnit[this.FIELD_UNIT.conversionUnitName] // Tên đơn vị cần thêm
                        // check bỏ trống đơn vị tính
                        validateEmpty([
                            {data: unitName, field: this.listRef.addConversionUnitName} 
                        ])
                    }
                    break;
                case this.FIELD_STOCK.tableName:
                    if(this.dataAddStock){
                        let stockCode = this.dataAddStock[this.FIELD_STOCK.stockCode], // Mã kho cần thêm
                            stockName = this.dataAddStock[this.FIELD_STOCK.stockName]; // Tên kho
                        // check bỏ trống kho ngầm định
                        validateEmpty([
                            {data: stockCode, field: this.listRef.addStockCode},
                            {data: stockName, field: this.listRef.addStockName},
                        ])
                    }
                    break;
                default:
                    break;
            }

            return {
                status: status,
                errorMess: errMess,
                errorList: errorList,
            };
        },

        /**
         * Thêm mới đơn vị tính
         */
        addUnit(){
            try {
                if(this.FIELD_UNIT){
                    let validate = this.validateData(this.FIELD_UNIT.tableName); // validate
                    if(validate.status){
                        this.loadingForm = true; // show loading

                        var data = {
                            ...this.dataAddUnit,
                            conversionUnitID: generateUUID(),
                            modifiedDate: new Date()
                        }
                        //Call API thêm mới đơn vị tính
                        fetchAPI(
                            ''.getAllUnit(),
                            HTTP_METHOD.POST,
                            (res) => {
                                this.loadingForm = false // dừng load form
                                if(res.userMsg){
                                    // show popup
                                    this.customPopup(
                                        true, 
                                        getErrorBE(res.errorCode), 
                                        process.env.VUE_APP_POPUP_ERROR
                                    );
                                    // Xử lý khi đóng poup
                                    this.popup.callbackFun.callbackProcessWhenClose = () => {
                                        if(this.unitForm.ref){
                                            this.$refs[this.unitForm.ref].focusFunc(); // focus vào ô ĐVT
                                        }
                                    };
                                }
                                else{
                                    // Thành công
                                    this.isShowFormUnit = false // đóng form
                                    this.dataAddUnit = {} // set dataAddUnit về object rỗng
                                    this.listUnit.unshift(data) // thêm mới đơn vị tính vào danh sách đơn vị tính
                                    // thêm toast message
                                    this.$emit(EMIT.addToastMessage, {
                                        icon: ICON_TOAST_MESSAGE.success,
                                        message: this.ALERT_POPUP.AddUnitSuccess,
                                    });
                                    if(this.unitForm.setData && this.unitForm.field && this.unitForm.ref){
                                        if(this.$refs[this.unitForm.ref])
                                            this.$refs[this.unitForm.ref].focusFunc(); // focus đơn vị tính vừa thêm
                                        // nếu là danh sách đơn vị tính
                                        if(this.unitForm.isListConversionUnit){
                                            this.$refs[this.unitForm.ref].itemOnSelect(data, 0) // set data cho selectbox đơn vị chuyển đổi vừa thêm
                                        }
                                        else{
                                            // focus vào đơn vị tính
                                            this.unitForm.setData[this.unitForm.field] = data.conversionUnitID //
                                        }
                                    }
                                }
                            },
                            data
                        )
                    }
                    else{
                        this.popup.callbackFun.callbackProcessWhenClose = () => {
                            // Focus vào input box lỗi đầu tiên
                            this.$refs[validate.errorList[0]].focusFunc();
                        };
                        this.customPopup(true, validate.errorMess, process.env.VUE_APP_POPUP_ERROR); // show poup lỗi
                    }
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Thêm mới kho ngầm định
         */
        addStock(){
            try {
                if(this.FIELD_STOCK){
                    // validate kho ngầm định
                    let validate = this.validateData(FIELD_STOCK.tableName);
                    if(validate.status){
                        this.loadingForm = true; // show loading
                        var data = {
                            ...this.dataAddStock,
                            stockID: generateUUID(),
                            modifiedDate: new Date()
                        }
                        //Call API thêm mới đơn vị tính
                        fetchAPI(
                            ''.getAllStock(),
                            HTTP_METHOD.POST,
                            (res) => {
                                this.loadingForm = false
                                // Lỗi
                                if(res.userMsg){
                                    // show popup lỗi
                                    this.customPopup(
                                        true, 
                                        getErrorBE(res.errorCode), 
                                        process.env.VUE_APP_POPUP_ERROR
                                    );
                                    // focus vào mã kho ngầm định khi đóng popup
                                    this.popup.callbackFun.callbackProcessWhenClose = () => {
                                        this.$refs[this.listRef.addStockCode].focusFunc();
                                    };
                                }
                                else{
                                    // Thành công
                                    this.isShowFormStock = false // đơngs form thêm kho ngầm định
                                    this.dataAddUnit = {} 
                                    this.listStock.unshift(data) // thêm vào list kho ngầm định
                                    // thêm toast message thành công
                                    this.$emit(EMIT.addToastMessage, {
                                        icon: ICON_TOAST_MESSAGE.success,
                                        message: this.ALERT_POPUP.AddStockSuccess,
                                    });
                                    this.dataFormDetail.material.stockID = data.stockID // set kho ngầm định với kho ngầm định vừa thêm
                                    // focus vào kho ngầm định
                                    this.$refs[this.listRef.stockID].focusFunc();
                                }
                            },
                            data
                        )
                    }
                    else {
                        this.popup.callbackFun.callbackProcessWhenClose = () => {
                            // Focus vào input box lỗi đầu tiên
                            this.$refs[validate.errorList[0]].focusFunc();
                        };
                        this.customPopup(true, validate.errorMess, process.env.VUE_APP_POPUP_ERROR);
                    }
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Nhấn nút cất
         */
        saveAndClose(){
            try {
                // Lưu nguyên vật liệu
                this.save()
                .then((res) => {
                    // Thành công
                    if(res.success)
                        this.$emit(EMIT.hideForm); // đóng form
                    else{
                        // Show popup thất bại
                        this.customPopup(true, res.userMsg, process.env.VUE_APP_POPUP_ERROR);
                        this.popup.callbackFun.callbackProcessWhenClose = () => { // xử lý khi đóng form
                            if(res.ref)
                                this.$refs[res.ref].focusFunc() // focus vào vị trí lỗi
                            else
                                this.$refs[this.listRef.materialCode].focusFunc() // focus vào mã nguyên vật liêu
                        }
                    }
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Cất và thêm
         */
        saveAndAdd(){
            try {
                this.save()
                .then((res) => {
                    if(res.success){
                        // Nếu thành công sẽ thêm mới 1 hàng vào đầu tiên
                        var a = [...this.dataTableMaterial];
                        a.unshift({}); // thêm mới dữ liệu vào bảng nguyên vật liệu
                        this.$emit(EMIT.updateDataTableMaterial, a); // update tham chiếu với dữ liệu của bảng
                        this.$emit(EMIT.updateType, process.env.VUE_APP_PROCESS_ADD); // set lại loại form là add
                        return {
                            success: true
                        }
                    }
                    else{
                        // Thông báo lỗi 
                        this.customPopup(true, res.userMsg, process.env.VUE_APP_POPUP_ERROR);
                        this.popup.callbackFun.callbackProcessWhenClose = () => { // hàm xử lý khi đóng popup
                            this.$refs[this.listRef.materialCode].focusFunc() // focus vào mã nguyên vật liệu
                        }
                        return {
                            success: false
                        }
                    }
                })
                .then((res) => {
                    if(res.success && this.dataTableMaterial[0]){
                        // set thông tin form bằng nguyên vật liệu trống vừa thêm
                        this.dataFormDetail = {
                            material: this.dataTableMaterial[0],
                            listUnit: []
                        }
                        this.dataOrigin = deleteReference(this.dataFormDetail) // set lại dữ liệu gốc để check khi đóng form
                        this.inforTable.data = this.dataFormDetail.listUnit // set lại danh sách đơn vị chuyển đổi
                        if(this.$refs[this.listRef.materialName]){ // focus tên nguyên vật liệu
                            this.$refs[this.listRef.materialName].focusFunc()
                        }
                    }
                })
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Thêm mới nguyên vật liệu
         */
        addMaterial(data){
            
            this.loadingForm = true; // loading
            data.material.materialID = generateUUID(); // tạo mới guid
            // lấy danh sách đơn vị chuyển đổi cần thêm
            data.listConversionUnits = data.listConversionUnits.map((item) => {
                var itemNew = item;
                delete itemNew.stt;
                itemNew.materialID = data.material.materialID
                return itemNew;
            })

            this.dataFormDetail.material.materialID = data.material.materialID;
            return new Promise((resolve) => {
                fetchAPI(
                    ''.addMaterial(), 
                    HTTP_METHOD.POST, 
                    (res) => {
                        this.loadingForm = false
                        if(res.userMsg){
                            // lỗi
                            resolve({
                                success: false,
                                userMsg: getErrorBE(res.errorCode), 
                                errorCode: res.errorCode
                            })
                        }
                        // Thành công
                        else{
                            if(this.ALERT_POPUP){
                                this.$emit(EMIT.afterAdd); // gọi hàm xử lý sau khi add
                                // add toast message thêm mới thành công
                                this.$emit(EMIT.addToastMessage, {
                                    icon: ICON_TOAST_MESSAGE.success,
                                    message: this.ALERT_POPUP.AddSuccessMaterial,
                                });
                                resolve({
                                    success: true
                                })
                            }
                        }
                    }, 
                    {
                        material: data.material, 
                        listConversionUnits: data.listConversionUnits
                    }
                );
            })
        },

        /**
         * Cập nhập thông tin nguyên vật liệu
         */
        updateMaterial(data){
            let dataDetail = deleteReference(this.dataFormDetail) // xóa tham chiếu, lấy thông tin form nguyên vật liệu
            if(dataDetail.material && dataDetail.listUnit){
                // Chuyển số lượng tồn về float
                if(dataDetail.material.inventoryNumber)
                    dataDetail.material.inventoryNumber = formatFloat(dataDetail.material.inventoryNumber)
                // chuyển tỷ lệ chuyển đổi về float                    
                dataDetail.listUnit = dataDetail.listUnit.map(item => {
                    if(item.conversionRate)
                        item.conversionRate = formatFloat(item.conversionRate)
                    return item
                })
            }
            // Nếu dữ liệu không thay đổi trả về thành công luôn nhưng không show toast message
            if(shallowEqual(this.dataOrigin, dataDetail)){
                return new Promise(resolve => {
                    resolve({
                        success: true
                    })
                })
            }   
            this.loadingForm = true; // loading
            // lấy thông tin những đơn vị chuyển đổi cần update hoặc thêm mới
            data.listConversionUnits = data.listConversionUnits.filter(convUnit => {
                delete convUnit.stt
                return convUnit.type == TYPE_UPDATE.ADD || convUnit.type == TYPE_UPDATE.UPDATE  
            })
            // Lấy thông tin đơn vị chuyển đổi cần xóa
            let listDelete = this.listConversionUnitDelete.map(item => {
                return {
                    ...item,
                    conversionRate: item.conversionRate.replace(/[.]/g,'').replace(',','.')
                }
            })
            // dữ liệu gửi lên sẻver
            var dataUpdate = {
                material: data.material,
                listConversionUnits: data.listConversionUnits.concat(listDelete)
            }
            return new Promise(resolve => {
                fetchAPI(
                    ''.updateMaterial(this.dataFormDetail.material.materialID),
                    HTTP_METHOD.PUT,
                    (res) => {
                        this.loadingForm = false // loading
                        if(res.userMsg){
                            // có lỗi
                            resolve({
                                success: false,
                                userMsg: getErrorBE(res.errorCode), 
                                errorCode: res.errorCode
                            })
                        }
                        // Thành công
                        else{
                            if(this.ALERT_POPUP){
                                // show toast message thành công
                                this.$emit(EMIT.addToastMessage, {
                                    icon: ICON_TOAST_MESSAGE.success,
                                    message: this.ALERT_POPUP.UpdateSuccessMaterial,
                                });
                                resolve({
                                    success: true
                                })
                            }
                        }
                    },
                    dataUpdate,
                );
            })
        },

        /**
         * Lưu dữ liệu form nguyên vật liệu
         */
        save() {
            if(this.FIELD_MATERIAL){
                let validate = this.validateData(this.FIELD_MATERIAL.tableName); // validate nguyên vật liệu
                if (
                    validate.status && // kiểm tra validate dữ liệu
                    this.$refs[this.listRef.conversionUnitID].getValueSelectBox().show// Kiểm tra tên đơn
                ) {
                    let unitName = this.$refs[this.listRef.conversionUnitID].getValueSelectBox().show; // Tên đơn vị
                    let stockName = '';
                    if(this.$refs[this.listRef.stockID].getValueSelectBox() ) // Kiểm tra tên đơn
                        stockName = this.$refs[this.listRef.stockID].getValueSelectBox().show; // Tên kho ngầm định
                                                    
                    let expiryDate = '' // Hạn sử dụng
                    if(this.dataFormDetail.material[this.FIELD_MATERIAL.valueExp] && this.dataFormDetail.material[this.FIELD_MATERIAL.typeExp]){
                        expiryDate = `${this.dataFormDetail.material[this.FIELD_MATERIAL.valueExp]}_${this.dataFormDetail.material[this.FIELD_MATERIAL.typeExp]}`
                    }
                    let inventoryNumberFormat = this.dataFormDetail.material[this.FIELD_MATERIAL.inventoryNumber].replace(/[.]/g,'').replace(',','.')
                    
                    // Thông tin nguyên vật liệu
                    let material = {
                        ...this.dataFormDetail.material,
                        inventoryNumber: inventoryNumberFormat,
                        unitName: unitName,
                        stockName: stockName,
                        expiryDate: expiryDate,
                        modifiedDate: new Date(),
                    }
                    delete material.valueExp;
                    delete material.typeExp;
    
                    // Danh sách đơn vị chuyển đổi
                    let listConversionUnits = [...this.dataFormDetail.listUnit].map(item => {
                        return {
                            ...item,
                            conversionRate: item.conversionRate.replace(/[.]/g,'').replace(',','.')
                        }
                    })
    
                    // format data về dạng chuẩn thêm trường tên đơn vị
                    let data = {
                        material,
                        listConversionUnits
                    };
    
                    if ( this.type) {
                        // hiển thị loading
                        switch (this.type) {
                            // Thêm mới dữ liệu
                            case TYPE_FORM.ADD:
                                data.material.status = 1
                                // gọi hàm thêm mới material
                                return this.addMaterial(data)
                            // Nhân bản
                            case TYPE_FORM.DUPLICATE:
                                data.material.status = 1
                                // gọi hàm thêm mới material
                                return this.addMaterial(data)
                            // Update dữ liệu
                            case TYPE_FORM.UPDATE:
                                // gọi hàm update dữ liệu
                                return this.updateMaterial(data)
                            default:
                                break;
                        }
                    }
                } else {
                    this.popup.callbackFun.callbackProcessWhenClose = () => {
                        // Focus vào input box lỗi đầu tiên
                        this.$refs[validate.errorList[0]].focusFunc();
                    };
    
                    return new Promise(
                        resolve => resolve(
                            {
                                success: false,
                                userMsg: validate.errorMess,
                                ref: validate.errorList[0]
                            }
                        )
                    ) 
                }
            }
        },

        /**
         * Sự kiện bàn phím với form chính
         */
        onKeyDownFormMain(dataKey){
            switch (dataKey.keyCode) {
                case 13: // Nhấn enter
                    if(dataKey.ctrlKey && dataKey.shiftKey) // Ctrl shift s cất và thêm
                        this.saveAndAdd()
                    else if(dataKey.ctrlKey) // cất 
                        this.saveAndClose()
                    break;
                case 27: // Nhấn esc
                    event.preventDefault();
                    this.closeFormMain();
                    break;
                default:
                    break;
            }
        },        

        /**
         * Focus item của form
         */
        focusItemForm(ref){
            try {
                if(this.$refs[ref])
                    this.$refs[ref].focusFunc()
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Ẩn hiện popup
         * @param {Boolean} isShow true- show popup, false - ẩn popup
         * @param {String} message nội dung của popup
         * @param {String} type loại popup
         * CreatedBy: NDCHIEN (18/8/2022)
         */
         customPopup(isShow = false, message = '', type = '') {
            if (this.popup) {
                this.popup.isShowPopup = isShow; // ẩn hiện popup
                this.popup.messagePopup = message; // nội dung popup
                this.popup.typePopup = type; // loại popup
            }
        },

        
    },
    created() {
        this.dataFormDetail = this.dataForm;
        this.dataOrigin = JSON.parse(JSON.stringify(this.dataFormDetail))

        if(this.dataTable && this.dataFormDetail){
            this.inforTable.field = this.dataTable.field;
            this.inforTable.data = this.dataFormDetail.listUnit;
        }
        // Lấy danh sách đơn vị chuyển đổi
        fetchAPI(
            ''.getAllUnit(),
            HTTP_METHOD.GET,
            (res) => {
                this.listUnit = res
            }
        )

        // Lấy danh sách kho ngầm định
        fetchAPI(
            ''.getAllStock(),
            HTTP_METHOD.GET,
            (res) => {
                this.listStock = res
            }
        )

        this.HTTP_METHOD = HTTP_METHOD
        this.ALERT_POPUP = ALERT_POPUP
        this.ERROR_VALIDATE_FE = ERROR_VALIDATE_FE
        this.FIELD_MATERIAL = FIELD_MATERIAL
        this.FIELD_CONVERSION_UNIT = FIELD_CONVERSION_UNIT
        this.FIELD_UNIT = FIELD_UNIT
        this.FIELD_STOCK = FIELD_STOCK
    },
    mounted() {
        this.$refs[this.listRef.materialName].focusFunc();
    },
}
</script>
<style scoped>
    .form_material{
        height: 100vh;
        width: 100vw;
        background-color: rgba(0, 0, 0, 0.5);
        position: fixed;
        z-index: 150;
        top: 0;
        left: 0;
    }

    .form_detail{
        position: absolute;
        height: 600px;
        width: 850px;
        border-radius: 6px;
        border: 5px solid #0072bc;
        background-color: #0072bc;
        top: calc(50% - 300px);
        left: calc(50% - 425px);
        display: flex;
        flex-direction: column;
    }

    .form_title{
        background-color: #0072bc;
        color: #fff;
        height: 30px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 10px;
    }

    .title_right{
        display: flex;
    }

    .title_right i{
        margin: 0;
        margin-left: 10px;
        cursor: pointer;
    }

    .info_container_1{
        display: flex;
        flex-wrap: wrap;
    }

    .info_material{
        width: 50%;
        padding: 0 10px;
        margin: 3px 0;
        display: flex;
    }

    .detail{
        flex: 1;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        padding: 8px 0;
        background-color: #fff;
    }

    .info_name{
        width: 100px
    }

    .info_w100{
        width: 100%;
        display: flex;
    }

    .info_w100 textarea{
        height: 60px;
        max-height: 60px;
        padding: 5px 10px;
        border: 1px solid #bbb;
        flex: 1;
    }

    .info_w100 textarea:focus{
        border: 1px solid #0072bc;
        outline: none;
    }

    .info_unit{
        flex-direction: column;
        padding: 0 10px;
    }

    .info_title_unit{
        height: 30px;
        background-color: rgb(233, 233, 233);
        padding: 0 8px;
        display: flex;
        align-items: center;
        margin-bottom: 8px;
    }

    .info_title_unit span{
        color:#0072bc;
        background-color: #fff;
        height: 100%;
        line-height: 26px;
        padding: 0 8px;
        border-top: 2px solid #0072bc;
    }

    .table_unit{
        height: 220px;
        border: 1px solid #bbb;
        overflow: auto;
        position: relative;
        z-index: 5;
    }

    .selectbox_table_unit{
        z-index: 10;
    }

    .func_unit{
        margin-top: 12px;
    }

    .func_form{
        padding: 0 10px;
        justify-content: space-between;
    }

    .content_add_unit{
        padding: 8px 10px;
    }

    /* .add_unit_list{

    } */

    .add_unit_item{
        display: flex;
        margin: 8px 0;
    }

    .add_unit_name{
        min-width: 120px;
    }

    .add_unit_func{
        display: flex;
        justify-content: space-between;
        margin-top: 12px;
    }

</style>