using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using SimuladoIronFit.Api.Models.DTOs;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/personals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personal>>> Getpersonals()
        {
            return await _context.Personais.ToListAsync();
            //return await _context.Personais.Include(v => v.Aluno).Select(v => new PersonalDto
            //{
            //    Nome = v.Nome,
            //    Cpf = v.Cpf,
            //    NomeAluno = v.Aluno.Nome
            //}).ToListAsync();
        }

        // GET: api/personais/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalDto>> GetPersonal(int id)
        {
            var personal = await _context.Personais.Include(p => p.Aluno).FirstOrDefaultAsync(p => p.Id == id);

            if (personal == null)
            {
                return NotFound();
            }

            return new PersonalDto
            {
                Nome = personal.Nome,
                Cpf = personal.Cpf,
                NomeAluno = personal.Aluno.Nome
            };
        }

        // POST: api/personal
        [HttpPost]
        public async Task<ActionResult<Personal>> CreatePersonal(CriarPersonalDto criarPersonalDto)
        {
            var personal = new Personal
            {
                Nome = criarPersonalDto.Nome,
                Cpf = criarPersonalDto.Cpf,
           
            };

            _context.Personais.Add(personal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonal), new { id = personal.Id }, personal);
        }

        // PUT: api/personais/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonal(int id, CriarPersonalDto criarPersonalDto)
        {
            var personal = await _context.Personais.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            personal.Nome = criarPersonalDto.Nome;
            personal.Cpf= criarPersonalDto.Cpf;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/personals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepersonal(int id)
        {
            var personal = await _context.Personais.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            _context.Personais.Remove(personal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}