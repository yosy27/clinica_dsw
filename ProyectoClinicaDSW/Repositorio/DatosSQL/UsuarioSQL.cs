using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class UsuarioSQL : IUsuario
    {
        private readonly string _sql;

        public UsuarioSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public IEnumerable<Usuario> ListaUsuario()
        {
            List<Usuario> temp = new List<Usuario>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Usuario()
                    {
                        idUsuario = r.GetInt32(0),
                        nombreUsuario = r.GetString(1),
                        correoUsuario = r.GetString(2),
                        clave = r.GetString(3),
                        idRol = r.GetInt32(4)

                    });
                }
                r.Close();
            }
            return temp;
        }

            public string RegistrarUsuario(Usuario usu)
            {
                throw new NotImplementedException();
            }
        }
    }


