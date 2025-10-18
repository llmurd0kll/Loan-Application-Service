<template>
  <div id="app">
    <el-menu mode="horizontal" router>
      <el-menu-item index="/">Список заявок</el-menu-item>
      <el-menu-item index="/create">Создать заявку</el-menu-item>
    </el-menu>

    <router-view />
  </div>
</template>

<script setup>
// Здесь ничего не нужно, Vue Router сам подставит компоненты
</script>

<style>
#app {
  margin: 20px;
}
</style>
