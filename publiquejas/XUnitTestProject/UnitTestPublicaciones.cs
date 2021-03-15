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
            administrador.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Today, "lugar");
            Assert.True(administrador.Ciudadanos.Count > 0, "La lista de ciudadanos esta vacia");
            Assert.Equal("Nombre Apellido", administrador.Ciudadanos[0].NombreCompleto);
            Assert.Equal(DateTime.Today, administrador.Ciudadanos[0].FechaDeNacimiento);
        }

        [Fact]
        public void AgregarPublicacion()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadano(administrador);
            administrador = CrearCategoria(administrador);
            administrador.AgregarPublicacion("username1", "Titulo", "Contenido", "NombreCategoria");
            //Assert.True(administrador.Publicaciones.Count > 0, "la lista de publicaciones esta vacia");
            //Assert.Equal("Titulo", administrador.Publicaciones[0].Titulo);
            //true); //Modificar este assert para realmente verificar si se ha agregado esta publicacion.
        }
    }
}
