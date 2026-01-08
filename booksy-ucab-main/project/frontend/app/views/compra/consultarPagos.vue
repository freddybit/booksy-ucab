<template>
  <div class="purchases">
    <h2>Historial de Compras</h2>
    <h2>{{ buyerEmail }}</h2>

    <table >
      <thead>
        <tr>
          <th>ID</th>
          <th>Libro</th>
          <th>Autor</th>
          <th>Precio</th>
          <th>Vendedor</th>
          <th>Contacto</th>
          <th>Fecha de Compra</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="purchase in purchases" :key="purchase.book._id">
          <td>{{ purchase.book._id }}</td>
          <td>{{ purchase.book._nameBook }}</td>
          <td>{{ purchase.book._author }}</td>
          <td>{{ formatPrice(purchase.book._cost) }}</td>
          <td>{{ purchase.book._seller._firstName }}</td>
          <td>{{ purchase.book._seller._email }}</td>
          <td>{{ formatDate(purchase.purchaseDate) }}</td>
          <td>
            <button @click="openModal(purchase.book._id)">Eliminar</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal separado -->
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

        console.log("Response data:", response.data);
        this.purchases = response.data;
        console.log("Purchases fetched:", this.purchases);
        console.log("cuantas compras:", this.purchases.length);

        if (this.purchases.length === 0) {
          console.log("No tiene compras registradas (front).");
        }
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
      this.purchases = this.purchases.filter(p => p.Book._id !== bookId);
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
      console.log("buscando compras de:", this.buyerEmail, "ese es el email");
      this.fetchPurchases();
    } else {
      console.warn("No se encontró buyerEmail en localStorage.");
    }
  }
};
</script>

<style scoped>
.purchases {
  margin: 20px;
  font-family: Arial, sans-serif;
}

h2 {
  margin-bottom: 10px;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

thead {
  background-color: #f4f4f4;
}

tr:nth-child(even) {
  background-color: #fafafa;
}

button {
  background-color: #3498db;
  color: white;
  border: none;
  padding: 6px 12px;
  cursor: pointer;
  border-radius: 4px;
  transition: background-color 0.2s ease;
}

button:hover {
  background-color: #2980b9;
}
</style>
