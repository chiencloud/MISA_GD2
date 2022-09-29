<!-- error-input-icon: border-red - thẻ input -->

<template lang="">
    <div class="field-input-icon" :style="styleVal">
        <div class="icon-background input-icon-background"></div>
        <input 
            class="input-icon" 
            type="text" 
            :placeholder="placeholder" 
            :class="{'error-input' : valRequired }"
            :style="styleVal"
            v-model="val"
            ref="inputFocus"
            @input="getValueOnInput"
        />
        <!-- <div class="error-input-icon-alert">
            <p class="error-input-icon-arrow"></p>
            <div class="error-input-icon-content">
                <p>{{messError}}</p>
            </div>
        </div> -->
    </div>
</template>
<script>
export default {
    data() {
        return {
            val: '', // v-model input
            valRequired: false, // không được bỏ trống dữ liệu
            showErorr: false // Thông báo lỗi
        }
    },
    props: {
        placeholder: String, // giá trị của placeholder input
        messError: String, // nội dung lỗi khi validate input
        type: String, // loại input
        value: String, // giá trị của input nhậ từ parent
        fieldName: String, // Trường dữ liệu trả về cho parent
        focus: Boolean, // focus vào input hay không
        styleVal: String, // css cho input
        required: Boolean, // bắt buộc phải nhập dữ liệu
    },
    methods: {
        /**
         * Lấy giá trị input trả về cho parent
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValueOnInput() {
            this.$emit('getValue', { val: this.val, fieldName: this.fieldName });
        },

        /**
         * getValue
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValue() {
            return this.val;
        }
    }
};
</script>
<style scoped>
@import url('@/style/components/input-icon.css');
</style>
