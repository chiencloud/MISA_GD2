<template>
    <table class="table-employee">
        <thead>
            <tr>
                <th
                    v-for="(item, index) in inforTable.field"
                    :key="index"
                    class="table-th"
                    :class="{
                        'table-text-left': item.textAlign == 'left',
                        'table-text-center': item.textAlign == 'center',
                        'table-text-right': item.textAlign == 'right',
                    }"
                    :style="`padding: 5px ;min-width: ${item.width}px; width: ${item.width}px`"
                >
                    {{ item.fieldName }}
                    <slot :name="item.nameComponentTHead"/>
                </th>
                
            </tr>
        </thead>
        <tbody>
            <tr
                v-for="(itemRow, indexRow) in inforTable.data"
                :key="indexRow"
                :class="{
                    'row-selected':
                        rowSelected == itemRow[dataTable.fieldId] ||
                        checkboxValue.indexOf(itemRow[dataTable.fieldId]) >= 0,
                }"
                @dblclick="dbClickTr(itemRow, itemRow[dataTable.fieldId])"
                @click="trClick(itemRow, itemRow[dataTable.fieldId])"
            >
                <td
                    v-for="(itemCol, indexCol) in inforTable.field"
                    :key="indexCol"
                    class="table-td"
                    :class="{
                        'table-text-left': itemCol.textAlign == 'left',
                        'table-text-center': itemCol.textAlign == 'center',
                        'table-text-right': itemCol.textAlign == 'right',
                    }"
                >

                    <span 
                        v-if="itemCol.type != 'component'" 
                        :style="`${itemCol.style}; width: ${itemCol.width - 20}px;`" 
                        :title="formatData(itemRow, itemCol)"
                        
                    >{{ formatData(itemRow, itemCol) }}</span>
                    <slot :name="itemCol.nameComponent" :data="itemRow" :index="indexRow" />
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script>
import BaseCheckBox from '@/components/Base/BaseCheckBox.vue';
import { clickoutside, formatDate, formatGender } from '@/lib/lib.js';
import BaseInput from './BaseInput.vue';
import BaseButton from './BaseButton.vue';

export default {
    components: {
    BaseCheckBox,
    BaseInput,
    BaseButton
},
    directives: {
        clickoutside, // ???n khi click outside
    },
    props: {
        dataTable: Object, // Th??ng tin v??? table ( field: c??c tr?????ng c???n l???y, url: link l???y data)
        isReloadTable: Boolean, // reload table
        inforTable: Object, // Th??ng tin v??? table ( field, data) sau jhi ???? ???????c format data
        listCheckBox: Array, // Danh s??ch c??c h??ng ???????c tick
        showSearchBox: Boolean, // box search
        hideColFixed: Boolean, // hi???n th??? column fixed
    },
    data() {
        return {
            isShow: -1, // show box function khi c?? gi?? tr??? b???ng index c???a h??ng
            // checkboxSelected: [], // Nh???ng h??ng ???????c t??ch ch???n checkbox
            checkboxValue: [], // Gi?? tr??? nh???ng h??ng ???????c t??ch ch???n checkbox
            rowSelected: -1, // v??? tr?? h??ng ???????c click ch???n
        };
    },
    created(){
    },
    methods: {
        /**
         * @param {} indexRow
         */
        showFunc(indexRow) {
            if (indexRow == this.isShow) this.isShow = -1;
            else this.isShow = indexRow;
        },

        /**
         * Click tr
         * @param {string} index id c???a h??ng
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        trClick(idRow) {
            this.rowSelected = idRow[this.dataTable.fieldId];
            this.$emit('clickTrTable', idRow);
        },

        /**
         * double click tr
         * @param {object} idRow data c???a h??ng
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        dbClickTr(data, id) {
            var dataRow = data;
            this.$emit('dbClickTrTable', dataRow, id);
        },

        /**
         * ???n box ch???c n??ng khi click outside
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        hideFunc() {
            this.isShow = -1;
        },

        /**
         * Click button s???a
         * @param {object} data g???i h??m c???a cha khi click n??t update
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        updateData(data, id) {
            this.$emit('clickUpdateButton', data, id);
        },

        /**
         * Nh??n b???n data
         * @param {object} data g???i h??m cha khi click nh??n b???n
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        copyData(data) {
            this.$emit('clickFuncCopy', data);
        },

        /**
         * X??a data
         * @param {Object} data // Th??ng tin nh??n vi??n ???????c chon
         * @param {Number} index // V?? tr??? nh??n vi??n c???n x??a
         * @param {Array} tableData // M???ng danh s??ch c??c nh??n vi??n
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        deleteData(data, index, tableData) {
            this.$emit('clickFuncDelete', {
                data: data,
                indexData: index,
                tableData,
            });
        },

        /**
         * Format d??? li???u t??? server tr??? v???
         * @param {Array} data
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        formatData(dataNeedFormat, fieldFormat) {
            if (fieldFormat.format == process.env.VUE_APP_FORMAT_DATE) {
                return formatDate(dataNeedFormat[fieldFormat.field]);
            }
            if (fieldFormat.format == process.env.VUE_APP_FORMAT_GENDER) {
                return formatGender(dataNeedFormat[fieldFormat.field]);
            }
            return dataNeedFormat[fieldFormat.field];
        },

        /**
         * l???y value c???a check box
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValueCheckBox(dataCheckBox) {
            if (dataCheckBox) {
                if (dataCheckBox.checked && dataCheckBox.val) {
                    // t??m xem gi?? tr??? c???n th??m c?? trong danh s??ch ch??a n???u c?? r???i th?? th??i k th?? add th??m
                    let searchValue = this.checkboxValue.find((item) => dataCheckBox.val == item);
                    if (!searchValue) this.checkboxValue.push(dataCheckBox.val);
                }

                if (!dataCheckBox.checked && dataCheckBox.val) {
                    // x??a ph???n t??? khi uncheckbox
                    this.checkboxValue.splice(this.checkboxValue.indexOf(dataCheckBox.val), 1);
                }
                this.$emit('update:listCheckBox', this.checkboxValue);
            }
        },

        /**
         * Set tick checkbox
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        setTickCheckbox(valueSet) {
            let findValue = this.checkboxValue.find((value) => valueSet == value);
            if (findValue) return true;
            return false;
        },

        /**
         * set tick ho???c untick t???t c??? checkbox
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getAllCheckBox(dataCheckBox) {
            if (dataCheckBox) {
                if (dataCheckBox.checked && this.inforTable.data) {
                    for (let i = 0; i < this.inforTable.data.length; i++) {
                        const employeeId = this.inforTable.data[i][this.dataTable.fieldId];
                        let searchValue = this.checkboxValue.find((item) => employeeId == item);
                        if (!searchValue) this.checkboxValue.push(employeeId);
                    }
                }
                if (!dataCheckBox.checked && this.inforTable.data) {
                    for (let i = 0; i < this.inforTable.data.length; i++) {
                        const employeeId = this.inforTable.data[i][this.dataTable.fieldId];
                        this.checkboxValue.splice(this.checkboxValue.indexOf(employeeId), 1);
                    }
                }
                this.$emit('update:listCheckBox', this.checkboxValue);
            }
        },

        /**
         * set tick all checkbox
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        setTickAllCheckbox() {
            if (this.inforTable.data) {
                for (let i = 0; i < this.inforTable.data.length; i++) {
                    // L???y id nh??n vi??n
                    const employeeId = this.inforTable.data[i][this.dataTable.fieldId];

                    let searchValue = this.checkboxValue.find((item) => employeeId == item);
                    if (!searchValue) return false;
                }
                if(this.inforTable.data.length == 0) return false;
                return true;
            }
            return false;
        },
    },
    watch: {
        listCheckBox(newVal) {
            this.checkboxValue = newVal;
        },
    },
};
</script>

<style>
@import url('@/style/components/table-default.css');
</style>
