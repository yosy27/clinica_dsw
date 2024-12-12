using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class EspecialidadSQL : IEspecialidad
    {
        private readonly string _sql;

        public EspecialidadSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public IEnumerable<Especialidad> ListaEspecialidades()
        {
            List<Especialidad> temp = new List<Especialidad>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_especialidades", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Especialidad()
                    {
                        idEspecialidad = r.GetInt32(0),
                        nombreEspecialidad = r.GetString(1),

                    });
                }
                r.Close();
            }
            return temp;
        }
    }
}
