using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
	[Table("location")]
	public class Location
	{
		public int Id { get; set; }
		public int cityId { get; set; }
		public string locationName { get; set; }
	}
}
