using System;
using Xunit;
using publiquejas;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XUnitTestProject
{
    public class UnitTestPublicaciones
    {
        internal string GeneradorDeCadenas(int longitud)
        {
            const string alfabeto = "0123456789";
            StringBuilder token = new StringBuilder();
            Random randon = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = randon.Next(alfabeto.Length);
                token.Append(alfabeto[indice]);
            }

            return token.ToString();
        }

        internal AdministradorDePublicaciones CrearCiudadanos(AdministradorDePublicaciones admin, uint startIndex = 0, uint endIndex = 1)
        {
            var modeloCiudadanos = Utilitarios.ObtenerListaModeloCiudadano();
            admin = Utilitarios.GeneradorCiudadanos(admin, modeloCiudadanos, startIndex, endIndex);
            
            return admin;
        }

        internal AdministradorDePublicaciones CrearCiudadanos(AdministradorDePublicaciones admin, string csvPath, uint startIndex = 0, uint endIndex = 1)
        {
            var modeloCiudadanos = Utilitarios.ObtenerListaModeloCiudadano(csvPath);
            admin = Utilitarios.GeneradorCiudadanos(admin, modeloCiudadanos, startIndex, endIndex);

            return admin;
        }

        internal AdministradorDePublicaciones CrearCategorias(AdministradorDePublicaciones admin, uint numeroDeCategorias = 1)
        {
            for (int i = 0; i < numeroDeCategorias; i++)
            {
                string nombreCategoria = $"Cat:{GeneradorDeCadenas(5)}";

                admin.AgregarCategoria(nombreCategoria);
            }
            
            return admin;
        }

        internal AdministradorDePublicaciones CrearPublicaciones(AdministradorDePublicaciones admin, int numeroDePublicaciones = 1)
        {
            Random randon = new Random();

            for (int i = 0; i < numeroDePublicaciones; i++)
            {
                string randomValue = GeneradorDeCadenas(4);
                string titulo = $"Titulo{randomValue}";
                string contenido = $"Contenido{randomValue}";

                int indexCiudadano = randon.Next(admin.Ciudadanos.Count);
                int indexCategoria = randon.Next(admin.Categorias.Count);

                admin.AgregarPublicacion(admin.Ciudadanos[indexCiudadano].UserName, titulo, contenido, admin.Categorias[indexCategoria].Nombre);
            }

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
        public void AgregarCiudadanoConNombreDeUsuarioRepetido()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AgregarCiudadano("userNameRepetido", "Nombre", "Apellido", DateTime.Now, "lugar");
            var excepcion = Assert.Throws<NombreDeUsuarioDuplicado>(() => administrador.AgregarCiudadano("userNameRepetido", "Nombre1", "Apellido1", DateTime.Now, "lugar2"));
            Assert.Equal(1, administrador.Ciudadanos.Count);
            Assert.Equal("Nombre Apellido", administrador.Ciudadanos[0].NombreCompleto);
            Assert.Equal("El nombre de usuario userNameRepetido ya existe", excepcion.Message);
        }

        // AgregarCiudadanoConMenosDe18A?os.

        // ActualizarLugarDeCiudadano.

        // EliminarCiudadanoYAnonimizarElCiudadanoEnLasPublicacionesCategoriasComentariosCreadas.
        // En realidad seria eliminar datos personales del usuario y reemplando su nombre de usuario por uno generico.

        // EliminarCiudadanoNoExistente.

        [Fact]
        public void AgregarPublicacion()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador.AgregarPublicacion(administrador.Ciudadanos[0].UserName, "Titulo", "Contenido", administrador.Categorias[0].Nombre);
            Assert.True(administrador.Publicaciones.Count > 0, "la lista de publicaciones esta vacia");
        }

        [Fact]
        public void AgregarMultiplesPublicaciones()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador, 5, 10);
            administrador = CrearCategorias(administrador, 2);
            administrador = CrearPublicaciones(administrador, 15);
            Assert.True(administrador.Publicaciones.Count > 0, "la lista de publicaciones esta vacia");
        }

        [Fact]
        public void BuscarPublicacionesPorCategoria()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);
            var categoriaABuscar = administrador.Categorias.First().Nombre;
            var publicacionesEnCategoria = administrador.Categorias.First().Publicaciones;
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoCategoria<Publicacion>(categoriaABuscar)
            };
            var publicacionesEncontradas = administrador.BuscarPublicacion(terminosDeBusqueda);
            Assert.Equal(publicacionesEnCategoria.Count, publicacionesEncontradas.Count);

            publicacionesEncontradas.ForEach((publicacion) =>
            {
                Assert.Equal(categoriaABuscar, publicacion.Categoria.Nombre);
            });
        }

        [Fact]
        public void BuscarPublicacionesPorCategoriaNoExistente()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>();
            terminosDeBusqueda.Add(new TerminoCategoria<Publicacion>("CategoriaNoExistente"));
            var publicacionesEncontradas = administrador.BuscarPublicacion(terminosDeBusqueda);
            Assert.Empty(publicacionesEncontradas);
        }

        // ***MODIFICAR LAS PUBLICACIONES DE TAL FORMA QUE NO USEN CATEGORIAS COMO TEXTO, DEBEN USAR CATEGORIAS COMO OBJETOS***

        // ModificarTituloY/OContenidoDePublicacionQueAunNoFueRankeadaOComentada.

        // EliminarPublicacionesQueNoTienenComentariosYNoEstanEnUnRanking.

        // AgregarOQuitarCategoriasEnPublicacionesYaCreadas.

        // Votar a favor o en contra de publicaciones, la votacion debe ser realizada por un ciudadano valido.

        [Fact]
        public void BuscarCiudadanosPorNombreDeUsuario()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            var ciudadanoAEncontrar = administrador.Ciudadanos.Last();
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Ciudadano>>
            {
                new TerminoTexto<Ciudadano>("UserName", ciudadanoAEncontrar.UserName)
            };
            var ciudadanosEncontrados = administrador.BuscarCiudadanos(terminosDeBusqueda);
            Assert.Single(ciudadanosEncontrados);
            Assert.Equal(ciudadanoAEncontrar.UserName, ciudadanosEncontrados.First().UserName);
        }

        [Fact]
        public void BuscarCiudadanosPorTerminoDeBusquedaInvalido()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Ciudadano>>
            {
                new TerminoTexto<Ciudadano>("CriterioNoValido", "userName2")
            };
            var ciudadanosEncontrados = administrador.BuscarCiudadanos(terminosDeBusqueda);
            Assert.Empty(ciudadanosEncontrados);
        }
    }

    // AgregarComentarios.

    // AgregarComentarioAPublicacionNoExistente.

    // EliminarComentarios.

    // ActualizarComentarios.

    // AgregarCategorias.

    // ModificarCategorias.

    // EliminarCategorias.
}
