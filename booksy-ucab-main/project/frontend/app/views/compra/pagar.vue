<template>
  <div class="page-container">
    <div v-if="!buyer || !buyer._email">
      <h3>⚠️ Usuario no registrado</h3>
      <p>Debe iniciar sesión para poder realizar una compra.</p>
    </div>

    <article v-else class="Pagar-libro">
      <section class="seller-contact">
        <h3>Correo del comprador: {{ buyer._email }}</h3>
      </section>

      <section class="payment-confirmation" v-if="showPayment">
        <h3>Confirmar Pago</h3>
        <p>¿Desea confirmar la compra?</p>

        <!-- Mostrar el ID del producto -->
        <p>ID del producto a comprar: {{ id }}</p>

        <!-- Campo para escribir el método de pago -->
        <label for="paymentMethod">
          Método de pago:
          <input v-model="paymentMethod" id="paymentMethod" placeholder="Ej: Tarjeta, PayPal, Transferencia" type="text" />
        </label>

        <button @click="confirmPurchase">Confirmar</button>
        <button @click="cancelPurchase">Cancelar</button>
      </section>

      <button v-if="!showPayment && !purchaseComplete" @click="showPayment = true">
        Realizar Pago
      </button>

      <section v-if="purchaseComplete">
        <h3>Compra Exitosa</h3>
        <p>El libro ha sido comprado exitosamente.</p>
        <p>¡Gracias por su compra! {{ buyer?._email }}.</p>
      </section>
    </article>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  props: ['id'],   // llega como string desde la ruta
  data() {
    return {
      buyer: (() => {
        const t = localStorage.getItem("isBuyerLogged");
        const f = localStorage.getItem("isSellerLogged");
        if (t==="true") {
          const buyerEmail = localStorage.getItem("buyerEmail");
          if (buyerEmail ) {
            return { _email: buyerEmail };
          }
        }
        return null;
      })(),
      showPayment: false,
      purchaseComplete: false,
      paymentMethod: ''   // 👈 nuevo campo
    };
  },
  methods: {
    async confirmPurchase() {
      if (!this.buyer || !this.buyer._email) {
        console.log("email", localStorage.getItem("buyerEmail"));
        console.log("buyer true?", localStorage.getItem("isBuyerLogged"));
        console.log("seller false?", localStorage.getItem("isSellerLogged"));
        alert('Debe iniciar sesión como comprador antes de confirmar la compra.');
        return;
      }

      try {
        const bookIdInt = parseInt(this.id, 10);
        const endpoint = 'http://localhost:5000/api';

        console.log("llego hasta el try");

        const response = await axios.post(`${endpoint}/buyer/confirm-purchase`, {
          email: this.buyer._email,
          bookId: bookIdInt,
             // 👈 se envía al backend
        });

        console.log("Respuesta confirmación:", response.data);

        if (response.data.success) {
          this.purchaseComplete = true;
          this.showPayment = false;
          console.log("compra exitosa");
        } else {
          alert('compra negada');
        }

        this.$router.push({ name: 'CatalogView' });

      } catch (error) {
        alert('Error al procesar la compra');
        console.error(error);
        this.$router.push({ name: 'CatalogView' });
      }
    },
    cancelPurchase() {
      this.showPayment = false;
    }
  }
};
</script>



 <style scoped> 
 .page-container { border: 2px solid green; border-radius: 8px; padding: 20px; 
  max-width: 700px; margin: 30px auto; background-color: #f9f9f9; font-family: Arial, sans-serif; } 
 h3 { color: #333; margin-bottom: 10px; } 
 section { margin-bottom: 20px; } 
 button { background-color: #007BFF; color: white; border: none; 
  padding: 10px 16px; 
  margin-right: 10px;
  border-radius: 4px; cursor: pointer; font-weight: bold; }
  button:hover { background-color: #0056b3; } 
  </style>

