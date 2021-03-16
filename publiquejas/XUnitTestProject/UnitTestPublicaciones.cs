using System;
using Xunit;
using publiquejas;

namespace XUnitTestProject
{
    public class UnitTestPublicaciones
    {

        internal AdministradorDePublicaciones CrearCiudadanos(AdministradorDePublicaciones admin)
        {
            admin.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            admin.AgregarCiudadano("userName2", "Nombre2", "Apellido2", DateTime.Now, "lugar2");
            admin.AgregarCiudadano("userName3", "Nombre3", "Apellido3", DateTime.Now, "lugar3");
            return admin;
        }

        internal AdministradorDePublicaciones CrearCategorias(AdministradorDePublicaciones admin)
        {
            admin.AgregarCategoria("Nombre");
            admin.AgregarCategoria("Nombre2");
            admin.AgregarCategoria("Nombre3");
            return admin;
        }

        internal AdministradorDePublicaciones CrearPublicaciones(AdministradorDePublicaciones admin)
        {
            admin.AgregarPublicacion("userName", "Titulo", "Contenido", "Categoria");
            admin.AgregarPublicacion("userName", "Titulo2", "Contenido2", "Categoria2");
            admin.AgregarPublicacion("userName", "Titulo3", "Contenido3", "Categoria3");
            admin.AgregarPublicacion("userName", "Titulo4", "Contenido4", "Categoria4");
            admin.AgregarPublicacion("userName", "Titulo5", "Contenido5", "Categoria5");

            admin.AgregarPublicacion("userName2", "Titulo", "Contenido", "Categoria");
            admin.AgregarPublicacion("userName2", "Titulo2", "Contenido2", "Categoria2");
            admin.AgregarPublicacion("userName2", "Titulo3", "Contenido3", "Categoria3");

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
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador.AgregarPublicacion("userName", "Titulo", "Contenido", "Nombre");
            Assert.True(administrador.Publicaciones.Count > 0, "la lista de publicaciones esta vacia");
        }

        [Fact]
        public void AgregarRanking()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearPublicaciones(administrador);

        }

    }

}
