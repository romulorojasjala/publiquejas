using System;
using Xunit;
using publiquejas;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using publiquejas.Votos;
using publiquejas.Exceptions;
using publiquejas.Excepciones;

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

                int indexCiudadano = randon.Next(admin.AdminDeUsuarios.ContarCiudadanos());
                int indexCategoria = randon.Next(admin.Categorias.Count);

                admin.AgregarPublicacion(admin.AdminDeUsuarios.GetCiudadano(indexCiudadano).UserName, titulo, contenido, admin.Categorias[indexCategoria].Nombre);
            }

            return admin;
        }    


        [Fact]
        public void AgregarCiudadano()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AdminDeUsuarios.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            Assert.True(administrador.AdminDeUsuarios.ContarCiudadanos() > 0, "La lista de ciudadanos esta vacia");
            Assert.Equal("Nombre Apellido", administrador.AdminDeUsuarios.GetCiudadano(0).NombreCompleto);
        }

        [Fact]
        public void AgregarCiudadanoConNombreDeUsuarioRepetido()
        {
            string nombreDeUsuario = "userNameRepetido";
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AdminDeUsuarios.AgregarCiudadano(nombreDeUsuario, "Nombre", "Apellido", DateTime.Now, "lugar");
            NombreDeUsuarioDuplicado excepcion = Assert.Throws<NombreDeUsuarioDuplicado>(() =>
                administrador.AdminDeUsuarios.AgregarCiudadano(nombreDeUsuario, "Nombre1", "Apellido1", DateTime.Now, "lugar2"));
            Assert.Equal(1, administrador.AdminDeUsuarios.ContarCiudadanos());
            Assert.Equal("Nombre Apellido", administrador.AdminDeUsuarios.GetCiudadano(0).NombreCompleto);
            Assert.Equal(NombreDeUsuarioDuplicado.MensajeDeError, excepcion.Message);
            Assert.Equal(nombreDeUsuario, excepcion.NombreDeUsuario);
        }
        // AgregarCiudadanoConNombreDeUsuarioRepetido. Daniela

        // AgregarCiudadanoConMenosDe18A�os. Carlos 

        // ActualizarLugarDeCiudadano. Carlos
        [Fact]
        public void ActualizarLugarDeCiudadano()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AdminDeUsuarios.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            Assert.True(administrador.AdminDeUsuarios.ContarCiudadanos() > 0, "La lista de ciudadanos esta vacia");
            Assert.Equal("Nombre Apellido", administrador.AdminDeUsuarios.GetCiudadano(0).NombreCompleto);
            
            administrador.AdminDeUsuarios.ActualizarUbicacionCiudadano("userName", "newLugar");

            var terminosDeBusqueda = new List<TerminoDeBusqueda<Ciudadano>>
            {
                new TerminoTexto<Ciudadano>("UserName", "userName")
            };

            var ciudadanosEncontrados = administrador.AdminDeUsuarios.BuscarCiudadanos(terminosDeBusqueda);
            Assert.Single(ciudadanosEncontrados);
            Assert.Equal("newLugar", ciudadanosEncontrados.First().Ubicacion);
        }

        [Fact]
        public void ActualizarLugarDeCiudadanoConUserNameInvalido()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AdminDeUsuarios.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            Assert.True(administrador.AdminDeUsuarios.ContarCiudadanos() > 0, "La lista de ciudadanos esta vacia");
            Assert.Equal("Nombre Apellido", administrador.AdminDeUsuarios.GetCiudadano(0).NombreCompleto);

        // AgregarCiudadanoConMenosDe18A?os.
            ActualizacionUbicacionUserNameCiudadanoException exception = Assert.Throws<ActualizacionUbicacionUserNameCiudadanoException>(() => administrador.AdminDeUsuarios.ActualizarUbicacionCiudadano("userNameError", "newLugar"));
            Assert.Equal(ActualizacionUbicacionUserNameCiudadanoException.GetMessage, exception.Message);

        }

        [Fact]
        public void ActualizarLugarDeCiudadanoConNuevaUbicacionInvalida()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AdminDeUsuarios.AgregarCiudadano("userName", "Nombre", "Apellido", DateTime.Now, "lugar");
            Assert.True(administrador.AdminDeUsuarios.ContarCiudadanos() > 0, "La lista de ciudadanos esta vacia");
            Assert.Equal("Nombre Apellido", administrador.AdminDeUsuarios.GetCiudadano(0).NombreCompleto);

            ActualizacionUbicacionNuevaUbicacionException exception = Assert.Throws<ActualizacionUbicacionNuevaUbicacionException>(() => administrador.AdminDeUsuarios.ActualizarUbicacionCiudadano("userName", ""));
            Assert.Equal(ActualizacionUbicacionNuevaUbicacionException.GetMessage, exception.Message);
        }

        // EliminarCiudadanoYAnonimizarElCiudadanoEnLasPublicacionesCategoriasComentariosCreadas. Maria
        // En realidad seria eliminar datos personales del usuario y reemplando su nombre de usuario por uno generico.
        [Fact]
        public void EliminarCiudadanoYAnonimizarlo()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador.AdminDeUsuarios.AgregarCiudadano("username", "Nombre", "Apellido", DateTime.Now, "lugar");
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            administrador.AdminDeUsuarios.EliminarCiudadano("username");
            Assert.False(administrador.AdminDeUsuarios.ExisteCiudadano("username"), "El ciudadano sigue existiendo");
        }
        
        [Fact]
        public void EliminarCiudadanoInexistente()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador.AgregarPublicacion(administrador.AdminDeUsuarios.GetCiudadano(0).UserName, "Titulo", "Contenido", administrador.Categorias[0].Nombre);
            
            Assert.Throws<CiudadanoInexistente>( () => administrador.AdminDeUsuarios.EliminarCiudadano("usernameInexistente"));
        }

        // EliminarCiudadanoNoExistente.

        [Fact]
        public void AgregarPublicacion()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador.AgregarPublicacion(administrador.AdminDeUsuarios.GetCiudadano(0).UserName, "Titulo", "Contenido", administrador.Categorias[0].Nombre);
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
                Assert.Equal(categoriaABuscar, publicacion.Categorias[0].Nombre);
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

        // ***MODIFICAR LAS PUBLICACIONES DE TAL FORMA QUE NO USEN CATEGORIAS COMO TEXTO, DEBEN USAR CATEGORIAS COMO OBJETOS*** Erick

        // ModificarTituloY/OContenidoDePublicacionQueAunNoFueRankeadaOComentada.

        [Fact]
        public void ModificarTituloYOContenidoDePublicacionQueAunNoFueComentada()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            string titulo = "Titulo";
            administrador.AgregarPublicacion(administrador.AdminDeUsuarios.GetCiudadano(0).UserName, titulo, "Contenido", administrador.Categorias[0].Nombre);
            string nuevoTitulo = "Nuevo Titulo";
            string nuevoContenido = "Nuevo Contenido";
            Ciudadano ciudadanoAutorizado = administrador.AdminDeUsuarios.GetCiudadano(0);
            DatosEditablesPublicacion nuevosDatosPublicacion = new DatosEditablesPublicacion()
            {
                Titulo = nuevoTitulo,
                Contenido = nuevoContenido,
            };
            administrador.ActualizarPublicacion(titulo, nuevosDatosPublicacion, ciudadanoAutorizado);
            Assert.True(administrador.Publicaciones[0].Titulo == nuevoTitulo, "el titulo de la publicacion no se actualizo");
            Assert.True(administrador.Publicaciones[0].Contenido == nuevoContenido, "el contenido de la publicacion no se actualizo");
        }

        // EliminarPublicacionesQueNoTienenComentariosYNoEstanEnUnRanking.

        [Fact]
        public void EliminarPublicacionesQueNoTienenComentariosYNoTienenVotos()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador.AgregarPublicacion(administrador.AdminDeUsuarios.GetCiudadano(0).UserName, "Titulo",
                "Contenido", administrador.Categorias[0].Nombre);
            Publicacion publicacion = administrador.Publicaciones.First();
            Ciudadano ciudadano = administrador.AdminDeUsuarios.GetCiudadano(0);
            administrador.EliminarPublicacion(publicacion, ciudadano);
            Assert.True(administrador.Publicaciones.Count == 0, "la publicaciones no se ha eliminado");
        }

        [Fact]
        public void EliminarPublicacionesQueTienenVotos()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador, 10, 20);
            CrearCategorias(administrador);

            var ciudadano1 = administrador.AdminDeUsuarios.GetCiudadano(0);
            var ciudadano2 = administrador.AdminDeUsuarios.GetCiudadano(1);

            administrador.AgregarPublicacion(ciudadano1.UserName, "Titulo", "Contenido",
                administrador.Categorias[0].Nombre);
            var publicacion = administrador.Publicaciones.FirstOrDefault();

            administrador.VotarPublicacion(publicacion, ciudadano2, TipoVoto.VotoPositivo);

            var excepcion =
                Assert.Throws<PublicacionConVotos>(() => administrador.EliminarPublicacion(publicacion, ciudadano1));
            Assert.Equal(PublicacionConVotos.MensajeDeError, excepcion.Message);
            Assert.Equal(publicacion.Titulo, excepcion.TituloDeDePublicacion);
            Assert.True(administrador.Publicaciones.Count > 0, "la publicaciones se ha eliminado");
        }

        [Fact]
        public void EliminarPublicacionesQueTienenComentarios()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador.AgregarPublicacion(administrador.AdminDeUsuarios.GetCiudadano(0).UserName, "Titulo",
                "Contenido", administrador.Categorias[0].Nombre);
            Publicacion publicacion = administrador.Publicaciones.First();
            Ciudadano ciudadano = administrador.AdminDeUsuarios.GetCiudadano(0);
            administrador.AgregarComentario(ciudadano.NombreCompleto, publicacion.Titulo, "textoComentario");

            var excepcion =
                Assert.Throws<PublicacionConComentarios>(
                    () => administrador.EliminarPublicacion(publicacion, ciudadano));
            Assert.Equal(PublicacionConComentarios.MensajeDeError, excepcion.Message);
            Assert.Equal(publicacion.Titulo, excepcion.TituloDeDePublicacion);
            Assert.True(administrador.Publicaciones.Count > 0, "la publicaciones se ha eliminado");
        }

        [Fact]
        public void EliminarPublicacionDeOtroCiudadano()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador, 10, 20);
            CrearCategorias(administrador);

            var ciudadano1 = administrador.AdminDeUsuarios.GetCiudadano(0);
            var ciudadano2 = administrador.AdminDeUsuarios.GetCiudadano(1);

            administrador.AgregarPublicacion(ciudadano1.UserName, "Titulo", "Contenido",
                administrador.Categorias[0].Nombre);
            var publicacion = administrador.Publicaciones.FirstOrDefault();

            var excepcion =
                Assert.Throws<CiudadanoConPermisosInsuficientes>(() =>
                    administrador.EliminarPublicacion(publicacion, ciudadano2));
            Assert.Equal(CiudadanoConPermisosInsuficientes.MensajeDeError, excepcion.Message);
            Assert.Equal(ciudadano2.NombreCompleto, excepcion.NombreDelCiudadano);
            Assert.True(administrador.Publicaciones.Count > 0, "la publicaciones se ha eliminado");
        }

        // AgregarOQuitarCategoriasEnPublicacionesYaCreadas. Ariel

        // Votar a favor o en contra de publicaciones, la votacion debe ser realizada por un ciudadano valido.

        [Fact]
        public void AgregarCategoriaEnPublicacionExistente()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            var publicacionEncontrar = administrador.Publicaciones.First();

            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoTexto<Publicacion>("Titulo", publicacionEncontrar.Titulo)
            };

            var publicacionEncontrada = administrador.BuscarPublicacion(terminosDeBusqueda).First();


            var nuevaCategoria =new Categoria("Categoria Nueva") ;

            publicacionEncontrada.agregarCategoria(nuevaCategoria);
            
            Assert.True(publicacionEncontrada.Categorias.Where(cat => cat.Nombre.Equals(nuevaCategoria.Nombre)).ToList().Count > 0);
        }

        [Fact]
        public void QuitarCategoriaEnPublicacionExistente()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            var publicacionEncontrar = administrador.Publicaciones.First();

            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoTexto<Publicacion>("Titulo", publicacionEncontrar.Titulo)
            };

            var publicacionEncontrada = administrador.BuscarPublicacion(terminosDeBusqueda).First();

            var publicacionAEliminar = "Publicacion a Eliminar";            

            var nuevaCategoria = new Categoria(publicacionAEliminar);

            publicacionEncontrada.agregarCategoria(nuevaCategoria);

            publicacionEncontrada.eliminarCategoria(publicacionAEliminar);            

            Assert.True(publicacionEncontrada.Categorias.Where(cat => cat.Nombre.Equals(nuevaCategoria.Nombre)).Count() == 0);
        }

        [Fact]
        public void BuscarCiudadanosPorNombreDeUsuario()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            //var ciudadanoAEncontrar = administrador.Ciudadanos.Last();
            int numCiudadanos = administrador.AdminDeUsuarios.ContarCiudadanos();
            var ciudadanoAEncontrar = administrador.AdminDeUsuarios.GetCiudadano(numCiudadanos -1);
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Ciudadano>>
            {
                new TerminoTexto<Ciudadano>("UserName", ciudadanoAEncontrar.UserName)
            };
            var ciudadanosEncontrados = administrador.AdminDeUsuarios.BuscarCiudadanos(terminosDeBusqueda);
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
            var ciudadanosEncontrados = administrador.AdminDeUsuarios.BuscarCiudadanos(terminosDeBusqueda);
            Assert.Empty(ciudadanosEncontrados);
        }

        [Fact]
        public void VotarAFavorPorUnaPublicacion()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador, 10, 20);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            var publicacion1 = administrador.Publicaciones.FirstOrDefault();
            var ciudadano1 = administrador.AdminDeUsuarios.GetCiudadano(0);
            var ciudadano2 = administrador.AdminDeUsuarios.GetCiudadano(1);

            administrador.VotarPublicacion(publicacion1, ciudadano1, TipoVoto.VotoPositivo);
            administrador.VotarPublicacion(publicacion1, ciudadano2, TipoVoto.VotoPositivo);

            Assert.Equal(2, administrador.GetVotosDePublicacion(publicacion1, TipoVoto.VotoPositivo).Count());
        }

        [Fact]
        public void VotarPorUnaPublicacionPostivamenteDosVecesDeberiaQuedarSinVoto()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador, 10, 20);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            var publicacion1 = administrador.Publicaciones.FirstOrDefault();
            var ciudadano1 = administrador.AdminDeUsuarios.GetCiudadano(0);
            var ciudadano2 = administrador.AdminDeUsuarios.GetCiudadano(1);

            administrador.VotarPublicacion(publicacion1, ciudadano1, TipoVoto.VotoPositivo);
            administrador.VotarPublicacion(publicacion1, ciudadano1, TipoVoto.VotoPositivo);

            Assert.Empty(administrador.GetVotosDePublicacion(publicacion1));
        }

        [Fact]
        public void VotarPorUnaPublicacionPostivamentePrimeroYLuegoNegativamenteDeberiaQuedarConElUltimoTipoDeVoto()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador, 10, 20);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);

            var publicacion1 = administrador.Publicaciones.FirstOrDefault();
            var ciudadano1 = administrador.AdminDeUsuarios.GetCiudadano(0);
            var ciudadano2 = administrador.AdminDeUsuarios.GetCiudadano(1);

            administrador.VotarPublicacion(publicacion1, ciudadano1, TipoVoto.VotoPositivo);
            administrador.VotarPublicacion(publicacion1, ciudadano1, TipoVoto.VotoNegativo);

            Assert.Single(administrador.GetVotosDePublicacion(publicacion1));
            Assert.Equal(TipoVoto.VotoNegativo, administrador.GetVotosDePublicacion(publicacion1, TipoVoto.VotoNegativo).FirstOrDefault().TipoVoto);
        }

        [Fact]
        public void AgregarComentarioAPublicacionNoExistente()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            CrearPublicaciones(administrador);
            var categoriaABuscar = administrador.Categorias.First().Nombre;
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoCategoria<Publicacion>(categoriaABuscar)
            };
            var publicacionAEliminar = administrador.BuscarPublicacion(terminosDeBusqueda).First();
            var ciudadano = publicacionAEliminar.Ciudadano;
            administrador.EliminarPublicacion(publicacionAEliminar, ciudadano);

            try
            {
                administrador.AgregarComentario(ciudadano.NombreCompleto, publicacionAEliminar.Titulo, "Comentario");
            }
            catch (PublicacionNoEncontradaExcepcion e)
            {
                Assert.Equal(PublicacionNoEncontradaExcepcion.Mensaje, e.Message);
            }
        }
        // AgregarComentarios. Emilio
        [Fact]
        public void AgregarComentarioAPublicacion()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            administrador = CrearCiudadanos(administrador);
            administrador = CrearCategorias(administrador);
            administrador = CrearPublicaciones(administrador);
            var buscarCatergoria = administrador.Categorias.First().Nombre;
            var terminoDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoCategoria<Publicacion>(buscarCatergoria)
            };

            var publicacionAComentar = administrador.BuscarPublicacion(terminoDeBusqueda).First();
            var ciudadano = publicacionAComentar.Ciudadano;
           
            administrador.AgregarComentario(ciudadano.NombreCompleto, publicacionAComentar.Titulo, "textoComentario");

            Assert.True(publicacionAComentar.Comentarios.Count > 0, "la lista de comentarios esta vacia");
            Assert.Equal("textoComentario",publicacionAComentar.Comentarios[0].Contenido );

        }  



            // AgregarComentarioAPublicacionNoExistente. Martin

            // EliminarComentarios.

            // ActualizarComentarios.

            // AgregarCategorias.
            [Fact]
        public void AgregarCategoriaValida()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            string nombreCategoria = "CategoriaNueva";

            administrador.AgregarCategoria(nombreCategoria);

            Assert.True(administrador.Categorias.Count > 0, "la lista de publicaciones esta vacia");
            Assert.Equal(administrador.Categorias[0].Nombre, nombreCategoria);
        }

        [Fact]
        public void AgregarCategoriaConNombreVacio()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            string nombreCategoria = "";

            Assert.Throws<CategoriaConNombreVacio>(
                () => administrador.AgregarCategoria(nombreCategoria)
            );
            Assert.Empty(administrador.Categorias);
        }

        [Fact]
        public void AgregarCategoriaConCaracteresIvalidos()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            string nombreCategoria = "@/Invalido";

            Assert.Throws<CategoriaConNombreTieneCaracteresInvalidos>(
                () => administrador.AgregarCategoria(nombreCategoria)
            );
            Assert.Empty(administrador.Categorias);
        }

        [Fact]
        public void AgregarCategoriaConNombreDuplicado()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            string nombreCategoria = "Duplicado";
            administrador.AgregarCategoria(nombreCategoria);
            Assert.Throws<CategoriaConNombreDuplicado>(
                () => administrador.AgregarCategoria(nombreCategoria)
            );
            Assert.Equal(1, administrador.Categorias.Count);
        }

        // ModificarCategorias.

        // EliminarCategorias.
    }
}
