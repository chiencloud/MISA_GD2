<template lang="">
    <button 
        class="btn-icon"
        :class="{'btn_border_none': noneBorder, 'btn_bg_none': noneBg}"
        :style="styleCss"
    >
        <!-- <img src="./resource/icon/add.png" alt="" /> -->
        <i :class="icon" :style="{color: colorIcon}"></i>
        <span>{{ val }}</span>
    </button>
</template>

<script>
export default {
    props: {
        val: String,
        colorIcon: String,
        icon: String,
        styleCss: String,   
        noneBorder: Boolean,
        noneBg: Boolean,
    },
};
</script>

<style scoped>
@import url('@/style/components/btn-icon.css');
</style>
