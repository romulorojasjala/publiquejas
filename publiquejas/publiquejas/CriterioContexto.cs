using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class CriterioContexto
    {
        ExpresionMasMenos expresionMasMenos = new ExpresionMasMenos();
        ExpresionValoracion expresionValoracion = new ExpresionValoracion();

        //Criterio criterio = InterpretarCriterio("6 Mas Likes");
        public Criterio InterpretarCriterio(string token)
        {
            string[] valoresATrabajar = token.Split(' ');
            int cantidad = Int32.Parse(valoresATrabajar[0]);
            expresionMasMenos.Interpretar(valoresATrabajar[1]);
            expresionValoracion.Interpretar(valoresATrabajar[2]);

            return null;
        }
    }
}
