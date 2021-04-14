using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Comentario
    {
        private Ciudadano _autor;
        private string _contenido;
        private DateTime _fecha;

        public Ciudadano Autor { get { return _autor; } }
        public string Contenido { get { return _contenido; } }
        public DateTime Fecha { get { return _fecha; } }

        public Comentario(Ciudadano autor, string contenido)
        {
            _autor = autor;
            _contenido = contenido;
            _fecha = DateTime.Now;
        }
       
    }
}
