<template>
  <div class="page-container">
    <div v-if="!buyer || !buyer._email">
      <h3>⚠️ Usuario no registrado</h3>
      <p>Debe iniciar sesión para poder realizar una compra.</p>
    </div>

    <article v-else class="Pagar-libro">
      <!-- Datos del comprador y vendedor -->
      <section class="seller-contact">
        <h3>Correo del comprador: {{ buyer._email }}</h3>
        <div v-if="seller">
          <p><strong>Correo del vendedor:</strong> {{ seller.email }}</p>
          <p><strong>Teléfono del vendedor:</strong> {{ seller.phoneNumber }}</p>
        </div>
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
      paymentMethod: '' ,  // 👈 nuevo campo
      seller: null // 👈 nuevo campo para guardar datos del vendedor

    };
  },
  methods: {
    async fetchSeller() {
      try {
        const endpoint = "http://localhost:5000/api"; // 👈 misma dirección que confirm-purchase
        const response = await axios.get(`${endpoint}/buyer/contactSeller`, {
          params: { id: this.id }
        });
        this.seller = response.data;
        console.log("Datos del vendedor:", this.seller);
      } catch (error) {
        console.error("Error al obtener vendedor:", error);
      }
    },
    async confirmPurchase() {
      if (!this.buyer || !this.buyer._email) {
        alert("Debe iniciar sesión como comprador antes de confirmar la compra.");
        return;
      }
      try {
        const bookIdInt = parseInt(this.id, 10);
        const endpoint = "http://localhost:5000/api/buyer";

        const response = await axios.post(`${endpoint}/confirm-purchase`, {
          email: this.buyer._email,
          bookId: bookIdInt,
          //metodo de pago podria ir aqui
        });

        console.log("Respuesta confirmación:", response.data);

        if (response.data.success) {
          this.purchaseComplete = true;
          this.showPayment = false;
          alert(response.data.message); // 👈 muestra el mensaje del backend
        } else {
          alert(response.data.message || "Compra negada");
        }

        this.$router.push({ name: "CatalogView" });
      } catch (error) {
        // Si hay un error real de red o excepción
        console.error("Error al procesar la compra:", error);
        this.$router.push({ name: "CatalogView" });
      }
    },
    cancelPurchase() {
      this.showPayment = false;
    }
  },
  mounted() {
    if (this.buyer) {
      this.fetchSeller(); // 👈 trae datos del vendedor al entrar
    }
  }
};
</script>


<style scoped>
.page-container {
  margin: 20px;
  font-family: "Space Grotesk", Arial, sans-serif;
  color: #fff;
}

h3 {
  margin-bottom: 10px;
}

.seller-contact {
  background-color: rgba(255, 255, 255, 0.1);
  padding: 16px;
  border-radius: 10px;
  margin-bottom: 20px;
  box-shadow: 0 4px 8px rgba(0,0,0,0.3);
}

.seller-contact p {
  margin: 6px 0;
  font-size: 0.95em;
  display: flex;
  align-items: center;
  gap: 8px;
}

.seller-contact p::before {
  font-size: 1.1em;
}

/* Íconos para cada línea */
.seller-contact p:nth-child(2)::before {
  content: "✉️"; /* correo */
}
.seller-contact p:nth-child(3)::before {
  content: "📞"; /* teléfono */
}

.payment-confirmation {
  background-color: rgba(0, 0, 0, 0.2);
  padding: 16px;
  border-radius: 8px;
}

button {
  background-color: #3498db;
  color: white;
  border: none;
  padding: 8px 14px;
  cursor: pointer;
  border-radius: 6px;
  transition: background-color 0.2s ease;
  margin: 8px 6px 0 0;
  font-weight: bold;
}

button:hover {
  background-color: #2980b9;
}
</style>
