using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using publiquejas;

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
                string randomValue = GeneradorDeCadenas(randon.Next(10));
                string titulo = $"Titulo{randomValue}";
                string contenido = $"Contenido{randomValue}";

                int indexCiudadano = randon.Next(admin.Ciudadanos.Count);
                int indexCategoria = randon.Next(admin.Categorias.Count);

                admin.AgregarPublicacion(admin.Ciudadanos[indexCiudadano].UserName, titulo, contenido, admin.Categorias[indexCategoria].Nombre);
            }

            return admin;
        }

        internal List<Publicacion> GenerarListaPorLikes(AdministradorDePublicaciones admin, int cantidad, bool highToLow = false)
        {
            Random _random = new Random();
            admin.Publicaciones.ToList().ForEach(p => {
                var numeroLikes = _random.Next(1, 100);
                p.Likes = numeroLikes;
            });

            var publicacionesPorLikes = new List<Publicacion>(admin.Publicaciones);
            publicacionesPorLikes.Sort((p1, p2) => {
                if (highToLow)
                {
                    return p2.Likes.CompareTo(p1.Likes);
                }
                else
                {
                    return p1.Likes.CompareTo(p2.Likes);
                }
            });

            return publicacionesPorLikes.Take(cantidad).ToList();
        }

        internal List<Publicacion> ObtenerListaOrdenadaPorTitulo(AdministradorDePublicaciones admin, int cantidad, bool highToLow = false)
        {
            var publicacionesPorTitulo = new List<Publicacion>(admin.Publicaciones);
            publicacionesPorTitulo.Sort((p1, p2) => {
                if (highToLow)
                {
                    return p2.Titulo.Length.CompareTo(p1.Titulo.Length);
                }
                else
                {
                    return p1.Titulo.Length.CompareTo(p2.Titulo.Length);
                }
            });

            return publicacionesPorTitulo.Take(cantidad).ToList();
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

        [Fact]
        public void GenerarRankingPorLikesMayorAMenor()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            administrador = CrearPublicaciones(administrador, 20);
            List<Publicacion> ResultadoVotacion = GenerarListaPorLikes(administrador, 10, true);

            var criterio = new Criterio<Publicacion>()
                .AgregarPropiedad("Likes")
                .AgregarAccion(AccionCriterio.Mayor)
                .AgregarTipo(TipoCriterio.Cantidad);

            administrador.GenerarRanking(criterio, 10);

            Assert.True(administrador.Rankings.Count > 0, "La lista de rankings esta vacia");
            Assert.Equal(ResultadoVotacion, administrador.Rankings[0].ElementosRanking);
            
        }

        [Fact]
        public void GenerarRankingPorLikesMenorAMayor()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            administrador = CrearPublicaciones(administrador, 20);
            List<Publicacion> ResultadoVotacion = GenerarListaPorLikes(administrador, 10);

            var criterio = new Criterio<Publicacion>()
                .AgregarPropiedad("Likes")
                .AgregarAccion(AccionCriterio.Menor)
                .AgregarTipo(TipoCriterio.Cantidad);

            administrador.GenerarRanking(criterio, 10);

            Assert.True(administrador.Rankings.Count > 0, "La lista de rankings esta vacia");
            Assert.Equal(ResultadoVotacion, administrador.Rankings[0].ElementosRanking);

        }

        [Fact]
        public void GenerarRankingLongitudDeNombreEnTituloMenorAMayor()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            administrador = CrearPublicaciones(administrador, 20);

            List<Publicacion> ResultadoTitulos = ObtenerListaOrdenadaPorTitulo(administrador, 10);

            var criterio = new Criterio<Publicacion>()
                .AgregarPropiedad("Titulo")
                .AgregarAccion(AccionCriterio.Menor)
                .AgregarTipo(TipoCriterio.Longitud);

            administrador.GenerarRanking(criterio, 10);

            Assert.True(administrador.Rankings.Count > 0, "La lista de rankings esta vacia");
            Assert.Equal(ResultadoTitulos, administrador.Rankings[0].ElementosRanking);
        }

        [Fact]
        public void GenerarRankingLongitudDeNombreEnTituloMayorAMenor()
        {
            AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
            CrearCiudadanos(administrador);
            CrearCategorias(administrador);
            administrador = CrearPublicaciones(administrador, 20);

            List<Publicacion> ResultadoTitulos = ObtenerListaOrdenadaPorTitulo(administrador, 10, true);

            var criterio = new Criterio<Publicacion>()
                .AgregarPropiedad("Titulo")
                .AgregarAccion(AccionCriterio.Mayor)
                .AgregarTipo(TipoCriterio.Longitud);

            administrador.GenerarRanking(criterio, 10);

            Assert.True(administrador.Rankings.Count > 0, "La lista de rankings esta vacia");
            Assert.Equal(ResultadoTitulos, administrador.Rankings[0].ElementosRanking);
        }

        //[Fact]
        //public void GenerarRankingLongitudCategoriaMasTituloMayorAMenor()
        //{
        //    AdministradorDePublicaciones administrador = new AdministradorDePublicaciones();
        //    CrearCiudadanos(administrador);
        //    CrearCategorias(administrador);
        //    administrador = CrearPublicaciones(administrador, 20);

        //    List<Publicacion> ResultadoTitulos = ObtenerListaOrdenadaPorTitulo(administrador, 10, true);

        //    var criterio = new Criterio<Publicacion>();
        //    var propiedadCompuesta = new PropiedadCompuesta();
        //    propiedadCompuesta.AgregarPropiedad("Categoria.Nombre");
        //    propiedadCompuesta.AgregarPropiedad("Titulo");
        //    propiedadCompuesta.AgregarTransformacion(TransformacionCriterio.Concatenacion);
        //    criterio.AgregarPropiedad(propiedadCompuesta);
        //    criterio.AgregarAccion(AccionCriterio.Mayor)
        //        .AgregarTipo(TipoCriterio.Longitud);


            //var criterio = new Criterio<Publicacion>()
            //    .AgregarPropiedad("Categoria.Nombre")
            //    .AgregarPropiedad("Titulo")
            //    .AgregarTransformacionPropiedad(TransformacionCriterio.Concatenacion)
            //    .AgregarAccion(AccionCriterio.Mayor)
            //    .AgregarTipo(TipoCriterio.Longitud);

            //administrador.GenerarRanking(criterio, 10);

        //    Console.WriteLine("Mayor a menor");
        //    administrador.Rankings[0].ElementosRanking.ToList().ForEach((p) => Console.WriteLine(p.Titulo + " - " + p.Titulo.Length));
        //    Console.WriteLine("------");
        //    ResultadoTitulos.ForEach((p) => Console.WriteLine(p.Titulo + " - " + p.Titulo.Length));
        //}
    }
}
