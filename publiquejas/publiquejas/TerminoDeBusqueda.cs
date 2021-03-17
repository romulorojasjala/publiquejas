using System;
using System.Collections.Generic;

namespace publiquejas
{
    public abstract class TerminoDeBusqueda
    {
        public abstract List<Publicacion> filtrar(List<Publicacion> publicaciones);
        public abstract bool cumple(Publicacion publicacion);
    }
}