using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;
using System.Security.Claims;

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
                        Nombre = r.GetString(1),
                        correoUsuario = r.GetString(2),
                        clave = r.GetString(3)

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

        public async Task<Usuario?> Login(Usuario usu)
        {
            Usuario? usuario = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_sql))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_validar_usuario", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = usu.correoUsuario;
                        cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = usu.clave;

                        await con.OpenAsync();
                        using (var dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                usuario = new Usuario
                                {
                                    idUsuario = dr.GetInt32(dr.GetOrdinal("IdUsuario")),
                                    Nombre = dr.GetString(dr.GetOrdinal("Nombre")), 
                                    correoUsuario = dr.GetString(dr.GetOrdinal("Correo")),
                                    clave = dr.GetString(dr.GetOrdinal("Clave"))
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al validar usuario", ex);
            }
            return usuario;
        }



    }
}


