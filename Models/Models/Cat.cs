using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		//hidden
		public int Id { get; set; }
		public Nullable<int> breedId { get; set; }
		public Nullable<int> detailsId { get; set; }

		//display
		public string name { get; set; }
		public Nullable<int> age { get; set; }
		public string pic { get; set; }
		public string gender { get; set; }
		public string mainColor { get; set; }
		public string secondColor { get; set; }
		public string thirdColor { get; set; }

		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public Nullable<DateTime> arrivalDate { get; set; }		

		//[NotMapped]
		////drop down
		//public List<CatBreed> catBreeds { get; set; }
	}
}
