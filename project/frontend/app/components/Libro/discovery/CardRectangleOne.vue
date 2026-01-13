<script setup lang="ts">
// CSS import
import '@assets/styles/components-style/card-rectangle.css';
</script>

<template>
  <router-link class="card-link" :to="{ name: 'CategoryCatalog', params: { category: 'Academico' } }">
    <section class="card-section">
      <p class="card-text">¡Aquí en la UCAB!</p>
      <h3 class="card-title">TODO PARA ESTAR AL DÍA EN TUS ESTUDIOS</h3>
      <button class="card-button">Ver más</button>
    </section>
    <img class="card-img" alt="Imagen de un libro" src="@assets/img/consultar-libros-img/libros-academicos.jpg" />
  </router-link>
</template>

<style scoped>
</style>