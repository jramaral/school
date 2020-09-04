<template>
  <div>
    <div style="display: flex; width:50%">
      <h1>{{ texto }}</h1>
      <div class="voltar">
        <slot></slot>
        <button v-show="btnVoltar" class="btn btn-voltar" @click="voltar()">Voltar</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    texto: String,
    btnVoltar: Boolean
  },
  methods: {
    voltar() {
      this.$router.back();
    }
  }
};
</script>

<style scoped>
.voltar {
  width: 50%;
  position: absolute;
  margin-top: 15px;
}
.btn-voltar {
  float: right;
  background-color: cornflowerblue;
}
</style>