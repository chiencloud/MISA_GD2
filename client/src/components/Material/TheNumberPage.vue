<template>
    <div 
        class="container-number-pages"
    >
        <div class="number-pages">
            
            <div class="number-pages-right">
                <div class="number-pages-navigate">
                    <div class="prev-page">
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angles-left" colorIcon="#bbb" @click="getPage(1)"/>
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angle-left" colorIcon="#bbb" @click="getPage(pageChoice - 1)"/>
                    </div>
                    <div class="list-pages">
                        Trang &ensp;
                        <span style="width: 60px">
                            <BaseInput 
                                @keyboard="changeNumberPage"
                                @onBlur="blurTextSearchNumberPage"
                                type="int"
                                v-model:value="pageChoice"
                            />
                        </span>
                        &ensp;trên {{totalPage}}
                    </div>
                    <div class="next-page">
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angle-right" colorIcon="#bbb" @click="getPage(pageChoice + 1)"/>
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-angles-right" colorIcon="#bbb" @click="getPage(totalPage) "/>
                    </div>
                    <div class="reload">
                        <BaseButtonIcon noneBg noneBorder icon="fa-solid fa-arrow-rotate-right" colorIcon="#bbb" @click="reloadAll() "/>
                    </div>
                </div>
                <div class="number-item-on-pages">
                    <BaseSelectBox
                        :data="listDataEmpInPage"
                        addSelectIcon="fa-solid fa-plus"
                        addSelectColor="#2281c1"
                        :style="`width: 100px`"
                        fieldNameShow="NumberWriteShow"
                        fieldNameValue="NumberWrite"
                        setData="50"
                        fieldName="NumberWrite"
                        :fieldListHide="[
                            { field: 'NumberWriteShow' },
                        ]"
                        focusFirstData
                        styleValHide="top"
                        ref="selectItemPerPage"
                        @getValue="getValueEmpInPage"
                    />
                </div>
            </div>
            <div class="number-pages-left">
                Hiển thị {{(pageChoiceOld - 1) * pageSize + 1}} - {{pageChoiceOld * pageSize < totalCount ? pageChoiceOld * pageSize : totalCount}} trên <b>{{ totalCount }}</b> kết quả
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
            pageChoiceOld: 1, // page number được chọn
            totalEmployee: 0, // tổng số nhân viên
            pageSize: 0, // kích thước một trang
            totalPage: 0, // tổng số trang
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
                this.totalPage = parseInt(this.totalCount / this.pageSize);
                if (this.totalCount % dataValue.val != 0) this.totalPage++;

                // this.pageNumber = pageNumberShow
                this.$emit('getDataNumberTable', {
                    pageNumber: this.pageChoice,
                    pageSize: this.pageSize,
                });
                this.pageChoiceOld = this.pageChoice
            }
        },


        /**
         * Blur text search number page
         */
        blurTextSearchNumberPage(data){
            try {
                if(data){
                    if(data.val <= this.totalPage && data.val >= 1){
                        this.pageChoice = data.val 
                        this.pageChoiceOld = data.val 
                        this.$emit('getDataNumberTable', { 
                            pageNumber: this.pageChoice,
                            pageSize: this.pageSize,
                        });
                        this.pageChoiceOld = this.pageChoice
                    }
                    else{
                        this.pageChoice = this.pageChoiceOld
                    }
                }
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * thay đổi
         */
        changeNumberPage(data){
            try {
                if(data){
                    switch (data.keyCode) {
                        case 13: // Enter
                            if(data.val <= this.totalPage && data.val >= 1){
                                this.pageChoice = data.val 
                                this.pageChoiceOld = data.val 
                                this.$emit('getDataNumberTable', {
                                    pageNumber: this.pageChoice,
                                    pageSize: this.pageSize,
                                });
                            }
                            else{
                                this.pageChoice = this.pageChoiceOld
                            }
                            break;
                    
                        default:
                            break;
                    }
                }
            } catch (error) {
                console.log(error)
            }
        },

        reloadAll(){
            this.$emit('reloadAll');
        },

        /**
         * Lấy trang
         * @param {*} pagePosition 
         */
        getPage(pagePosition){
            if(pagePosition <= this.totalPage && pagePosition >= 1){
                this.pageChoice = pagePosition
                this.pageChoiceOld = pagePosition 
                this.$emit('getDataNumberTable', {
                    pageNumber: this.pageChoice,
                    pageSize: this.pageSize,
                });
            }
            else{
                this.pageChoice = this.pageChoiceOld
            }
        },

        /**
         * khởi tạo sô trang 
        */ 
        initNumberPage(pageSize = 50){
            if(this.$refs.selectItemPerPage.getValueSelectBox().value){
                pageSize = this.$refs.selectItemPerPage.getValueSelectBox().value
            }
            this.pageSize = pageSize;
            this.totalPage = parseInt(this.totalCount / this.pageSize);
            if (this.totalCount % this.pageSize != 0) this.totalPage++;
        }

        
    }
};
</script>
<style scoped>
@import url('@/style/layout/number-pages.css');
</style>
