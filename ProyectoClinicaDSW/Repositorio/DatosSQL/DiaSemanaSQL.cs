using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class DiaSemanaSQL : IDiaSemana
    {
        private readonly string _sql;

        public DiaSemanaSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public IEnumerable<DiaSemana> ListaDiaSemana()
        {
            List<DiaSemana> temp = new List<DiaSemana>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_diaSemana", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new DiaSemana()
                    {
                        idDia = r.GetInt32(0),
                        nombreDia = r.GetString(1),

                    });
                }
                r.Close();
            }
            return temp;
        }
    }
}
