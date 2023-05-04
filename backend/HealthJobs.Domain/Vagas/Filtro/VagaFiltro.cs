namespace HealthJobs.Domain.Vagas.Filtro
{
    public class VagaFiltro : PageFiltro
    {
        public List<string> Especialidades { get; set; } = new List<string>();
        public List<string> Locais { get; set; } = new List<string>();
    }
}
