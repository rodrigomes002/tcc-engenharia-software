namespace HealthJobs.Domain.Vagas
{
    public class Candidatura
    {
        public int Id { get; private set; }
        public Vaga Vaga { get; private set; }
        public string Candidato { get; private set; }

        private Candidatura() { }

        public Candidatura(Vaga vaga, string candidato)
        {
            if (String.IsNullOrEmpty(candidato)) throw new ArgumentException("Candidato inválido!");

            Vaga = vaga;
            Candidato = candidato;
        }
    }
}
