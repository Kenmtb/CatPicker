using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Models;

namespace Models.ViewModels
{	
	public class CatDetailsVM
	{		
		//Record list
		public List<CatDetail> catDetailList { get; set; }
		public CatDetail catDetail { get; set; }

		//Foriegn fields
		public int catId { get; set; }
		public string catName { get; set; }
		public string catLocation { get; set; }
		public string catPersonality { get; set; }

		//dropdown foriegn keys
		public int? stateId { get; set; }
		public int? cityId { get; set; }
		public int? locationId { get; set; }

		//Drop downs and foriegn key lookup lists
		public List<CatDetailPic> catDetailPicList { get; set; }
		public List<CatLocation> catLocationList { get; set; }
		public List<CatPersonality> catPersonalityList { get; set; }
		//static drop down
		public  List<State> stateList { get; set; }
		public List<City> cityList { get; set; }
		public List<Location> locationList { get; set; }
	}
}
