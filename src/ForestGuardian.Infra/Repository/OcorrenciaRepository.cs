using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Infra.Context;

namespace ForestGuardian.Infra.Repository;

public class OcorrenciaRepository : BaseRepository<Ocorrencia>, IOcorrenciaRepository
{
    public OcorrenciaRepository(ForestGuardianContext context) : base(context) { }
}