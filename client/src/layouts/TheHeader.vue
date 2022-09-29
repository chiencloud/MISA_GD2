<template>
    <header>
        <div class="header_left">
            <div class="icon_background_1 bars"></div>
            <div class="logo"></div>
        </div>
        <div class="header_right">
            <div class="header_right_1">
                <select name="" id="">
                    <option value="">Chuỗi nhà hàng</option>
                </select>
            </div>
            <div class="header_right_2">
                <div class="r58">R58.0</div>
                <div class=""> 
                    <select name="" id="">
                        <option value="">Tiếng Việt (Tiếng Việt)</option>
                    </select>
                </div>
                <div class="">
                    <div class="icon_background"></div>
                </div>
                <div class="head_noti">
                    <i class="fa-solid fa-earth-asia"></i>
                </div>
                <div class="head_phone">
                    <i class="fa-solid fa-phone"></i>
                </div>
                <div class="user_info">
                    <div class=" icon_background_1 user_icon"></div>
                    <div> <p>Nguyễn Thị Dung</p> </div>
                    <div class="icon_background_1 arrow_down"></div>
                </div>
            </div>
        </div>
    </header>
</template>
<script>
export default {
    
}
</script>
<style scoped>
    header{
        display: flex;
        background-color: #0072bc;
        height: 60px;
        padding-right: 15px;
        color: #fff;
    }

    .header_left, .header_right{
        display: flex;
        align-items: center;
    }

    .header_left{
        padding-left: 15px;
        width: 250px;
    }

    .header_right{
        flex: 1;
        justify-content: space-between;
    }

    .header_right_2{
        display: flex;
    }

    .bars{
        height: 24px;
        width: 24px;
        margin-right: 15px;
        background-position: -312px -32px;
    }

    .logo{
        background-image: url('@/assets/resource/logoCukCuk207x45.png');
        height: 34px;
        width: 155px;
    }

    select{
        height: 30px;
        width: 200px;
        padding: 0 5px;
        margin: 0 5px;
        border: none;
        color: white;
        background-color: #004f82;
    }

    .r58{
        font-size: 15px;
        margin: auto 15px;
    }

    .head_noti, .head_phone{
        margin: auto;
        padding: 0 12px;
        height: 100%;
    }
    
    .head_noti i, .head_phone i {
        font-size: 18px !important;
    }

    .user_info{
        display: flex;
        align-items: center;
    }
    
    .user_info p{
        font-size: 15px !important;
    }

    .user_icon{
        height: 30px;
        width: 30px;
        background-position: -352px -894px;
        margin: 0 10px;
        border-radius: 100px;
    }

    .arrow_down{
        margin: 0 5px;
        height: 16px;
        width: 16px;
        background-position: -848px -360px;
    }
</style>