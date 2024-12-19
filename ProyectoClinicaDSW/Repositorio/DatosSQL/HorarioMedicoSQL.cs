using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class HorarioMedicoSQL : IHorarioMedico
    {
        private readonly string _sql;

        public HorarioMedicoSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }

        public IEnumerable<HorarioMedico> ListaHorarioMedico()
        {
            List<HorarioMedico> temp = new List<HorarioMedico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_ListHorarioMedico", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new HorarioMedico()
                    {
                        idHorarioMedico = r.GetInt32(0),
                        idMedico = r.GetInt32(1),
                        horaInicio = r.GetTimeSpan(2),
                        horaFin = r.GetTimeSpan(3),
                        idDia = r.GetInt32(4)
                    });
                }
                r.Close();
            }

            return temp;
        }
    

        public string RegistrarHorario(HorarioMedico hor)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_insert_horario_medico", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPACIENTE", hor.idHorarioMedico);
                    cmd.Parameters.AddWithValue("@APELLIDO", hor.horaInicio);
                    cmd.Parameters.AddWithValue("@CORREO", hor.horaFin);
                    cmd.Parameters.AddWithValue("@TELEFONO", hor.idMedico);
                    cmd.Parameters.AddWithValue("@DNI", hor.idDia);

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha actualizado {i} paciente correctamente."
                        : "Error al actualizar el paciente.";
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
