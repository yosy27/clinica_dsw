using Microsoft.AspNetCore.Mvc;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IUsuario
    {
        IEnumerable<Usuario> ListaUsuario();
        string RegistrarUsuario(Usuario usu);
        Task<Usuario?> Login(Usuario usu);


    }
}
