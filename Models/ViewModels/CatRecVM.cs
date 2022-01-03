using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.ViewModels
{
	[NotMapped]
	public class CatRecVM
	{

		//Models
		public Cat catRec { get; set; }
		public CatDetail detailRec { get; set; }
		public CatPersonality personalityRec { get; set; }
		public CatBreed breedRec { get; set; }
		public CatLocation locationRec { get; set; }

		//Extended view model fields

		//Edit fields
		//public string catDetail { get; set; }		
		//public string catPersonalityType { get; set; }
		//public string catBreed { get; set; }
		//public string catLocation { get; set; }

		//Drop downs and foriegn key lookup lists
		public List<CatPersonality> catPersonalityList { get; set; }
		public List<CatDetail> catDetailList { get; set; }
		public List<CatBreed> catBreedList { get; set; }
		public List<CatLocation> catLocationList { get; set; }
	}
}
