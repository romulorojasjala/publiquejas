using System;
using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class AdministradorDePublicaciones
    {
        

        private List<Categoria> _categorias;
        private List<Publicacion> _publicaciones;
        private AdministradorDeUsuarios _administradorDeUsuarios;

        public AdministradorDePublicaciones()
        {
            _categorias = new List<Categoria>();
            _publicaciones = new List<Publicacion>();
            _administradorDeUsuarios = new AdministradorDeUsuarios();
        }

        public IList<Publicacion> Publicaciones => _publicaciones.AsReadOnly();
        public IList<Categoria> Categorias => _categorias.AsReadOnly();

        public void AgregarCiudadano(string userName, string nombre, string apellido, DateTime fechaDeNacimiento, string ubicacion) 
        {
            _administradorDeUsuarios.AgregarCiudadano(userName, nombre, apellido, fechaDeNacimiento, ubicacion);
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
            Ciudadano ciudadano = _administradorDeUsuarios.BuscarCiudadano(userNameDeCiudadano);
            Categoria categoria = BuscarCategoria(nombreDeCategoria);

            if (ciudadano != null && categoria != null) {
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

        public List<Ciudadano> BuscarCiudadanos(List<TerminoDeBusqueda<Ciudadano>> terminosDeBusqueda)
        {
            return _administradorDeUsuarios.BuscarCiudadanos(terminosDeBusqueda);
        }

        public void EliminarCiudadano(string nombreCiudadano)
        {
            _administradorDeUsuarios.EliminarCiudadano(nombreCiudadano);
        }

        public bool ExisteCiudadano(string nombreCiudadano)
        {
            return _administradorDeUsuarios.ExisteCiudadano(nombreCiudadano);
        }

        public int ContarCiudadanos()
        {
            return _administradorDeUsuarios.ContarUsuarios();
        }

        public Ciudadano GetNCiudadano(int index)
        {
            return _administradorDeUsuarios.GetCiudadano(index);
        }

    }

}
