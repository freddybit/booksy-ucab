<script setup lang="js">

import Characteristics from "@/components/Libro/book-detail/Characteristics.vue";
import Description from "@/components/Libro/book-detail/Description.vue";
import CommentarySection from "@/components/Libro/book-detail/CommentarySection.vue";
import BookCardDetail from "@/components/Libro/book-detail/BookCardDetail.vue";
import {useRoute} from "vue-router";
import InnerImageZoom from 'vue-inner-image-zoom';
import { getBookList } from "@/services/Libro/bookService.js"
import {onMounted, ref} from "vue";
import {Book} from "@assets/js/Book.js";
import SellerBookCardDetail from "./SellerBookCardDetail.vue";
import EditBookView from "@/views/Libro/EditBookView.vue";

let books = ref([])
let book = ref(null)

let route = useRoute()
let bookId = route.params.id


onMounted(async () => {
  try {
    books.value = await getBookList();
    book.value = books.value.find(b => String(b._id) === String(bookId))
  } catch (error) {
    console.error("Error cargando libros:", error);
  }
})
</script>

<template>
  <article class="article-book" v-if="book">
    <section class="section-left">
      <section id="img-section"><inner-image-zoom class="zoom-img" :src="book._urlImg" alt="Portada del libro" :zoomSrc="book._urlImg" zoomType="click" moveType="pan" :zoomScale="1" /></section>
      <Characteristics :book = "book"></Characteristics>
      <Description :book="book"></Description>
      <CommentarySection></CommentarySection>
    </section>
    <section class="section-right">
      <SellerBookCardDetail :book = "book"></SellerBookCardDetail>
    </section>
  </article>

  <div v-else class="loading">
    <p>Cargando información del libro...</p>
  </div>
</template>

<style scoped>

.article-book {
  display: flex;
  height: 250vh;
  width: 65vw;

  margin: 8rem 0 8rem 0;
  padding: 2rem;

  justify-self: center;
  justify-content: space-between;

  background-color: rgba(255,255,255);
  border-radius: 1rem;
}

.section-left {
  margin: 0 0 0 0;
  display: flex;
  flex-direction: column;
  width: 46vw;
  height: 200vh;
  gap: 2rem;
}

.section-right {
  width: 18vw;
  padding: 2rem 0 0 0;
  background-color: rgb(5, 33, 52);
  border-radius: 1rem;
  justify-items: center;
}

#img-section {
  display: flex;
  height: 45vh;
  width: 46vw;
  background-color: rgb(240, 240, 240);
  border-radius: 1rem;

  align-items: center;
  justify-content: center;
  padding: 1rem;
  overflow: hidden;
}

:deep(.iiz) {
  width: 100% !important;
  height: 100% !important;
  display: flex !important;
  align-items: center;
  justify-content: center;
}

/* 2. ESTE ES EL PASO CLAVE: El trigger debe estar limitado */
:deep(.iiz__trigger) {
  display: flex !important;
  height: 100% !important; /* Obliga al disparador a no ser más alto que el contenedor gris */
  width: 100% !important;
  justify-content: center;
  align-items: center;
}

/* 3. La imagen debe ajustarse proporcionalmente */
:deep(.iiz__img) {
  max-height: 45vh !important; /* Un poco menos que el contenedor (45vh) para dejar margen */
  width: auto !important;
  object-fit: contain !important;
}

</style>