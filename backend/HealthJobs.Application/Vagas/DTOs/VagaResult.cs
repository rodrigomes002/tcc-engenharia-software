using HealthJobs.Domain.Vagas;

namespace HealthJobs.Application.Vagas.DTOs
{
    public class VagaResult
    {
        public List<Vaga> Vagas { get; set; }
        public int Count { get; set; }
    }
}
