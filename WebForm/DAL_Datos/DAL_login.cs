using System;
using System.Data.SqlClient;
using System.Diagnostics;
using WebForm.Objects;

namespace WebForm.DAL_Datos
{
	public class DAL_login
	{
		private string connectionQuery = "Server=localhost;Database=Market_123;Integrated Security=True;";

		public users selectUserlogin(users us)
		{
			string query = @"SELECT ID_USE
                                 FROM USERS
                                 WHERE NAM_USE = @NAM_USE AND PAS_USE = @PAS_USE";
			Debug.WriteLine("== Ejecutando Login ==");
			Debug.WriteLine("Usuario recibido: " + us.NAM_USE);
			Debug.WriteLine("Contraseña (HASH): " + us.PAS_USE);
			using (SqlConnection conn = new SqlConnection(connectionQuery)) { 
					conn.Open();
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@NAM_USE", us.NAM_USE);
					cmd.Parameters.AddWithValue("@PAS_USE", us.PAS_USE);

					Debug.WriteLine("Query ejecutado: " + cmd.CommandText);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								int val = reader.GetInt32(0);
								Debug.WriteLine("ID encontrado: " + val);

								if (val != 0)
								{
									us.ID_USE = val;
									us.error = false;
									us.message = "Login exitoso";
								}
								else
								{
									us.error = true;
									us.message = "ID nulo o cero.";
									Debug.WriteLine("El ID era cero.");
								}
							}
						}
						else
						{
							us.error = true;
							us.message = "No existen Datos";
							Debug.WriteLine("No se encontraron filas para ese usuario/contraseña.");
						}

					}
				}
			}
				return us;
		}
	}
}