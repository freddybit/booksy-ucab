<script setup>
import { ref, onMounted } from "vue";
import { getBookList, editBook } from "../../services/Libro/bookService.js";
import { consultarVendedor, updateVendedor } from "@/services/Perfil/sellerService.js";

const props = defineProps({
  id: { type: String, required: true }, // Recibimos el ID desde la URL
});

// 1. Declaramos todas las variables fuera para que sean accesibles por el template
const error = ref(false);
const id = ref("");
const urlImg = ref("");
const bookName = ref("");
const subtitle = ref("");
const seriesBook = ref("");
const authorBook = ref("");
const publisherBook = ref("");
const volumeBook = ref("");
const heightBook = ref(0);
const widthBook = ref(0);
const numPagesBook = ref(0);
const publishYearBook = ref("");
const costBook = ref(0);
const description = ref("");
const seller = ref(null);
const categoriesBook = ref([]);
const coverBook = ref("");
const typeBook = ref("");
const languageBook = ref("");

const categoriesBookItems = ["Otro", "Novela", "Cuento", "Poesía", "Teatro", "Ensayo", "Crónica", "Biografía", "Autobiografía", "Memorias", "Diarios", "Epistolar", "Fábula", "Leyenda", "Mito", "Relato corto", "Narrativa", "Prosa poética", "Literatura infantil", "Literatura juvenil", "Literatura fantástica", "Ciencia ficción", "Distopía", "Utopía", "Realismo mágico", "Romántica", "Histórica", "Policíaca", "Thriller", "Suspenso", "Terror", "Humor", "Satírica", "Didáctica", "Académica", "Periodística", "Divulgación científica", "Filosófica", "Política", "Religiosa", "Espiritual", "Viajes", "Aventura", "Erótica", "Gótica", "Costumbrista", "Experimental", "Minimalista", "Posmoderna"];
const coverBookItem = ['Dura', 'Blanda'];
const typeBookItem = ['Físico', 'Digital'];
const languageBookItem = ['Español', 'Ingles', 'Portuguese', 'Alemán', 'Italiano', 'Frances'];

// 2. En onMounted buscamos los datos y llenamos los campos
onMounted(async () => {
  try {
    const books = await getBookList(); // Asumiendo que es una función asíncrona
    const foundBook = books.find(b => b._id == props.id);

    if (foundBook) {
      // Llenamos los refs con la información encontrada
      urlImg.value = foundBook._urlImg;
      id.value = foundBook._id;
      bookName.value = foundBook._nameBook;
      subtitle.value = foundBook._subtitle;
      seriesBook.value = foundBook._series;
      authorBook.value = foundBook._author;
      publisherBook.value = foundBook._publisher;
      languageBook.value = foundBook._language;
      volumeBook.value = foundBook._bookVolume;
      heightBook.value = foundBook._bookHeight;
      widthBook.value = foundBook._bookWidth;
      numPagesBook.value = foundBook._numPages;
      publishYearBook.value = foundBook._publishYear.substring(0, 10);
      costBook.value = foundBook._cost;
      description.value = foundBook._description;
      seller.value = foundBook._seller;
      categoriesBook.value = foundBook._categoryList;
      coverBook.value = foundBook._bookCover;
      typeBook.value = foundBook._typeBook;
    } else {
      console.error("Libro no encontrado");
    }
  } catch (err) {
    console.error("Error al cargar la lista de libros:", err);
  }
});

async function handleSubmit() {
  try {
    // 1. Construye el objeto del libro
    const payload = {
      _id: Number(id.value),
      _urlImg: urlImg.value,
      _nameBook: bookName.value,
      _subtitle: subtitle.value,
      _series: seriesBook.value,
      _author: authorBook.value,
      _language: languageBook.value,
      _publisher: publisherBook.value,
      _bookCover: coverBook.value,
      _typeBook: typeBook.value,
      _bookVolume: volumeBook.value,
      _bookHeight: parseFloat(heightBook.value) || 0,
      _bookWidth: parseFloat(widthBook.value) || 0,
      _categoryList: categoriesBook.value,
      _numPages: parseInt(numPagesBook.value) || 0,
      _publishYear: publishYearBook.value ? new Date(publishYearBook.value).toISOString() : null,
      _cost: parseFloat(costBook.value) || 0,
      _description: description.value,
      _seller: seller.value 
    };

    // 2. Actualizar el libro en la base de datos general
    const response = await editBook(payload); 
    const libroEditado = response.data; // <--- Guardamos la respuesta del servidor

    // 3. Consultar la versión más reciente del vendedor
    const realSeller = await consultarVendedor(payload._seller._email);

    if (realSeller && realSeller._catalog) {
      // Buscamos el índice del libro viejo
      const index = realSeller._catalog.findIndex(b => b._id == payload._id);

      if (index !== -1) {
        // Reemplazamos el libro viejo por el devuelto por el servidor
        realSeller._catalog[index] = libroEditado;
        
        console.log("Actualizando catálogo del vendedor...");
        
        // 4. Persistir al vendedor actualizado
        await updateVendedor(realSeller);
      } else {
        console.warn("El libro no se encontró en el catálogo del vendedor.");
      }
    }

    console.log("¡Éxito!", libroEditado);
    alert('Libro y catálogo actualizados exitosamente');

  } catch (err) {
    if (err.response && err.response.data) {
      console.error("ERRORES DE VALIDACIÓN:", err.response.data.errors);
    } else {
      console.error("Error inesperado:", err);
    }
    error.value = true;
  }
  
}
</script>

<template>
<article>
  <form action="" @submit.prevent="handleSubmit">

    <h1>EDITAR LIBRO</h1>

    <fieldset id="partOne">
      <legend>Características principales</legend>

      <fieldset class="fieldsetPart">

        <label for="nameBook">
          Titulo
          <input v-model="bookName" id="nameBook" type="text" required />
        </label>

        <label for="subtitleBook">
          Sub-titulo
          <input v-model="subtitle" id="subtitleBook" type="text" required />
        </label>

      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="seriesBook">
          Serie
          <input v-model="seriesBook" id="seriesBook" type="text" required />
        </label>

        <label for="authorBook">
          Autor
          <input v-model="authorBook" id="authorBook" type="text" required />
        </label>
      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="publishYearBook">
          Año de publicación
          <input v-model="publishYearBook" id="publishYearBook" type="date" />
        </label>

        <label for="publisherBook">
          Editorial
          <input v-model="publisherBook" id="publisherBook" type="text" required />
        </label>
      </fieldset>

    </fieldset>

    <fieldset id="partTwo">
      <legend>Detalles y dimensiones</legend>

      <fieldset class="fieldsetPart">
        <label for="volumeBook">
          Volumen
          <input v-model="volumeBook" id="volumeBook" type="text" />
        </label>

        <label for="heightBook">
          Altura (cm)
          <input v-model="heightBook" id="heightBook" type="number" step="0.01" />
        </label>
      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="widthBook">
          Ancho (cm)
          <input v-model="widthBook" id="widthBook" type="number" step="0.01" />
        </label>

        <label for="numPagesBook">
          Número de páginas
          <input v-model="numPagesBook" id="numPagesBook" placeholder="Ingresar número de páginas del libro" type="number" />
        </label>
      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="costBook">
          Coste
          <input v-model="costBook" id="costBook" placeholder="Ingresar coste del libro" type="number" step="0.01" />
        </label>

        <label for="description">
          Descripción
          <input v-model="description" id="description" placeholder="Ingresar descripción del libro" type="text" />
        </label>
      </fieldset>

    </fieldset>

    <fieldset id="partThree">

      <legend>Lenguaje, formato, tipo de tapa y categorías del libro</legend>

      <v-combobox  v-model="languageBook" :items="languageBookItem" label="Lenguaje del libro" chips clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

      <v-combobox  v-model="typeBook" :items="typeBookItem" label="Formato del libro" chips clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

      <v-combobox  v-model="coverBook" :items="coverBookItem" label="Tapa del libro" chips clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

      <v-combobox v-model="categoriesBook" :items="categoriesBookItems" label="Categorías del libro" chips multiple clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

    </fieldset>

    <fieldset class="buttonPart">
      <input class="edit-book-button" type="submit" value="Editar libro" />
      <input class="delete-book-button" type="submit" value="Eliminar libro" />
    </fieldset>

  </form>
</article>
</template>

<style scoped>

article {
  justify-items: center;
  align-content: center;

  height: 275vh;
}

form {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  height: 250vh;
  width: 70vw;

  align-items: center;

  box-shadow: 0 0 10px rgba(0,0,0,1);
  background-color: rgb(255,255,255);
  border-radius: 1rem;
  border: none;
}

h1 {
  width: 70vw;
  height: 20vh;
  background-position: center;
  background-repeat: no-repeat;
  font-size: 3rem;
  text-align: center;
  background-image: url("@assets/img/consultar-libros-img/books-banner.jpg");
  background-size: cover;
  align-content: center;
  margin: 0 0 5rem 0;
  border-radius: 1rem 1rem 0 0;
}

#partOne, #partTwo {
  display: flex;
  flex-direction: column;
  height: 55vh;
  width: 50vw;
  border: none;
  justify-items: center;
}

#partThree {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 90vh;
  width: 50vw;
  border: none;
}

.fieldsetPart, .buttonPart{
  display: flex;
  flex-direction: row;
  justify-content: center;
  gap: 5rem;
  border: none;
}

.fieldsetID {
  display: flex;
  flex-direction: row;
  justify-content: left;
  border: none;
  padding: 0 0 0 5rem;
}

legend {
  font-size: 2rem;
  font-weight: bold;
  width: 40vw;
  margin: 0 0 2rem 0
}

label {
  display: flex;
  flex-direction: column;
  font-size: 1.5rem;
  height: 13vh;
  width: 20vw;
  font-weight: normal;
}

input {
  height: 6vh;
  width: 20vw;
  font-size: 1.5rem;
  padding: 0 0 0 2rem;

  margin: 0 0 0 0;
  border:none;
  border-radius: 1rem;
  background-color: rgb(163, 189, 224);
}

.edit-book-button {
  height: 10vh;
  width: 23vw;
  background-color: rgb(0,117,235);
  border-radius: 1rem;
  border: none;
  color: rgb(255,255,255);
  font-size: 2rem;
}

.edit-book-button:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
}

.delete-book-button {
  height: 10vh;
  width: 23vw;
  background-color: rgb(235, 0, 0);
  border-radius: 1rem;
  border: none;
  color: rgb(255,255,255);
  font-size: 2rem;
}

.delete-book-button:hover {
  background-color: rgb(141, 0, 0);
  animation: jump 1s ease;
}

/* Componentes de Vuetify */

:deep(.v-field) {
  height: 15vh;
}

:deep(.v-field-label) {
  font-size: 1.5rem;
  font-weight: normal;
}

:deep(.v-input){
  width:  50vw;
  border-radius: 1rem;
}

</style>