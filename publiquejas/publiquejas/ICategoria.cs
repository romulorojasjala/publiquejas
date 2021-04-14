
namespace publiquejas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoria
    {
        void AgregarPublicacion(Publicacion publicacion);
        string Nombre { get; }
        IList<Publicacion> Publicaciones { get; }
    }
}
