using HealthJobs.Application.Vagas.Commands;
using HealthJobs.Application.Vagas.DTOs;
using HealthJobs.Domain.Vagas;
using HealthJobs.Domain.Vagas.Filtro;
using HealthJobs.Domain.Vagas.Interface;
using HealthJobs.Infra.UoW;

namespace HealthJobs.Application.Vagas.Handlers
{
    public class VagaService
    {
        private readonly IVagaRepository _vagaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public VagaService(IVagaRepository vagaRepository, IUnitOfWork unitOfWork)
        {
            this._vagaRepository = vagaRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Vaga>> Listar()
        {
            return await _vagaRepository.ListarAsync();
        }

        public async Task<VagaResult> ListarPorFiltro(VagaFiltro filtro)
        {
            var result = new VagaResult();
            result.Vagas = await _vagaRepository.ListarPorFiltroAsync(filtro); ;
            result.Count = await _vagaRepository.Count(filtro);
            return result;
        }

        public async Task<Filtros> ListarFiltros()
        {
            var filtros = new Filtros();
            var cargos = await _vagaRepository.ListarCargosAsync();
            var locais = await _vagaRepository.ListarLocaisAsync();

            filtros.Cargos = cargos;
            filtros.Locais = locais;

            return filtros;
        }

        public async Task Cadastrar(CadastrarVagaDTO request)
        {
            var vaga = new Vaga(request.Empresa, request.Cargo, request.Salario, request.Local, request.Descricao);

            await _vagaRepository.CadastrarAsync(vaga);
            await _unitOfWork.CommitAsync();
        }

        public async Task Candidatar(CandidaturaDTO request)
        {
            var vaga = await _vagaRepository.ListarPorIdAsync(request.Id);
            var candidatura = new Candidatura(vaga, request.Candidato);
            vaga.InserirCandidatura(candidatura);

            _vagaRepository.Atualizar(vaga);
            await _unitOfWork.CommitAsync();
        }
    }
}
