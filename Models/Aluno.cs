namespace SimuladoIronFit.Api.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public ICollection<Personal> Personais { get; set; } = new List<Personal>();
    }
}
