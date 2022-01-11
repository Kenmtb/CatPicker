using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
	[Table("states")]
	public class State
	{
		public int Id { get; set; }
		public string name { get; set; }
	}
}
