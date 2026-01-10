<template>
  <div class="purchases">
    <h2>Historial de Compras</h2>
    <h2>{{ buyerEmail }}</h2>

    <div class="cards-container">
      <div class="purchase-card" v-for="purchase in purchases" :key="purchase.book._id">
        <h3>📚 {{ purchase.book._nameBook }}</h3>
        <p><strong>Autor:</strong> {{ purchase.book._author }}</p>
        <p><strong>Precio:</strong> {{ formatPrice(purchase.book._cost) }}</p>
        <p><strong>Vendedor:</strong> {{ purchase.book._seller._firstName }}</p>
        <p><strong>Contacto:</strong> {{ purchase.book._seller._email }}</p>
        <p><strong>Fecha:</strong> {{ formatDate(purchase.purchaseDate) }}</p>
        <button @click="openModal(purchase.book._id)">Eliminar</button>
      </div>
    </div>

    <!-- Modal -->
    <eliminarPago
      v-if="showModal"
      :book-id="selectedBookId"
      @close="closeModal"
      @deleted="handleDeleted"
    />
  </div>
</template>

<script>
import axios from "axios";
import eliminarPago from "@/views/compra/eliminarPago.vue";

export default {
  name: "compras",
  components: { eliminarPago },
  data() {
    return {
      purchases: [],
      showModal: false,
      selectedBookId: null,
      buyerEmail: localStorage.getItem("buyerEmail") || ""
    };
  },
  methods: {
    async fetchPurchases() {
      try {
        const response = await axios.get("http://localhost:5000/api/buyer/purchases", {
          params: { email: this.buyerEmail }
        });
        this.purchases = response.data;
      } catch (error) {
        console.error("Error al obtener las compras:", error);
      }
    },
    openModal(bookId) {
      this.selectedBookId = bookId;
      this.showModal = true;
    },
    closeModal() {
      this.showModal = false;
      this.selectedBookId = null;
    },
    handleDeleted(bookId) {
      this.purchases = this.purchases.filter(p => p.book._id !== bookId);
      this.closeModal();
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      return date.toLocaleDateString("es-VE", {
        year: "numeric",
        month: "long",
        day: "numeric"
      });
    },
    formatPrice(price) {
      return new Intl.NumberFormat("es-VE", {
        style: "currency",
        currency: "USD"
      }).format(price);
    }
  },
  mounted() {
    if (this.buyerEmail) {
      this.fetchPurchases();
    }
  }
};
</script>

<style scoped>
.purchases {
  margin: 20px;
  font-family: 'Space Grotesk', Arial, sans-serif;
  color: #fff;
}

h2 {
  margin-bottom: 10px;
  color: #fff;
}

.cards-container {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 20px;
}

.purchase-card {
  background: linear-gradient(135deg, rgba(255,255,255,0.1), rgba(255,255,255,0.05));
  color: #fff;
  border-radius: 12px;
  padding: 20px;
  box-shadow: 0 6px 12px rgba(0,0,0,0.3);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.purchase-card:hover {
  transform: translateY(-6px);
  box-shadow: 0 10px 16px rgba(0,0,0,0.4);
}

.purchase-card h3 {
  margin-top: 0;
  margin-bottom: 12px;
  font-size: 1.3em;
  color: #ffd700; /* dorado para destacar el título */
}

.purchase-card p {
  margin: 6px 0;
  font-size: 0.95em;
}

button {
  background-color: #e74c3c;
  color: white;
  border: none;
  padding: 8px 14px;
  cursor: pointer;
  border-radius: 6px;
  transition: background-color 0.2s ease;
  margin-top: 12px;
  font-weight: bold;
}

button:hover {
  background-color: #c0392b;
}
</style>