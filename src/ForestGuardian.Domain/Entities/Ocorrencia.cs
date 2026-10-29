using ForestGuardian.Domain.Enums;
using ForestGuardian.Domain.Validations;
using ForestGuardian.Domain.ValueObjects;

namespace ForestGuardian.Domain.Entities
{
    public class Ocorrencia : BaseEntity
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public Coordenadas Coordenadas { get; private set; }
        public StatusOcorrencia StatusOcorrencia { get; private set; }
        public Guid IdUsuarioAberturaOcorrencia { get; private set; }

        public Ocorrencia(string titulo, string descricao, double latitude, double longitude, Guid idUsuarioAberturaOcorrencia)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Descricao = descricao;
            DataRegistro = DateTime.UtcNow;
            Coordenadas = new Coordenadas(latitude, longitude);
            StatusOcorrencia = StatusOcorrencia.Aberta;
            IdUsuarioAberturaOcorrencia = idUsuarioAberturaOcorrencia;

            if (!EhValido())
            {
                var erros = string.Join("; ", ValidationResult.Errors.Select(erro => erro.ErrorMessage));

                throw new ArgumentException(
                    $"Ocorrência inválida: {erros}");
            }
        }

        public override bool EhValido()
        {
            ValidationResult = new OcorrenciaValidacao().Validate(this);
            return ValidationResult.IsValid;
        }

        public void AtualizarStatus(StatusOcorrencia novoStatus)
        {
            if (!Enum.IsDefined(typeof(StatusOcorrencia), novoStatus))
                throw new ArgumentOutOfRangeException(nameof(novoStatus));

            StatusOcorrencia = novoStatus;
        }
    }
}
