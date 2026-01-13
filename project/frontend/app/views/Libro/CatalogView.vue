<script setup lang="js">
import { onMounted, ref, computed } from 'vue';
import { useRoute } from 'vue-router';
import { getBookList } from '@/services/Libro/bookService.js';
import BookCard from '@/components/Libro/catalog-card/BookCard.vue';
import ZeroResults from '@/components/Libro/catalog-card/ZeroResults.vue';

const books = ref([]);
const error = ref(true);
const route = useRoute();

onMounted(async () => {
  try {
    books.value = await getBookList();
  } catch (err){
    console.error(err?.message ?? err);
  } finally {
    error.value = false;
  }
});

const query = computed(() => (route.query.q || '').toString().toLowerCase());

const filteredBooks = computed(() => {
  if (!query.value) return books.value || [];
  return (books.value || []).filter(b => {
    const title = (b._nameBook || '').toString().toLowerCase();
    const author = (b._author || '').toString().toLowerCase();
    return title.includes(query.value) || author.includes(query.value);
  });
});
</script>

<template>
  <article class="catalog-article">
    <section class="section-left">
      <h1 class="catalog-title">CATÁLOGO</h1>
      <p>{{ filteredBooks.length }} resultados</p>
    </section>
    <section id="catalog" class="section-right">
      <p v-if="error">Cargando resultados...</p>
      <section v-else>
        <ZeroResults v-if="filteredBooks.length === 0"></ZeroResults>
        <BookCard v-for="b in filteredBooks" :key="b._id" :book="b" />
        <p v-if="!query && books.length === 0">No hay libros</p>
      </section>
    </section>
  </article>
    <router-link :to="{ name: 'compras' }">
      <button>Consultar Pagos</button>
    </router-link>
</template>

<style scoped>

.catalog-article {
  display: flex;
  justify-content: space-between;
  height: 88vh;
}

.section-left p {
  font-size: 1.5rem;
}

.section-right {
  padding: 2rem;
  overflow-x: auto;
}

@media (max-width: 1599px){

  .section-left {
    display: flex;
    flex-direction: column;
    padding: 4rem 1rem 0 2rem;
    width: 19vw;
    background-color: rgba(245,245,245);
  }

  .catalog-title {
    font-size: 3.5rem;
    color: rgba(0,0,0)
  }
}

@media (min-width: 1600px){

  .section-left {
    display: flex;
    flex-direction: column;
    padding: 4rem 1rem 0 2rem;
    width: 21vw;
    background-color: rgba(245,245,245);
  }

  .catalog-title {
    font-size: 4.5rem;
    color: rgba(0,0,0)
  }
}

.catalog-title:hover {
  animation: jump 1s ease;
}

</style>