namespace publiquejas
{
    public interface IEditable<T> where T : INuevosDatos
    {
        void Editar(T nuevosDatos, Ciudadano ciudadanoAutorizado);
    }

    public interface INuevosDatos
    {
    }
}
