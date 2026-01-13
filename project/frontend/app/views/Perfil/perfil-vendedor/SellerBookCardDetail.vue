<script setup lang="js">
import { onMounted, ref } from 'vue';
import { deleteBook } from '../../../services/Libro/bookService'; // Asegúrate de importar tu función
import { consultarVendedor, updateVendedor } from '@/services/Perfil/sellerService';

const props = defineProps({
  book: { type: Object, required: true }
})

let bolivarPrice = ref(0);

const handleDelete = async () => {
  const confirmacion = confirm(`¿Estás seguro de que quieres eliminar "${props.book._nameBook}"?`);
  if (!confirmacion) return;

  try {
    // 1. Borrar el libro de la base de datos
    const result = await deleteBook(props.book._id);

    // 2. Obtener el vendedor actualizado
    const realSeller = await consultarVendedor(props.book._seller._email);

    if (realSeller && realSeller._catalog) {
      
      // CORRECCIÓN AQUÍ: findIndex necesita una función (b => b._id === ...)
      // O si tu catálogo es solo una lista de IDs simples, usa indexOf(props.book._id)
      const index = realSeller._catalog.findIndex(item => 
        (item._id ? item._id === props.book._id : item === props.book._id)
      );

      if (index !== -1) {
        // 4. ELIMINAR del array
        realSeller._catalog.splice(index, 1);
        
        console.log("Sincronizando catálogo del vendedor...");
        
        // 5. ENVIAR los cambios al servidor (Sin esto, el borrado no persiste)
        await updateVendedor(realSeller);
        
        if (result) {
          alert("Libro eliminado con éxito.");
          // router.push({ name: 'SellerCatalog' }); 
        }
      } else {
        console.warn("No se encontró el libro en el catálogo del vendedor localmente.");
      }
    }
  } catch (e) {
    console.error('Error detallado en la eliminación:', e);
    alert("Hubo un error al intentar eliminar el libro.");
  }
};

onMounted(async () => {
  try {
    const response = await fetch('https://ve.dolarapi.com/v1/dolares/oficial');
    const data = await response.json();
    const dollarAverage = data.promedio;

    if (props.book && props.book._cost) {
        bolivarPrice.value = (props.book._cost * dollarAverage);
    }
  } catch (e) {
    console.log('Error en tasa:', e);
  }
})
</script>

<template>
  <section class="book-detail-section">
    <h1 class="book-detail-title">{{ book._nameBook }} <br /> {{ book._subtitle }}</h1>
    <section class="price-section">
      <p class="book-detail-cost">US ${{ book._cost }}</p>
      <p class="book-detail-cost-bs">Bs.{{ bolivarPrice.toFixed(2) }}</p>
    </section>
    <p class="book-detail-delivery">Entrega: Acordar con el vendedor</p>
  </section>

  <section class="book-detail-seller-section" v-if="book && book._seller">
    <h2 class="seller-information">Información sobre el vendedor</h2>
    <p class="seller-name">Nombre del vendedor: {{ book._seller._firstName }}</p>
    <p class="seller-phone">Calificación del vendedor: {{ book._seller._qualification }}</p>
    <p class="seller-email">Correo UCAB: {{ book._seller._email }}</p>
  </section>

  <router-link class="book-detail-link" :to="{name: 'EditBook', params: { id: book._id }}">Editar libro</router-link>
  <button id="delete-button" @click="handleDelete">Eliminar libro</button>
</template>

<style scoped>

.book-detail-section {
  display: flex;
  flex-direction: column;
  min-height: 30vh;
  max-height: 70vh;
  width: 15vw;
  gap: 1rem;
  padding: 2rem 0 2rem 0;

  background-color: rgb(255,255,255);
  justify-content: center;
  align-items: center;
  border-radius: 1rem;
}

.book-detail-title {
  font-size: 3rem;
  text-align: center;
  padding: 0.5rem;
}

.price-section {
  display: flex;
  flex-direction: column;

  justify-content: center;
  align-items: center;
}

.book-detail-cost {
  font-size: 2.8rem;
  font-weight: lighter;
}

.book-detail-cost-bs {
  font-size: 1.2rem;
  font-weight: lighter;
}

.book-detail-seller-section {
  margin: 2rem;
  min-height: 20vh;
  max-height: 40vh;
  padding: 2rem 0 2rem 1rem;
  width: 15vw;

  background-color: rgb(255,255,255);
  justify-items: center;
  align-content: center;
  word-wrap: break-word;
  border-radius: 1rem;
}

.seller-information {
  font-size: 1.6rem;
  font-weight: bold;
}

.seller-name, .seller-phone, .seller-email {
  font-size: 1.4rem;
  font-weight: lighter;
}

.book-detail-link {
  height: 5vh;
  width: 15vw;
  margin: 0 0 0 8%;

  display: inline-block;
  box-sizing: border-box;

  justify-items: center;
  align-content: center;
  text-align: center;

  text-decoration: none;
  background-color: rgb(0,117,235);
  color: rgb(255,255,255);
  font-size: 1.3em;
  font-weight: bold;
  border-radius: 1rem;
  border: none;
}

.book-detail-link:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
}

button {
  height: 5vh;
  width: 15vw;
  margin: 5% 0 0 8%;

  display: inline-block;
  box-sizing: border-box;

  justify-items: center;
  align-content: center;
  text-align: center;

  text-decoration: none;
  background-color: rgb(235, 0, 0);
  color: rgb(255,255,255);
  font-size: 1.3em;
  font-weight: bold;
  border-radius: 1rem;
  border: none;
}

button:hover {
  background-color: rgb(143, 0, 0);
  animation: jump 1s ease;
}

</style>