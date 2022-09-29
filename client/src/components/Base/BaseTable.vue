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
                    <div v-if="showSearchBox" class="search_filter">
                        <BaseButton val="*" />
                        <BaseInput />
                    </div>
                </th>
                <th v-if="!hideColFixed" class="table-th table-text-center" style="min-width: 150px;  top: -70px; z-index: 1; ">
                    Ngừng theo dõi
                    <div class="search_filter">
                        <select class="not_folow" name="" id="">
                            <option value="">Không</option>
                            <option value="">Có</option>
                        </select>
                    </div>
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
                @click="trClick(itemRow[dataTable.fieldId])"
            >
                <td
                    v-for="(itemCol, indexCol) in inforTable.field"
                    :key="indexCol"
                    class="table-td"
                >

                    <span v-if="itemCol.type != 'component'">{{ formatData(itemRow, itemCol) }}</span>
                    <slot :name="itemCol.nameComponent" />
                </td>
                <td
                    class="table-td table-text-center"
                    :class="{ showFunction: isShow == indexRow }"
                    v-if="!hideColFixed"
                >
                    <div class="function-employee-account" style="text-align: center;">
                        <BaseCheckBox />
                    </div>
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
        clickoutside, // ẩn khi click outside
    },
    props: {
        dataTable: Object, // Thông tin về table ( field: các trường cần lấy, url: link lấy data)
        isReloadTable: Boolean, // reload table
        inforTable: Object, // Thông tin về table ( field, data) sau jhi đã được format data
        listCheckBox: Array, // Danh sách các hàng được tick
        showSearchBox: Boolean, // box search
        hideColFixed: Boolean, // hiển thị column fixed
    },
    data() {
        return {
            isShow: -1, // show box function khi có giá trị bằng index của hàng
            // checkboxSelected: [], // Những hàng được tích chọn checkbox
            checkboxValue: [], // Giá trị những hàng được tích chọn checkbox
            rowSelected: -1, // vị trí hàng được click chọn
        };
    },
    created(){
        console.log(this.inforTable)
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
         * @param {string} index id của hàng
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        trClick(idRow) {
            this.rowSelected = idRow;
        },

        /**
         * double click tr
         * @param {object} idRow data của hàng
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        dbClickTr(data) {
            var dataRow = data;
            this.$emit('dbClickTrTable', dataRow);
        },

        /**
         * Ẩn box chức năng khi click outside
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        hideFunc() {
            this.isShow = -1;
        },

        /**
         * Click button sửa
         * @param {object} data gọi hàm của cha khi click nút update
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        updateData(data, id) {
            this.$emit('clickUpdateButton', data, id);
        },

        /**
         * Nhân bản data
         * @param {object} data gọi hàm cha khi click nhân bản
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        copyData(data) {
            this.$emit('clickFuncCopy', data);
        },

        /**
         * Xóa data
         * @param {Object} data // Thông tin nhân viên được chon
         * @param {Number} index // Ví trị nhân viên cần xóa
         * @param {Array} tableData // Mảng danh sách các nhân viên
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
         * Format dữ liệu từ server trả về
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
         * lấy value của check box
         * CreatedBy: NDCHIEN (18/8/2022)
         */
        getValueCheckBox(dataCheckBox) {
            if (dataCheckBox) {
                if (dataCheckBox.checked && dataCheckBox.val) {
                    // tìm xem giá trị cần thêm có trong danh sách chưa nếu có rồi thì thôi k thì add thêm
                    let searchValue = this.checkboxValue.find((item) => dataCheckBox.val == item);
                    if (!searchValue) this.checkboxValue.push(dataCheckBox.val);
                }

                if (!dataCheckBox.checked && dataCheckBox.val) {
                    // xóa phần tử khi uncheckbox
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
         * set tick hoặc untick tất cả checkbox
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
                    // Lấy id nhân viên
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
