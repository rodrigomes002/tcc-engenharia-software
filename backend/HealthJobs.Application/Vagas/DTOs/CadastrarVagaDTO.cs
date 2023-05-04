namespace HealthJobs.Application.Vagas.Commands
{
    public class CadastrarVagaDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public int Salario { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
    }
}
