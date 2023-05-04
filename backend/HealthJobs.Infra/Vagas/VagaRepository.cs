using HealthJobs.Domain.Vagas;
using HealthJobs.Domain.Vagas.Filtro;
using HealthJobs.Domain.Vagas.Interface;
using HealthJobs.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthJobs.Infra.Vagas
{
    public class VagaRepository : IVagaRepository
    {
        private readonly ApplicationContext _context;

        public VagaRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public void Atualizar(Vaga vaga)
        {
            _context.Vagas.Update(vaga);
        }

        public async Task CadastrarAsync(Vaga vaga)
        {
            await _context.Vagas.AddAsync(vaga);
        }

        public async Task<int> Count(VagaFiltro filtro)
        {
            return await _context.Vagas
                .Where(v => filtro.Locais.Any() ? filtro.Locais.Contains(v.Local) : !String.IsNullOrEmpty(v.Local))
                .Where(v => filtro.Especialidades.Any() ? filtro.Especialidades.Contains(v.Cargo) : !String.IsNullOrEmpty(v.Cargo))
                .CountAsync();
        }

        public async Task<List<Vaga>> ListarAsync()
        {
            return await _context.Vagas
                .Include(c => c.Candidaturas)
                .ToListAsync();
        }

        public async Task<List<string>> ListarCargosAsync()
        {
            return await _context.Vagas
                .Select(c => c.Cargo)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<string>> ListarLocaisAsync()
        {
            return await _context.Vagas
                .Select(c => c.Local)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Vaga>> ListarPorFiltroAsync(VagaFiltro filtro)
        {
            var dados = await _context.Vagas
                .Where(v => filtro.Locais.Any() ? filtro.Locais.Contains(v.Local) : !String.IsNullOrEmpty(v.Local))
                .Where(v => filtro.Especialidades.Any() ? filtro.Especialidades.Contains(v.Cargo) : !String.IsNullOrEmpty(v.Cargo))
                .Include(c => c.Candidaturas)
                .Skip((filtro.Page - 1) * filtro.PageSize).Take(filtro.PageSize)
                .ToListAsync();

            return dados;
        }

        public async Task<Vaga> ListarPorIdAsync(int id)
        {
            return await _context.Vagas.Where(v => v.Id == id).FirstOrDefaultAsync();
        }
    }
}
