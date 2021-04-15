using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class PublicacionNoExistenteException : Exception
    {
        public const string MensajeDeError = "La publicacion no existe.";

        public PublicacionNoExistenteException(): base(MensajeDeError)
        {

        }
    }
}
