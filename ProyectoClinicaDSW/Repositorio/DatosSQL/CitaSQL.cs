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

        public string EliminarCita(int idCita)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Cita> FilterCita(string dni)
        {
            List<Cita> temp = new List<Cita>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_citasyfilter @DNI", cn);
                cmd.Parameters.AddWithValue("@DNI", dni + "%");
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Cita()
                    {
                        idCita = r.GetInt32(0),
                        nombreCita = r.GetString(1),
                        dniPaciente = r.GetString(2),
                        idPaciente = r.GetInt32(3),
                        idMedico = r.GetInt32(4),
                        fechaHora = r.GetDateTime(5),
                        idEstado = r.GetInt32(6),
                        nombreEstado = r.GetString(7)
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

                    SqlCommand cmd = new SqlCommand("usp_insert_cita", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPACIENTE", cit.idPaciente);
                    cmd.Parameters.AddWithValue("@NOMCITA",cit.nombreCita);
                    cmd.Parameters.AddWithValue("@IDMEDICO", cit.idMedico);
                    cmd.Parameters.AddWithValue("@FECHAHORA", cit.fechaHora);
                    cmd.Parameters.AddWithValue("@IDESTADO", cit.idEstado);
                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha registrado {i} cita correctamente."
                        : "No se pudo insertar la cita.";
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
