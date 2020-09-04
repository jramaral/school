using System.Linq;
using System.Threading.Tasks;
using aluno_API.Models;
using Microsoft.EntityFrameworkCore;

namespace aluno_API.Data
{
  public class Repository : IRepository
  {
    public DataContext _context { get; }
    public Repository(DataContext context)
    {
      this._context = context;
    }

    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }
    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public async Task<bool> SaveChangeAsync()
    {
      return (await _context.SaveChangesAsync() > 0);
    }

    //Aluno
    public async Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeProfessor)
      {
        query = query.Include(a => a.Professor);
      }

      query = query
                .AsNoTracking()
                .OrderBy(a => a.Id);

      return await query.ToArrayAsync();
    }

    public async Task<Aluno[]> GetAlunosAsyncByProfessorId(int professorId, bool includeProfessor = false)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeProfessor)
      {
        query = query.Include(a => a.Professor);
      }

      query = query
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(aluno => aluno.ProfessorId == professorId);

      return await query.ToArrayAsync();
    }

    public async Task<Aluno> GetAlunoAsyncById(int alunoId, bool includeProfessor = false)
    {
      IQueryable<Aluno> query = _context.Alunos;

      if (includeProfessor)
      {
        query = query.Include(a => a.Professor);
      }

      query = query
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(aluno => aluno.Id == alunoId);


      return await query.FirstOrDefaultAsync();
    }

    //Professores
    public async Task<Professor[]> GetAllProfessoresAsync(bool includeAluno = false)
    {
      IQueryable<Professor> query = _context.Professores;

      if (includeAluno)
      {
        query = query.Include(a => a.Alunos);
      }

      query = query
                .AsNoTracking()
                .OrderBy(p => p.Id);


      return await query.ToArrayAsync();
    }

    public async Task<Professor> GetProfessorAsyncById(int professorId, bool includeAluno = false)
    {
      IQueryable<Professor> query = _context.Professores;

      if (includeAluno)
      {
        query = query.Include(a => a.Alunos);
      }

      query = query
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(prof => prof.Id == professorId);


      return await query.FirstOrDefaultAsync();
    }
  }
}