<template>
    <div class="filter_search">
        <BaseButton :val="filterSelectedShow" v-click-out-side="hideBoxFilter" @click="setShowBoxFilter"/>
        <BaseInput 
            class="filter_input"
            v-model:value="valueSearch"
            @keyup="onKeyUp"
            @change="onChange"
        />
        <div class="list_filter" v-if="showBoxFilter">
            <div 
                v-for="(itemFilter, index) in listFilterShow" 
                :key="index"
                class="item_filter"
                @click="() => setFilterSelected(itemFilter)"
            >
                <div 
                    class="filter_selected"
                    :class="{'filter_selected_background': itemFilter.value == filterSelected}"
                ></div>
                {{itemFilter.show}}
            </div>
        </div>
    </div>
</template>
<script>

import BaseButton from './BaseButton.vue';
import clickOutSide from "@mahdikhashan/vue3-click-outside";
import BaseInput from './BaseInput.vue';

export default {
    components: { BaseButton, BaseInput },
    directives: {
        clickOutSide,
    },
    props: {
        type: String, // loại tìm kiếm
        field: String, // Trường cần search Server
        addition: String, // ADD, OR nối chuỗi search
    },
    data() {
        return {
            showBoxFilter: false, // Hiển thị box chọn loại lọc tìm kiếm
            filterSelected: "", // loại filter được chọn
            filterSelectedShow: "", // loại filter hiển thị
            listFilterShow: [], // danh sách list filter show
            valueSearch: "", // giá trị cần tìm kiếm
            setTimeSearch: null, // Thời gian search 
            // list filter cho chuỗi
            filterString: [
                {show: "* : Chứa", value: "LIKE", showValue: "*"},
                {show: "= : Bằng", value: "=", showValue: "="},
                {show: "+ : Bắt đầu bằng", value: "STARTWIDTH", showValue: "+"},
                {show: "- : Kết thúc bằng", value: "ENDWIDTH", showValue: "-"},
                {show: "! : Không chứa", value: "NOTLIKE", showValue: "!"},
            ],
            // list filter cho số
            filterNumber: [
                {show: "> : Lớn hơn", value: ">", showValue: ">"},
                {show: "< : Nhỏ hơn", value: "<", showValue: "<"},
                {show: "= : Bằng", value: "=", showValue: "="},
                {show: ">= : Lớn hơn bằng", value: ">=", showValue: ">="},
                {show: "<= : Nhỏ hơn bằng", value: "<=", showValue: "<="},
            ],

        }
    },
    methods:{
        /**
         * set giá trị cho filter
         */
        setFilterSelected(filter){
            this.filterSelected = filter.value;
            this.filterSelectedShow = filter.showValue;
            this.$emit('changeFilter', {filterType: filter.value, filterVal: this.valueSearch, field: this.field, addition: this.addition})
        },

        /**
         * Show box chọn loại filter
         */
        setShowBoxFilter(){
            this.showBoxFilter = !this.showBoxFilter
        },

        /**
         * Ẩn box chọn loại filter
         */
        hideBoxFilter(){
            this.showBoxFilter = false
        },

        /**
         * Lấy dữ liệu từ input trong form
         */
         getData(data) {
            if (data.val) {
                clearTimeout(this.setSearchTime); // clear timeout
                // Đặt thời gian chờ khi người dùng nhập
                this.setSearchTime = setTimeout(async () => {
                    this.$emit('changeFilter', {filterType: this.filterSelected, filterVal: this.valueSearch, field: this.field, addition: this.addition})    
                }, 800);
            } else {
                clearTimeout(this.setSearchTime); // clear timeout
                this.$emit('changeFilter', {filterType: this.filterSelected, filterVal: this.valueSearch, field: this.field, addition: this.addition})
            }
        },

        /** 
         * Sự kiện khi click bàn phím
         */
        onKeyUp(valueKeyUp){
            switch (valueKeyUp.keyCode) {
                case 13: // Enter
                    this.$emit('changeFilter', {filterType: this.filterSelected, filterVal: this.valueSearch, field: this.field, addition: this.addition})
                    break;
            
                default:
                    break;
            }
        },

        /**
         * Sự kiện khi onchange
         */
        onChange(){
            this.$emit('changeFilter', {filterType: this.filterSelected, filterVal: this.valueSearch, field: this.field, addition: this.addition})
        }
    },
    created(){
        if(this.type == "string"){
            this.listFilterShow = this.filterString
        }
        else if(this.type == "number"){
            this.listFilterShow = this.filterNumber
        }
        this.filterSelected = this.listFilterShow[0].value
        this.filterSelectedShow = this.listFilterShow[0].showValue
    }
}
</script>
<style scoped>
    .filter_search{
        position: relative;
        display: flex;
        width: 100%;
    }

    .list_filter{
        position: absolute;
        background-color: #fff;
        width: 150px;
        top: calc(100% + 4px);
        border-radius: 4px;
        box-shadow: 0 0 6px rgb(66, 66, 66);
    }

    .filter_input{
        flex: 1;
    }
    
    .item_filter{
        padding: 5px 4px;
        display: flex;
        cursor: pointer;
    }

    .item_filter:hover{
        background-color: #def2ff;
    }

    .filter_selected{
        width: 15px;
        height: 15px;
        margin: 0px 6px;
    }

    .filter_selected_background{
        background-image: url('https://cukcukcomnew.misacdn.net/QLNH/resources/Image/table-circle-blue16.png');
    }
</style>