using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface ICita
    {
        string RegistrarCita(CitaReg citReg);
        string EliminarCita(int idCita);

        IEnumerable<Cita> FilterCita(string nombre);

    }
}
