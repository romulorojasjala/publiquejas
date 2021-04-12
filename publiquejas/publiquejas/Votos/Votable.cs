using System.Collections.Generic;

namespace publiquejas.Votos
{
    public interface Votable
    {
        void Votar(Ciudadano ciudadano, TipoVoto tipoVoto);
        IEnumerable<Voto> GetVotos(TipoVoto tipoVoto);
        IEnumerable<Voto> GetVotos();
    }
}
