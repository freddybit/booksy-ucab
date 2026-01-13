<script setup lang="ts">
// CSS import
import '@assets/styles/components-style/card-rectangle.css';
</script>

<template>
  <router-link class="card-link" :to="{ name: 'CategoryCatalog', params: { category: 'Economia' } }">
    <section class="card-section">
      <p class="card-text">¡Aquí en la UCAB!</p>
      <h3 class="card-title">CONVIERTE TUS IDEAS EN CAPITAL</h3>
      <button class="card-button">Ver más</button>
    </section>
    <img class="card-img" alt="Imagen de un libro" src="@assets/img/consultar-libros-img/libros-financieros.jpeg" />
  </router-link>
</template>

<style scoped>
</style>