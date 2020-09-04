<template>
  <div>
    <titulo texto="Professores" />
    <table>
      <thead>
        <th>Cod.</th>
        <th>Nome</th>
        <th>Alunos</th>
      </thead>
      <tbody v-if="professores.length>0">
        <tr v-for="(professor, index) in professores" :key="index">
          <td class="centralizarTexto">{{professor.id}}</td>

          <router-link
            :to="`/alunos/${professor.id}`"
            tag="td"
            style="cursor: pointer"
          >{{professor.nome}} {{professor.sobrenome}}</router-link>

          <td class="centralizarTexto">{{professor.qtdAluno}}</td>
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
      professores: [],
      Alunos: []
    };
  },
  props: {},
  created() {
    this.$http
      .get("http://localhost:3000/alunos")
      .then(res => res.json())
      .then(alu => {
        this.Alunos = alu;
        this.carregarProfessores();
      });
  },
  methods: {
    pegarQuantidadeAluno() {
      this.professores.forEach((professor, index) => {
        professor = {
          id: professor.id,
          nome: professor.nome,
          qtdAluno: this.Alunos.filter(
            aluno => aluno.professor.id === professor.id
          ).length
        };
        this.professores[index] = professor;
      });
    },
    carregarProfessores() {
      this.$http.get("http://localhost:3000/professores").then(res => {
        this.professores = res.data;
        this.pegarQuantidadeAluno();
      });
    }
  }
};
</script>

<style scoped>
.centralizarTexto {
  text-align: center;
}
</style>