using System.Collections.Generic;
using System.Linq;

namespace publiquejas
{
    public class TerminoTexto<T> : TerminoDeBusqueda<T> where T : Buscable
    {
        private string _campo;
        private string _texto;

        public TerminoTexto(string campo, string texto)
        {
            this._campo = campo;
            this._texto = texto;
        }

        public List<T> filtrar(List<T> elementosAFiltrar)
        {
            return elementosAFiltrar.Where((buscable) =>
            {
                var texto = buscable.getPropertyValue(_campo);
                if (texto != null)
                {
                    return texto.Equals(_texto);
                }

                return false;
            }).ToList();
        }
    }
}