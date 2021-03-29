using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Ranking<T> where T : IRankeable
    {
        private Criterio<T> _criterio;
        private int _cantidad;
        private List<T> _ranking;

        public IList<T> ElementosRanking => _ranking;

        public Ranking(Criterio<T> criterio, int cantidad)
        {
            _criterio = criterio;
            _cantidad = cantidad;
            
        }

        public void CalcularRanking(List<T> elementosARankear)
        {
            _ranking = new List<T>(elementosARankear);
            _ranking.Sort(_criterio);
            _ranking = _ranking.Take(_cantidad).ToList();
        }
    }
}
