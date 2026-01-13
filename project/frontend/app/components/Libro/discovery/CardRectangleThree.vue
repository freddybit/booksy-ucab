<script setup lang="ts">
// CSS import
import '@assets/styles/components-style/card-rectangle.css';
</script>

<template>
  <router-link class="card-link" :to="{ name: 'CategoryCatalog', params: { category: 'Motivacional' } }">
    <section class="card-section">
      <p class="card-text">¡Aquí en la UCAB!</p>
      <h3 class="card-title">LECTURAS QUE TE ACERCAN A LA META</h3>
      <button class="card-button">Ver más</button>
    </section>
    <img class="card-img" alt="Imagen de un libro" src="@assets/img/consultar-libros-img/libros-deportivos.jpg" />
  </router-link>
</template>

<style scoped>
</style>