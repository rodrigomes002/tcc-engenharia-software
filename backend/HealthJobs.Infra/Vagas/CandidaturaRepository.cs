using HealthJobs.Domain.Vagas.Interface;
using HealthJobs.Infra.Context;

namespace HealthJobs.Infra.Vagas
{
    public class CandidaturaRepository : ICandidaturaRepository
    {
        private readonly ApplicationContext context;

        public CandidaturaRepository(ApplicationContext context)
        {
            this.context = context;
        }
    }
}
