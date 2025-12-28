<script setup lang="js">
import { onMounted, ref } from 'vue';
import { loadBuyer } from '@/services/Perfil/buyerService.js';

const buyer = ref(null);

function closeProfile(){
    localStorage.clear();
    buyer.value = null;
    window.location.reload();
}

onMounted(async () => {
  const email = localStorage.getItem('buyerEmail');
  if (email) {
    try {

      buyer.value = await loadBuyer(email); 
    } catch (error) {
      console.error("Error al cargar perfil:", error);
    }
  }
});
</script>

<template>
    <details v-if="buyer">
        <summary>
            <img src="../../../assets/img/common/profile.png" alt="Foto perfil"/>
            {{ buyer._firstName }}
        </summary>
        <section class="menu">
            <section class="head-menu">
                <p class="profileName">Hola {{ buyer._firstName }}</p>
                <p class="profileEmail">{{ buyer._email }}</p>
            </section>
            <ul>
                <li><router-link class="link" :to="{ name: 'MyProfile', params: { email: buyer._email }}" >Mi Perfil</router-link></li>
                <li><router-link to="/seller/sale" class="link">Compras</router-link></li>
                <li><router-link class="link" :to="{name: 'Seller', params: { email: buyer._email } }" >Vender</router-link></li>
                <li><button @click="closeProfile" >Salir</button></li>
            </ul>
        </section>
    </details>
    <span v-else>Cargando...</span>
</template>

<style scoped>

@media (min-width:1600px) {

    img {
        width: 2vw;
        height: fit-content;
    }

    details {
        position: relative;
        height: 100%;
        width: 100%;
        display: flex;
        flex-direction: column;

        justify-content: center;
        align-items: center;
        
    }

    summary{
        display: flex;
        flex-direction: row;
        list-style: none;
        color: black;
        font-size: 1.3rem;
        font-weight: bold;
        border-radius: 1rem;
        justify-content: center;
        align-items: center;
    }

    .menu {
        margin: 1rem 0 0 0;
        height: 30vh;
        width: 20vw;
        position: absolute;
        top: 100%;
        left: -2.5rem;
        background-color: white;
        box-shadow: 0px 4px 10px rgba(0,0,0,0.1);
        z-index: 100;
        min-width: 150px;
        border-radius: 1rem;
    }

    .head-menu {
        height: 10vh;
        width: 100%;
        padding: 0 0 0 2rem;
        justify-items: left;
        align-content: center;
    }

    .profileName {
        font-size: 2rem;
        font-weight: bold;
    }

    .profileEmail {
        font-size: 1.2rem;
        color: rgb(0, 0, 0, 0.5);
    }

    ul {
        height: 20vh;
        width: 100%;
        list-style: none;
        border-radius: 1rem 1rem 0 0;
    }

    li {
        display: flex;
        height: 25%;
        width: 100%;
        padding: 0;
        align-items: center;
    }

:deep(.link) {
        flex: 1; 
        display: flex;
        align-items: center;   
        justify-content: left; 

        text-align: left;
        
        text-decoration: none;
        color: black;
        font-size: 1.3rem;
        font-weight: normal;
        transition: background-color 0.3s;
        padding: 2rem 0 3.5rem 2rem;
    }

        :deep(.link):hover {
        background-color: rgb(240, 240, 240);
    }

    button {
        width: 100%;
        height: 100%;
        display: flex;
        align-items: center;   
        justify-content: left; 
        text-align: left;
        
        text-decoration: none;
        color: black;
        font-size: 1.3rem;
        font-weight: normal;
        transition: background-color 0.3s;
        padding: 0 0 0 2rem;
        border: 0;
        border-radius: 1rem;
        background-color: rgb(255, 255, 255);
    }

    button:hover{
       background-color: rgb(240, 240, 240); 
    }
}

@media (max-width:1599px){
    img {
        width: 1.5vw;
        height: fit-content;
    }

    details {
        position: relative;
        height: 100%;
        width: 100%;
        display: flex;
        flex-direction: column;

        justify-content: center;
        align-items: center;
        
    }

    summary{
        display: flex;
        flex-direction: row;
        list-style: none;
        color: black;
        font-size: 1.3rem;
        font-weight: bold;
        border-radius: 1rem;
        justify-content: center;
        align-items: center;
    }

    .menu {
        margin: 1rem 0 0 0;
        height: 30vh;
        width: 20vw;
        position: absolute;
        top: 100%;
        left: -2.5rem;
        background-color: white;
        box-shadow: 0px 4px 10px rgba(0,0,0,0.1);
        z-index: 100;
        min-width: 150px;
        border-radius: 1rem;
    }

    .head-menu {
        height: 10vh;
        width: 100%;
        padding: 0 0 0 2rem;
        justify-items: left;
        align-content: center;
    }

    .profileName {
        font-size: 2rem;
        font-weight: bold;
    }

    .profileEmail {
        font-size: 1.2rem;
        color: rgb(0, 0, 0, 0.5);
    }

    ul {
        height: 20vh;
        width: 100%;
        list-style: none;
        border-radius: 1rem 1rem 0 0;
    }

    li {
        display: flex;
        height: 25%;
        width: 100%;
        padding: 0;
        align-items: center;
    }

:deep(.link) {
        flex: 1; 
        display: flex;
        align-items: center;   
        justify-content: left; 

        text-align: left;
        
        text-decoration: none;
        color: black;
        font-size: 1.3rem;
        font-weight: normal;
        transition: background-color 0.3s;
        padding: 1rem 0 2.5rem 1rem;
    }

        :deep(.link):hover {
        background-color: rgb(240, 240, 240);
    }

    button {
        width: 100%;
        height: 100%;
        display: flex;
        align-items: center;   
        justify-content: left; 
        text-align: left;
        
        text-decoration: none;
        color: black;
        font-size: 1.3rem;
        font-weight: normal;
        transition: background-color 0.3s;
        padding: 0 0 0 2rem;
        border: 0;
        border-radius: 1rem;
        background-color: rgb(255, 255, 255);
    }

    button:hover{
       background-color: rgb(240, 240, 240); 
    }
}

</style>