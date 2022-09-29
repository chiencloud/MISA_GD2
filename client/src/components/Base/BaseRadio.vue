<template>
    <label 
        class="radio-default"
        v-for="(item, index) in data"
        :key="index"    
    >
        {{ item[fieldShow] }}
        <input 
            type="radio" 
            :name="name || 'inputRadio'" 
            :value="item[fieldValue]"
            v-model="modelRadioBox"
            @change="getValue"
            :checked="item[fieldValue] == radioSelected || index == 0"
        />
        <span class="checkmark-radio"></span>
    </label>
</template>
<script>
export default {
    data() {
        return {
            modelRadioBox: '', // v-model radiobox 
        }
    },
    emits: ["getValue"],
    props: {
        data: Array,  //Mảng data cho list radiobox
        fieldShow: String, //Trường dữ liệu object để hiển hiển thị thông tin radiobox 
        fieldValue: String, //Trường dữ liệu object để đặt giá trị cho radiobox
        fieldName: String, //Trường dữ liệu trả về cho parent
        name: String, // name của radiobox không truyền mặc định là 'inputRadio'
        radioSelected: Number || String, // Giá trị hiển thị của radiobox muốn selected khi được mount 
    },
    methods: {
        /**
         * Lấy giá trị radiobox
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValue() {
            this.$emit('getValue', {val: this.modelRadioBox,fieldName: this.fieldName});
        },
    }
};  
</script>
<style scoped>
@import url('@/style/components/radiobox-default.css');
</style>
