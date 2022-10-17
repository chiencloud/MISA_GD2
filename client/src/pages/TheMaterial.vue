<template>
    <div class="page_container">
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
        <div class="toast-message-alert" v-if="listToastMessage.length > 0">
            <BaseToastMessage
                v-for="toastItem in listToastMessage"
                :key="toastItem.id"
                :icon="toastItem.icon"
                :messageToast="toastItem.messageToast"
                @closeToast="closeToastItem(toastItem)"
            />
        </div>
        <TheForm
            v-if="formMaterial.isShowForm"
            v-model:dataTableMaterial="inforTable.data"
            v-model:dataRowTableTrClick="dataRowTableTrClick"
            :dataForm="dataFormMaterial"
            v-model:type="formMaterial.type"
            @addToastMessage="addToastMessage"
            @hideForm="hideFormMaterial"
            @afterAdd="afterAddMaterial"
        />
        <div class="material_page">
            <div>
                <div class="material_title">
                    <h2>Nguyên vật liệu</h2>
                    <div>
                        <BaseButtonIcon icon="fa-solid fa-bullhorn" colorIcon="#2281c1" val="Phản hổi"/>
                    </div>
                </div>
                <div class="material_function">
                    <BaseButtonIcon noneBg noneBorder class="btn-function-main" icon="fa-solid fa-add" colorIcon="#2281c1" val="Thêm" @click="formDetailOnClick(TYPE_FORM.ADD)"/>
                    <BaseButtonIcon noneBg noneBorder class="btn-function-main" icon="fa-solid fa-copy" colorIcon="#2281c1" val="Nhân bản" @click="formDetailOnClick(TYPE_FORM.DUPLICATE)"/>
                    <BaseButtonIcon noneBg noneBorder class="btn-function-main" icon="fa-solid fa-pen-to-square" colorIcon="#2281c1" val="Sửa" @click="formDetailOnClick(TYPE_FORM.UPDATE)"/>
                    <BaseButtonIcon noneBg noneBorder class="btn-function-main" icon="fa-solid fa-xmark" colorIcon="red" val="Xóa" @click="deleteOneMaterial"/>
                    <BaseButtonIcon noneBg noneBorder class="btn-function-main" icon="fa-solid fa-rotate" colorIcon="#2281c1" val="Nạp" @click="reloadTable"/>
                </div>
            </div>
            <div class="material_table">
                <div class="table_main">
                    <BaseTable
                        :dataForm="dataFormMaterial"
                        :dataTable="dataTable"
                        :inforTable="inforTable"
                        showSearchBox
                        ref="tableMain"
                        @clickTrTable="clickRowTable"
                        @dbClickTrTable="updateMaterial"
                    >
                        <template #selectBoxMaterialCode>
                            <div class="search_filter">
                                <BaseFilterSearch type="string" field="materialCode" addition="AND" @changeFilter="changeFilter"/>
                            </div>
                        </template>
                        <template #selectBoxMaterialName>
                            <div class="search_filter">
                                <BaseFilterSearch type="string" field="materialName" addition="AND" @changeFilter="changeFilter"/>
                            </div>
                        </template>
                        <template #selectBoxFeature>
                            <div class="search_filter">
                                <BaseFilterSearch type="string" field="feature" addition="AND" @changeFilter="changeFilter"/>
                            </div>
                        </template>
                        <template #selectBoxUnitName>
                            <div class="search_filter">
                                <BaseFilterSearch type="string" field="unitName" addition="AND" @changeFilter="changeFilter"/>
                            </div>
                        </template>
                        <template #selectBoxCategoryName>
                            <div class="search_filter">
                                <BaseFilterSearch type="string" field="categoryName" addition="AND" @changeFilter="changeFilter"/>
                            </div>
                        </template>
                        <template #selectBoxDescription>
                            <div class="search_filter">
                                <BaseFilterSearch type="string" field="description" addition="AND" @changeFilter="changeFilter"/>
                            </div>
                        </template>
                        <template #selectBoxStatus>
                            <div class="search_filter">
                                <BaseSelectBox 
                                    :data="
                                        [
                                            { status: 'Có', value: '2' },
                                            { status: 'Không', value: '1' }
                                        ]
                                    "
                                    :style="`background-color: #fff;`"
                                    fieldNameShow="status"
                                    fieldNameValue="value"
                                    fieldName="Status"
                                    :setData="dataFilter.find((item) => item.Field == 'Status').Value"
                                    @getValue="changeFilterStatus"
                                    :fieldListHide="[
                                        { field: 'status' },
                                    ]"
                                />
                            </div>
                        </template>
                        <template #inputUnit="{data}">
                            <BaseCheckBox 
                                class="checkbox_table_status"
                                disable
                                :modelCheckbox="data.status == '2'"
                                :value="data.status"
                            />
                        </template>

                    </BaseTable>
                </div>
                <div class="nb_pages">
                    <TheNumberPage 
                        :totalCount="totalMaterial" 
                        ref="pageNumber"
                        @getDataNumberTable="getDataNumberTable"
                        @reloadAll="reloadTable"
                    />
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseButtonIcon from '@/components/Base/BaseButtonIcon.vue';
import BaseTable from '@/components/Base/BaseTable.vue';
import TheNumberPage from '@/components/Material/TheNumberPage.vue';
import TheForm from '@/components/Material/TheForm.vue';
import BasePopUp from '@/components/Base/BasePopUp.vue';

import { 
    TYPE_FORM,
    HTTP_METHOD,
    ERROR_VALIDATE_FE,
    ICON_TOAST_MESSAGE,
    FIELD_MATERIAL,
    FIELD_NAME_MATERIAL,
} from '@/lib/resource'
import { fetchAPI, clickoutside, generateUUID, getErrorBE } from '@/lib/lib.js';
import ERROR_CODE from '@/lib/errorCodeFE.js';
import BaseButton from '@/components/Base/BaseButton.vue';
import BaseInput from '@/components/Base/BaseInput.vue';
import BaseFilterSearch from '@/components/Base/BaseFilterSearch.vue';
import BaseToastMessage from '@/components/Base/BaseToastMessage.vue';
import BaseSelectBox from '@/components/Base/BaseSelectBox.vue';
import BaseCheckBox from '@/components/Base/BaseCheckBox.vue';

export default {
    components: { BaseButtonIcon, BaseTable, TheNumberPage, TheForm, BasePopUp, BaseButton, BaseInput, BaseFilterSearch, BaseToastMessage, BaseSelectBox, BaseCheckBox },
    data(){
        return{
            dataTable: {
                // Link get data
                url: ''.filterMaterialAPI(50, 1),
                //Id tương ứng cho mỗi hàng
                fieldId: FIELD_MATERIAL.materialID,
                // Khai báo các trường hiện thị trong bảng ()
                field: [
                    {
                        field: FIELD_MATERIAL.materialCode, // Trường dữ liệu
                        fieldName: FIELD_NAME_MATERIAL.materialCode, // Tên cột
                        textAlign: 'left', // Căn lề,
                        width: 130,
                        nameComponentTHead: 'selectBoxMaterialCode'
                    },
                    {
                        field: FIELD_MATERIAL.materialName,
                        fieldName: FIELD_NAME_MATERIAL.materialName,
                        textAlign: 'left',
                        width: 320,
                        nameComponentTHead: 'selectBoxMaterialName'
                    },
                    {
                        field: FIELD_MATERIAL.feature,
                        fieldName: FIELD_NAME_MATERIAL.feature,
                        textAlign: 'left',
                        width: 320,
                        nameComponentTHead: 'selectBoxFeature'
                        // format: 'date', // Định dạng kiểu dữ liệu
                    },
                    {
                        field: FIELD_MATERIAL.unitName,
                        fieldName: FIELD_NAME_MATERIAL.unitName,
                        textAlign: 'left',
                        width: 130,
                        nameComponentTHead: 'selectBoxUnitName'
                    },
                    {
                        field: FIELD_MATERIAL.categoryName,
                        fieldName: FIELD_NAME_MATERIAL.categoryName,
                        textAlign: 'left',
                        nameComponentTHead: 'selectBoxCategoryName'
                    },
                    {
                        field: FIELD_MATERIAL.description,
                        fieldName: FIELD_NAME_MATERIAL.description,
                        textAlign: 'left',
                        width: 320,
                        style: 'display: block;',
                        nameComponentTHead: 'selectBoxDescription'
                    },
                    {
                        field: FIELD_MATERIAL.status,
                        fieldName: FIELD_NAME_MATERIAL.status,
                        textAlign: 'center',
                        width: 150,
                        nameComponentTHead: 'selectBoxStatus',
                        type: 'component', // loại
                        nameComponent: 'inputUnit'
                    }
                ],
            },

            inforTable: {}, // Dữ liệu bảng

            dataFormMaterial: {}, // dữ liệu đổ vào form material 

            totalMaterial: 0, // Tổng số nguyên vật liệu 

             //Kiểm tra form employee, loại form( update, add )
             formMaterial: {
                isShowForm: false,
                type: '',
            },

            //loại form
            TYPE_FORM: {},

            //Data filter
            dataFilter: [
                {
                    Field: 'Status',
                    Operate: '=',
                    Addition: '',
                    Value: '1'
                }
            ],

            // hàng được chọn trong table
            dataRowTableTrClick: '',

            listToastMessage: [], // danh sách toast message

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
    methods: {
        /**
         * Lấy dữ liệu từ server
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        loadData(url, setFocusFirstPaging = true) {
            this.isLoadingTable = true; // bật hiệu ứng loading table

            // Không truyền url sẽ lấy mặc định khi khởi tạo table của dataTable
            if (!url) {
                url = this.dataTable.url;
            }
            fetchAPI(url, HTTP_METHOD.GET, (data) => {
                this.isLoadingTable = false; // dưng loading table
                // lỗi server
                if(data.userMsg){
                    this.customPopup(true, getErrorBE(res.errorCode), process.env.VUE_APP_POPUP_ERROR);
                }
                // thành công
                else if (data.data.length >= 0 && data.totalCount >= 0) {
                    let field = this.dataTable.field;
                    this.inforTable = {
                        field,
                        data: data.data,
                    };
                    this.totalMaterial = data.totalCount;
                }
                // Có lỗi không xác định
                else {
                    // hiển thị popup với mã lỗi không xác định sau khi duyêt hết
                    this.customPopup(true, ERROR_CODE[0].noDefine, process.env.VUE_APP_POPUP_ERROR);
                }
            }).finally(() => {
                if (setFocusFirstPaging) 
                    this.$refs.pageNumber.initNumberPage(); // set lại số trang sau khi load dữ liệu
            });
        },

        // Xử lý với form
        /**
         * Hiển thị form employee
         * @param {String} type // Loại form ('add', 'update')
         * CreatedBy: NDCHIEN (18/8/2022) 
         */
         showFormMaterial(type) {
            if (this.formMaterial) {
                this.formMaterial.isShowForm = true;
                this.formMaterial.type = type;
            }
        },

        /**
         * Đóng form employee
         * CreatedBy: NDCHIEN (18/8/2022) 
         */
        hideFormMaterial() {
            if (this.formMaterial) {
                this.formMaterial.isShowForm = false;
                this.formMaterial.type = '';
            }
        },

        /**
         * Thêm mới nguyên vật liệu
         * @param {string} type // loại form
         * @param {string} id // Nếu cần nhân bản truyền id mã nguyên vật liệu cần lấy
         */
        addMaterial(type, id){
            if(this.inforTable.data){
                this.dataFormMaterial = {
                    material: {},
                    listUnit: []
                }
                // nhân bản
                if(id){
                    var dataAddTable = {
                        material: {},
                        listUnit: []
                    }
                    // Lấy thông tin nguyên vật liệu từ server với id
                    return fetchAPI(
                        ''.updateMaterial(id),
                        HTTP_METHOD.GET,
                        (res) => {
                            if(res.userMsg){
                                // popup thông báo lỗi
                                this.customPopup(true, getErrorBE(res.errorCode), process.env.VUE_APP_POPUP_ERROR);
                            }
                            // Thành công
                            else{
                                dataAddTable = res
                            }

                        }
                    )
                    // Sau khi lấy thành công nguyên vật liệu sẽ lấy mã mới
                    .then(() => {
                        if(dataAddTable.material.materialName){
                            let string = dataAddTable.material.materialName.split(" ").map(item => {
                                return item[0].toUpperCase()
                            })
                            .filter( item => {
                                return item != "" && !/[!@#$%^&*()+\=\[\]{};':"\\|,.<>\/?]/.test(item)
                            })                            
                            .join(""); // Cắt khoảng trống nối chuỗi viết hoa chữ cái đầu
                            // Lấy mã mới
                            fetchAPI(
                                ''.newMaterialCode(string),
                                HTTP_METHOD.GET,
                                (res) => {
                                    if(res.userMsg){
                                        this.customPopup(true, getErrorBE(res.errorCode), process.env.VUE_APP_POPUP_ERROR);
                                    }
                                    else{
                                        dataAddTable.material.materialCode = res
    
                                        // Xử lý hạn sử dụng cắt chuỗi "_"
                                        if(dataAddTable.material.expiryDate){
                                            var exp = dataAddTable.material.expiryDate.split("_");
                                            dataAddTable.material.valueExp = exp[0];
                                            dataAddTable.material.typeExp = exp[1]
                                        }
                                        dataAddTable.listConversionUnits = dataAddTable.listConversionUnits.map((item, index) => {
                                            // return {...item, stt: index + 1};
                                            return {...item, stt: generateUUID()};
                                        })
                                        // Thêm mới data vào bảng 
                                        this.inforTable.data.unshift(dataAddTable.material)
                                        // Set dữ liệu cho data form
                                        this.dataFormMaterial = {
                                            material: this.inforTable.data[0],
                                            listUnit: dataAddTable.listConversionUnits
                                        }
                                        this.showFormMaterial(type) // hiển thị form thêm mới
                                    }
                                }
                            )

                        }
                    })
                }
                // thêm mới
                else{
                    this.inforTable.data.unshift({}) // Khởi tạo data rỗng thêm vào table
                    // set dữ liệu cho data form
                    this.dataFormMaterial = {
                        material: this.inforTable.data[0],
                        listUnit: []
                    }
                    this.showFormMaterial(type) // hiển thị form
                }
            }
        },

        /**
         * Cập nhập thông tin nguyên vật liệu
         */
        updateMaterial(data, id){
            // Lấy thông tin nguyên vật liệu
            fetchAPI(
                ''.updateMaterial(id), 
                HTTP_METHOD.GET, 
                (res) => {
                    if(res.userMsg){
                        // show lỗi backend
                        this.customPopup(true, getErrorBE(res.errorCode), process.env.VUE_APP_POPUP_ERROR);
                    }
                    else{
                        // Thành công
                        for (const key in res.material) {
                            data[key] = res.material[key]
                        }
                        if(data.expiryDate){
                            var exp = data.expiryDate.split("_");
                            data.valueExp = exp[0];
                            data.typeExp = exp[1]
                        }
                        res.listConversionUnits = res.listConversionUnits.map((item, index) => {
                            // return {...item, stt: index + 1}                                            
                            return {...item, stt: generateUUID()};

                        })
                        var listUnit = res.listConversionUnits.map(item => {
                            return {...item, conversionUnitIDOld: item.conversionUnitID}
                        })
                        this.dataFormMaterial = {
                            material: data,
                            listUnit
                        } // Truyền dữ liệu cho form
                        this.showFormMaterial(process.env.VUE_APP_PROCESS_UPDATE); // loại form
                    }
                    
                })
        },

        /**
         * reload lại table
         * CreatedBy: NDCHIEN (18/8/2022) 
         */
         reloadTable() {
            try {
                let pageChoice = this.$refs.pageNumber.pageChoice // trang được chọn 
                let pageSize = this.$refs.pageNumber.pageSize // kích thước mỗi trang
                this.loadData(
                    ''.filterMaterialAPI(pageSize, pageChoice, JSON.stringify(this.dataFilter)), 
                    false
                );
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Click mở form 
         */
        formDetailOnClick(type){
            try {
                switch (type){
                    case TYPE_FORM.ADD:
                        this.addMaterial(type)
                        break;
                    case TYPE_FORM.UPDATE:
                        if(this.dataRowTableTrClick){
                            this.updateMaterial(this.dataRowTableTrClick, this.dataRowTableTrClick.materialID)
                        }
                        break;
                    case TYPE_FORM.DUPLICATE:
                        if(!this.dataRowTableTrClick && this.popup.callbackFun){
                            this.customPopup(
                                true, 
                                `${ERROR_VALIDATE_FE.RequireChoiceMaterial}`,
                                process.env.VUE_APP_POPUP_ERROR
                            )
                        }
                        else{
                            this.addMaterial(type, this.dataRowTableTrClick.materialID)
                        }
                        break;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /** 
         * Click 1 hàng trong table
         */
        clickRowTable(data){
            try {
                this.dataRowTableTrClick = data
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Xóa 1 nguyên vật liệu
         */
        deleteOneMaterial(){
            if(this.dataRowTableTrClick && this.popup.callbackFun){
                // show popup xác nhận xóa
                this.customPopup(
                    true, 
                    `${ERROR_VALIDATE_FE.QuestionDeleteMaterial} << ${this.dataRowTableTrClick.materialCode} - ${this.dataRowTableTrClick.materialName} >> ?`,
                    process.env.VUE_APP_POPUP_WARNING_DEL
                )
                // Hàm xử lý khi click có
                this.popup.callbackFun.callbackWarningDeleteYes = () => {
                    // Call API xóa
                    fetchAPI(
                        ''.deleteOneMaterial(this.dataRowTableTrClick.materialID),
                        HTTP_METHOD.DELETE,
                        (res) => {
                            // Thất bại
                            if(res.userMsg){
                                this.customPopup(); // Đóng popup
                                // Thêm toast message thông báo
                                this.listToastMessage.push({
                                    id: generateUUID(),
                                    icon: ICON_TOAST_MESSAGE.error ,
                                    messageToast: `${ERROR_VALIDATE_FE.DeleteFail} << ${this.dataRowTableTrClick.materialCode} - ${this.dataRowTableTrClick.materialName} >>`,
                                });
                            }
                            // Thành công
                            else{
                                this.customPopup(); // Đóng popup
                                this.setCountTotal(-1) // Set lại tổng số bản ghi
                                // Thêm toast message thông báo
                                this.listToastMessage.push({
                                    id: generateUUID(),
                                    icon: ICON_TOAST_MESSAGE.success,
                                    messageToast: `${ERROR_VALIDATE_FE.DeleteSuccess} << ${this.dataRowTableTrClick.materialCode} - ${this.dataRowTableTrClick.materialName} >>`,
                                });
                                // Xóa phần tử bị xóa ra khỏi mảng table
                                this.inforTable.data = this.inforTable.data.filter(item => {
                                    return item.materialID != this.dataRowTableTrClick.materialID
                                })
                                this.dataRowTableTrClick = null;
                            }
                        }
                    )
                }
            }
            else if(!this.dataRowTableTrClick && this.popup.callbackFun){
                this.customPopup(
                    true, 
                    `${ERROR_VALIDATE_FE.RequireChoiceMaterial}`,
                    process.env.VUE_APP_POPUP_ERROR
                )
            }
        },

        /**
         * Lấy data cho table khi có pagesize
         */
         getDataNumberTable(data) {
            this.loadData(
                ''.filterMaterialAPI(data.pageSize, data.pageNumber, JSON.stringify(this.dataFilter)), 
                false
            );
        },

        /**
         * Set tổng số bản ghi
         * Không truyền tham số sẽ tăng lên 1
         * Truyền -1 giảm đi 1
         * Truyền lớn > 0 được set bằng giá trị đó không sẽ không được set
         * CreatedBy: NDCHIEN (18/8/2022) 
         */
         setCountTotal(total) {
            
            if (!total) this.totalMaterial += 1;
            else {
                if (total == -1) this.totalMaterial -= 1;
                if (total > 0) this.totalMaterial = total;
            }
        },

        /**
         * Xử lý sau khi thêm material
         */
        afterAddMaterial(){
            this.setCountTotal()
            if(this.$refs.tableMain && this.inforTable.data){
                this.$refs.tableMain.trClick(this.inforTable.data[0])
            }
        },

        /**
         * Thêm toast message
         */
         addToastMessage({icon: icon, message: message}){
            this.listToastMessage.push({
                id: generateUUID(),
                icon: icon,
                messageToast: message,
            })
        },

        /**
         * Đóng 1 toast message
         */
         closeToastItem(toast) {
            try {
                if (this.listToastMessage) {
                    let index = this.listToastMessage.indexOf(toast);
                    this.listToastMessage.splice(index, 1);
                }
            } catch (error) {
                console.log(error);
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

        /**
         * Thay đổi trạng thái ngưng sử dụng trong selectbox
         */
        changeFilterStatus(value){
            if(value){
                // Thông tin gửi lên server search
                var addFilter = {
                    Field: value.fieldName,
                    Operate: "=",
                    Addition: 'AND',
                    Value: value.val
                }
                this.dataFilter = this.dataFilter.filter((data) => {
                    return data.Field != addFilter.Field
                })
                this.dataFilter.push(addFilter);
                this.dataFilter[0].Addition = "";
                // call lấy data
                this.loadData(
                    ''.filterMaterialAPI(50,1,JSON.stringify(this.dataFilter)),
                    true
                )
            }
        },

        /**
         * Thay đổi loại lọc tìm kiếm
         */
        changeFilter(value){
            try {
                if(value.filterVal != ""){
                    // Tạo đối tượng search lọc thông tin
                    var addFilter = {
                        Field: value.field.charAt(0).toUpperCase() + value.field.slice(1),
                        Operate: value.filterType,
                        Addition: value.addition,
                        Value: value.filterVal
                    }
                    // Xóa phần tử cần tìm kiếm nếu có Field cần search trùng với addFilter vừa tạo ở trên
                    this.dataFilter = this.dataFilter.filter((data) => {
                        return data.Field != addFilter.Field
                    })
                    this.dataFilter.push(addFilter);
                    this.dataFilter[0].Addition = "";
                    // Call hàm load lại dữ liệu cần search
                    this.loadData(
                        ''.filterMaterialAPI(50,1,JSON.stringify(this.dataFilter)),
                        true
                    )
                }
                else{
                    var lengthOrigin = this.dataFilter.length // Độ dài của dữ liệu cần lọc ban đầu
                    this.dataFilter = this.dataFilter.filter((data) => {
                        return data.Field != value.field.charAt(0).toUpperCase() + value.field.slice(1)
                    })
                    // Nếu sau khi xóa phần từ cần lọc độ dài thay đổi thì call api
                    if(lengthOrigin > this.dataFilter.length){
                        this.loadData(
                        ''.filterMaterialAPI(50,1,JSON.stringify(this.dataFilter)),
                        true
                    )
                    }
                }
            } catch (error) {
                console.log(error)
            }
        }
    },
    created() {
        // lấy dữ liệu
        // this.loadData();
        this.loadData(
            ''.filterMaterialAPI(50, 1, JSON.stringify(this.dataFilter)), 
            true
        );

        this.TYPE_FORM = TYPE_FORM
    },
}
</script>
<style>
    h2{
        font-size: 24px;
        font-weight: 500;
    }

    .page_container{
        flex: 1;
        margin-right: 7px;
        margin-bottom: 5px;
        position: relative;
        background-color: #fff;
    }
    
    .material_page{
        height: calc(100vh - 60px);
        margin: 18px 0 5px 7px;
    }

    .material_title{
        display: flex;
        justify-content: space-between;
        margin-bottom: 15px;
    }

    .material_function{
        background-color: #eaeaec;
        padding: 3px 3px;
        height: 29px;
        display: flex;
        align-items: center;
        border-top: 1px solid rgb(203, 203, 203);
        border-left: 1px solid rgb(203, 203, 203);
        border-right: 1px solid rgb(203, 203, 203);
    }

    .material_table{
        height: calc(100vh - 190px) !important;
        overflow: auto;
        /* border: 1px solid rgb(203, 203, 203); */
    }

    .nb_pages{
        position: absolute;
        width: calc(100% - 7px);
        bottom: 0;
    }

    .table_main{
        max-height: calc(100% - 40px);
    }

    .search_filter{
        width: 100%;
    }

    /* Toast message */
    .toast-message-alert{
        position: fixed;
        top: 10px;
        left: calc(50vw - 250px);
        z-index: 500;
    }

    .checkbox_table_status{
        display: inline
    }

    .btn-function-main{
        border: 1px solid transparent;
    }

    .btn-function-main:hover{
        background-color: #fff;
        border: 1px solid #2281c1;
    }
</style>