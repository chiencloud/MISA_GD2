<template>
    <button class="btn-default" ref="buttonDefault" :autofocus="autofocus && focusFunc()" :class="{ 'btn-noneBg': noneBg }">{{ val }}</button>
</template>

<script>

export default {
    props: {
        val: String,
        noneBg: Boolean,
        refBtn: String,
        autofocus: Boolean,
    },
    methods: {
        focusFunc(){
            this.$refs.buttonDefault.focus();
        }
    },
};
</script>

<style scoped>
@import url('@/style/components/btn-default.css');
</style>
