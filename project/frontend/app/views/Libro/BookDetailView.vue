<script setup lang="js">

import Characteristics from "@/components/Libro/book-detail/Characteristics.vue";
import Description from "@/components/Libro/book-detail/Description.vue";
import CommentarySection from "@/components/Libro/book-detail/CommentarySection.vue";
import BookCardDetail from "@/components/Libro/book-detail/BookCardDetail.vue";
import {useRoute} from "vue-router";

import { getBookList } from "@/services/Libro/bookService.js"
import {onMounted, ref} from "vue";
import {Book} from "@assets/js/Book.js";

let books = ref([])
let book = ref({})

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
      <section id="img-section"><img :src="book._urlImg" alt="Portada del libro" /></section>
      <Characteristics :book = "book"></Characteristics>
      <Description :book="book"></Description>
      <CommentarySection></CommentarySection>
    </section>
    <section class="section-right">
      <BookCardDetail :book = "book"></BookCardDetail>
    </section>
  </article>
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
  border: 1rem;
  background-color: rgb(240, 240, 240);
  border-radius: 1rem;

  align-items: center;
  justify-content: center;
  padding: 0;
}

section img {
  min-width: 10%;
  max-width: 100%;
  height: 100%;

  border-radius: 1rem;
}

</style>