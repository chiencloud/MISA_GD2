<template>
    <div class="form_material">
        <!-- 
        <BaseNotice>
            <template #title>
                Thêm đơn vị tính
            </template>
            <template #content>
                <div class="content_add_unit">
                    <div class="add_unit_item">
                        <div class="add_unit_name">
                            Đơn vị tính <span style="color: red">(*)</span>
                        </div>    
                        <BaseInput style="width: 300px" />
                    </div>
                    <div class="add_unit_func">
                        <div>
                            <BaseButtonIcon icon="fa-solid  fa-circle-question" colorIcon="#2281c1" val="Giúp"/>
                        </div>
                        <div>
                            <BaseButtonIcon icon="fa-solid fa-file-export" colorIcon="#2281c1" val="Cất"/>
                            <BaseButtonIcon icon="fa-regular fa-circle-xmark" colorIcon="red" val="Hủy bỏ"/>
                        </div>
                    </div>
                </div>
            </template>
        </BaseNotice>
        <BaseNotice>
            <template #title>
                Nhóm nguyên vật liệu
            </template>
            <template #content>
                <div class="content_add_unit">
                    <div class="add_unit_list">
                        <div class="add_unit_item">
                            <div class="add_unit_name">
                                Mã nhóm <span style="color: red">(*)</span>
                            </div>    
                            <BaseInput style="width: 300px" />
                        </div>
                        <div class="add_unit_item">
                            <div class="add_unit_name">
                                Tên nhóm <span style="color: red">(*)</span>
                            </div>    
                            <BaseInput style="width: 300px" />
                        </div>
                    </div>
                    <div class="add_unit_func">
                        <div>
                            <BaseButtonIcon icon="fa-solid  fa-circle-question" colorIcon="#2281c1" val="Giúp"/>
                        </div>
                        <div>
                            <BaseButtonIcon icon="fa-solid fa-file-export" colorIcon="#2281c1" val="Cất"/>
                            <BaseButtonIcon icon="fa-regular fa-circle-xmark" colorIcon="red" val="Hủy bỏ"/>
                        </div>
                    </div>
                </div>
            </template>
        </BaseNotice> -->
        <div class="form_detail">
            <div class="form_title">
                <div class="title_left">Thêm nguyên vật liệu</div>
                <div class="title_right">
                    <div><i class="fa-solid fa-up-right-and-down-left-from-center"></i></div>
                    <div><i class="fa-solid fa-xmark"></i></div>
                </div>
            </div>
            <div class="detail">
                <div class="info">
                    <div class="info_container_1">
                        <div class="info_material">
                            <div class="info_name">Tên<span><span style="color: red">(*)</span></span></div>
                            <BaseInput style="width: 300px" />
                        </div>
                        <div class="info_material">
                            <div class="info_name">Mã<span><span style="color: red">(*)</span></span></div>
                            <BaseInput style="width: 300px" />
                        </div>
                        <div class="info_material">
                            <div class="info_name" title="Đơn vị tính">ĐVT<span><span style="color: red">(*)</span></span></div>
                            <BaseInput style="width: 300px" />
                        </div>
                        <div class="info_material">
                            <div class="info_name">Kho ngầm định</div>
                            <BaseSelectBox
                                :data="
                                    [
                                        { NumberWriteShow: '20', NumberWrite: 20 },
                                        { NumberWriteShow: '40', NumberWrite: 40 },
                                        { NumberWriteShow: '50', NumberWrite: 50 },
                                        { NumberWriteShow: '70', NumberWrite: 70 },
                                        { NumberWriteShow: '100', NumberWrite: 100 },
                                    ]
                                "
                                addSelect
                                addSelectIcon="fa-solid fa-plus"
                                addSelectColor="#2281c1"
                                width="100px"
                                fieldNameShow="NumberWriteShow"
                                fieldNameValue="NumberWrite"
                                setData="20"
                                fieldName="NumberWrite"
                                :fieldListHide="[
                                    { field: 'NumberWriteShow' },
                                ]"
                                focusFirstData
                                styleValHide="bottom: calc(100% + 5px)"
                                ref="selectItemPerPage"
                                @getValue="getValueEmpInPage"
                            />
                        </div>
                        <div class="info_material">
                            <div class="info_name">Hạn sử dụng</div>
                            <BaseInput style="width: 300px" />
                        </div>
                        <div class="info_material">
                            <div class="info_name" title="Số lượng tồn tối thiểu">SL tồn tối thiểu</div>
                            <BaseInput style="width: 300px" />
                        </div>
                    </div>
                    <div class="info_material info_w100">
                        <div class="info_name" title="Số lượng tồn tối thiểu">SL tồn tối thiểu</div>
                        <textarea></textarea>
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
                        >
                            <template #selectBox>
                                <BaseSelectBox
                                    :data="
                                        [
                                            { NumberWriteShow: '20', NumberWrite: 20 },
                                            { NumberWriteShow: '40', NumberWrite: 40 },
                                            { NumberWriteShow: '50', NumberWrite: 50 },
                                            { NumberWriteShow: '70', NumberWrite: 70 },
                                            { NumberWriteShow: '100', NumberWrite: 100 },
                                        ]
                                    "
                                    addSelect
                                    addSelectIcon="fa-solid fa-plus"
                                    addSelectColor="#2281c1"
                                    width="100px"
                                    showBorder
                                    fieldNameShow="NumberWriteShow"
                                    fieldNameValue="NumberWrite"
                                    setData="20"
                                    fieldName="NumberWrite"
                                    :fieldListHide="[
                                        { field: 'NumberWriteShow' },
                                    ]"
                                    focusFirstData
                                    styleValHide="bottom: calc(100% + 5px)"
                                    ref="selectItemPerPage"
                                    @getValue="getValueEmpInPage"
                                />
                            </template>
                        </BaseTable>
                    </div>
                    <div class="func_unit">
                        <BaseButtonIcon icon="fa-solid fa-add" colorIcon="#2281c1" val="Thêm dòng"/>
                        <BaseButtonIcon icon="fa-solid fa-close" colorIcon="red" val="Xóa dòng"/>
                    </div>
                </div>
                <div class="info_w100 func_form">
                    <div class="func_form_left">
                        <BaseButtonIcon icon="fa-solid fa-circle-question" colorIcon="#2281c1" val="Giúp"/>
                    </div>
                    <div class="func_form_right">
                        <BaseButtonIcon icon="fa-sharp fa-solid fa-floppy-disk" colorIcon="#2281c1" val="Thêm dòng"/>
                        <BaseButtonIcon icon="fa-solid fa-file-export" colorIcon="#2281c1" val="Thêm dòng"/>
                        <BaseButtonIcon icon="fa-regular fa-circle-xmark" colorIcon="red" val="Thêm dòng"/>
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
export default {
    components: { BaseInput, BaseTable, BaseButtonIcon, BaseSelectBox, BaseNotice },
    data(){
        return {
            dataTable: {
                // Link get data
                url: ''.filterEployeeAPI(20, 1),
                //Id tương ứng cho mỗi hàng
                fieldId: 'employee',
                // Khai báo các trường hiện thị trong bảng ()
                field: [
                    {
                        field: 'materialId', // Trường dữ liệu
                        fieldName: 'STT', // Tên cột
                        textAlign: 'left', // Căn lề
                    },
                    {
                        field: 'employeeName',
                        fieldName: 'Đơn vị chuyển đổi',
                        textAlign: 'left',
                    },
                    {
                        field: 'gender',
                        fieldName: 'Tỷ lệ chuyển đổi',
                        textAlign: 'left',
                    },
                    {
                        field: 'dateOfBirth',
                        fieldName: 'Phép tính',
                        textAlign: 'center',
                        // format: 'date', // Định dạng kiểu dữ liệu
                    },
                    {
                        field: 'identityNumber',
                        fieldName: 'Mô tả',
                        textAlign: 'left',
                    },
                ],
            },
            inforTable: {
                field: [
                {
                        field: 'materialId', // Trường dữ liệu
                        fieldName: 'STT', // Tên cột
                        textAlign: 'left', // Căn lề
                    },
                    {
                        field: 'employeeName',
                        fieldName: 'Đơn vị chuyển đổi',
                        textAlign: 'left',
                        type: 'component', // loại
                        nameComponent: 'selectBox'
                    },
                    {
                        field: 'gender',
                        fieldName: 'Tỷ lệ chuyển đổi',
                        textAlign: 'left',
                    },
                    {
                        field: 'dateOfBirth',
                        fieldName: 'Phép tính',
                        textAlign: 'center',
                        // format: 'date', // Định dạng kiểu dữ liệu
                    },
                    {
                        field: 'identityNumber',
                        fieldName: 'Mô tả',
                        textAlign: 'left',
                    },
                ],
                data: [
                    {
                        materialId: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    }
                ]
            }
        }
    }
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
        background-color: rgb(252, 252, 252);
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
        border: 1px solid #bbb;
        flex: 1;
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

    .add_unit_list{

    }

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