<template>
  <div id="app">
    <Navegacao></Navegacao>
    <div class="marginPrincipal">
      <router-view :key="$route.fullPath"></router-view>
    </div>
  </div>
</template>

<script>
 import Navegacao from "./components/_navegacao/Navegacao";
 import Professores from "./components/Professor/Professor"
 import Alunos from "./components/Aluno/Alunos.vue";
 import Sobre from "./components/Sobre/Sobre"

export default {
  name: "App",
  components: {
    Navegacao,
    Professores,
     Alunos,
     Sobre
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Montserrat:400,700");

body {
  background-color: #eee;
  font-family: "Montserrat", sans-serif;
  display: grid;
  justify-items: center;
}
body,
html {
  margin: 0;
  height: 100%;
}
.marginPrincipal {
  width: 50%;
  margin: auto;
}
#app {
  width: 100%;
}

.btn {
  background-color: red;
  padding: 10px 20px;
  cursor: pointer;
  border: 1px solid #3e5252;
  font-weight: bold;
  color: white;
  border-radius: 10px;
}
.btn:hover {
  text-shadow: 1px 1px 1px black;
}

table {
  margin: 0px;
  padding: 0px;
  list-style-type: none;
  width: 100%;
}
table tr td {
  padding: 20px;
  font-size: 1.3em;
  background-color: #e0edf4;
  margin-bottom: 2px;
  color: #3e5252;
}
table thead th {
  background-color: rgb(184, 208, 216) !important;
  font-size: 1.2em;
  padding: 10px 0px;
  text-align: center !important;
}
.colPequeno {
  width: 5%;
}
</style>
