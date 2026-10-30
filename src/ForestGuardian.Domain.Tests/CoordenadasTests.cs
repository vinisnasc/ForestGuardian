using Bogus;
using ForestGuardian.Domain.ValueObjects;

namespace ForestGuardian.Domain.Tests
{
    public class CoordenadasTests
    {
        [Theory(DisplayName = "Coordenadas com latitude inválida devem lançar exceção")]
        [InlineData(-91.0)]
        [InlineData(90.01)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void Coordenadas_LatitudeInvalida_DeveLancarExcecao(double latitudeInvalida)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordenadas(latitudeInvalida, 0.0));

        }

        [Theory(DisplayName = "Coordenadas com longitude inválida devem lançar exceção")]
        [InlineData(-181.0)]
        [InlineData(180.01)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void Coordenadas_LongitudeInvalida_DeveLancarExcecao(double longitudeInvalida)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordenadas(0.0, longitudeInvalida));
        }

        [Fact(DisplayName = "Coordenadas com valores válidos devem criar objeto")]
        public void Coordenadas_ValoresValidos_DeveCriarObjeto()
        {
            // Arrange
            double latitudeValida = new Faker().Random.Double(-90, 90);
            double longitudeValida = new Faker().Random.Double(-180, 180);

            // Act
            var coordenadas = new Coordenadas(latitudeValida, longitudeValida);

            // Assert
            Assert.Equal(latitudeValida, coordenadas.Latitude);
            Assert.Equal(longitudeValida, coordenadas.Longitude);
            Assert.IsType<Coordenadas>(coordenadas);
        }

        [Theory]
        [InlineData(-90.0, -180.0)]
        [InlineData(90.0, 180.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(-90.0, 180.0)]
        [InlineData(90.0, -180.0)]
        public void Coordenadas_ValoresLimite_DeveCriarObjeto(double latitude, double longitude)
        {
            // Arrange & Act
            var coordenadas = new Coordenadas(latitude, longitude);

            // Assert
            Assert.Equal(latitude, coordenadas.Latitude);
            Assert.Equal(longitude, coordenadas.Longitude);
            Assert.IsType<Coordenadas>(coordenadas);
        }
    }
}
