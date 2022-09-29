<template lang="">
    <div class="field-input">
        <input
            class="input"
            :type="type || 'text'"
            :class="{ 'error-input': valRequired }"
            :placeholder="placeholder || ''"
            :style="styleVal"
            :value="type == 'date' ? val : value"
            ref="inputFocus"
            @input="onInput"
            @change="getValue"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        />
        <!-- v-model="val" -->
        <!-- <div class="clear-input"><i class="fa-solid fa-xmark"></i></div> -->
        <div v-show="showErorr" class="error-input-alert">
            <p class="error-input-arrow"></p>
            <div class="error-input-content">
                <p>
                    {{ messError }}
                </p>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            val: '', // v-model input
            valRequired: false, // không được bỏ trống dữ liệu
            showErorr: false, // Thông báo lỗi
        };
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
        maxLength: Number, // Độ dài tối đa của input
    },
    methods: {
        /**
         * Lấy giá trị input trả về cho parent
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValue() {
            this.$emit('getValue', { val: this.value, fieldName: this.fieldName });
        },

        /**
         * Check dữ liệu khi ng dùng nhập
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        validate() {
            if (this.required) {
                this.valRequired = true;
                this.showErorr = false;
                return {
                    status: false,
                    messError: this.messError,
                };
            } else {
                this.valRequired = false;
                return {
                    status: true,
                };
            }
        },

        /**
         * sự kiện oninput
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        onInput() {
            if (this.maxLength) {
                if (parseInt(this.maxLength) <= event.target.value.length) {
                    event.target.value = event.target.value.substr(0, this.maxLength);
                }
                this.val = event.target.value;
                this.$emit('update:value', event.target.value);
                this.showErorr = false; 
                this.valRequired = false;
            } else {
                this.val = event.target.value;
                this.$emit('update:value', event.target.value);
                this.showErorr = false;
                this.valRequired = false;
            }
        },

        /**
         * Hover input
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        mouseOver() {
            if (this.valRequired) this.showErorr = true;
        },

        /**
         * mouseOut
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        mouseOut() {
            this.showErorr = false;
        },

        /**
         * focus
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        focusFunc() {
            this.$refs.inputFocus.focus();
        },
    },
    watch: {
        value(newVal) {
            if (this.type == 'date') {
                if (newVal) this.val = new Date(newVal).toISOString().split('T')[0];
                else this.val = null;
            }
        },
    },
    created() {
        switch (this.type) {
            case 'date':
                if (this.value) {
                    this.val = new Date(this.value);
                    this.val = new Date(this.value).toISOString().split('T')[0];
                } else this.val = ``;

                break;
            default:
                this.val = this.value || '';
        }
    },
    mounted() {
        if (this.focus) {
            this.$refs.inputFocus.focus();
        }
    },
};
</script>
<style scoped>
@import url('@/style/components/input-default.css');
</style>
