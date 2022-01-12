using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	[Table("cats")]
	public class Cat 
	{
		public int Id { get; set; }
		public string name { get; set; }
		public Nullable<int> breedId { get; set; }
		public int locationId { get; set; }
		//public Nullable<int> catDetailsId { get; set; }
		public Nullable<int> catPersonalityId { get; set; }							 
		public Nullable<int> age { get; set; }
		public string pic { get; set; }
		public string gender { get; set; }
		public string mainColor { get; set; }
		public string secondColor { get; set; }
		public string thirdColor { get; set; }
		public Nullable<double> weight { get; set; }
		public Nullable<DateTime> arrivalDate { get; set; }
	}
}
