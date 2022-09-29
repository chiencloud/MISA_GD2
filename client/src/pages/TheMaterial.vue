<template>
    <div class="page_container">
        <TheForm/>
        <div class="material_page">
            <div>
                <div class="material_title">
                    <h2>Nguyên vật liệu</h2>
                    <div>
                        <BaseButtonIcon icon="fa-solid fa-bullhorn" colorIcon="#2281c1" val="Phản hổi"/>
                    </div>
                </div>
                <div class="material_function">
                    <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-add" colorIcon="#2281c1" val="Thêm"/>
                    <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-copy" colorIcon="#2281c1" val="Nhân bản"/>
                    <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-pen-to-square" colorIcon="#2281c1" val="Sửa"/>
                    <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-xmark" colorIcon="red" val="Xóa"/>
                    <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-rotate" colorIcon="#2281c1" val="Nạp"/>
                </div>
            </div>
            <div class="material_table">
                <div class="table_main">
                    <BaseTable
                        :dataTable="dataTable"
                        :inforTable="inforTable"
                        showSearchBox
                    />
                </div>
                <div class="nb_pages"><TheNumberPage /></div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseButtonIcon from '@/components/Base/BaseButtonIcon.vue';
import BaseTable from '@/components/Base/BaseTable.vue';
import TheNumberPage from '@/components/Material/TheNumberPage.vue';
import TheForm from '@/components/Material/TheForm.vue';
export default {
    components: { BaseButtonIcon, BaseTable, TheNumberPage, TheForm },
    data(){
        return{
            dataTable: {
                // Link get data
                url: ''.filterEployeeAPI(20, 1),
                //Id tương ứng cho mỗi hàng
                fieldId: 'employeeId',
                // Khai báo các trường hiện thị trong bảng ()
                field: [
                    {
                        field: 'employeeCode', // Trường dữ liệu
                        fieldName: 'Mã nguyên vật liệu', // Tên cột
                        textAlign: 'left', // Căn lề
                    },
                    {
                        field: 'employeeName',
                        fieldName: 'Tên nguyên vật liệu',
                        textAlign: 'left',
                    },
                    {
                        field: 'dateOfBirth',
                        fieldName: 'Tính chất',
                        textAlign: 'center',
                        // format: 'date', // Định dạng kiểu dữ liệu
                    },
                    {
                        field: 'gender',
                        fieldName: 'ĐVT tính',
                        textAlign: 'left',
                    },
                    {
                        field: 'identityNumber',
                        fieldName: 'Nhóm nguyên vật liệu',
                        textAlign: 'left',
                    },
                    {
                        field: 'identityIssuedDate',
                        fieldName: 'Ghi chú',
                        textAlign: 'center',
                    }
                ],
            },
            inforTable: {
                data: [
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    }
                ]
            }
        }
    },
    created() {
        let field = this.dataTable.field;
        this.inforTable = {
            field,
            data: [
                    {
                        employeeCode: "Chiến",
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                    {
                        employeeCode: 1,
                        employeeName: 2,
                        dateOfBirth: 3,
                        gender: 4,
                        identityNumber: 5,
                        identityIssuedDate: 6
                    },
                ]
        };
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
        height: 25px;
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
</style>