using System;

namespace publiquejas.Excepciones
{
    [Serializable]
    class CiudadanoNoEncontradoExcepcion : Exception
    {
        public CiudadanoNoEncontradoExcepcion()
        : base("Ciudadano no encontrado")
        {

        }
    }
}
