using System;
using Xunit;
using publiquejas;
using System.Collections.Generic;
using System.Linq;

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

        internal AdministradorDePublicaciones CrearPublicaciones(AdministradorDePublicaciones admin, int cantidad)
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

        internal List<string> VotarPorPublicaciones(AdministradorDePublicaciones admin)
        {
            Random _random = new Random();
            SortedDictionary<int, string> resultado = new SortedDictionary<int, string>();
            foreach(var publicacion in admin.Publicaciones) {
                int numeroLikes = _random.Next(1, 100);
                publicacion.Likes = numeroLikes;
                resultado.Add(numeroLikes, publicacion.Titulo);
            }
            return resultado.Values.ToList();
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
            administrador = CrearPublicaciones(administrador, 10);
            List<string> ResultadoVotacion = VotarPorPublicaciones(administrador);
            administrador.GenerarRanking(["Likes"]);
            Assert.True(administrador.Rankings.Count > 0, "La lista de rankings esta vacia");
            Assert.Equal(ResultadoVotacion, administrador.Rankings[0].Publicaciones);
        }

    }

}
