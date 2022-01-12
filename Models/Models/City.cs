using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
	[Table("cities")]
	public class City
	{
		public int Id { get; set; }
		public int stateID { get; set; }
		public string cityName { get; set; }
	}
}
