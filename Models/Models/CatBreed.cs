using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	[Table("breeds")]
	public class CatBreed
	{
		public int Id { get; set; }
		public string breedName { get; set; }
		public string breedPic { get; set; }
		public string breedDescription { get; set; }
	}
}
