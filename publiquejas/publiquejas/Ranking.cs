using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Ranking
    {
        private IList<ICriterio> _criterios;
        //private DateTime _fechaCalculo;
        private int _cantidad;
        private SortedDictionary<int, Publicacion> _publicaciones;

        public SortedDictionary<int, Publicacion> Publicaciones => _publicaciones;

        public Ranking(IList<ICriterio> criterios, int cantidad)
        {
            _criterios = criterios;
            _cantidad = cantidad;
            _publicaciones = new SortedDictionary<int, Publicacion>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        }

        public void CalcularRanking(List<Publicacion> publicaciones)
        {
            _publicaciones.Clear();
            foreach(Publicacion publicacion in  publicaciones)
            {
                int valoracion = 0;
                foreach(ICriterio criterio in _criterios)
                {
                    valoracion += criterio.Calcular(publicacion);
                }
                this._publicaciones.Add(valoracion, publicacion);
                if (this._publicaciones.Count() >= this._cantidad)
                {
                    break;
                }
            }
        }
    }
}
