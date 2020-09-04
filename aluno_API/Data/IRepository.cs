using System.Threading.Tasks;
using aluno_API.Models;

namespace aluno_API.Data
{
  public interface IRepository
  {
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveChangeAsync();

    //Aluno
    Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor);
    Task<Aluno[]> GetAlunosAsyncByProfessorId(int professorId, bool includeProfessor);
    Task<Aluno> GetAlunoAsyncById(int alunoId, bool includeProfessor);

    //Professor
    Task<Professor[]> GetAllProfessoresAsync(bool includeAluno);

    Task<Professor> GetProfessorAsyncById(int professorId, bool includeAluno);
  }
}