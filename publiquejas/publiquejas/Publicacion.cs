using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Publicacion
    {
        string _publicacionId;
        string _titulo;
        string _contenido;
        Ciudadano _ciudadano;
        private List<Comentario> _comentarios;
        public string PublicacionID { get { return _publicacionId; } }  
        public Publicacion(string publicacionId,string titulo, string contenido, Ciudadano ciudadano)
        {
            _publicacionId = publicacionId;
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _comentarios = new List<Comentario>();
        }

        public void AgregarComentario(Ciudadano ciudadano, string comentarioContenido)
        {
            Comentario comentario = new Comentario(ciudadano, comentarioContenido);
            _comentarios.Add(comentario);
        }
    }
}
