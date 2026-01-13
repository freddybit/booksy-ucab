<script setup lang="js">
  import NavBar from "@/components/common/NavBar.vue";
  import '@assets/styles/animations.css';
  import FooterBooksy from "./components/common/FooterBooksy.vue";

</script>

<template>
  <header>
    <NavBar />
  </header>
  <main id="welcome-page">
    <router-view />
  </main>
</template>

<style scoped>
</style>
