<script setup lang="js">
defineProps({
  book: { type: Object, required: true }
})

</script>

<template>
  <article class="description-detail">
    <h2 class="description-title">Descripción</h2>
    <p class="description-text">
      {{ book._description }}
    </p>
  </article>
</template>

<style scoped>

@media (min-width: 1600px){
  .description-detail {
    display: flex;
    margin: 2rem 0 0 0;
    flex-direction: column;
    width: 60vw;
    height: 20vh;
    gap: 2rem;
  }
}

@media (min-width: 1200px) and (max-width: 1599px) {
  .description-detail {
    display: flex;
    margin: 10rem 0 0 0;
    flex-direction: column;
    width: 60vw;
    height: 20vw;
    gap: 2rem;
  }
}

.description-title{
  height: 5vh;
  width: 30vw;
  font-size: 3rem;
  font-weight: lighter;
}

.description-text {
  width: 35vw;

  font-size: 1.5rem;
  font-weight: lighter;
  color: rgb(100,100,100);
}

</style>