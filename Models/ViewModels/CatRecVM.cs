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
	
		//Record list
		public List<Cat> catList { get; set; }
		
		//Foriegn fields
		public string breedName { get; set; }
		public int breedId { get; set; }

		//Drop downs and foriegn key lookup lists
		public List<CatBreed> catBreedList { get; set; }
		public List<Gender> genderList { get; set; }
		public List<CatColor> catColorList { get; set; }		
	}
}
