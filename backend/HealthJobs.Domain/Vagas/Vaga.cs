namespace HealthJobs.Domain.Vagas
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Empresa { get; private set; }
        public string Cargo { get; private set; }
        public int Salario { get; private set; }
        public string Descricao { get; private set; }
        public string Local { get; private set; }
        public List<Candidatura> Candidaturas { get; private set; } = new List<Candidatura>();

        public Vaga(string empresa, string cargo, int salario, string local, string descricao)
        {
            if (String.IsNullOrEmpty(empresa)) throw new ArgumentException("Empresa inválida!");
            if (String.IsNullOrEmpty(cargo)) throw new ArgumentException("Cargo inválido!");
            if (String.IsNullOrEmpty(local)) throw new ArgumentException("Local inválido!");
            if (String.IsNullOrEmpty(descricao)) throw new ArgumentException("Descrição inválida!");

            this.Empresa = empresa;
            this.Cargo = cargo;
            this.Salario = salario;
            this.Local = local;
            this.Descricao = descricao;
        }

        public void InserirCandidatura(Candidatura candidatura)
        {
            if (candidatura is null) throw new ArgumentException("Candidatura inválida!");

            Candidaturas.Add(candidatura);
        }
    }
}