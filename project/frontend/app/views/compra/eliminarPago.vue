<template>
  <div class="modal-overlay">
    <div class="modal">
      <h3>Confirmar eliminación</h3>
      <p>¿Está seguro de que desea eliminar esta compra?</p>
      <div class="modal-actions">
        <button class="cancel" @click="$emit('close')">Cancelar</button>
        <button class="confirm" @click="deletePurchase">Eliminar</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "deletecompra",
  props: ['bookId'],   // llega como string desde la ruta
  data() {
    return {
      buyerEmail: localStorage.getItem("buyerEmail") || ""
    };
  },
  methods: {
    async deletePurchase() {
      try {
        console.log("Deleting purchase for bookId:", this.bookId, "and buyerEmail:", this.buyerEmail);
        await axios.delete("http://localhost:5000/api/buyer/delete-purchase", {
          data: { 
            email: this.buyerEmail, 
            bookId: parseInt(this.bookId)
          }
        });
        this.$emit("deleted", this.bookId);
        alert("Compra eliminada correctamente.");
      } catch (error) {
        console.error("Error al eliminar la compra:", error);
        alert("No se pudo eliminar la compra.");
      }
    }
  }
};
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0,0,0,0.6);
  display: flex;
  align-items: center;
  justify-content: center;
}
.modal {
  background: white;
  padding: 20px;
  border-radius: 8px;
  width: 300px;
  text-align: center;
}
.modal-actions {
  margin-top: 20px;
  display: flex;
  justify-content: space-around;
}
.modal-actions .cancel {
  background-color: #7f8c8d;
}
.modal-actions .confirm {
  background-color: #e74c3c;
}
.modal-actions button {
  color: white;
  border: none;
  padding: 8px 16px;
  cursor: pointer;
  border-radius: 4px;
}
.modal-actions button:hover {
  opacity: 0.9;
}
</style>