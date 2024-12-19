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

        public string ActualizarMedico(HorarioMedico hor)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_update_horario_medico", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDHORARIO", hor.idHorarioMedico);
                    cmd.Parameters.AddWithValue("@HORAINICIO", hor.horaInicio);
                    cmd.Parameters.AddWithValue("@HORAFIN", hor.horaFin);
                    cmd.Parameters.AddWithValue("@IDDIA", hor.idDia);
                    cmd.Parameters.AddWithValue("@IDMEDICO", hor.idMedico);
      

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

        public string EliminarHorario(int idHorarioMedico)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_delete_medico", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDHORARIO", idHorarioMedico);

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha eliminado {i} horario correctamente."
                        : "Error al eliminar el horario.";
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

        public IEnumerable<HorarioMedico> FilterHorario(string nomMedico)
        {
            List<HorarioMedico> temp = new List<HorarioMedico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_horarioandfilter @NOMMEDICO", cn);
                cmd.Parameters.AddWithValue("@NOMMEDICO", nomMedico + "%");
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new HorarioMedico()
                    {
                        idHorarioMedico = r.GetInt32(0),
                        horaInicio = r.GetTimeSpan(1),
                        horaFin = r.GetTimeSpan(2),
                        idDia = r.GetInt32(3),
                        nombreDia = r.GetString(4),
                        idMedico = r.GetInt32(5),
                        nombreMedico = r.GetString(6)

                    });
                }
                r.Close();
            }
            return temp;
        }

        public IEnumerable<HorarioMedico> ListaHorarioMedico()
        {
            List<HorarioMedico> temp = new List<HorarioMedico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_horario", cn);
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

                    cmd.Parameters.AddWithValue("@HORAINICIO", hor.horaInicio);
                    cmd.Parameters.AddWithValue("@HORAFIN", hor.horaFin);
                    cmd.Parameters.AddWithValue("@IDDIA", hor.idDia);
                    cmd.Parameters.AddWithValue("@IDMEDICO", hor.idMedico);

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
