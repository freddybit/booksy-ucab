<script setup>
import { ref } from 'vue';
import { loadBuyer } from '@/services/Perfil/buyerService.js';

const buyer = ref(null)
const id = ref(null);
const phoneNumber = ref('');
const bankName = ref('');

const emit = defineEmits(['submit']);

async function handleSubmit() {
  const email = localStorage.getItem('buyerEmail');

  if (!email) {
    console.error('No se encontro un email en el LocalStorage');
    return;
  }

  try {
    buyer.value = await loadBuyer(email);
    emit('submit', {
      _email: buyer.value._email,
      _firstName: buyer.value._firstName,
      _lastName: buyer.value._lastName,
      _age: buyer.value._age,
      _password: buyer.value._password,
      _id: parseInt(id.value),
      _phoneNumber: phoneNumber.value,
      _bankName: bankName.value
    });
  } catch (error) {
    console.error(error.message);
  }

}
</script>

<template>
  <form class="form-profile" @submit.prevent="handleSubmit">
    <h1>REGISTRAR VENDEDOR</h1>

    <label for="id">
      C.I.
      <input id="id" v-model="id" placeholder="Ej: 31703151  " required />
    </label>

    <label for="phoneNumber">
      Número de telefono
      <input id="phoneNumber" v-model="phoneNumber" placeholder="Ej: 0424-1540999" required />
    </label>

    <label for="bankName"> 
      Bancos que utilizas
      <input id="bankName" v-model="bankName" placeholder="Ej: Banesco, BNC, Mercantil..." required />
    </label>

    <input type="submit" value="Crear cuenta" />
  </form>
</template>

<style scoped>

form {
  display: flex;
  height: 75vh;
  flex-direction: column;
  align-items: center;
  padding: 2rem;
  gap: 2rem;
  width: 38vw;
  background-color: rgba(255,255,255);
  border-radius: 1rem;
  box-shadow: 0 0 10px rgba(0,0,0,0.5);
}

label {
  display: flex;
  flex-direction: column;
  font-size: 1.5rem;
  gap: 1rem;
}

fieldset label input {
  width: 15vw;
}

form fieldset {
  border: none;
}

input {
  width: 35vw;
  padding: 1rem;
  font-size: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}

input[type="submit"] {
  margin: 2rem;
  width: 18vw;
  height: 7.5vh;
  border-radius: 1rem;
  background-color: #0077cc;
  border: none;
  color: rgb(255,255,255);
}

input[type="submit"]:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
  cursor: pointer;
}

h1 {
  font-size: 2.5rem;
  margin: 2rem 0 1rem 0;
  color: rgb(0,0,0);
}

</style>
