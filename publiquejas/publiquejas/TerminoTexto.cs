using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class TerminoTexto : TerminoDeBusqueda
    {
        private string _campo;
        private string _texto;

        public TerminoTexto(string campo, string texto)
        {
            this._campo = campo;
            this._texto = texto;
        }

        public List<Buscable> filtrar(List<Buscable> buscables)
        {
            return buscables.Where((buscable) =>
            {
                var texto = buscable.getPropertyValue(_campo);
                return texto.Equals(_texto);
            }).ToList();
        }
    }
}