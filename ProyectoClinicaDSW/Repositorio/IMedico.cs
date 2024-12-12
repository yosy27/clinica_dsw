using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IMedico
    {
        IEnumerable<Medico> ListaMedico();

        string RegistrarMedico(Medico med);

        string ActualizarMedico(Medico med);

        string EliminarMedico(int idMedico);

        IEnumerable<Medico> FilterName(string nombre);
    }
}
