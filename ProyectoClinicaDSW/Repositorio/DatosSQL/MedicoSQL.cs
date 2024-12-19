using Microsoft.Data.SqlClient;
using ProyectoClinicaDSW.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

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
                    cmd.Parameters.AddWithValue("@DNI", med.dni);
                    cmd.Parameters.AddWithValue("@IDESPECIALIDAD", med.idEspecialidad);
                    cmd.Parameters.AddWithValue("@CONTACTO", med.contacto);   

                    int i = cmd.ExecuteNonQuery();

                    mensaje = i > 0
                        ? $"Se ha actualizado {i} medico correctamente."
                        : "Error al actualizar el medico.";
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

        public IEnumerable<Medico> FilterMedico(string inicial)
        {
            List<Medico> temp = new List<Medico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_get_medicoandfilter @INICIAL", cn);
                cmd.Parameters.AddWithValue("@INICIAL", inicial +"%");
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Medico()
                    {
                        idMedico = r.GetInt32(0),
                        nombreMedico = r.GetString(1),
                        dni = r.GetString(2),
                        idEspecialidad = r.GetInt32(3),
                        contacto = r.GetString(4)
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
                        dni = r.GetString(2),
                        idEspecialidad = r.GetInt32(3),
                        contacto = r.GetString(4)
                    });
                }
                r.Close();
            }
            return temp;
        }

        public IEnumerable<Medico> ListMedicoCb()
        {
            List<Medico> temp = new List<Medico>();
            using (SqlConnection cn = new SqlConnection(_sql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_listar_medicos", cn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    temp.Add(new Medico()
                    {
                        idMedico = r.GetInt32(0),
                        nombreMedico = r.GetString(1)
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
