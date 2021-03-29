using System;
using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class AdministradorDePublicaciones
    {
        private List<Ciudadano> _ciudadanos;
        private List<Categoria> _categorias;
        private List<Publicacion> _publicaciones;
        private List<Ranking<Publicacion>> _rankings;

        public IList<Ciudadano> Ciudadanos => _ciudadanos.AsReadOnly();
        public IList<Publicacion> Publicaciones => _publicaciones.AsReadOnly();
        public IList<Ranking<Publicacion>> Rankings => _rankings.AsReadOnly();
        public IList<Categoria> Categorias => _categorias.AsReadOnly();

        public AdministradorDePublicaciones()
        {
            _ciudadanos = new List<Ciudadano>();
            _categorias = new List<Categoria>();
            _publicaciones = new List<Publicacion>();
            _rankings = new List<Ranking<Publicacion>>();
        }

        public void AgregarCiudadano(string userName, string nombre, string apellido, DateTime fechaDeNacimiento, string ubicacion) 
        { 
            var ciudadano = new Ciudadano(userName, nombre, apellido, fechaDeNacimiento, new Ubicacion(ubicacion));
            _ciudadanos.Add(ciudadano);
        }

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
            Ciudadano ciudadano = BuscarCiudadano(userNameDeCiudadano);
            Categoria categoria = BuscarCategoria(nombreDeCategoria);

            if (ciudadano != null && categoria != null) {
                Publicacion publicacion = new Publicacion(titulo, contenido, ciudadano, categoria);
                categoria.AgregarPublicacion(publicacion);
                _publicaciones.Add(publicacion);
            }
        }

        private Ciudadano BuscarCiudadano(string userNameDeCiudadano)
        {
            return _ciudadanos.Where(ciudadano => ciudadano.UserName.Equals(userNameDeCiudadano)).FirstOrDefault();
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

        public List<Ciudadano> BuscarCiudadanos(List<TerminoDeBusqueda<Ciudadano>> terminosDeBusqueda)
        {
            var ciudadanos = _ciudadanos;

            terminosDeBusqueda.ForEach(termino =>
            {
                ciudadanos = termino.filtrar(ciudadanos);
            });

            return ciudadanos;
        }

        public void GenerarRanking(Criterio<Publicacion> criterio, int cantidad)
        {
            Ranking<Publicacion> ranking = new Ranking<Publicacion>(criterio, cantidad);
            ranking.CalcularRanking(_publicaciones);
            _rankings.Add(ranking);
        }
    }
}
