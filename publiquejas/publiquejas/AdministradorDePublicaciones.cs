﻿using publiquejas.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using publiquejas.Excepciones;
using publiquejas.Votos;

namespace publiquejas
{
    public class AdministradorDePublicaciones
    {
        private List<Ciudadano> _ciudadanos;
        private List<ICategoria> _categorias;
        private List<Publicacion> _publicaciones;
        private IValidadorCategoria _validadorCategoria;
        private AdministradorDeUsuarios _adminDeUsuarios;

        public AdministradorDePublicaciones()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IValidadorCategoria, ValidadorCategoria>()
                .BuildServiceProvider();
            
            _ciudadanos = new List<Ciudadano>();
            _categorias = new List<ICategoria>();
            _publicaciones = new List<Publicacion>();
            _validadorCategoria = serviceProvider.GetService<IValidadorCategoria>();
            _adminDeUsuarios = new AdministradorDeUsuarios();
        }

        public IList<Publicacion> Publicaciones => _publicaciones.AsReadOnly();
        public IList<ICategoria> Categorias => _categorias.AsReadOnly();

        public AdministradorDeUsuarios AdminDeUsuarios { get { return _adminDeUsuarios; } }
        
        public void AgregarCategoria(string nombreDeCategoria)
        {            
            Categoria categoria = new Categoria(nombreDeCategoria);
            _validadorCategoria.VerificarDuplicados(categoria, _categorias);
            _validadorCategoria.Validar(categoria);
            _categorias.Add(categoria);
        }

        public void AgregarPublicacion(string userNameDeCiudadano, string titulo, string contenido, string nombreDeCategoria)
        {
            Ciudadano ciudadano = _adminDeUsuarios.BuscarCiudadano(userNameDeCiudadano);
            ICategoria categoria = AdministradorDePublicaciones.BuscarCategoria(nombreDeCategoria, _categorias);

            if (ciudadano != null && categoria != null) {
                Publicacion publicacion = new Publicacion(titulo, contenido, ciudadano, categoria);
                categoria.AgregarPublicacion(publicacion);
                _publicaciones.Add(publicacion);
            }
        }       

        public void ActualizarPublicacion(string tituloPublicacion, DatosEditablesPublicacion nuevosDatos, Ciudadano ciudadanoAutorizado)
        {
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoTexto<Publicacion>("Titulo", tituloPublicacion)
            };
            var publicacionesEncontradas = this.BuscarPublicacion(terminosDeBusqueda);
            if (publicacionesEncontradas.Count == 0)
                throw new PublicacionNoEncontradaExcepcion(tituloPublicacion);
            terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>
            {
                new TerminoTexto<Publicacion>("Titulo", nuevosDatos.Titulo)
            };
            var publicacionesRepetidas = this.BuscarPublicacion(terminosDeBusqueda);
            if (publicacionesRepetidas.Count > 0)
                throw new TituloDePublicacionRepetida(nuevosDatos.Titulo);
            publicacionesEncontradas.First().Editar(nuevosDatos, ciudadanoAutorizado);
        }

        public void EliminarPublicacion(Publicacion publicacion, Ciudadano ciudadano)
        {
            if (!publicacion.Ciudadano.Equals(ciudadano))
            {
                throw new CiudadanoConPermisosInsuficientes(ciudadano.NombreCompleto);
            }

            if (publicacion.Votos.Count > 0)
            {
                throw new PublicacionConVotos(publicacion.Titulo);
            }

            if (publicacion.Comentarios.Count > 0)
            {
                throw new PublicacionConComentarios(publicacion.Titulo);
            }

            _publicaciones.Remove(publicacion);
        }

        public static ICategoria BuscarCategoria(string nombreDeCategoria, IList<ICategoria> categorias)
        {
            return categorias.Where(categoria => categoria.Nombre.Equals(nombreDeCategoria)).FirstOrDefault();
        }

        public List<Publicacion> BuscarPublicacion(List<TerminoDeBusqueda<Publicacion>> terminosDeBusqueda)
        {
            var publicaciones = _publicaciones;

            terminosDeBusqueda.ForEach(termino =>
            {
                publicaciones = termino.filtrar(publicaciones);
            });

            return publicaciones;
        }

        public void VotarPublicacion(Publicacion publicacion, Ciudadano ciudadano, TipoVoto tipoVoto)
        {
            var publicacionEncontrada = Publicaciones.FirstOrDefault(p => p == publicacion);
            if (publicacionEncontrada == null)
                throw new PublicacionNoEncontradaExcepcion();
            var ciudadanoEncontrado = _adminDeUsuarios.BuscarCiudadano(ciudadano);
            if (ciudadanoEncontrado == null)
                throw new CiudadanoNoEncontradoExcepcion();

            publicacion.Votar(ciudadano, tipoVoto);
        }

        public IEnumerable<Voto> GetVotosDePublicacion(Publicacion publicacion)
        {
            var publicacionEncontrada = Publicaciones.FirstOrDefault(p => p == publicacion);
            if (publicacionEncontrada == null)
                throw new PublicacionNoEncontradaExcepcion();

            return publicacionEncontrada.GetVotos();
        }

        public IEnumerable<Voto> GetVotosDePublicacion(Publicacion publicacion, TipoVoto tipoVoto)
        {
            var publicacionEncontrada = Publicaciones.FirstOrDefault(p => p == publicacion);
            if (publicacionEncontrada == null)
                throw new PublicacionNoEncontradaExcepcion();

            return publicacionEncontrada.GetVotos(tipoVoto);
        }

        public void AgregarComentario(string nombreCiudadano, string tituloPublicacion, string contenidoComentario)
        {
            var terminosDeBusqueda = new List<TerminoDeBusqueda<Publicacion>>()
            {
                  new TerminoTexto<Publicacion>("titulo", tituloPublicacion)
            };
            Publicacion publicacion = BuscarPublicacion(terminosDeBusqueda).FirstOrDefault();
            if (publicacion == null)
            {
                throw new PublicacionNoEncontradaExcepcion(tituloPublicacion);
            }
            Ciudadano ciudadano = _adminDeUsuarios.BuscarCiudadano(nombreCiudadano);
            publicacion.AgregarComentario(ciudadano, contenidoComentario);
        }
    }

}
