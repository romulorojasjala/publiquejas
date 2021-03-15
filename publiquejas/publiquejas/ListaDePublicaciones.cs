using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    class ListaDePublicaciones
    {
        private List<Publicacion> _listaDePublicaciones;

        public void AñadirPublicación(Publicacion publicacion)
        {
            _listaDePublicaciones.Add(publicacion);
        }

        public Publicacion BuscarPublicación(string titulo)
        {
            foreach (Publicacion publicacion in _listaDePublicaciones)
            {
                if (publicacion.TituloPublicacion().Equals(titulo))
                {
                    return publicacion;
                }
            }
            return null;
        }
    }
}
