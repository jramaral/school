<template>
  <div>
    <titulo
      :texto="
				professorid != undefined
					? 'Professor: ' + professor.nome
					: 'Todos os alunos'
			"
    />
    <div v-if="professorid != undefined">
      <input type="text" placeholder="Nome do Aluno" v-model="nome" v-on:keyup.enter="addAluno()" />
      <button class="btn btnInput" @click="addAluno()">Adicionar</button>
    </div>

    <table>
      <thead>
        <th>Matricula</th>
        <th>Nome</th>
        <th>Opções</th>
      </thead>
      <tbody v-if="alunos.length > 0">
        <tr
          v-for="(aluno, index) in alunos"
          :key="index"
          :lista="aluno"
          @selecionarItem="itemSelecionado = $event"
        >
          <td>{{ aluno.id }}</td>

          <router-link
            :to="`/alunoDetalhe/${aluno.id}`"
            tag="td"
            style="cursor: pointer"
          >{{ aluno.nome }} {{ aluno.sobrenome }}</router-link>

          <td>
            <button class="btn" @click="remover(aluno)">Remover</button>
          </td>
        </tr>
      </tbody>
      <tfoot v-else>
        <p>Não há registros</p>
      </tfoot>
    </table>
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
      titulo: "Aluno",
      professorid: this.$route.params.prof_id,
      professor: {},
      nome: "",
      alunos: []
    };
  },
  props: {},
  created() {
    if (this.professorid) {
      this.carregarProfessores();
      this.$http
        .get(`http://localhost:3000/alunos?professor.id=${this.professorid}`)
        .then(res => {
          this.alunos = res.data;
        });
    } else {
      this.$http
        .get("http://localhost:3000/alunos")
        .then(res => res.json())
        .then(alu => (this.alunos = alu));
    }
  },
  methods: {
    addAluno() {
      let _aluno = {
        id: "",
        nome: this.nome,
        sobrenome: "",
        professor: {
          id: this.professor.id,
          nome: this.professor.nome
        }
      };

      this.$http.post("http://localhost:3000/alunos", _aluno).then(res => {
        console.log(res.data);
        this.alunos.push(res.data);
      });

      console.log(this.alunos);
      this.nome = "";
    },
    remover(aluno) {
      this.$http.delete(`http://localhost:3000/alunos/${aluno.id}`).then(() => {
        const indice = this.alunos.findIndex(t => t.id === aluno.id);
        this.alunos.splice(indice, 1);
      });
    },
    carregarProfessores() {
      this.$http
        .get(`http://localhost:3000/professores/${this.professorid}`)
        .then(res => {
          this.professor = res.data;
        });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->

<style scoped>
input {
  width: calc(100% - 195px);
  border: 0;
  padding: 20px;
  font-size: 1.3em;
  color: cadetblue;
  display: inline;
}

.btnInput {
  border: 0px;
  padding: 20px;
  font-size: 1.3em;
  display: inline;
  background-color: darkgray;
}
</style>
