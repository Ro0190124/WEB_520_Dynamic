using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
	public class UserViewModel
	{
		
		public NGUOI_DUNG SingleUser { get; set; } 
		public IEnumerable<NGUOI_DUNG>? UserList { get; set; }
	}
}
