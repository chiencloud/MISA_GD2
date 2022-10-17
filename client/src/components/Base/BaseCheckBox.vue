<template>
    <label 
        class="checkbox-default checkbox-spaceleft"
        :class="{'padding-checkbox': valLabel}"
    >
        {{ valLabel || '' }}
        <input 
            type="checkbox" 
            v-model="modelCb" 
            :value="valCheckbox || ''"
            @change="getValue"
            :disabled="disable"
        />
        <span class="checkmark">
            <i v-show="modelCb" class="fa-solid fa-check"></i>
        </span>
    </label>
</template>

<script>
export default {
    data() {
        return {
            setClassSpace: false, // class set khoảng trắng giữa label và input
            modelCb: false, // model checkbox
        };
    },
    props: [
        'valLabel', // giá trị label show lên màn hình
        'valCheckbox', // giá trị của checkbox
        'fieldName', // Trường dữ liệu trả về cho parent
        'modelCheckbox', // v-model-checkbox
        'disable'
    ],
    methods: {

        /**
         * set checkbox
         * @param {boolean} setCb
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        setCheckbox1() {
            // this.modelCheckbox = true;
        },

        /**
         * Trả giá trị về cho parent 
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValue() {
            // 
            let checked = false
            if(this.modelCb)
                checked = true
            this.$emit('getValue', {val: this.valCheckbox,fieldName: this.fieldName, checked: checked});

            // set modelCheckbox
            // this.$emit('update:modelCheckbox', this.modelCb)
        },
    },
    created() {
        // thêm class tạo khoảng trống giữa label với input
        if (this.val) {
            this.setClassSpace = true;
        }

        // set checkbox
        this.modelCb = this.modelCheckbox
    },
    watch: {
        modelCheckbox(){
            this.modelCb = this.modelCheckbox
        }
    }
};
</script>

<style scoped>
@import url('@/style/components/checkbox-default.css');
</style>
