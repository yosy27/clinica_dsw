using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class HorarioSQL : IHorario
    {
        private readonly string _sql;

        public HorarioSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public IEnumerable<Horario> ListaHorario()
        {
            List<Horario> temp = new List<Horario>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("u", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Horario()
                    {
                        idHorario = r.GetInt32(0),
                        IdDia = r.GetInt32(1),
                        horaInicio = r.GetTimeSpan(2),
                        horaFin = r.GetTimeSpan(3)

                    });
                }
                r.Close();
            }
            return temp;
        }

        public string RegistrarHorario(Horario hor)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_insert_horario", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DIAID", hor.IdDia);
                    cmd.Parameters.AddWithValue("@HORAINICIO", hor.horaInicio);
                    cmd.Parameters.AddWithValue("@HORARIOFIN", hor.horaFin);
                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha insertado {i} horario correctamente."
                        : "No se pudo insertar el médico.";
                }
                catch (SqlException ex)
                {
                    mensaje = $"Error SQL: {ex.Message}";
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }
    }
}
