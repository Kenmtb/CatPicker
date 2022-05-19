using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.ViewModels
{
	public class CascadeStateCityLocationVM
	{
		State state { get; set; }
		City city { get; set; }
		Location location { get; set; }

		//static drop down
		List<State> stateList { get; set; }
	}
}
