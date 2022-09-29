<template>
    <div 
        class="container-number-pages"
    >
        <div class="number-pages">
            
            <div class="number-pages-right">
                <div class="number-pages-navigate">
                    <div class="prev-page">
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angles-left" colorIcon="#bbb"/>
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angle-left" colorIcon="#bbb"/>
                    </div>
                    <div class="list-pages">
                        Trang &ensp;
                        <span style="width: 60px"><BaseInput /></span>
                        &ensp;trên 1
                        <!-- <button
                            v-for="(item, index) in pageNumber"
                            :key="item"
                            :class="{ 'page-choice': pageChoice == item && parseInt(item) }"
                            @click="clickNumberTable(pageNumber[index - 1], item, pageNumber[index + 1])"
                            >
                            {{ item }} -->
                        <!-- </button> -->
                    </div>
                    <div class="next-page">
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angle-right" colorIcon="#bbb"/>
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angles-right" colorIcon="#bbb"/>
                    </div>
                </div>
                <div class="number-item-on-pages">
                    <BaseSelectBox
                        :data="listDataEmpInPage"
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
            </div>
            <div class="number-pages-left">
                Hiển thị 1 - 100 trên <b>{{ totalCount }}</b> kết quả
            </div>
        </div>
    </div>
</template>
<script>
import BaseSelectBox from '@/components/Base/BaseSelectBox.vue';
import BaseButtonIcon from '../Base/BaseButtonIcon.vue';
import BaseInput from '../Base/BaseInput.vue';

export default {
    components: { BaseSelectBox, BaseButtonIcon, BaseInput },
    props: {
        totalCount: Number, // Tổng số bản ghi
        widthNumberPage: Number, // Đô rộng của number page
    },
    data() {
        return {
            pageChoice: 1, // page number được chọn
            pageNumber: [], // danh sách số trang
            totalEmployee: 0, // tổng số nhân viên
            pageSize: 0, // kích thước một trang
            // Danh sách hiển thị số bản ghi trên trang
            listDataEmpInPage: [
                { NumberWriteShow: '20', NumberWrite: 20 },
                { NumberWriteShow: '40', NumberWrite: 40 },
                { NumberWriteShow: '50', NumberWrite: 50 },
                { NumberWriteShow: '70', NumberWrite: 70 },
                { NumberWriteShow: '100', NumberWrite: 100 },
            ],
        };
    },

    methods: {
        /**
         * lấy số trang phân trang
         * @param {*} dataValue dữ liệu để phân tranng
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValueEmpInPage(dataValue) {
            if (dataValue.val && dataValue.fieldName) {
                this.pageNumber = []; // Khỏi tạo lại danh sách số trang
                this.pageSize = dataValue.val; // gán kích thước một trang
                var a = parseInt(this.totalCount / this.pageSize);
                if (this.totalCount % dataValue.val != 0) a++;

                // Nếu số trang > 5 sẽ hiển thị dấu ...
                if (a > 5) {
                    this.pageNumber.push(1);
                    if (this.pageChoice <= 2) {
                        this.pageNumber.push(2);
                        this.pageNumber.push(3);
                        this.pageNumber.push('...');
                    }
                    this.pageNumber.push(a);
                } else {
                    for (let i = 1; i <= a; i++) {
                        this.pageNumber.push(i);
                    }
                }

                // this.pageNumber = pageNumberShow
                this.pageChoice = this.pageNumber[0];
                this.$emit('getDataNumberTable', {
                    pageNumber: this.pageChoice,
                    pageSize: this.pageSize,
                });
            }
        },

        /**
         * click vào số trang
         * CreatedBy: NDCHIEN (18/8/2022)
        */ 
        clickNumberTable(pagePrev, pageNumber, pageNext) {
            // Khởi tạo lại page number

            var a = parseInt(this.totalCount / this.pageSize);
            if (this.totalCount % this.pageSize != 0) a++;

            // Click vào số trang
            if (parseInt(pageNumber)) {
                this.pageChoice = pageNumber;

                if (a > 5) {
                    this.pageNumber = []; // Khỏi tạo lại danh sách số trang
                    this.pageNumber.push(1);
                    if (this.pageChoice <= 2) {
                        this.pageNumber.push(2);
                        this.pageNumber.push(3);
                        this.pageNumber.push('...');
                    } else if (this.pageChoice > 2 && this.pageChoice < a - 2) {
                        this.pageNumber.push('...');
                        this.pageNumber.push(this.pageChoice);
                        this.pageNumber.push(this.pageChoice + 1);
                        this.pageNumber.push(this.pageChoice + 2);
                        this.pageNumber.push('...');
                    } else {
                        this.pageNumber.push('...');
                        this.pageNumber.push(a - 2);
                        this.pageNumber.push(a - 1);
                    }
                    this.pageNumber.push(a);
                }

                // Load bảng khi chọn trang
                this.$emit('getDataNumberTable', {
                    pageNumber: this.pageChoice,
                    pageSize: this.pageSize,
                });
            } 
            // Khi click vào ...
            else if (pageNext && pagePrev) {
                if (parseInt(pageNext) == a) {
                    this.pageNumber = []; // Khỏi tạo lại danh sách số trang
                    this.pageNumber.push(1);
                    if (parseInt(pagePrev) >= a - 3) {
                        this.pageNumber.push('...');
                        this.pageNumber.push(a - 2);
                        this.pageNumber.push(a - 1);
                    } else if (parseInt(pagePrev) >= 3 && parseInt(pagePrev) < a - 3) {
                        this.pageNumber.push('...');
                        this.pageNumber.push(parseInt(pagePrev) + 1);
                        this.pageNumber.push(parseInt(pagePrev) + 2);
                        this.pageNumber.push(parseInt(pagePrev) + 3);
                        this.pageNumber.push('...');
                    } else {
                        this.pageNumber.push(2);
                        this.pageNumber.push(3);
                        this.pageNumber.push('...');
                    }
                    this.pageNumber.push(a);
                } else if (parseInt(pagePrev) == 1) {
                    this.pageNumber = []; // Khỏi tạo lại danh sách số trang
                    this.pageNumber.push(1);
                    if (parseInt(pageNext) <= 4) {
                        this.pageNumber.push(2);
                        this.pageNumber.push(3);
                        this.pageNumber.push('...');
                    } else if (parseInt(pageNext) > 4 && parseInt(pageNext) <= a - 2) {
                        this.pageNumber.push('...');
                        this.pageNumber.push(parseInt(pageNext) - 3);
                        this.pageNumber.push(parseInt(pageNext) - 2);
                        this.pageNumber.push(parseInt(pageNext) - 1);
                        this.pageNumber.push('...');
                    } else {
                        this.pageNumber.push('...');
                        this.pageNumber.push(a - 2);
                        this.pageNumber.push(a - 1);
                    }
                    this.pageNumber.push(a);
                }
            }
        },

        /**
         * khởi tạo sô trang
         * CreatedBy: NDCHIEN (18/8/2022) 
        */ 
        initNumberPage(pageSize = 20){
            if(this.$refs.selectItemPerPage.getValueSelectBox().value){
                pageSize = this.$refs.selectItemPerPage.getValueSelectBox().value
            }
            this.pageSize = pageSize;
            var a = parseInt(this.totalCount / this.pageSize);
            if (this.totalCount % this.pageSize != 0) a++;
            
            this.pageNumber = []; // Khỏi tạo lại danh sách số trang
            if (a > 5) {
                this.pageNumber.push(1); 
                this.pageNumber.push(2);
                this.pageNumber.push(3);
                this.pageNumber.push('...');
                this.pageNumber.push(a);
            }
            else {
                for (let i = 1; i <= a; i++) {
                    this.pageNumber.push(i);
                }
            }
        }
    }
};
</script>
<style scoped>
@import url('@/style/layout/number-pages.css');
</style>
