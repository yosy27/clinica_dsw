using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class EstadoSQL : IEstados
    {
        private readonly string _sql;

        public EstadoSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public IEnumerable<Estado> ListaEstados()
        {
            List<Estado> temp = new List<Estado>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_estados", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Estado()
                    {
                        idEstado = r.GetInt32(0),
                        nombreEstado = r.GetString(1)
                    });
                }
                r.Close();
            }
            return temp;
        }
    }
}
