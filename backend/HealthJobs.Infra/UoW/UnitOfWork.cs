using HealthJobs.Infra.Context;

namespace HealthJobs.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext applicationContext;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task CommitAsync()
        {
           await this.applicationContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await this.applicationContext.DisposeAsync();
        }
    }
}
