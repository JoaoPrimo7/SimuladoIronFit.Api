namespace SimuladoIronFit.Api.Models
{
    public class Personal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
    }
}
