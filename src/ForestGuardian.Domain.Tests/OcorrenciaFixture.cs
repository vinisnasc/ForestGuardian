using Bogus;
using ForestGuardian.Domain.Entities;

namespace ForestGuardian.Domain.Tests;

[CollectionDefinition(nameof(OcorrenciaCollection))]
public class OcorrenciaCollection : ICollectionFixture<OcorrenciaFixture>
{}

public class OcorrenciaFixture : IDisposable
{
    public Ocorrencia GerarOcorrenciaValida()
    {
        var ocorrencia = new Faker<Ocorrencia>("pt_BR").CustomInstantiator(f => new Ocorrencia(
            f.Lorem.Sentence(5),
            f.Lorem.Letter(500),
            f.Random.Double(-90, 90),
            f.Random.Double(-180, 180),
            Guid.NewGuid()
        ));

        return ocorrencia;
    }

    public string GerarTituloValido()
    {
        return new Faker().Lorem.Sentence(5);
    }

    public string GerarDescricaoValida()
    {
        return new Faker().Lorem.Letter(500);
    }

    public double GerarLatitudeValida()
    {
        return new Faker().Random.Double(-90, 90);
    }

    public double GerarLongitudeValida()
    {
        return new Faker().Random.Double(-180, 180);
    }

    public void Dispose() { }
}