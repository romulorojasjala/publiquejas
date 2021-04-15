using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class TerminoCiudadano<T> : TerminoDeBusqueda<T> where T : Buscable
    {
        private string _nombreCiudadano;

        public TerminoCiudadano(string nombreCiudadano)
        {
            _nombreCiudadano = nombreCiudadano;
        }

        public List<T> filtrar(List<T> elementosAFiltrar)
        {
            return elementosAFiltrar.Where((buscable) =>
            {
                var ciudadano = buscable.getPropertyValue("ciudadano") as Ciudadano;
                return ciudadano.NombreCompleto.Equals(_nombreCiudadano);
            }).ToList();
        }
    }
}
