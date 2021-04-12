using publiquejas.Votos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publiquejas
{
    public class Publicacion : Buscable, Votable
    {
        string _titulo;
        string _contenido;
        Ciudadano _ciudadano;
        ICategoria _categoria;
        List<Comentario> _comentarios;

        public string Titulo { get { return _titulo; } }
        public string Contenido { get { return _contenido; } }
        public Ciudadano Ciudadano { get { return _ciudadano; } }
        public ICategoria Categoria { get { return _categoria; } }
        public List<Comentario> Comentarios { get { return _comentarios; } }

        public List<Voto> Votos { get; set; } = new List<Voto>();

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _comentarios = new List<Comentario>();
        }

        public Publicacion(string titulo, string contenido, Ciudadano ciudadano, ICategoria categoria)
        {
            _titulo = titulo;
            _contenido = contenido;
            _ciudadano = ciudadano;
            _categoria = categoria;
            _comentarios = new List<Comentario>();
        }

        public object getPropertyValue(string propertyName)
        {
            var property = GetType().GetProperties().ToList()
                .Find((prop) => propertyName.ToLower().Equals(prop.Name.ToLower()));

            if (property != null)
            {
                return property.GetValue(this);
            }

            return null;
        }

        public void Votar(Ciudadano ciudadano, TipoVoto tipoVoto)
        {
            var voteFound = Votos.FirstOrDefault(v => v.Ciudadano.UserName == ciudadano.UserName && v.Ciudadano.NombreCompleto == ciudadano.NombreCompleto); // TODO: Add comparador para ciudadano
            if(voteFound != null)
            { 
                Votos.Remove(voteFound);
                if (voteFound.TipoVoto == tipoVoto)
                    return;
            }
            Votos.Add(new Voto(ciudadano, tipoVoto));
        }

        public IEnumerable<Voto> GetVotos(TipoVoto tipoVoto)
        {
            var votosFiltrados = Votos.Where(v => v.TipoVoto == tipoVoto);

            return votosFiltrados;
        }

        public IEnumerable<Voto> GetVotos()
        {
            return Votos;
        }

        public void AgregarComentario(Ciudadano ciudadano, string contenidoComentario)
        {
            Comentario comentario = new Comentario(ciudadano, contenidoComentario);
            _comentarios.Add(comentario);
        }
    }
}
