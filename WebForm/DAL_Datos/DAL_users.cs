using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebForm.Objects;

namespace WebForm.DAL_Datos
{
	public class DAL_users
	{
		private string connectionQuery = "Server=localhost;Database=Market_123;Integrated Security=True;";

		public users SaveUserDataBase(users us)
		{
			string query = "INSERT INTO USERS (NAM_USE, LAS_USE, IDC_USE, ADD_USE, PAS_USE) " +
												 "VALUES (@NAM_USE, @LAS_USE, @IDC_USE, @ADD_USE, @PAS_USE)";

			using (SqlConnection connection = new SqlConnection(connectionQuery))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@NAM_USE", us.NAM_USE);
					command.Parameters.AddWithValue("@LAS_USE", us.LAS_USE);
					command.Parameters.AddWithValue("@IDC_USE", us.IDC_USE);
					command.Parameters.AddWithValue("@ADD_USE", us.ADD_USE);
					command.Parameters.AddWithValue("@PAS_USE", us.PAS_USE);

					//try
					//{
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							us.error = false;
							us.message = "Usuario guardado correctamente.";
						}
						else
						{
							us.error = true;
							us.message = "No se insertó ningún dato.";
						}
					//}
					//catch (Exception ex)
					//{
					//	us.error = true;
					//	us.message = "Error al guardar usuario: " + ex.Message;
					//}
				}
			}

			return us;
		}

		public List<users> GetListUserData()
		{
			List<users> lus = new List<users>();
			string query = "Select ID_USE, NAM_USE, LAS_USE, IDC_USE FROM USERS";
			using (SqlConnection connection = new SqlConnection(connectionQuery))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read()) // <- Aquí está el bucle necesario
						{
							lus.Add(new users
							{
								ID_USE = Convert.ToInt16(reader["ID_USE"].ToString()),
								NAM_USE = reader["NAM_USE"].ToString(),
								LAS_USE = reader["LAS_USE"].ToString(),
								IDC_USE = reader["IDC_USE"].ToString()
							});
						}
					}
				}
			}
			return lus;
		}

		public users UpdateUserData(users us)
		{
			string query = "UPDATE USERS SET NAM_USE=@NAM_USE, LAS_USE=@LAS_USE, IDC_USE=@IDC_USE WHERE ID_USE=@ID";

			using (SqlConnection connection = new SqlConnection(connectionQuery))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@NAM_USE", us.NAM_USE);
					command.Parameters.AddWithValue("@LAS_USE", us.LAS_USE);
					command.Parameters.AddWithValue("@IDC_USE", us.IDC_USE);
					command.Parameters.AddWithValue("@ID", us.ID_USE);

					connection.Open();
					int rowsAffected = command.ExecuteNonQuery();
          us.message = "Filas Afectadas " + rowsAffected;
					connection.Close();

					// Devuelve el objeto actualizado si la actualización fue exitosa
					if (rowsAffected > 0)
					{
						return us;

					}
					else
					{
						return null;
					}
				}
			}
		}


		public users DeleteUserData(users us)
		{
			using (SqlConnection connection = new SqlConnection(connectionQuery))
			{
				connection.Open();

				try
				{
					// Eliminar ventas (SALES) relacionadas con los productos del usuario
					string deleteSales = @"
                DELETE FROM SALES 
                WHERE ID_PRO_FK IN (SELECT ID_PRO FROM PRODUCTS WHERE ID_USE_FK = @ID)";
					using (SqlCommand cmdDeleteSales = new SqlCommand(deleteSales, connection))
					{
						cmdDeleteSales.Parameters.AddWithValue("@ID", us.ID_USE);
						cmdDeleteSales.ExecuteNonQuery();
					}

					// 2️⃣ Eliminar productos (PRODUCTS) relacionados con el usuario
					string deleteProducts = "DELETE FROM PRODUCTS WHERE ID_USE_FK = @ID";
					using (SqlCommand cmdDeleteProducts = new SqlCommand(deleteProducts, connection))
					{
						cmdDeleteProducts.Parameters.AddWithValue("@ID", us.ID_USE);
						cmdDeleteProducts.ExecuteNonQuery();
					}

					// 3️⃣ Eliminar clientes (CUSTOMERS) relacionados con el usuario
					string deleteCustomers = "DELETE FROM CUSTOMERS WHERE ID_USE_FK = @ID";
					using (SqlCommand cmdDeleteCustomers = new SqlCommand(deleteCustomers, connection))
					{
						cmdDeleteCustomers.Parameters.AddWithValue("@ID", us.ID_USE);
						cmdDeleteCustomers.ExecuteNonQuery();
					}

					// 4️⃣ Finalmente, eliminar el usuario (USERS)
					string deleteUser = "DELETE FROM USERS WHERE ID_USE = @ID";
					using (SqlCommand cmdDeleteUser = new SqlCommand(deleteUser, connection))
					{
						cmdDeleteUser.Parameters.AddWithValue("@ID", us.ID_USE);
						int rowsAffected = cmdDeleteUser.ExecuteNonQuery();

						us.message = "Filas afectadas en USERS: " + rowsAffected;

						if (rowsAffected > 0)
						{
							us.error = false;
						}
						else
						{
							us.error = true;
							us.message = "No se eliminó ninguna fila en USERS.";
							us = null;
						}
					}
				}
				catch (Exception ex)
				{
					us.error = true;
					us.message = "Error: " + ex.Message;
				}
				finally
				{
					connection.Close();
				}
			}

			return us;
		}
                public System.Data.DataTable GetCustomersByUser(int userId)
                {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        string query = "SELECT NAM_CUS, LAS_CUS, IDC_CUS FROM CUSTOMERS WHERE ID_USE_FK = @UserId";

                        using (SqlConnection connection = new SqlConnection(connectionQuery))
                        using (SqlCommand command = new SqlCommand(query, connection))
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                                command.Parameters.AddWithValue("@UserId", userId);
                                adapter.Fill(dt);
                        }

                        return dt;
                }




	}
}