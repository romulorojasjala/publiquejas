using System.Collections.Generic;

namespace publiquejas.Votos
{
    public class Voto
    {
        public TipoVoto TipoVoto { get; }
        public Ciudadano Ciudadano { get; }

        public Voto(Ciudadano ciudadano, TipoVoto tipoVoto)
        {
            TipoVoto = tipoVoto;
            Ciudadano = ciudadano;
        }
    }
}
