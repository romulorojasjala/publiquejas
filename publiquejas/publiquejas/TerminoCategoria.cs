using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class TerminoCategoria<T> : TerminoDeBusqueda<T> where T : Buscable
    {
        private string _categoria;

        public TerminoCategoria(string categoria)
        {
            this._categoria = categoria;
        }

        public List<T> filtrar(List<T> elementosAFiltrar)
        {
            return elementosAFiltrar.Where((buscable) =>
            {
                var categoria = buscable.getPropertyValue("Categoria") as Categoria;
                return categoria.Nombre.Equals(_categoria);
            }).ToList();
        }
    }
}