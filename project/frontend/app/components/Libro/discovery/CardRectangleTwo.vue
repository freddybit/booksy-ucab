<script setup lang="ts">
// CSS import
import '@assets/styles/components-style/card-rectangle.css';
</script>

<template>
  <router-link class="card-link" :to="{ name: 'CategoryCatalog', params: { category: 'Autoayuda' } }">
    <section class="card-section">
      <p class="card-text">¡Aquí en la UCAB!</p>
      <h3 class="card-title">REFUERZA TU MENTE DESDE DONDE ESTÉS</h3>
      <button class="card-button">Ver más</button>
    </section>
    <img class="card-img" alt="Imagen de un libro" src="@assets/img/consultar-libros-img/habitos-atomicos.jpg" />
  </router-link>
</template>

<style scoped>

</style>