using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas.Excepciones
{
    public class CategoriaConNombreTieneCaracteresInvalidos : Exception
    {
        private const string MensajeDeError = "El nombre de la categoria esta vacio";
        public CategoriaConNombreTieneCaracteresInvalidos()
        {
        }

        public CategoriaConNombreTieneCaracteresInvalidos(string message) : base(MensajeDeError)
        {
        }

        public CategoriaConNombreTieneCaracteresInvalidos(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoriaConNombreTieneCaracteresInvalidos(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
