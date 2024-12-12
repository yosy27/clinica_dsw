using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IPaciente
    {
        IEnumerable<Paciente> ListaPacientes();

        string RegistrarPaciente(Paciente pac);

        string ActualizarPaciente(Paciente pac);

        string EliminarPaciente(int idPaciente);

        IEnumerable<Paciente> FilterName(string nombre);


    }
}
