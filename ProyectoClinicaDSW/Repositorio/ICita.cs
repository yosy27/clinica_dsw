using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface ICita
    {

        string RegistrarCita(Cita cit);

        string EliminarCita (int idCita);

        IEnumerable<Cita> FilterCita(string dni);

    }
}
