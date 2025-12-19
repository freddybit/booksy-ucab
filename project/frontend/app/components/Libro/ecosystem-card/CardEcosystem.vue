<script setup lang="js">

// CSS Import
import '@assets/styles/components-style/card-ecosystem.css';
import { useLink } from 'vuetify/lib/composables/router.mjs';

const props = defineProps({
    link: { type: String, required: true },
    titleCard: { type: String, required: true },
    imgCard: { type: String, required: true },
    descriptionCard: { type: String, required: true },
    buttonCard: { type: String, required: true }
})

const srcImg = new URL(`../../../../assets/img/consultar-libros-img/${props.imgCard}`, import.meta.url).href;

</script>

<template>
  <router-link class="link-card" :to="link">
    <h2 class="title-card">{{ titleCard }}</h2>
    <img class="image-card" alt="Libro cuatro" :src="srcImg" />
    <p class="description-card">{{ descriptionCard }}</p>
    <button class="button-card">{{ buttonCard }}</button>
  </router-link>
</template>

<style scoped>
</style>