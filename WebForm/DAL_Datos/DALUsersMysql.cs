using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebForm.Objects;

namespace WebForm.DAL_Datos
{
	public class DALUsersMysql
{
    string connectionString = "Server=127.0.0.1;Port=3306;Database=market1234;Uid=root;password=;";

   public List<users> GetUsersDataMysql()
	{
		List<users> lus = new List<users>();
		string query = "Select ID_USE,NAM_USE,LAS_USE,IDC_USE FROM USERS";
			using (MySqlConnection conn = new MySqlConnection(connectionString))
			{
				conn.Open();
				using (MySqlCommand cmd = new MySqlCommand(query, conn))
				{
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							lus.Add(
								new users
								{
									ID_USE = Convert.ToInt16(reader["ID_USE"].ToString()),
									NAM_USE = reader["NAM_USE"].ToString(),
									LAS_USE = reader["LAS_USE"].ToString(),
									IDC_USE = reader["IDC_USE"].ToString()
								}
							);
						}
					}
				}
			}
			return lus;
		}


		public users SaveUserDataMysql(users us)
		{
			string query = "INSERT INTO USERS (NAM_USE, LAS_USE, IDC_USE, ADD_USE, PAS_USE) VALUES (@NAM_USE, @LAS_USE, @IDC_USE, @ADD_USE, @PAS_USE)";
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@NAM_USE", us.NAM_USE);
					command.Parameters.AddWithValue("@LAS_USE", us.LAS_USE);
					command.Parameters.AddWithValue("@IDC_USE", us.IDC_USE);
					command.Parameters.AddWithValue("@ADD_USE", us.ADD_USE);
					command.Parameters.AddWithValue("@PAS_USE", us.PAS_USE);

					connection.Open();
					int rowsAffected = command.ExecuteNonQuery();
					us.error = rowsAffected <= 0;
					us.message = rowsAffected > 0 ? "Usuario guardado correctamente." : "No se insertó ningún dato.";
				}
			}
			return us;
		}

		public users UpdateUserDataMysql(users us)
		{
			string query = "UPDATE USERS SET NAM_USE=@NAM_USE, LAS_USE=@LAS_USE, IDC_USE=@IDC_USE WHERE ID_USE=@ID";
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@NAM_USE", us.NAM_USE);
					command.Parameters.AddWithValue("@LAS_USE", us.LAS_USE);
					command.Parameters.AddWithValue("@IDC_USE", us.IDC_USE);
					command.Parameters.AddWithValue("@ID", us.ID_USE);

					connection.Open();
					int rowsAffected = command.ExecuteNonQuery();
					us.message = "Filas Afectadas " + rowsAffected;
					return rowsAffected > 0 ? us : null;
				}
			}
		}

		public users DeleteUserDataMysql(users us)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				try
				{
					string deleteUser = "DELETE FROM USERS WHERE ID_USE = @ID";
					using (MySqlCommand cmd = new MySqlCommand(deleteUser, connection))
					{
						cmd.Parameters.AddWithValue("@ID", us.ID_USE);
						int rowsAffected = cmd.ExecuteNonQuery();

						us.message = "Filas afectadas en USERS: " + rowsAffected;
						us.error = rowsAffected <= 0;
						if (rowsAffected <= 0)
						{
							us.message = "No se eliminó ninguna fila en USERS.";
							return null;
						}
					}
				}
				catch (Exception ex)
				{
					us.error = true;
					us.message = "Error: " + ex.Message;
				}
				return us;
			}
		}

	}
}