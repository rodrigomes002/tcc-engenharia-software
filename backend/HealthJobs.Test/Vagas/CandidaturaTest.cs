using ExpectedObjects;
using HealthJobs.Domain.Vagas;

namespace HealthJobs.Test.Vagas
{
    public class CandidaturaTest
    {
        [Fact]
        public void DeveCriarCandidatura()
        {
            var vaga = new Vaga("Clinica de saúde", "Fisioterapeuta", 10000, "Rio de Janeiro", "Uma descrição");

            var candidaturaEsperada = new
            {
                Vaga = vaga,
                Candidato = "Lara"
            };

            var candidatura = new Candidatura(candidaturaEsperada.Vaga, candidaturaEsperada.Candidato);

            candidaturaEsperada.ToExpectedObject().ShouldMatch(candidatura);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarCandidaturaComCandidatoInvalido(string candidato)
        {
            var vaga = new Vaga("Clinica de saúde", "Fisioterapeuta", 10000, "Rio de Janeiro", "Uma descrição");

            var candidaturaEsperada = new
            {
                Vaga = vaga,
                Candidato = candidato
            };

            var ex = Assert.Throws<ArgumentException>(() => new Candidatura(candidaturaEsperada.Vaga, candidaturaEsperada.Candidato));

            Assert.Equal("Candidato inválido!", ex.Message);
        }
    }
}
