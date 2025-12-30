<script setup lang="js">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { loadBuyer } from '@/services/Perfil/buyerService.js';
import ProfileSellerCard from './ProfileSellerCard.vue';
import { consultarVendedor } from '@/services/Perfil/sellerService';

const props = defineProps({
  email: { type: String, required: true }
});

const router = useRouter();
const seller = ref(null);
const loading = ref(true);

onMounted(async () => {
  try {
    seller.value = await consultarVendedor(props.email);

    if (seller.value && seller.value._email === props.email) {
      console.log("Vendedor validado, permaneciendo en el menú.");
      loading.value = false;
    } else {
      alert(seller._email);
      router.push('/registerSellerSection'); 
    }
  } catch (error) {
    console.error("Error validando vendedor:", error);
    router.push('/');
  }
});

</script>

<template>
  <article v-if="seller">

    <section class="left-section">
      <section class="menu-title">
        <img alt="icono del submenu" src="../../../../assets/img/common/menu.png"/>
        <h2>Vender</h2>
      </section>
      <ol>
        <li>
          <router-link class="link-left">
            <img alt="Icono de mi perfil" class="menu-icon" src="../../../../assets/img/common/shopping-bag.png" />
            Compras
          </router-link>
        </li>
        <li>
          <router-link class="link-left">
            <img alt="Icono de mi perfil" class="menu-icon" src="../../../../assets/img/common/bill.png" />
            Facturacion
          </router-link>
        </li>
        <li>
          <router-link class="link-left" :to="{name: 'Seller', params: { email: seller._email } }" >
            <img alt="Icono de mi perfil" class="menu-icon" src="../../../../assets/img/common/tag.png" />
            Vender
          </router-link>
        </li>
        <li>
          <router-link class="link-left" :to="{ name: 'MyProfile', params: { email: seller._email }}" >
            <img alt="Icono de mi perfil" class="menu-icon" src="../../../../assets/img//common/user.png" />
            Mi perfil
          </router-link>
        </li>
      </ol>
    </section>

    <section class="right-section">
      <div>
        <img alt="Foto de perfil" src="../../../../assets/img/common/profile-big.png" /> 
        <section>
          <h1>{{ seller._firstName }} {{ seller._lastName }}</h1>
          <p class="email-subtitle">{{ seller._email }}</p>
        </section>
      </div>

      <ul>
        <li><ProfileSellerCard src-img="../../../../assets/img/common/search-icon.png" description="Registra, revisa y elimina ventas cuando sea necesario." title="Gestionar Ventas" link="/gestionarVenta/sale" /></li>
        <li><ProfileSellerCard src-img="../../../../assets/img/common/search-icon.png" description="Registra todos los libros que deseas vender." title="Registrar libro" link="/seller/registerBook" /></li>
        <li><ProfileSellerCard src-img="../../../../assets/img/common/search-icon.png" description="Modifica los libros que tienes en venta" title="Editar libro" link="/seller/editBook" /></li>
        <li><ProfileSellerCard src-img="../../../../assets/img/common/search-icon.png" description="Echale un viztazo a todos tus libros en venta" title="Ver stock" link="/seller/editBook" /></li>
        <li><ProfileSellerCard src-img="../../../../assets/img/common/search-icon.png" description="Saca los libros del stock." title="Eliminar libro" link="/seller/editBook" /></li>
      </ul>
    </section>
  </article>
  
  <div v-else-if="loading">Cargando perfil...</div>
  <div v-else>No se pudo encontrar el perfil.</div>
</template>

<style scoped>

  article {
    display: flex;
    justify-content: space-between;
    height: 88vh;
    width: 100%;
    background-color: rgb(230, 230, 230);
  }

  .left-section {
    display: flex;
    flex-direction: column;
    height: 100%;
    width: 17%;

    background-color: rgb(250, 250, 250);
    box-shadow: 0.2rem 0.1rem 0.5rem rgb(0,0,0, 0.1);
    margin: 0;
    padding: 0;

    align-items: center;
  }

  .right-section {
    display: flex;
    flex-direction: column;
    height: 80%;
    width: 83%;
    margin: 0;
    padding: 0;
    align-items: center;
  }



  div {
    display: flex;
    flex-direction: row;
    width: 70%;
    height: 25%;
    justify-content: center;
    align-items: center;
    gap: 2rem;
  }

  img {
    width: 7rem;
    height: fit-content;
  }

  h1 {
    font-size: 2.1rem;
    font-weight: bold;
  }

  p {
    font-size: 1.5rem;
    font-weight: lighter;
  }

  ul {
    height: 50%;
    width: 72%;
    list-style: none;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    gap: 0rem 5rem;
    justify-content: left;
    align-items: center;
    margin: 0;
  }

  li {
    height: 13vh;
    width: 17vw;
    margin: 0;
  }

  .menu-title {
    display: flex;
    flex-direction: row;
    width: 85%;
    height: 20%;

    justify-content: left;
    align-items: center;
    padding: 0 0 0 15%;
    gap: 2rem;
  }

  img[alt="icono del submenu"] {
    width: 1vw;
    height: fit-content;
  }

  h2 {
    font-size: 2rem;
    font-weight: lighter;
  }

  ol {
    height: 80%;
    width: 100%;
    display: flex;
    flex-direction: column;
  }

  ol li {
    display: flex;
    height: 10%;
    width: 100%;
    align-items: center;
  }

  .menu-icon {
    width: 1.5vw;
    height: fit-content;
  }

  :deep(.link-left){
    display: flex;
    height: 100%;
    width: 100%;
    align-items: center;
    padding: 0 0 0 15%;

    text-decoration: none;
    font-size: 1.25rem;
    gap: 2rem;
    color: rgb(0, 0, 0);
  }

  :deep(.link-left):hover {
    display: flex;
    height: 100%;
    width: 100%;
    background-color: rgb(146,230,255);
    align-items: center;
    padding: 0 0 0 15%;

    text-decoration: none;
    font-size: 1.25rem;
    gap: 2rem;
    color: rgb(0, 0, 0);
  }

</style>