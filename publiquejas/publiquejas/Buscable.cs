using System.Collections.Generic;

namespace publiquejas
{
    public interface Buscable
    {
        bool cumple(List<TerminoDeBusqueda> terminos);
    }
}
