using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Ciudadano
    {
        public string Nombre { get; set; }        
        public string Apellido { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public Ciudadano(string nombre, string apellido, DateTime fechaDeNacimiento, Ubicacion ubicacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaDeNacimiento = fechaDeNacimiento;
            Ubicacion = ubicacion;
        }
    }
}
