<template>
    <div class="popup-main" @keyup="keyboard">
        <!-- Popup Warning delete  -->
        <div class="popup"  v-if="isShowWarningDelete || false">
            <div class="popup-background"></div>
            <div class="popup-main-content">
                <div class="popup-main-top">
                    <div class="icon-background icon-warning"></div>
                    <div class="popup-content">{{ message }}</div>
                </div>
                <div class="popup-main-center"></div>
                <div class="popup-main-bottom">
                    <div>
                        <div ref="refFirstWD" @focus="focusLast('btn1WD')" tabindex="0"></div>
                        <BaseButton ref="btn1WD" val="Hủy" noneBg @click="closePopup" />
                    </div>
                    <div>
                        <BaseButton ref="btn2WD" val="Có" :autofocus="isFocusWarningDelete" @click="clickWarningDeleteYes" />
                        <div ref="refLastWD" @focus="focusFirst('btn1WD')" tabindex="0"></div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Popup Warning -->
        <div class="popup" v-if="isShowWarning || false">
            <div class="popup-background"></div>
            <div class="popup-main-content">
                <div class="popup-main-top">
                    <div class="icon-background icon-warning"></div>
                    <div class="popup-content">
                        {{ message }}
                    </div>
                </div>
                <div class="popup-main-center"></div>
                <div class="popup-main-bottom popup-right">
                    <BaseButton val="Đồng ý" :autofocus="isFocusWarning" @click="clickConfirmPopup" />
                </div>
            </div>
        </div>

        <!-- Popup Error -->
        <div class="popup" v-if="isShowError || false">
            <div class="popup-background"></div>
            <div class="popup-main-content">
                <div class="popup-main-top">
                    <div class="icon-background icon-error"></div>
                    <div class="popup-content">{{ message }}</div>
                </div>
                <div class="popup-main-center"></div>
                <div class="popup-main-bottom popup-center">
                    <BaseButton val="Đóng" :autofocus="isFocusError" @click="closePopup" />
                </div>
            </div>
        </div>

        <!-- Popup Question -->
        <div class="popup" v-if="isShowQuestion || false">
            <div class="popup-background"></div>
            <div class="popup-main-content">
                <div class="popup-main-top">
                    <div class="icon-background icon-question"></div>
                    <div class="popup-content">{{ message }}</div>
                </div>
                <div class="popup-main-center"></div>
                <div class="popup-main-bottom">
                    <div>
                        <div ref="refFirstQ" @focus="focusLast('btn3Q')" tabindex="0"></div>
                        <BaseButton ref="btn1Q" val="Hủy" noneBg @click="closePopup" />
                    </div>
                    <div>
                        <BaseButton ref="btn2Q" val="Không" noneBg @click="clickQuestionNo" />
                        <BaseButton ref="btn3Q" val="Có" :autofocus="isFocusQuestion" @click="clickQuestionYes" />
                        <div ref="refLastQ" @focus="focusFirst('btn1Q')" tabindex="0"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseButton from './BaseButton.vue';

export default {
    components: { BaseButton },
    data() {
        return {
            isShowWarningDelete: false, // popup cảnh báo xóa
            isShowWarning: false, // popup cảnh báo
            isShowError: false, // popup lỗi
            isShowQuestion: false, //popup hỏi
            isFocusWarningDelete: false, // focus popup cảnh báo
            isFocusWarning: false, // focus popup cảnh báo
            isFocusError: false, // focus popup cảnh báo
            isFocusQuestion: false, // focus popup cảnh báo
        };
    },
    props: {
        message: String, // Message thông báo
        type: String, // Type popup (warning-delete, warning, error, question)
    },
    methods: {
        /**
         * Đóng popup
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        closePopup() {
            this.$emit('closePopup'); 
            this.$emit('processWhenClose')
        },

        /**
         * Click yes warning delete
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        clickWarningDeleteYes() {
            this.$emit('clickWarningDeleteYes');
        },

        /**
         * Click đồng ý popup Warning
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        clickConfirmPopup() {
            this.$emit('confirmPopup', true);
        },

        /**
         * Click no popup Question
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        clickQuestionNo() {
            this.$emit('clickQuestionNo');
        },

        /**
         * Click yes popup Question
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        clickQuestionYes() {
            this.$emit('clickQuestionYes');
        },

        /**
         * Sự kiện bàn phím
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        keyboard(){
            switch (event.keyCode) {
                case 27:
                    this.closePopup();
                    break;
            
                default:
                    break;
            }
        },

        /**
         * Focus giá trị đầu tiên
         * @param {*} ref 
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        focusFirst(ref){
            this.$refs[ref].focusFunc();
        },

        /**
         * Focus giá trị cuối cùng
         * @param {*} ref 
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        focusLast(ref){
            this.$refs[ref].focusFunc();
        }
    },
                                                
    created() {
        if (this.type == process.env.VUE_APP_POPUP_WARNING_DEL) {
            // Popup warning delete
            this.isShowWarningDelete = true;
            this.isShowWarning = false;
            this.isShowError = false;
            this.isShowQuestion = false;
        } else if (this.type == process.env.VUE_APP_POPUP_WARNING) {
            // Popup warning
            this.isShowWarningDelete = false;
            this.isShowWarning = true;
            this.isShowError = false;
            this.isShowQuestion = false;
        } else if (this.type == process.env.VUE_APP_POPUP_ERROR) {
            // Popup error
            this.isShowWarningDelete = false;
            this.isShowWarning = false;
            this.isShowError = true;
            this.isShowQuestion = false;
        } else if (this.type == process.env.VUE_APP_POPUP_QUESTION) {
            //Popup question
            this.isShowWarningDelete = false;
            this.isShowWarning = false;
            this.isShowError = false;
            this.isShowQuestion = true;
        }
    },
    mounted() {
        if (this.type == process.env.VUE_APP_POPUP_WARNING_DEL) {
            // Popup warning delete
            this.isFocusWarningDelete = true;
        } else if (this.type == process.env.VUE_APP_POPUP_WARNING) {
            // Popup warning
            this.isFocusWarning = true;
        } else if (this.type == process.env.VUE_APP_POPUP_ERROR) {
            // Popup error
            this.isFocusError = true;
        } else if (this.type == process.env.VUE_APP_POPUP_QUESTION) {
            // Popup question
            this.isFocusQuestion = true;
        }
    }
};
</script>
<style scoped>
@import url('@/style/components/popup.css');
</style>
