using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class TerminoCategoria : TerminoDeBusqueda
    {
        private string _categoria;

        public TerminoCategoria(string categoria)
        {
            this._categoria = categoria;
        }

        public override List<Publicacion> filtrar(List<Publicacion> publicaciones)
        {
            return publicaciones.Where((publicacion) => _categoria.Equals(publicacion.Categoria.Nombre)).ToList();
        }


        public override bool cumple(Publicacion publicacion)
        {
            return publicacion.Categoria.Nombre.Equals(_categoria);
        }
    }
}