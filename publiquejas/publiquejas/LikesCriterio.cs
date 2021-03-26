using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class LikesCriterio : ICriterio
    {
        public LikesCriterio() {}

        public int Calcular(Publicacion publicacion)
        {
            return publicacion.Likes;
        }
    }
}
