namespace publiquejas
{
    public class Ubicacion
    {
        private string _ubicacion;

        public string UbicacionName => _ubicacion;
        public Ubicacion(string ubicacion) {
            _ubicacion = ubicacion;
        }
    }
}
