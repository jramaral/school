<template>
  <div>
    <Titulo :texto="`Aluno: ${aluno.nome}`" :btnVoltar="!readOnly">
      <button v-show="readOnly" class="btn btn-editar" @click="editar()">Editar</button>
    </Titulo>
    <table>
      <tbody>
        <tr>
          <td class="colPequeno">Matricula</td>
          <td>{{ aluno.id }}</td>
        </tr>
        <tr>
          <td class="colPequeno">Nome</td>
          <td>
            <label v-if="readOnly">{{ aluno.nome }}</label>
            <input v-else type="text" v-model="aluno.nome" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Sobrenome</td>
          <td>
            <label v-if="readOnly">{{ aluno.sobrenome }}</label>
            <input v-else type="text" v-model="aluno.sobrenome" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Data nascimento</td>
          <td>
            <label v-if="readOnly">{{ aluno.dataNascimento }}</label>
            <input v-else type="datetime" v-model="aluno.dataNascimento" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Professor</td>
          <td>
            <div v-if="aluno.professor != undefined">
              <label v-if="readOnly">{{ aluno.professor.nome }}</label>
              <select v-else v-model="aluno.professor.id">
                <option
                  v-for="(professor, index) in professores"
                  :key="index"
                  v-bind:value="professor.id"
                >{{ professor.nome }}</option>
              </select>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div style="margin-top:15px">
      <div v-if="!readOnly">
        <button @click="salvar(aluno)">Salvar</button>
        <button @click="cancelar()">Cancelar</button>
      </div>
    </div>
  </div>
</template>

<script>
import Titulo from "../share/Titulo";
export default {
  components: {
    Titulo
  },
  data() {
    return {
      professores: [],
      aluno: {},
      idAluno: this.$route.params.aluno_id,
      readOnly: true
    };
  },
  created() {
    this.$http
      .get("http://localhost:5000/api/aluno/" + this.idAluno)
      .then(res => {
        console.log(res.data);
        this.aluno = res.data;
      });

    this.$http
      .get("http://localhost:5000/api/professor")
      .then(res => res.json())
      .then(professor => (this.professores = professor));
  },
  methods: {
    editar() {
      this.readOnly = !this.readOnly;
    },
    cancelar() {
      this.editar();
    },
    salvar(aluno) {
      let aluEditado = {
        id: aluno.id,
        nome: aluno.nome,
        sobrenome: aluno.sobrenome,
        dataNascimento: aluno.dataNascimento,
        professorid: aluno.professor.id
      };
      this.$http
        .put(`http://localhost:5000/api/aluno/${aluEditado.id}`, aluEditado)
        .then(res => res.json())
        .then((aluno = this.aluno = aluno));
    }
  }
};
</script>

<style scoped>
.colPequeno {
  width: 20%;
  text-align: right;
  background-color: rgb(125, 217, 245);
  font-weight: bold;
}
input,
select {
  margin: 0px;
  padding: 5px 10px;
  font-size: 0.9em;
  font-family: Montserrat;
  border-radius: 6px;
  border: 1px solid silver;
  width: 95%;
}
select {
  height: 38px;
  width: 100%;
}
.btn-editar {
  float: right;
  background-color: cornflowerblue;
}
</style>
