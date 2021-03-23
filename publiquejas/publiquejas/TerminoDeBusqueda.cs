using System;
using System.Collections;
using System.Collections.Generic;

namespace publiquejas
{
    public interface TerminoDeBusqueda<T> where T : Buscable
    {
        List<T> filtrar(List<T> elementosAFiltrar);
    }
}