<template>
    <div class="select-box" :style="`${style}` " @mouseover="mouseOver" @mouseout="mouseOut"  v-click-out-side="hideListData">
        <div class="select_container" v-click-out-side="hideBorder" ref="selectBoxShow">
            <div class="select-box-show" :class="{ 'select-box-error': isRequired, 'hidden_border':  setShowBorder}">
                <input
                    type="text"
                    class="select-box-main"
                    v-model="textInput"
                    ref="inputSelectBox"
                    @input="inputOnChange"
                    @focus="onFocusInput"
                    @keydown="selecItemUpDown"
                    @click="setShowBorder = false"
                />
                <button  v-if="!setShowBorder" tabindex="-1" @click="btnSelectDataOnClick" @keydown="selecItemUpDown">
                    <div class="icon_background_1 select-box-icon"></div>
                </button>
                <div v-if="addSelect && !setShowBorder" class="add_select_box" @click="clickAddSelect">
                    <i :class="addSelectIcon" :style="{color: addSelectColor}"></i>
                </div>
                <div v-show="showError" class="error-selecbox-alert">
                    <p class="error-selecbox-arrow"></p>
                    <div class="error-selecbox-content">
                        <p>
                            {{ messError }}
                        </p>
                    </div>
                </div>
                
            </div>
            
        </div>
        
        <!-- :style="styleValHide" -->
        <div v-show="isShowListData" ref="selectBoxHidden" class="select-box-hidden" style="top: 0; left: 0" >
            <button v-if="fieldListHide.length > 1" class="select-box-title">
                <div
                    v-for="(showItem, index) in fieldListHide"
                    :key="index"
                    class="selecbox-show-list-item"
                    :style="'width: ' + showItem.width + '%'"
                >
                    {{ showItem.title }}
                </div>
            </button>
            <button
                v-for="(item, index) in dataMain"
                :key="index"
                :ref="'toFocus_' + index"
                :class="{
                    'select-box-focus': index == indexItemFocus,
                    'select-box-choice': index == indexItemSelected,
                }"
                :value="item[fieldNameShow]"
                @click="itemOnSelect(item, index)"
                @focus="saveItemFocus(index)"
                @keydown="selecItemUpDown(index)"
                @keyup="selecItemUpDown(index)"
                tabindex="1"
            >
                <div
                    v-for="(showItem, index) in fieldListHide"
                    :key="index"
                    class="selecbox-show-list-item"
                    :style="'width: ' + showItem.width + '%'"
                >
                    {{ item[showItem.field] }}
                </div>
            </button>
        </div>
    </div>
</template>
<script>
const clickoutside = {
    // mounted(el, binding) {
    //     el.clickOutsideEvent = (event) => {
    //         // N???u element hi???n t???i kh??ng ph???i l?? element ??ang click v??o
    //         // Ho???c element ??ang click v??o kh??ng ph???i l?? button trong combobox hi???n t???i th?? ???n ??i.
    //         if (
    //             !(
    //                 (
    //                     el === event.target || // click ph???m vi c???a combobox__data
    //                     el.contains(event.target) || //click v??o element con c???a combobox__data
    //                     el.previousElementSibling.contains(event.target)
    //                 ) //click v??o element button tr?????c combobox data (nh???n v??o button th?? hi???n th???)
    //             )
    //         ) {
    //             // Th???c hi???n s??? ki???n t??y ch???nh:
    //             binding.value(event, el);
    //             // vnode.context[binding.expression](event); // vue 2
    //         }
    //     };
    //     document.body.addEventListener('click', el.clickOutsideEvent);
    // },
    // beforeUnmount: (el) => {
    //     document.body.removeEventListener('click', el.clickOutsideEvent);
    // },
};

const keyCode = {
    Enter: 13,
    ArrowUp: 38,
    ArrowDown: 40,
    ESC: 27,
};

import clickOutSide from "@mahdikhashan/vue3-click-outside";

export default {
    data() {
        return {
            dataMain: [], //D??? li???u
            textInput: null, // input v-model
            dataFilter: [], //D??? li???u g???c
            isShowListData: false, // Hi???n th??? list data hay kh??ng
            indexItemFocus: null, // Index c???a item focus hi???n t???i
            indexItemSelected: null, // Index c???a item ???????c selected
            isRequired: false, // Tr?????ng d??? li???u b???t bu???c ph???i ch???n
            showError: false, // Hi???n th??? box n???i dung l???i
            setShowBorder: false, // set show border
        };
    },
    directives: {
        clickOutSide,
    },
    props: {
        data: Array, //Truy???n data c??? ?????nh kh??ng get t??? API
        url: String, //Get data t??? url
        fieldListHide: Array, // Danh s??ch c??c tr?????ng c???n hi???n th???
        fieldNameShow: String, //Tr?????ng d??? li???u show combobox
        fieldNameValue: String, //Tr?????ng d??? li???u l???y value
        fieldName: String, // Tr?????ng d??? li???u c???a data khi g???i l??n server
        styleValHide: String, //style form hide
        style: String, // Css cho selectbox
        required: Boolean, // Tr?????ng d??? li???u b???t bu???c ph???i ch???n
        focusFirstData: Boolean, // Ch???n d??? li???u ?????u ti??n trong data show l??n combobox
        setData: [String, Number], // Set d??? li???u cho checkbox
        messError: String, // n???i dung l???i khi validate selectbox
        width: String, // set ????? r???ng c???a selectbox
        addSelect: Boolean, // c?? ch???c n??ng th??m select hay kh??ng
        addSelectIcon: String, // icon select add
        addSelectColor: String, // color select
        showBorder: Boolean, // show border
        id: [String, Number]
    },

    methods: {
        /**
         * L??u l???i index c???a item ???? focus
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        saveItemFocus(index) {
            this.indexItemFocus = index;
        },

        /**
         * ???n danh s??ch item
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        hideListData() {
            this.isShowListData = false;
        },

        /**
         * Nh???n v??o button th?? hi???n th??? ho???c ???n List Item
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        btnSelectDataOnClick() {
            this.dataFilter = this.data;
            this.isShowListData = !this.isShowListData;
        },

        /**
         * Click ch???n item trong danh s??ch
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        itemOnSelect(item, index) {
            if((item && index) || (item && index == 0)){
                const text = item[this.fieldNameShow];
                const value = item[this.fieldNameValue];
                this.textInput = text; // Hi???n th??? text l??n input.
                this.indexItemSelected = index;
                this.isShowListData = false;
                this.isRequired = false; // ???n class error n???u c?? tr?????ng b???t bu???c
                this.$emit('getValue', { val: value, fieldName: this.fieldName, id: this.id });
                this.$emit('update:setData', value);
            }
        },

        selecItemUpDown() {
            var me = this;
            var keyCodePress = event.keyCode;
            var elToFocus = null;
            var elToFocusPrev = null;
            var selecboxHide = this.$refs['selectBoxHidden']
            switch (keyCodePress) {
                case keyCode.ESC:
                    this.isShowListData = false;
                    break;
                case keyCode.ArrowDown:
                    this.isShowListData = true;
                    elToFocus = this.$refs[`toFocus_${me.indexItemFocus + 1}`];
                    elToFocusPrev = this.$refs[`toFocus_${me.indexItemFocus}`];
                    if (this.indexItemFocus == null || !elToFocus || elToFocus.length == 0) {
                        this.indexItemFocus = 0;
                        selecboxHide.scrollTop = 0
                    } else {
                        if(
                            selecboxHide.scrollTop <= elToFocusPrev[0].offsetTop + elToFocusPrev[0].offsetHeight - selecboxHide.offsetHeight &&
                            elToFocusPrev[0].offsetTop + elToFocusPrev[0].offsetHeight - selecboxHide.offsetHeight >= 0
                        ){
                            selecboxHide.scrollTop = elToFocus[0].offsetTop + elToFocus[0].offsetHeight - selecboxHide.offsetHeight
                        }
                        this.indexItemFocus += 1;
                    }

                    break;
                case keyCode.ArrowUp:
                    this.isShowListData = true;
                    elToFocus = this.$refs[`toFocus_${me.indexItemFocus - 1}`];
                    if (this.indexItemFocus == null || !elToFocus || elToFocus.length == 0) {
                        this.indexItemFocus = this.dataFilter.length - 1;
                        elToFocus = this.$refs[`toFocus_${this.indexItemFocus}`];
                        selecboxHide.scrollTop = elToFocus[0].offsetTop + elToFocus[0].offsetHeight
                    } else {
                        if(elToFocus[0].offsetTop <= selecboxHide.scrollTop){
                            selecboxHide.scrollTop = elToFocus[0].offsetTop - elToFocus[0].offsetHeight
                        }
                        // else{
                        //     selecboxHide.scrollTop = 0;
                        // }
                        this.indexItemFocus -= 1;
                        
                        
                    }
                    break;
                case keyCode.Enter:
                    elToFocus = this.$refs[`toFocus_${me.indexItemFocus}`];
                    if (elToFocus && elToFocus.length > 0) {
                        elToFocus[0].click();
                        this.isShowListData = false;
                    }
                    break;
                default:
                    break;
            }
        },

        /**
         * L???y gi?? tr??? c???a selectbox
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValueSelectBox() {
            var value = {
                show: null,
                value: null,
            };
            if (!this.dataFilter && this.url) {
                fetch(
                    this.url, 
                    { 
                        method: 'GET',
                        headers: {
                            Accept: 'application/json, text/plain',
                            'Content-Type': 'application/json;charset=UTF-8',
                        }
                    },
                )
                    .then((response) => response.json())
                    .then((res) => {
                        this.dataMain = res;
                        this.dataFilter = res;
                        if (this.dataFilter[this.indexItemSelected])
                            value = {
                                show: this.dataFilter[this.indexItemSelected][this.fieldNameShow],
                                value: this.dataFilter[this.indexItemSelected][this.fieldNameValue],
                            };
                    })
                    .catch((error) => {});
            } else if (this.dataFilter[this.indexItemSelected])
                value = {
                    show: this.dataFilter[this.indexItemSelected][this.fieldNameShow],
                    value: this.dataFilter[this.indexItemSelected][this.fieldNameValue],
                };

            return value;
        },

        /**
         * Nh???p text th?? th???c hi???n filter d??? li???u v?? hi???n th???
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        inputOnChange() {
            var me = this;
            // Th???c hi???n l???c c??c ph???n t??? ph?? h???p trong data:
            this.dataMain = this.dataFilter.filter((e) => {
                if (e[me.fieldNameShow].toLowerCase().search(me.textInput.toLowerCase()) >= 0) {
                    return true;
                }
            });
            if (this.textInput == '') {
                this.dataMain = this.dataFilter;
                this.indexItemSelected = null;
                this.$emit('getValue', { val: '', fieldName: this.fieldNameid, id: this.id });
                if (this.required && !this.indexItemSelected && !this.textInput && Object.keys(this.dataMain).length) {
                    this.isRequired = true;
                } else {
                    this.isRequired = false;
                }
            }
            this.isShowListData = true;
            // Ki???m tra d??? li???u b???t bu???c
        },

        /**
         * Hover selectbox
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        mouseOver() {
            if (this.isRequired) this.showError = true;
        },

        /**
         * mouseOut
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        mouseOut() {
            this.showError = false;
        },

        /**
         * focus
         */
        onFocusInput(){
            this.setShowBorder = false
        },

        /**
         * check d??? li???u validate
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        validate() {
            if (this.required && !this.textInput) {
                this.isRequired = true;
                this.showErorr = false;
                return {
                    status: false,
                    messError: this.messError,
                };
            } else {
                this.isRequired = false;
                return {
                    status: true,
                };
            }
        },

        /**
         * Set combobox v???i gi?? tr??? ???????c truy???n v??o
         * @param {*} dataSet 
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        setDataSelecBox(dataSet) {
            if (dataSet) {
                var indexChoiced;
                // L???y item ???????c ch???n
                var itemChoiced = this.dataMain.find((item, index) => {
                    indexChoiced = index; // Set v??? tr?? ch???a item ???????c ch???n
                    return item[this.fieldNameValue] == dataSet;
                });

                if (itemChoiced) {
                    const text = itemChoiced[this.fieldNameShow];
                    this.textInput = text; // Hi???n th??? text l??n input.
                    this.indexItemSelected = indexChoiced;
                    this.isShowListData = false;
                    this.isRequired = false;
                }
            } else {
                this.textInput = ''; // Hi???n th??? text l??n input.
                this.indexItemSelected = -1;
            }
        },

        /**
         * focus
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        focusFunc() {
            this.$refs.inputSelectBox.focus();
        },

        /**
         * G???i h??m hi???n th??? show box th??m select
         * CreatedBy: NDCHIEN (29/9/2022)
         */
        clickAddSelect(){
            this.$emit('showBoxAddSelect', true);
        },

        /**
         * ???n border khi click outside
         */
        hideBorder(){
            if(this.showBorder){
                this.setShowBorder = true
            }
        }
    },

    watch: {
        setData(newVal) {
            this.setDataSelecBox(newVal);
        },
        data(newVal){
            this.dataFilter = newVal;
            this.dataMain = newVal;
            this.setDataSelecBox(this.setData)
        },
        isShowListData(newVal){
            if(newVal){
                if(this.styleValHide == 'top'){
                    this.$nextTick(() => {
                        this.$refs.selectBoxHidden.style.left =  this.$refs.selectBoxShow.getBoundingClientRect().left + 'px'
                        this.$refs.selectBoxHidden.style.top =  this.$refs.selectBoxShow.getBoundingClientRect().top - this.$refs.selectBoxHidden.offsetHeight - 3 + 'px'
                    })
                }
                else{
                    this.$nextTick(() => {
                        this.$refs.selectBoxHidden.style.left =  this.$refs.selectBoxShow.getBoundingClientRect().left + 'px'
                        this.$refs.selectBoxHidden.style.top =  this.$refs.selectBoxShow.getBoundingClientRect().bottom + 3 + 'px'
                    })
                }
            }
        }
    },

    async created() {
        if(this.showBorder){
            this.setShowBorder = this.showBorder
        }
        this.dataMain = this.data;
        // L??y d??? li???u cho b???ng t??? url
        if (this.url) {
            await fetch(
                this.url, 
                { 
                    method: 'GET',
                    headers: {
                        Accept: 'application/json, text/plain',
                        'Content-Type': 'application/json;charset=UTF-8',
                    },
                }
            )
                .then((response) => response.json())
                .then((res) => {
                    this.dataMain = res;
                    this.dataFilter = res;
                })
                .catch((error) => {
                    console.log(error)
                });
        }

        //Focus gi?? tr??? ?????u ti??n c???a combobox
        if (this.focusFirstData && this.dataMain.length > 0) {
            const text = this.dataMain[0][this.fieldNameShow];
            const value = this.dataMain[0][this.fieldNameValue];
            this.textInput = text; // Hi???n th??? text l??n input.
            this.indexItemSelected = 0;
            this.isShowListData = false;
            this.isRequired = false;
        }

        // Set combobox v???i gi?? tr??? ???????c truy???n v??o
        this.setDataSelecBox(this.setData);
    },
    mounted() {
        this.$refs.selectBoxHidden.style.width = this.$refs.selectBoxShow.offsetWidth + 'px'
    },
};
</script>
<style scoped>
@import url('@/style/components/select-box.css');
</style>
