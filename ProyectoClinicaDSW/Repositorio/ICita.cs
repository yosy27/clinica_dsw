using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface ICita
    {
        IEnumerable<Cita> ListaCita();

        string RegistrarCita(Cita cit);

        string ActualizarCita(Cita cit);

        string EliminarCitaint (int idCita);

        IEnumerable<Cita> FilterCita(string inicial);

    }
}
