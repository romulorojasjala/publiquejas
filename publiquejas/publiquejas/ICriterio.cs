using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public interface ICriterio
    {
        int Calcular(Publicacion publicacion);
    }
}
