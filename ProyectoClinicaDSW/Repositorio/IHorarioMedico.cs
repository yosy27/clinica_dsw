using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IHorarioMedico
    {
        IEnumerable<HorarioMedico> ListaHorarioMedico();
        string RegistrarHorario(HorarioMedico hor);
    }
}
