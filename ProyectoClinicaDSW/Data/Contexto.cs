namespace ProyectoClinicaDSW.Data
{
    public class Contexto
    {
        public String Conexion { get; }

        public Contexto(String valor)
        {

            Conexion = valor;
        }
    }
}
