namespace ForestGuardian.Domain.Interfaces.Infra;

public interface IUnitOfWork
{
    public IOcorrenciaRepository OcorrenciaRepository { get; }
}