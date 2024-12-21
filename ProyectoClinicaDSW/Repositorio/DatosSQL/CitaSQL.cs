using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;
using System.Data;

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

        public IEnumerable<Cita> FilterCita(string nombre)
        {
            List<Cita> temp = new List<Cita>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_citayfilter @NOMBRE", cn);
                cmd.Parameters.AddWithValue("@NOMBRE", nombre + "%");
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Cita()
                    {
                        idCita = r.GetInt32(0),
                        nombreCita = r.GetString(1),
                        idPaciente = r.GetInt32(2),
                        dniPaciente = r.GetString(3),
                        nombrePaciente = r.GetString(4),
                        idMedico = r.GetInt32(5),
                        nombreMedico = r.GetString(6),
                        fechaHora = r.GetDateTime(7),
                        idEstado = r.GetInt32(8),
                        nombreEstado = r.GetString(9)
                    });
                }
                r.Close();
            }
            return temp;
        }

        public string RegistrarCita(CitaReg citReg)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_insert_cita", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPACIENTE", citReg.idPaciente);
                    cmd.Parameters.AddWithValue("@NOMCITA", citReg.nombreCita);
                    cmd.Parameters.AddWithValue("@IDMEDICO", citReg.idMedico);
                    cmd.Parameters.AddWithValue("@FECHAHORA", citReg.fechaHora);
                    cmd.Parameters.AddWithValue("@IDESTADO", citReg.idEstado);
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
