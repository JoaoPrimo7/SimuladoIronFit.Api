using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using SimuladoIronFit.Api.Models.DTOs;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/agendamentos
        [HttpPost]
        public async Task<ActionResult<Agendamento>> CreateAgendamento(CriarAgendamentoDto agendamentoDto)
        {
            var agendamento = new Agendamento
            {
                DataHora = agendamentoDto.DataHora,
                PersonalId = agendamentoDto.PersonalId,
                AlunoId = agendamentoDto.AlunoId,
            };

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateAgendamento), new { id = agendamento.Id }, agendamento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoAgendamentoDto>>> GetAgendamentos()
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.Personal)
                .ThenInclude(p => p.Aluno)
                .Select(a => new InfoAgendamentoDto
                {
                    Id = a.Id,
                    DataHora = a.DataHora,
                    NomePersonal = a.Personal.Nome,
                    Cpfpersonal = a.Aluno.Cpf,
                    NomeAluno = a.Aluno.Nome,
                  
                })
                .ToListAsync();

            return Ok(agendamentos);
        }

        // GET: api/agendamentos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoAgendamentoDto>> GetAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Personal)
                .ThenInclude(p => p.Aluno)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }
            var agendamentoDto = new InfoAgendamentoDto
            {
                DataHora = agendamento.DataHora,
                NomePersonal = agendamento.Personal.Nome,
                Cpfpersonal = agendamento.Personal.Cpf,
                NomeAluno = agendamento.Aluno.Nome,
                CpfAluno = agendamento.Aluno.Cpf
            };
            return Ok(agendamentoDto);
        }

        //PUT: api/agendamentos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgendamento(int id, CriarAgendamentoDto agendamentoDto)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            agendamento.DataHora = agendamentoDto.DataHora;
            agendamento.PersonalId = agendamentoDto.PersonalId;
            agendamento.AlunoId = agendamentoDto.AlunoId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/agendamentos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}