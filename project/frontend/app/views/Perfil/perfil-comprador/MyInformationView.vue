<script setup lang="js">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router'; 
import { eliminarComprador, loadBuyer } from '@/services/Perfil/buyerService.js';

const props = defineProps({
  email: { type: String, required: true }
});

const buyer = ref(null);
const loading = ref(true);
const router = useRouter();

onMounted(async () => {
  try {  
    if (props.email) {
      buyer.value = await loadBuyer(props.email);
    }
  } catch (error) {
    console.error("Error cargando comprador:", error);
  } finally {
    loading.value = false;
  }
});

</script>

<template>
  <div class="profile-container">
    
    <div v-if="loading" class="loading-state">
      <p>Cargando información...</p>
    </div>

    <article v-else-if="buyer" class="info-card">
      <h1>Mi información</h1> 
      <section class="info-group">
        <h3>Nombre Completo</h3>
        <p>{{ buyer?._firstName }} {{ buyer?._lastName }}</p>
      </section>
    <section class="info-group">
        <h3>Edad</h3>
        <p>{{ buyer?._age }}</p>
      </section>
    </article>

    <div v-else>
      <p>No se pudo cargar la información del usuario.</p>
    </div>

  </div>
</template>

<style scoped>
.profile-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 88vh;
  width: 100%;
  background-color: rgb(230, 230, 230);
  padding: 20px;
}

.info-card {
  background-color: white;
  padding: 2rem;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);
  width: 100%;
  max-width: 500px;
  text-align: center;
}

.info-group {
  margin-bottom: 1.5rem;
  border-bottom: 1px solid #eee;
  padding-bottom: 10px;
}

h1 {
    font-size: 2.5rem;
}

h3 {
    font-size: 1.7rem;
    margin: 0;
    color: #555;
}

p {
  margin: 5px 0 0 0;
  font-weight: bold;
  font-size: 1.5rem;
  color: #333;
}
</style>
