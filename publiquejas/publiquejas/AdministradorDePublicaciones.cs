using System;
using System.Collections.Generic;
using System.Linq;
using publiquejas.Excepciones;
using publiquejas.Votos;

namespace publiquejas
{
    public class AdministradorDePublicaciones
    {
        private List<Categoria> _categorias;
        private List<Publicacion> _publicaciones;
        private AdministradorDeUsuarios _adminDeUsuarios;

        public AdministradorDePublicaciones()
        {
            _categorias = new List<Categoria>();
            _publicaciones = new List<Publicacion>();
            _adminDeUsuarios = new AdministradorDeUsuarios();
        }

        public IList<Publicacion> Publicaciones => _publicaciones.AsReadOnly();
        public IList<Categoria> Categorias => _categorias.AsReadOnly();
        public AdministradorDeUsuarios AdminDeUsuarios { get { return _adminDeUsuarios; } }

        public void AgregarCategoria(string nombreDeCategoria)
        {
            Categoria categoria = BuscarCategoria(nombreDeCategoria);
            if (categoria == null)
            {
                categoria = new Categoria(nombreDeCategoria);
                _categorias.Add(categoria);
            }
        }

        public void AgregarPublicacion(string userNameDeCiudadano, string titulo, string contenido, string nombreDeCategoria)
        {
            Ciudadano ciudadano = _adminDeUsuarios.BuscarCiudadano(userNameDeCiudadano);
            Categoria categoria = BuscarCategoria(nombreDeCategoria);

            if (ciudadano != null && categoria != null)
            {
                Publicacion publicacion = new Publicacion(titulo, contenido, ciudadano, categoria);
                categoria.AgregarPublicacion(publicacion);
                _publicaciones.Add(publicacion);
            }
        }

        private Categoria BuscarCategoria(string nombreDeCategoria)
        {
            return _categorias.Where(categoria => categoria.Nombre.Equals(nombreDeCategoria)).FirstOrDefault();
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
    }

}
