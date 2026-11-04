namespace SimuladoIronFit.Api.Models.DTOs
{
    public class InfoAgendamentoDto
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomePersonal { get; set; }
        public string Cpfpersonal { get; set; }
        public string NomeAluno { get; set; }

        public string CpfAluno { get; set; }

      

    }
}
