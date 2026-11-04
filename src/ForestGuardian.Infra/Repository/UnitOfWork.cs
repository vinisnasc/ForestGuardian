using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Infra.Context;

namespace ForestGuardian.Infra.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ForestGuardianContext _context;
    public IOcorrenciaRepository OcorrenciaRepository { get; }

    public UnitOfWork(ForestGuardianContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        OcorrenciaRepository = new OcorrenciaRepository(context);
    }
}