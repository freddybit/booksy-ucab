<script>
</script>

<template>
    <section>
        <img src="../../../../assets/img/common/searching.png" alt="Icono de búsqueda" />
        <div>
        <h2>No hay publicaciones que coincidan con tu búsqueda.</h2>
        <ul>
            <li>Revisa la ortografía de la palabra.</li>
            <li>Utiliza palabras más genéricas o menos palabras.</li>
            <li>Navega por las categorías para encontrar un producto similar.</li>
        </ul>
        </div>
    </section>
</template>

<style scoped>

section {
  display: flex;
  height: 25rem;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  gap: 5rem;
  border-radius: 1rem;
  background-color: rgb(255, 255, 255);
}

div {
    display: flex;
    flex-direction: column;
    gap: 2rem;
}

img {
    height: 20vh;
}

h2 {
  font-weight: bold;
  font-size: 2rem;
  margin: 0;
}

ul {
    padding: 0 0 0 4rem;
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

li {
    font-size: 1.5rem;
}

</style>