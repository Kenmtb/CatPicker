using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	[Table("catDetails")]
	public class CatDetail
	{
		public int Id { get; set; }		
		public int catId { get; set; }
		public string description { get; set; }
	}
}
