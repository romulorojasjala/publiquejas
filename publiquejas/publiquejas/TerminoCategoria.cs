using System.Collections;
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

        public List<Buscable> filtrar(List<Buscable> elementosAFiltrar)
        {
            return elementosAFiltrar.Where((buscable) =>
            {
                var categoria = buscable.getPropertyValue("categoria") as Categoria;
                return categoria.Nombre.Equals(_categoria);
            }).ToList();
        }
    }
}