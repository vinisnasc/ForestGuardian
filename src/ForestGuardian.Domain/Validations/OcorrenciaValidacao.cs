using FluentValidation;
using ForestGuardian.Domain.Entities;

namespace ForestGuardian.Domain.Validations;

public class OcorrenciaValidacao : AbstractValidator<Ocorrencia>
{
    public OcorrenciaValidacao()
    {
        RuleFor(o => o.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(100).WithMessage("O título não pode exceder 100 caracteres.");
        RuleFor(o => o.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(500).WithMessage("A descrição não pode exceder 500 caracteres.");
        RuleFor(o => o.Coordenadas)
            .NotNull().WithMessage("As coordenadas são obrigatórias.");
        RuleFor(o => o.IdUsuarioAberturaOcorrencia)
            .NotEmpty().WithMessage("O ID do usuário que abriu a ocorrência é obrigatório.")
            .WithMessage("O ID do usuário que abriu a ocorrência é inválido.");
        RuleFor(o => o.StatusOcorrencia)
            .IsInEnum().WithMessage("O status da ocorrência é inválido.");
    }
}