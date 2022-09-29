<template>
  <div class="main">
    <TheHeader/>
    <div class="page_main">
      <TheNavbar/>
      <TheMaterial/>
    </div>
  </div>
</template>

<script>
import TheHeader from './layouts/TheHeader.vue';
import TheNavbar from './layouts/TheNavbar.vue';
import TheMaterial from './pages/TheMaterial.vue';
import { stringApiEmployee, stringApiDepartment} from '@/lib/stringAPI'

// Khởi tạo danh sách chuỗi API Employee
stringApiEmployee();

// Khởi tạo danh sách chuỗi API Department
stringApiDepartment();

export default {
  name: 'App',
  components: {
    TheHeader,
    TheNavbar,
    TheMaterial
}
}
</script>

<style>

  @font-face {
    font-family: 'Tahoma1';
    src: url('@/assets/font/tahoma.ttf');
  }

  *{
    margin: 0;
    padding: 0;
    font-family: Tahoma1;
    font-size: 13px;
    box-sizing: border-box;
  }

  .main{
    width: 100vw;
    height: 100vh;
    overflow: hidden;
  }

  .page_main{
    height: calc(100% - 60px);
    display: flex;
  }

  .icon_background_1{
    background-image: url('@/assets/resource/Sprites.64af8f61.svg');
  }

  .content{
  }

</style>
