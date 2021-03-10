using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class AdministradorDePublicaciones
    {
        private List<Ciudadano> _ciudadanos;

        public List<Ciudadano>  Ciudadanos { get { return _ciudadanos; } }

        public AdministradorDePublicaciones()
        {
            _ciudadanos = new List<Ciudadano>(); 
        }
        
        public void AgregarCiudadano(string nombre, string apellido, DateTime fechaDeNacimiento, string ubicacion) 
        { 
            var ciudadano = new Ciudadano(nombre, apellido, fechaDeNacimiento, new Ubicacion(ubicacion));
            _ciudadanos.Add(ciudadano);
        }
    }
}
