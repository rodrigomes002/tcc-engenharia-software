namespace HealthJobs.Infra.UoW
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task DisposeAsync();
    }
}
