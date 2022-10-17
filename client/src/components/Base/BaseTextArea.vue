<template>
    <textarea 
        style="" 
        :value="value"
        @input="getValueOnInput" 
    ></textarea>
</template>
<script>
export default {
    data() {
        return {
            val: '' 
        }
    },
    props: {
        style: [String], // css cho text area
        value: [String, Number], // giá trị của input nhậ từ parent
    },
    methods: {
        /**
         * Lấy giá trị input trả về cho parent
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValueOnInput() {
            this.val = event.target.value;
            this.$emit('update:value', this.val)
            this.$emit('onInput', { val: this.val, fieldName: this.fieldName });
        },
    },
}
</script>
<style>
    
</style>