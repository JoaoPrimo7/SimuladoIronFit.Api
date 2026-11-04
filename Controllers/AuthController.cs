using Microsoft.AspNetCore.Mvc;
using SimuladoIronFit.Api.Data;
using SimuladoIronFit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace SimuladoIronFit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        // O DbContext é injetado aqui via construtor
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // 1. Busca o usuário no BANCO DE DADOS de forma assíncrona
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequest.Username.ToLower());

            // 2. Verifica se o usuário existe e se a senha está correta
            // ATENÇÃO: Lembre-se que em produção a senha deve ser hasheada!
            if (usuario == null || usuario.Password != loginRequest.Password)
            {
                return Unauthorized("Username ou senha inválidos.");
            }

            // 3. Login bem-sucedido: retorna o nome do usuário
            return Ok(new { Nome = usuario.Nome });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario registerRequest)
        {
            // 1. Verificar se o username já está em uso
            // Usamos AnyAsync para uma verificação mais eficiente (retorna true/false)
            if (await _context.Usuarios.AnyAsync(u => u.Username.ToLower() == registerRequest.Username.ToLower()))
            {
                return BadRequest("Este username já está em uso."); // Retorna 400 Bad Request
            }

            var novoUsuario = new Usuario
            {
                Nome = registerRequest.Nome,
                Username = registerRequest.Username,
                Password = registerRequest.Password
            };

            // 3. Adicionar o novo usuário ao context
            _context.Usuarios.Add(novoUsuario);

            // 4. Salvar as mudanças no banco de dados
            await _context.SaveChangesAsync();

            // 5. Retornar uma resposta de sucesso
            // Retornar o usuário criado (sem a senha) é uma boa prática.
            return CreatedAtAction(nameof(Register), new { id = novoUsuario.Id }, new { novoUsuario.Nome, novoUsuario.Username });
        }
    }
}
   
