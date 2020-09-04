using System.Threading.Tasks;
using aluno_API.Data;
using aluno_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aluno_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AlunoController : Controller
  {
    public IRepository _repo { get; }
    public AlunoController(IRepository repo)
    {
      this._repo = repo;

    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var result = await _repo.GetAllAlunosAsync(true);
        return Ok(result);
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
    }
    [HttpGet("{AlunoId}")]
    public async Task<IActionResult> Get(int AlunoId)
    {
      try
      {
        var result = await _repo.GetAlunoAsyncById(AlunoId, true);
        return Ok(result);

      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
    }


    [HttpGet("ByProfessor/{ProfessorId}")]
    public async Task<IActionResult> GetByProfessorId(int ProfessorId)
    {
      try
      {
        var result = await _repo.GetAlunosAsyncByProfessorId(ProfessorId, true);
        return Ok(result);

      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Aluno model)
    {
      try
      {
        _repo.Add(model);

        if (await _repo.SaveChangeAsync())
        {
          return Created($"/api/aluno/{model.Id}", model);
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
      return BadRequest();
    }


    [HttpPut("{AlunoId}")]
    public async Task<IActionResult> Put(int AlunoId, Aluno model)
    {
      try
      {
        var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);

        if (aluno == null) return NotFound();
        _repo.Update(model);

        if (await _repo.SaveChangeAsync())
        {
          aluno = await _repo.GetAlunoAsyncById(AlunoId, true);
          return Created($"/api/aluno/{model.Id}", model);
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
      return BadRequest();
    }
    [HttpDelete("{AlunoId}")]
    public async Task<IActionResult> Delete(int AlunoId)
    {
      try
      {
        var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);

        if (aluno == null) return NotFound();
        _repo.Delete(aluno);

        if (await _repo.SaveChangeAsync())
        {
          return Ok();
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
      return BadRequest();
    }
  }
}