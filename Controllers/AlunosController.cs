using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using SimuladoIronFit.Api.Models.DTOs;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
        {
            return await _context.Alunos.Select(a => new AlunoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Cpf = a.Cpf
            }).ToListAsync();
        }

        // GET: api/Alunos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDto>> GetAluno(int id)
        {
            var Aluno = await _context.Alunos.FindAsync(id);

            if (Aluno == null)
            {
                return NotFound();
            }

            return new AlunoDto
            {
                Nome = Aluno.Nome,
                Cpf = Aluno.Cpf
            };

        }

        // POST: api/Alunos
        [HttpPost]
        public async Task<ActionResult<AlunoDto>> CreateAluno(AlunoDto AlunoDto)
        {
            var Aluno = new Aluno
            {
                Nome = AlunoDto.Nome,
                Cpf = AlunoDto.Cpf
            };

            _context.Alunos.Add(Aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = Aluno.Id }, AlunoDto);
        }

        //PUT: api/Alunos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAluno(int id, AlunoDto AlunoDto)
        {
            var Aluno = await _context.Alunos.FindAsync(id);
            if (Aluno == null)
            {
                return NotFound();
            }
            Aluno.Nome = AlunoDto.Nome;
            Aluno.Cpf = AlunoDto.Cpf;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Alunos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var Aluno = await _context.Alunos.FindAsync(id);
            if (Aluno == null)
            {
                return NotFound();
            }
            _context.Alunos.Remove(Aluno);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}