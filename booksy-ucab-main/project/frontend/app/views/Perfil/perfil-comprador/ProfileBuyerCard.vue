<script setup lang="js">
    import { ref, onMounted } from 'vue';
    import ProfileCard from '@/components/Perfil/perfil-comprador/ProfileCard.vue';
    import { loadBuyer } from '@/services/Perfil/buyerService.js';
    import { Buyer } from '@assets/js/Buyer';

    const perfil = ref(null);

    const props = defineProps({
        email: { type: String, required: true },
        srcImg: { type: String, required: true },
        title: { type: String, required: true },
        description: { type: String, required: true }
    });

</script>
 
<template>
  <router-link to="/" class="link">
    <section class="img-section">
        <img alt="icono" :src="props.srcImg" />
    </section>
    <section>
        <h3>{{ props.title }}</h3>
        <p>{{ props.description }}</p>
    </section>
  </router-link>
</template>

<style scoped>

@media (max-width:1599px) {
  img {
    width: 1vw;
    height: fit-content;
  }

  .link {
    width: 17vw;
    height: 13vh;
    display: flex;
    flex-direction: column;

    background-color: rgb(255, 255, 255);
    border-radius: 1rem;
    text-decoration: none;

    align-items: center;
    padding: 1rem 1.5rem 0.5rem 1.5rem;

    box-shadow: 0.1rem 0.1rem 1rem rgb(0, 0, 0, 0.2);
    margin: 0;
  }

  .link:hover {
    background-color: rgb(225, 225, 225);
   }


  h3 {
    font-size: 1.5rem;
    font-weight: lighter;
    color: rgb(0, 0, 0);
  }

  p {
    font-size: 1.3rem;
    color: rgb(0, 0, 0, 0.5);
  }

  .img-section {
    width: 100%;
    height: 25%;
  }
}

@media (min-width:1600px) {
  img {
    width: 1vw;
    height: fit-content;
  }

  .link {
    width: 17vw;
    height: 13vh;
    display: flex;
    flex-direction: column;

    background-color: rgb(255, 255, 255);
    border-radius: 1rem;
    text-decoration: none;

    align-items: center;
    padding: 1rem 1.5rem 0.5rem 1.5rem;

    box-shadow: 0.1rem 0.1rem 1rem rgb(0, 0, 0, 0.2);
    margin: 0;
  }

  .link:hover {
    background-color: rgb(225, 225, 225);
   }


  h3 {
    font-size: 1.8rem;
    font-weight: lighter;
    color: rgb(0, 0, 0);
  }

  p {
    font-size: 1.5rem;
    color: rgb(0, 0, 0, 0.5);
  }

  .img-section {
    width: 100%;
    height: 40%;
  }
}

</style>