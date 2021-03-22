using System;
using System.Collections;
using System.Collections.Generic;

namespace publiquejas
{
    public interface TerminoDeBusqueda
    {
        List<T> filtrar<T> (List<T> elementosAFiltrar) where T : Buscable;
    }
}