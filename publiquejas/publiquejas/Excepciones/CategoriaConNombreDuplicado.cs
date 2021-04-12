using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas.Excepciones
{
    public class CategoriaConNombreDuplicado : Exception
    {
        private const string MensajeDeError = "El nombre de la categoria ya existe";
        public CategoriaConNombreDuplicado()
        {
        }

        public CategoriaConNombreDuplicado(string message) : base(MensajeDeError)
        {
        }

        public CategoriaConNombreDuplicado(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoriaConNombreDuplicado(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
