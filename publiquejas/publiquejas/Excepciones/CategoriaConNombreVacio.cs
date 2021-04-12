using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas.Excepciones
{
    public class CategoriaConNombreVacio : Exception
    {
        private const string MensajeDeError = "El nombre de la categoria esta vacio";
        public CategoriaConNombreVacio()
        {
        }

        public CategoriaConNombreVacio(string message) : base(MensajeDeError)
        {
        }

        public CategoriaConNombreVacio(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoriaConNombreVacio(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
