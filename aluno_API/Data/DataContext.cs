using System.Collections.Generic;
using aluno_API.Models;
using Microsoft.EntityFrameworkCore;

namespace aluno_API.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Professor>()
      .HasData(
        new List<Professor>(){
          new Professor(){
            Id=1,
            Nome="Jose"
          },
          new Professor(){
            Id=2,
            Nome="Pedro"
          },
          new Professor(){
            Id=3,
            Nome="Maria"
          }
        }
      );

      builder.Entity<Aluno>()
      .HasData(
              new List<Aluno>(){
          new Aluno(){
            Id=1,
            Nome="Adriano",
            Sobrenome="Silva",
            DataNascimento="01/01/1985",
            ProfessorId=1,
          },
          new Aluno(){
            Id=2,
            Nome="Bruno",
             Sobrenome="Santos",
            DataNascimento="01/01/1985",
            ProfessorId=3,
          },
          new Aluno(){
            Id=3,
            Nome="Florinda",
             Sobrenome="Gomes",
            DataNascimento="01/01/1985",
            ProfessorId=2,
          }
        }
      );

    }

  }
}