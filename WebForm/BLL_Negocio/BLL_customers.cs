using System.Data;
using WebForm.DAL_Datos;

namespace WebForm.BLL_Negocio
{
    public class BLL_customers
    {
        private readonly DAL_users _dal = new DAL_users();

        public DataSet GetCustomersByUser(int userId)
        {
            return _dal.GetCustomersByUser(userId);
        }
    }
}
