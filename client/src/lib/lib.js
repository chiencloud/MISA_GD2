import {ERROR_BE} from './resource'

/**
 * So sánh object
 * @param {Object} object1
 * @param {Object} object2
 * @returns
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
function shallowEqual(object1, object2) {
    const keys1 = JSON.stringify(object1);
    const keys2 = JSON.stringify(object2);
    if(keys1 == keys2){
        return true
    }
    return false;
}

/**
 * Ẩn khi click outside
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
const clickoutside = {
    mounted(el, binding) {
        el.clickOutsideEvent = (event) => {
            // Nếu element hiện tại không phải là element đang click vào
            // Hoặc element đang click vào không phải là button trong combobox hiện tại thì ẩn đi.
            if (
                !(
                    (
                        el === event.target || // click phạm vi của combobox__data
                        el.contains(event.target) || //click vào element con của combobox__data
                        el.previousElementSibling.contains(event.target)
                    ) //click vào element button trước combobox data (nhấn vào button thì hiển thị)
                )
            ) {
                // Thực hiện sự kiện tùy chỉnh:
                binding.value(event, el);
                // vnode.context[binding.expression](event); // vue 2
            }
        };
        document.body.addEventListener('click', el.clickOutsideEvent);
    },
    beforeUnmount: (el) => {
        document.body.removeEventListener('click', el.clickOutsideEvent);
    },
};

/**
 * Hàm format thời gian
 * Không có tham số truyền vào trả về chuoix rỗng
 * @param {*} dataDate ngày cần format
 * @param {*} setData 
 * @returns 
 * CreatedBy: NDCHIEN (18/8/2022)
 */
function formatDate(dataDate) {
    if (dataDate) {
        let date = new Date(dataDate);
        let year = date.getFullYear();
        let month = date.getMonth() + 1;
        let day = date.getDate();

        if (month < 10) {
            month = `0${month}`;
        }
        if (day < 10) {
            day = `0${day}`;
        }

        return `${day}/${month}/${year}`;
    }

    return '';
}

/**
 * hàm format giới tính
 * Không có tham số truyền vào sẽ trả về chuỗi rỗng
 * @param {*} data Giới tính enum cần format 
 * @returns 
 * CreatedBy: NDCHIEN (18/8/2022)
 */
function formatGender(data) {
    if (data == process.env.VUE_APP_GENDER_MALE) {
        return 'Nam';
    } else if (data == process.env.VUE_APP_GENDER_FEMALE) {
        return 'Nữ';
    } else if (data == process.env.VUE_APP_GENDER_ORTHER) {
        return 'Khác';
    }

    return '';
}

/**
 * fetch API
 * @param {string} url link call API
 * @param {string} method loại method call API 
 * @param {function} fnCallBack hàm xử lý sau khi call api
 * @param {*} data dữ liệu truyền lên server
 * @param {*} typeRes loại dữ liệu trả về
 * @returns 
 * CreatedBy: NDCHIEN (18/8/2022)
 */
function fetchAPI(url, method, fnCallBack, data, typeRes) {
    return fetch(url, {
        method: method,
        headers: {
            Accept: 'application/json, text/plain',
            'Content-Type': 'application/json;charset=UTF-8',
        },
        body: JSON.stringify(data),
    })
        .then((res) => {
            switch (typeRes) {
                case 'blob':
                    return res.blob();
                default:
                    break;
            }
            return res.json()
        })
        .then((res) => {
            return fnCallBack(res)
        })
}

/**
 * Tạo guid
 * @returns
 * CreatedBy: NDCHIEN (18/8/2022) 
 */
function generateUUID() { 
    var d = new Date().getTime();
    var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16;
        if(d > 0){
            r = (d + r)%16 | 0;
            d = Math.floor(d/16);
        } else {
            r = (d2 + r)%16 | 0;
            d2 = Math.floor(d2/16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

/**
 * Xóa tham chiếu
 * @param {*} value Dữ liệu cần xóa tham chiếu
 * @returns 
 */
function deleteReference(value){
    return JSON.parse(JSON.stringify(value))
}

/**
 * Lấy tên lỗi
 * @param {string} errorCode Mã lỗi
 */
function getErrorBE(errorCode){
    if(ERROR_BE[errorCode]){
        return ERROR_BE[errorCode]
    }
    return ERROR_BE.e000
}

export { shallowEqual, clickoutside, formatDate, formatGender, fetchAPI, generateUUID, deleteReference, getErrorBE };
