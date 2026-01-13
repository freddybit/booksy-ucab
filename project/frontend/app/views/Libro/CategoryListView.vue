<script setup lang="js">
import { ref } from 'vue';
import { useRouter } from 'vue-router'; 

// Lista de categorías
const categoriesBookItems = [
  "Otro", "Novela", "Cuento", "Poesía", "Teatro", "Ensayo", "Crónica", "Biografía",
  "Autobiografía", "Memorias", "Diarios", "Epistolar", "Fábula", "Leyenda", "Mito", 
  "Relato corto", "Narrativa", "Prosa poética", "Literatura infantil", "Literatura juvenil", 
  "Literatura fantástica", "Ciencia ficción", "Distopía", "Utopía", "Realismo mágico", 
  "Romántica", "Histórica", "Policíaca", "Thriller", "Suspenso", "Terror", "Humor",
  "Satírica", "Didáctica", "Académica", "Periodística", "Divulgación científica", 
  "Filosófica", "Política", "Religiosa", "Espiritual", "Viajes", "Aventura", "Erótica", 
  "Gótica", "Costumbrista", "Experimental", "Minimalista", "Posmoderna", "Ingenieria"
];
</script>

<template>
    <article>
        <section>
            <h2>Explorar por Categorías</h2>
            <ul class="category-list">
                <li v-for="category in categoriesBookItems" :key="category">
                    <router-link 
                        class="category-link" :to="{ name: 'CategoryCatalog', params: { category: category } }">{{ category }}
                    </router-link>
                </li>
            </ul>
        </section>
    </article>
</template>

<style scoped>
    article {
        display: flex;
        flex-direction: row;
        width: 100%;
        height: 88vh;
        justify-content: center;
        align-items: center;
    }

    section {
        display: flex;
        flex-direction: column;
        width: 70%;
        height: 80%;
        padding: 2rem;
        background-color: rgb(255, 255, 255);
        border-radius: 1rem;
        box-shadow: 0 4px 6px rgba(0,0,0,0.1);

        justify-items: center;
        align-items: center;
    }

    h2 {
        font-size: 2rem;
        margin: 2% 0 2% 0;
        text-align: center;
    }

    .category-list {
        display: flex;
        width: 70%;
        padding: 2rem;
        flex-wrap: wrap;
        gap: 5rem;
        justify-content: center;
        list-style: none; 
    }

    .category-link {
        display: inline-block;
        text-decoration: none;
        color: #333;
        border: 1px solid #ddd;
        padding: 0.5rem 1rem;
        border-radius: 20px;
        transition: all 0.3s ease;
        background-color: #fff;
        font-size: 1.2rem;
    }

    .category-link:hover {
        background-color: #007bff;
        color: white;
        border-color: #007bff;
        transform: translateY(-2px);
        box-shadow: 0 2px 5px rgba(0,0,0,0.2);
    }
</style>