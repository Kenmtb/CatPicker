using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	[Table("catPersonality")]
	public class CatPersonality
	{
		public int Id { get; set; }
		public int personalityRank { get; set; }
		public string personalityType { get; set; }
		//public string bogus { get; set; }
	}
}
