<script setup lang="js">

  import { useLink } from 'vuetify/lib/composables/router.mjs';

const props = defineProps({
  link: { type: String, required: true },
  imgCard: { type: String, required: true },
  title: { type: String, required: true },
})

const srcImg = new URL(`../../../../assets/img/consultar-libros-img/${props.imgCard}`, import.meta.url).href;

</script>

<template>
  <router-link class="card-link" :to="link">
    <img class="card-img"  alt="Libros" :src="srcImg" />
    <section class="card-section">
      <h3 class="card-title">{{ title }}</h3>
    </section>
  </router-link>
</template>

<style scoped>

@media (min-width: 1600px) {
  .card-link {
    display: flex;
    height: 12vh;
    width: 15vw;

    border: 0.07rem solid rgba(0, 0, 0, 0.2);
    border-radius: 1rem;
    text-decoration: none;
    color: black;
  }

  .card-img {
    height: 12vh;
    width: 6vw;
    border-radius: 1rem 0 0 1rem;
  }

  .card-section {
    align-content: center;
    padding: 0 0 0 2rem;
    background-color: rgba(250,250,250, 0.5);
  }

  .card-title {
    font-size: 1.6rem;
  }

  .card-link:hover {
    animation: jump 1s ease;
    background-color: rgba(240,240,240, 0.5);
    color: rgb(0,44,235);
  }
}

@media (min-width: 1200px) and (max-width: 1599px) {
  .card-link {
    display: flex;
    height: 12vh;
    width: 18vw;

    border: 0.07rem solid rgba(0, 0, 0, 0.2);
    border-radius: 1rem;
    text-decoration: none;
    color: black;
  }

  .card-img {
    height: 12vh;
    width: 6vw;
    border-radius: 1rem 0 0 1rem;
  }

  .card-section {
    align-content: center;
    padding: 0 0 0 2rem;
    background-color: rgba(250,250,250, 0.5);
  }

  .card-title {
    font-size: 1.6rem;
  }

  .card-link:hover {
    animation: jump 1s ease;
    background-color: rgba(240,240,240, 0.5);
    color: rgb(0,44,235);
  }
}
</style>