using ForestGuardian.Domain.ValueObjects;

namespace ForestGuardian.Domain.Tests
{
    public class CoordenadasTests
    {
        [Theory]
        [InlineData(-91.0)]
        [InlineData(91.0)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void Coordenadas_LatitudeInvalida_DeveLancarExcecao(double latitudeInvalida)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordenadas(latitudeInvalida, 0.0));

        }

        [Theory]
        [InlineData(-181.0)]
        [InlineData(181.0)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void Coordenadas_LongitudeInvalida_DeveLancarExcecao(double longitudeInvalida)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordenadas(0.0, longitudeInvalida));
        }

        [Fact]
        public void Coordenadas_ValoresValidos_DeveCriarObjeto()
        {
            // Arrange
            double latitudeValida = -90.0;
            double longitudeValida = 180.0;

            // Act
            var coordenadas = new Coordenadas(latitudeValida, longitudeValida);

            // Assert
            Assert.Equal(latitudeValida, coordenadas.Latitude);
            Assert.Equal(longitudeValida, coordenadas.Longitude);
            Assert.IsType<Coordenadas>(coordenadas);
        }
    }
}
