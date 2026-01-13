<script setup>
import { computed } from 'vue';

const currentYear = computed(() => new Date().getFullYear());
</script>

<template>
  <footer class="footer">
    <div class="container">
      
      <div class="footer-col">
        <h3 class="brand-title">Booksy <span class="ucab-text">UCAB</span></h3>
        <p class="description">
          La plataforma oficial de intercambio de libros para estudiantes de la 
          Universidad Católica Andrés Bello. Compra, vende y aprende.
        </p>
      </div>

      <div class="footer-col">
        <h4>Navegación</h4>
        <ul class="footer-links">
          <li><router-link :to="{ name: 'Home' }">Inicio</router-link></li>
          <li><router-link :to="{ name: 'CategoryList' }">Categorías</router-link></li>
          <li><router-link to="/beginSellerSection">Vender un libro</router-link></li>
          <li><a href="#">Ayuda / Soporte</a></li> 
        </ul>
      </div>

      <div class="footer-col">
        <h4>Contacto</h4>
        <ul class="contact-info">
          <li>📍 UCAB - Montalbán, Caracas</li>
          <li>📧 soporte@booksy.ucab.edu.ve</li>
          <li>📚 Biblioteca Central</li>
        </ul>
        <div class="socials">
            <span class="social-icon">IG</span>
            <span class="social-icon">X</span>
        </div>
      </div>

    </div>

    <div class="footer-bottom">
      <p>&copy; {{ currentYear }} Booksy UCAB. Todos los derechos reservados.</p>
      <p class="credits">Desarrollado para Ingeniería de Software</p>
    </div>
  </footer>
</template>

<style scoped>
/* Variables de color sugeridas (puedes ajustarlas a tu paleta) */
.footer {
  background-color: #1a1a1a; /* Fondo oscuro elegante */
  color: #b0b0b0; /* Texto gris suave para no cansar la vista */
  padding: 3rem 0 0 0;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  margin-top: auto; /* Truco: empuja el footer al fondo si usas flex en el padre */
}

.container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 2rem;
  gap: 2rem;
}

.footer-col {
  flex: 1 1 250px; /* Crecen y se encogen, base de 250px */
  margin-bottom: 2rem;
}

/* Tipografía y Estilos */
.brand-title {
  color: #fff;
  font-size: 1.5rem;
  margin-bottom: 1rem;
}

.ucab-text {
  color: #00B6BD; /* Color cyan/verde característico de identidad UCAB moderna o el amarillo */
  font-weight: bold;
}

.description {
  font-size: 0.9rem;
  line-height: 1.6;
}

h4 {
  color: #fff;
  margin-bottom: 1.2rem;
  font-size: 1.1rem;
  position: relative;
}

/* Decoración debajo de los títulos */
h4::after {
  content: '';
  position: absolute;
  left: 0;
  bottom: -5px;
  width: 30px;
  height: 2px;
  background-color: #00B6BD;
}

/* Listas */
ul {
  list-style: none;
  padding: 0;
}

.footer-links li, .contact-info li {
  margin-bottom: 0.8rem;
}

.footer-links a {
  color: #b0b0b0;
  text-decoration: none;
  transition: color 0.3s ease;
}

.footer-links a:hover {
  color: #00B6BD;
  padding-left: 5px; /* Pequeño efecto de movimiento */
}

/* Redes Sociales */
.socials {
  margin-top: 1rem;
  display: flex;
  gap: 10px;
}

.social-icon {
  background: #333;
  color: #fff;
  padding: 5px 10px;
  border-radius: 4px;
  font-size: 0.8rem;
  cursor: pointer;
}

/* Barra inferior */
.footer-bottom {
  background-color: #111;
  padding: 1.5rem;
  text-align: center;
  font-size: 0.85rem;
  margin-top: 2rem;
  border-top: 1px solid #333;
}

.credits {
  margin-top: 5px;
  font-style: italic;
  opacity: 0.6;
}

/* Responsividad para móviles */
@media (max-width: 768px) {
  .container {
    flex-direction: column;
  }
  
  .footer-col {
    text-align: center;
  }
  
  h4::after {
    left: 50%;
    transform: translateX(-50%);
  }
}
</style>