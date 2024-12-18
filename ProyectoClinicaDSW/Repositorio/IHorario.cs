using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IHorario
    {
        IEnumerable<Horario> ListaHorario();
        string RegistrarHorario(Horario hor);

    }
}
