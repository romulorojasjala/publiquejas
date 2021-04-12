using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public interface IValidadorCategoria
    {
        void Validar(ICategoria categoria);
        void VerificarDuplicados(ICategoria categoria, IList<ICategoria> categorias);
    }
}
