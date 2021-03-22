using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Ranking
    {
        private Criterio _criterio;
        //private DateTime _fechaCalculo;
        private int _cantidad;
        private List<Publicacion> _publicaciones;

        public IList<Publicacion> Publicaciones => _publicaciones.AsReadOnly();

        public Ranking(Criterio criterio, int cantidad)
        {
            _criterio = criterio;
            _cantidad = cantidad;
        }

        public void CalcularRanking(List<Publicacion> publicaciones)
        {
            // No deberiamos asignar todas las publicaciones del argumento del metodo al atributo del objeto.
            // Pero por el momento no tenemos el mecanismo para evaluar el criterio contra las publicaciones
            // que recibimos.
            this._publicaciones = publicaciones;
        }
    }
}
