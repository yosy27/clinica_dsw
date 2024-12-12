using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;
using System.Data;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class PacienteSQL : IPaciente
    {
        private readonly string _sql;

        public PacienteSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public string ActualizarPaciente(Paciente pac)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_update_paciente", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPACIENTE", pac.idPaciente);
                    cmd.Parameters.AddWithValue("@NOMBRE", pac.nombrePaciente);
                    cmd.Parameters.AddWithValue("@APELLIDO", pac.apellidoPaciente);
                    cmd.Parameters.AddWithValue("@CORREO", pac.correoPaciente);
                    cmd.Parameters.AddWithValue("@TELEFONO", pac.telefonoPaciente);
                    cmd.Parameters.AddWithValue("@DNI", pac.dni);

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

        public string EliminarPaciente(int idPaciente)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_delete_paciente", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPACIENTE", idPaciente);

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha eliminado {i} paciente correctamente."
                        : "Error al eliminar el paciente.";
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

        public IEnumerable<Paciente> FilterName(string nombre)
        {
            List<Paciente> temp = new List<Paciente>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_filter_name @NOMBRE", cn);
                cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Paciente()
                    {
                        idPaciente = r.GetInt32(0),
                        nombrePaciente = r.GetString(1),
                        apellidoPaciente = r.GetString(2),
                        correoPaciente = r.GetString(3),
                        telefonoPaciente = r.GetString(4),
                        dni = r.GetString(5),
                        fechaRegistro = r.GetDateTime(6)
                    });
                }
                r.Close();
            }
            return temp;
        }

        public IEnumerable<Paciente> ListaPacientes()
        {
            List<Paciente> temp = new List<Paciente>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_pacientes", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Paciente()
                    {
                        idPaciente = r.GetInt32(0),
                        nombrePaciente = r.GetString(1),
                        apellidoPaciente = r.GetString(2),
                        correoPaciente = r.GetString(3),
                        telefonoPaciente = r.GetString(4),
                        dni = r.GetString(5),
                        fechaRegistro = r.GetDateTime(6)

                    });
                }
                r.Close();
            }
            return temp;
        }


        public string RegistrarPaciente(Paciente pac)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_insert_paciente", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NOMBRE", pac.nombrePaciente);
                    cmd.Parameters.AddWithValue("@APELLIDO",pac.apellidoPaciente);
                    cmd.Parameters.AddWithValue("@CORREO", pac.correoPaciente);
                    cmd.Parameters.AddWithValue("@TELEFONO",pac.telefonoPaciente);
                    cmd.Parameters.AddWithValue("@DNI",pac.dni);

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha insertado {i} paciente correctamente."
                        : "No se pudo insertar el paciente.";
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
