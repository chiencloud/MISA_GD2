<template>
    <div class="toast-message">
        <div class="toast-message-left">
            <div class="toast-icon">
                <div class="icon-background" :class="iconShow"></div>
            </div>
            <div class="toast-message-content">
                <span class="toast-message-color" :class="toastColor">
                    {{ messageMain }}
                </span>
                {{ messageToast }}
            </div>
        </div>
        <div class="icon-background toast-message-close" @click="closeToast()"></div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            iconShow: '',
            messageMain: '',
            color: '',
        };
    },
    props: {
        icon: String, // icon toast message
        messageToast: String, // Nội dung toast message
    },
    methods: {
        /**
         * Đóng toast message
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        closeToast() {
            this.$emit('closeToast');
        },

        /**
         * Thời gian đóng toast message
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        setTimeRemove() {
            // Thời gian đóng toast message
            setTimeout(() => {
                this.$emit('closeToast');
            }, 5000);
        }
    },
    created() {
        // Chọn toast message

        switch (this.icon) {
            case 'success':
                this.iconShow = 'toast-icon-success';
                this.messageMain = 'Thành công!';
                this.toastColor = 'toast-color-green';
                break;
            case 'warning':
                this.iconShow = 'toast-icon-warning';
                this.messageMain = 'Cảnh báo!';
                this.toastColor = 'toast-color-yellow';
                break;
            case 'error':
                this.iconShow = 'toast-icon-error';
                this.messageMain = 'Lỗi!';
                this.toastColor = 'toast-color-red';
                break;
            default:
                break;
        }
    },
    mounted() {
        this.setTimeRemove()
    },
};
</script>
<style scoped>
.toast-message {
    max-width: 500px;
    background-color: #fff;
    border-radius: 4px;
    padding: 16px;
    margin:10px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    box-shadow: 0px 0px 8px rgb(167, 167, 167);
    color: #111;
    animation: fadeOut linear .8s 4s forwards;
}

.toast-message-left{
    display: flex;
    align-items: center;
}

.toast-icon{
    height: 100%;
}

.toast-message-content{
    margin-left: 8px;
    font-size: 14px;
}

.toast-icon-success{
    background-position: -1600px -256px ;
    height: 24px;
    width: 24px;
}

.toast-icon-warning{
    background-position: -592px -144px ;
    height: 24px;
    width: 24px;
}

.toast-icon-error{
    background-position: -536px -144px ;
    height: 24px;
    width: 24px;
}


.toast-message-color{
    font-weight: 600;
}

.toast-color-green{
    color: #2ca01c;
}

.toast-color-yellow{
    color: #ffc218;
}

.toast-color-red{
    color: #ff7777;
}

.toast-message-close{
    height: 16px;
    width: 16px;
    background-position: -1176px -360px;
    cursor: pointer;
    margin-left: 16px;
}

@keyframes fadeOut {
    from {
        opacity: 1;
    }
    to {
        opacity: 0;
    }
}

</style>
