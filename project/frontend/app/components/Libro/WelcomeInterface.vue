<script setup lang="ts">
  import CardEcosystem from "./ecosystem-card/CardEcosystem.vue";
</script>

<template>
  <article class="welcome-article">
    <cite class="text">"Para viajar lejos, no hay mejor nave que un libro."</cite>
    <ul class="filtrer-bar">
      <li class="card"><CardEcosystem link="/beginSection" titleCard="Ingresa a tu cuenta" imgCard="user.png" descriptionCard="Disfruta sin límites con tu cuenta de Booksy UCAB" buttonCard="Ingresa a tu cuenta" /></li>
      <li class="card"><CardEcosystem link="/beginSection" titleCard="Nuestras categorías" imgCard="application.png" descriptionCard="Encuentra libros educativos,de entretenimiento y más" buttonCard="Ir a categorías" /></li>
      <li class="card"><CardEcosystem link="/beginSection" titleCard="Historial de compras" imgCard="receipt.png" descriptionCard="Revisa tus ultimas compras" buttonCard="Ver ultimas compra" /></li>
    </ul>
  </article>
</template>

<style scoped>

@media (min-width: 1600px){


  .welcome-article {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100vh;

    background-image: linear-gradient(to bottom, rgba(0, 0, 0, 0.5), transparent),
    linear-gradient(to top, rgb(5, 33, 52), transparent),
    url("@assets/img/consultar-libros-img/background.png");
    background-position: center;
    background-size: cover;
    background-repeat: no-repeat;

    align-items: center;
    font-weight: bolder;

  }

  .text {
    text-align: center;
    align-content: center;
    height: 10%;
    width: 100%;

    margin: 0 0 10rem 0;
    font-size: 2.5rem;
    color: white;
  }

  .filtrer-bar {
    display: flex;
    list-style: none;
    gap: 3rem;
  }

  .card {
    flex-flow: wrap;
  }

  .card:hover {
    animation: jump 1s ease;
  }
}

@media (min-width: 1200px) and (max-width: 1599px) {


  .welcome-article {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 110vh;

    background-image: linear-gradient(to bottom, rgba(0, 0, 0, 0.5), transparent),
    linear-gradient(to top, rgb(5, 33, 52), transparent),
    url("@assets/img/consultar-libros-img/background.png");
    background-position: center;
    background-size: cover;
    background-repeat: no-repeat;

    align-items: center;
    font-weight: bolder;

  }

  .text {
    text-align: center;
    align-content: center;
    height: 10%;
    width: 100%;

    margin: 0 0 10rem 0;
    font-size: 2.5rem;
    color: white;
  }

  .filtrer-bar {
    display: flex;
    list-style: none;
    gap: 3rem;
  }

  .card {
    flex-flow: wrap;
  }

  .card:hover {
    animation: jump 1s ease;
  }
}

</style>