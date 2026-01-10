<script setup lang="js">
import { consultarVendedor } from '@/services/Perfil/sellerService';
import { useRoute } from 'vue-router';
import { onMounted, ref } from 'vue';
import SellerBookCard from '../perfil-vendedor/SellerBookCard.vue';
import ZeroResults from '@/components/Libro/catalog-card/ZeroResults.vue';
import CategoryListVue from '@/components/Libro/catalog-card/CategoryList.vue';

const props = defineProps({
    email: {type: String, required: true}
})

const seller = ref(null);
const books = ref([]);
const error = ref(true);
const route = useRoute();

onMounted(async () => {
  try {
    seller.value = await consultarVendedor(props.email);
    books.value = seller.value._catalog;
  } catch (err){
    console.error(err?.message ?? err);
  } finally {
    error.value = false;
  }
});


</script>

<template>
  <article class="catalog-article">
    <section class="section-left">
      <h1 class="catalog-title">CATÁLOGO</h1>
      <h2 class="catalog-title">Del vendedor</h2>
      <p>{{ books.length }} resultados</p>
    </section> 
    <section id="catalog" class="section-right">
      <p v-if="error">Cargando resultados...</p>
      <section v-else>
        <ZeroResults v-if="query && books.length === 0"></ZeroResults>
        <SellerBookCard v-for="b in books" :key="b._id" :book="b" />
        <p v-if="!query && books.length === 0">No hay libros</p>
      </section>
    </section>
  </article>
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