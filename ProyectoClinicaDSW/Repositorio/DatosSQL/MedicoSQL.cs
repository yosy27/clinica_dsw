using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio.DatosSQL
{
    public class MedicoSQL : IMedico
    {
        private readonly string _sql;

        public MedicoSQL()
        {
            _sql = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build()
              .GetConnectionString("cnx");
        }
        public string ActualizarMedico(Medico med)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_update_medico", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDMEDICO", med.idMedico);
                    cmd.Parameters.AddWithValue("@NOMBRE", med.nombreMedico);
                    cmd.Parameters.AddWithValue("@APELLIDO", med.apellidoMedico);
                    cmd.Parameters.AddWithValue("@DNI", med.dni);
                    cmd.Parameters.AddWithValue("@IDESPECIALIDAD", med.idEspecialidad);
                    cmd.Parameters.AddWithValue("@CONTACTO", med.contacto);
                    cmd.Parameters.AddWithValue("@HORARIOATENCION", med.horario);
   

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

        public string EliminarMedico(int idMedico)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(_sql))
            {
                try
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("usp_delete_medico", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDMEDICO", idMedico);

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

        public IEnumerable<Medico> FilterName(string nombre)
        {
            List<Medico> temp = new List<Medico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_filter_medico @NOMBRE", cn);
                cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Medico()
                    {
                        idMedico = r.GetInt32(0),
                        nombreMedico = r.GetString(1),
                        apellidoMedico = r.GetString(2),
                        dni = r.GetString(3),
                        idEspecialidad = r.GetInt32(4),
                        contacto = r.GetString(5),
                        horario = r.GetString(6)
                    });
                }
                r.Close();
            }
            return temp;
        }

        public IEnumerable<Medico> ListaMedico()
        {
            List<Medico> temp = new List<Medico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_medicos", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Medico()
                    {
                        idMedico = r.GetInt32(0),
                        nombreMedico = r.GetString(1),
                        apellidoMedico = r.GetString(2),
                        dni = r.GetString(3),
                        idEspecialidad = r.GetInt32(4),
                        contacto = r.GetString(5),
                        horario = r.GetString(6)

                    });
                }
                r.Close();
            }
            return temp;
        }

        public string RegistrarMedico(Medico med)
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
                    cmd.Parameters.AddWithValue("@APELLIDO", med.apellidoMedico);
                    cmd.Parameters.AddWithValue("@DNI", med.dni);
                    cmd.Parameters.AddWithValue("@IDESPECIALIDAD", med.idEspecialidad);
                    cmd.Parameters.AddWithValue("@CONTACTO", med.contacto);
                    cmd.Parameters.AddWithValue("@HORARIOATENCION", med.horario);
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
