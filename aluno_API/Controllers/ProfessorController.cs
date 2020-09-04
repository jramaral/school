using System.Threading.Tasks;
using aluno_API.Data;
using aluno_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aluno_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProfessorController : Controller
  {
    public IRepository _repo { get; }
    public ProfessorController(IRepository repo)
    {
      this._repo = repo;

    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var result = await _repo.GetAllProfessoresAsync(true);
        return Ok(result);
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
    }
    [HttpGet("{ProfessorId}")]
    public async Task<IActionResult> GetByIdProfessor(int ProfessorId)
    {
      try
      {
        var result = await _repo.GetProfessorAsyncById(ProfessorId, true);
        return Ok(result);
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
    }
    [HttpPost]
    public async Task<IActionResult> Post(Professor model)
    {
      try
      {
        _repo.Add(model);

        if (await _repo.SaveChangeAsync())
        {
          return Created($"/api/professor/{model.Id}", model);
        }
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
      return BadRequest();
    }
    [HttpPut("{ProfessorId}")]
    public async Task<IActionResult> Put(int ProfessorId, Professor model)
    {
      try
      {
        var professor = await _repo.GetProfessorAsyncById(ProfessorId, false);

        if (professor == null) return NotFound();
        _repo.Update(model);

        if (await _repo.SaveChangeAsync())
        {
          professor = await _repo.GetProfessorAsyncById(ProfessorId, true);
          return Created($"/api/professor/{model.Id}", model);
        }
      }
      catch (System.Exception)
      {

        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
      }
      return BadRequest();

    }
    [HttpDelete("{ProfessorId}")]
    public async Task<IActionResult> Delete(int ProfessorId)
    {
      try
      {
        var professor = await _repo.GetProfessorAsyncById(ProfessorId, false);

        if (professor == null) return NotFound();
        _repo.Delete(professor);

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