<template>
    <div class="full_screen">
        <div class="div_center">
            <div class="form_notice" :style="{'width': width}">
                <div class="form_title">
                    <div class="title_left"><slot name="title"/></div>
                    <div class="title_right">
                        <div><i class="fa-solid fa-xmark"></i></div>
                    </div>
                </div>
                <div class="content">
                    <slot name="content" />
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: {
        width: String // style css 
    }
}
</script>
<style scoped>
    .full_screen {
        position: fixed;
        height: 100%;
        width: 100%;
        background-color: rgba(0, 0, 0, 0.4);
        z-index: 150;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .form_notice{
        background-color: #fff;
        border: 2px solid #0072bc;
        border-radius: 5px;
    }

    .form_title{
        background-color: #0072bc;
        display: flex;
        justify-content: space-between;
        color: #fff;
        padding: 4px 10px;
    }

    .form_title i{
        margin: 0;
        cursor: pointer;
    }
</style>