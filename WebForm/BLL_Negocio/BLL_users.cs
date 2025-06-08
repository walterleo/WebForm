using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebForm.Objects;
using WebForm.DAL_Datos;
using WebForm.Presentation;



namespace WebForm.BLL_Negocio
{
	public class BLL_users
	{

		private readonly DAL_users _dal = new DAL_users();

		public List<users> GetAll()
						=> _dal.GetListUserData();

		public List<users> SearchByCedula(string prefix)
				=> _dal
						.GetListUserData()
						.Where(u => u.IDC_USE.StartsWith(prefix))
						.ToList();


		public users SaveNewUSers(users us)
		{
			BLL_Negocio.BLL_General blg = new BLL_General();
			us.PAS_USE = blg.GetHashSHA256(us.PAS_USE);
			DAL_Datos.DAL_users dlu = new DAL_Datos.DAL_users();
			us = dlu.SaveUserDataBase(us);

			return us;

		}

		public List<users> GetListUsers()
		{
			DAL_Datos.DAL_users dlu = new DAL_Datos.DAL_users();

			return dlu.GetListUserData();
		}

		public users UpdateUser(users us)
		{
			us.NAM_USE = us.NAM_USE.ToUpper();
			us.LAS_USE = us.LAS_USE.ToUpper();

			DAL_Datos.DAL_users dlu = new DAL_users();
			return dlu.UpdateUserData(us);
		}

		public users DeleteUser(users us)
		{
			DAL_users dlu = new DAL_users();
			return dlu.DeleteUserData(us);
		}

		



	}
}