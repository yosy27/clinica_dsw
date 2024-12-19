using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class CitaSQL : ICita
    {
        private readonly string _sql;

        public CitaSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public string ActualizarCita(Cita cit)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_update_cita", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDCITA", cit.idCita);
                    cmd.Parameters.AddWithValue("@IDPACIENTE", cit.idPaciente);
                    cmd.Parameters.AddWithValue("@IDMEDICO", cit.idMedico);
                    cmd.Parameters.AddWithValue("@FECHAHORA", cit.fechaHora);
                    cmd.Parameters.AddWithValue("@IDESTADO", cit.idEstado);

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha actualizado {i} cita correctamente."
                        : "Error al actualizar la cita.";
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

        public string EliminarCitaint(int idCita)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_delete_cita", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDCITA", idCita);

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha eliminado {i} cita correctamente."
                        : "Error al eliminar cita.";
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

        public IEnumerable<Cita> FilterCita(string inicial)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cita> ListaCita()
        {
            List<Cita> temp = new List<Cita>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_cita", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Cita()
                    {
                        idCita = r.GetInt32(0),
                        idPaciente = r.GetInt32(1),
                        idMedico = r.GetInt32(2),
                        fechaHora = r.GetDateTime(3),
                        idEstado = r.GetInt32(4)
                    });
                }
                r.Close();
            }
            return temp;
        }

        public string RegistrarCita(Cita cit)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_insert_medico", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NOMBRE", med.nombreMedico);
                    cmd.Parameters.AddWithValue("@DNI", med.dni);
                    cmd.Parameters.AddWithValue("@IDESPECIALIDAD", med.idEspecialidad);
                    cmd.Parameters.AddWithValue("@CONTACTO", med.contacto);
                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha insertado {i} médico correctamente."
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
