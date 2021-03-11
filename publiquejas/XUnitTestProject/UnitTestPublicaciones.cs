using System;
using Xunit;
using publiquejas;

namespace XUnitTestProject
{
    public class UnitTestPublicaciones
    {
        
        internal AdministradorDePublicaciones CrearCiudadano(AdministradorDePublicaciones admin)
        {
            admin.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            return admin;
        }

        internal AdministradorDePublicaciones CrearCategoria(AdministradorDePublicaciones admin)
        {
            admin.AgregarCategoria("Nombre");
            return admin;
        }

        [Fact]
        public void AgregarCiudadano()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            Assert.True(administrador.Ciudadanos.Count > 0, "La lista de ciudadanos esta vacia");
            Assert.Equal("Nombre Apellido", administrador.Ciudadanos[0].NombreCompleto);
        }

        [Fact]
        public void AgregarPublicacion()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadano(administrador);
            administrador = CrearCategoria(administrador);
            administrador.AgregarPublicacion("username1", "Titulo", "Contenido", "NombreCategoria");
            Assert.True(true); //Modificar este assert para realmente verificar si se ha agregado esta publicacion.
        }
    }
}
