using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IUsuario
    {
        IEnumerable<Usuario> ListaUsuario();
        string RegistrarUsuario(Usuario usu);






    }
}
