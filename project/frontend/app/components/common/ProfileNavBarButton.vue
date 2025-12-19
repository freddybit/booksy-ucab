<script setup lang="js">
import { onMounted, ref } from 'vue';
import { loadBuyer } from '@/services/Perfil/buyerService.js';

const buyer = ref(null);

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

<div class="profile-button">
    <span v-if="buyer">{{ buyer._firstName }}</span>
    
    <div v-else class="loader">...</div>
  </div>

</template>

<style>

</style>