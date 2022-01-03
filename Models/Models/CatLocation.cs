using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public class CatLocation
	{
		public int Id { get; set; }
		public string locationName { get; set; }
		public int cityId { get; set; }
		public int stateId { get; set; }
	}
}
