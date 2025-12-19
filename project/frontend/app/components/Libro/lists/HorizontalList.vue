<script setup lang="js">
  import CategoryCard from "../Category/CategoryCard.vue";
</script>

<template>
  <article class="book-list-article">
    <section class="horizontal-section">
      <h2 class="article-title">Categorías</h2>
      <div class="div-category-card">
        <CategoryCard link="book" imgCard="classic-literature-category.jpg" title="Literatura clásica" />
        <CategoryCard link="book" imgCard="art-design-category.png" title="Arte y diseño" />
        <CategoryCard link="book" imgCard="math-category.jpg" title="Matemáticas y Estadística" />
        <CategoryCard link="book" imgCard="fiction-category.jpg" title="Ficción" />
        <CategoryCard link="book" imgCard="poesia-category.jpg" title="Poesía" />
        <CategoryCard link="book" imgCard="no-fiction-category.jpg" title="Juvenil" />
      </div>
    </section>
  </article>
</template>

<style scoped>

@media (min-width: 1600px) {
  .book-list-article {
    display: flex;
    height: 50vh;
    justify-content: center;
    align-items: center;

    background-color: rgb(5, 33, 52);

  }

  .horizontal-section {
    display: flex;
    flex-direction: column;
    background-color: rgb(255,255,255);
    height: 40vh;
    width: 54vw;
    padding: 0 2rem 0 2rem;
    border-radius: 1rem;
  }

  .article-title {
    height: 5vh;
    color: black;
    justify-content: left;
    align-content: center;
    margin: 2rem 0 2rem 2rem;

    font-size: 2rem;
  }

  .div-category-card {
    display: flex;
    width: 53vw;
    flex-direction: row;
    flex-wrap: wrap;

    justify-content: center;

    gap: 2rem;
  }

  .horizontal-section:hover {
    animation: jump 1s ease;
  }

  .article-title:hover {
    animation: jump 1s ease;
  }
}

@media (min-width: 1200px) and (max-width: 1599px) {
  .book-list-article {
    display: flex;
    height: 60vh;
    justify-content: center;
    align-items: center;

    background-color: rgb(5, 33, 52);

  }

  .horizontal-section {
    display: flex;
    flex-direction: column;
    background-color: rgb(255,255,255);
    height: 48vh;
    width: 70vw;
    padding: 0 2rem 0 2rem;
    border-radius: 1rem;
  }

  .article-title {
    height: 5vh;
    color: black;
    justify-content: left;
    align-content: center;
    margin: 2rem 0 2rem 2rem;

    font-size: 2rem;
  }

  .div-category-card {
    display: flex;
    width: 70vw;
    flex-direction: row;
    flex-wrap: wrap;

    justify-content: center;

    gap: 2rem;
  }

  .horizontal-section:hover {
    animation: jump 1s ease;
  }

  .article-title:hover {
    animation: jump 1s ease;
  }
}

</style>