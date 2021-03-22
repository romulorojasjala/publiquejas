using System;
using System.Collections;
using System.Collections.Generic;

namespace publiquejas
{
    public interface TerminoDeBusqueda
    {
        List<Buscable> filtrar(List<Buscable> elementosAFiltrar);
    }
}