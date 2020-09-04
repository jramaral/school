using System.Collections.Generic;

namespace aluno_API.Models
{
  public class Professor
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Aluno> Alunos { get; set; }
  }
}