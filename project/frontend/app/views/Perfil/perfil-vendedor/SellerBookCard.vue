<script setup lang="js">
defineProps({
  book: { type: Object, required: true }
});
</script>

<template>
  <router-link :to="{ name: 'SellerBookDetail', params: { id: book._id } }" :key="book._id" class="book-card">
    <section id="img-section">
      <img :src="book._urlImg" alt="Portada del libro" />
    </section>
    <section class="card-main">
      <div class="div-title">
        <h2 class="title-card">{{ book._nameBook }}</h2>
        <h3 class="subtitle-card">{{ book._subtitle }}</h3>
      </div>
      <p class="author-card ">Autor: {{ book._author }}</p>
      <p class="year-card" v-if="book._publishYear">Fecha de publicación: {{ book._publishYear }}</p>
      <p class="author-card ">Vendedor: {{ book._seller._firstName }} {{ book._seller._lastName }}</p>
    </section>
    <section class="card-meta">
      <p class="cost-card" v-if="book._cost">${{ book._cost }}</p>
    </section>
  </router-link>
</template>

<style scoped>
  
.book-card {
  height: 20vh;
  display: flex;
  justify-content: space-between;
  text-decoration: none;
  color: black;
  background: rgba(255, 255, 255);
  padding: 1rem;
  margin: 2rem;
  border-radius: 1rem;
  box-shadow: 0 1rem 2rem rgba(0,0,0,0.6);
}

.card-main {
  width: 90vw;
  align-content: center;
  padding: 0 0 0 3rem;
}

#img-section {
  display: flex;
  height: 20vh;
  width: 40vw;
  border-radius: 1rem;
  background-color: rgb(240, 240, 240);
  align-items: center;
  justify-content: center;
}

img {
  min-width: 10%;
  max-width: 100%;
  height: 100%;

  border-radius: 1rem;
}

.div-title {
  height: 10vh;
  width: 35vw;
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 1rem;
}

.title-card {
  margin: 0;
  font-size: 3rem;
}

.subtitle-card {
  margin: 0;
  font-size: 2.5rem;
}

.author-card {
  color: rgba(0, 0, 0, 0.5);
  font-size: 1.5rem;
}

.card-meta {
  justify-items: right;
  align-content: center;
}

.cost-card {
  margin: 0 4rem;
  font-weight: bolder;
  color: rgba(0,0,0);
  font-size: 5rem;
}

.year-card {
  color: #888;
  font-size: 1.5rem;
}

.book-card:hover {
  animation: jump 1s ease;
  color: rgb(0,44,235);
  background-color: rgba(250,250,250, 0.95);
}

</style>