using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Objects
{
	public class users
	{
		public int ID_USE { get; set; }              // Clave primaria (no nulo)
		public string NAM_USE { get; set; }          // Nombre (no nulo)
		public string LAS_USE { get; set; }          // Apellido (no nulo)
		public string IDC_USE { get; set; }          // Cédula (puede ser nula)
		public string ADD_USE { get; set; }  

		public string PAS_USE { get; set; }
		public bool error { get; set; }

		public string message { get; set; }


	}
}