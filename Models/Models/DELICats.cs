using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
	public interface ICats
	{
		int Id { get; set; }
		string name { get; set; }
		 Nullable<int> breedId { get; set; }
		 Nullable<int> locationId { get; set; }
		 Nullable<int> catDetailsId { get; set; }
		 Nullable<int> age { get; set; }
		 string pic { get; set; }
		 string gender { get; set; }
		 string mainColor { get; set; }
		 string secondColor { get; set; }
		 string thirdColor { get; set; }
		 Nullable<double> weight { get; set; }
		 Nullable<DateTime> arrivalDate { get; set; }
	}
}
