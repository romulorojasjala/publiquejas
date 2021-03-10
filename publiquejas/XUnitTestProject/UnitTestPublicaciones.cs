using System;
using Xunit;
using publiquejas;

namespace XUnitTestProject
{
    public class UnitTestPublicaciones
    {
        [Fact]
        public void AgregarCiudadano()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AgregarCiudadano("Nombre", "Apellido", DateTime.Now, "lugar");

            Assert.True(administrador.Ciudadanos.Count > 0, "La lista de ciudadanos esta vacia");
            //Assert.Equal(administrador.Ciudadanos[0].Nombre, "Nombre");
            //Assert.Equal(administrador.Ciudadanos[0].Apellido, "Apellido");
            //Assert.Equal(administrador.Ciudadanos[0].Edad, 30);
            //Assert.Equal(administrador.Ciudadanos[0].Ubicacion, "lugar");
        }
    }
}
