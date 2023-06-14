using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;
using Models.Models;

namespace DAL.Repositories
{
	public class CatDetailVMRepository
	{
		private CatDetailsVM cdvm; //The cat view model which serves the view(s)
		private CatDetailRepository<CatDetail> detailsRep;
		private CatDetailPicsRepository<CatDetailPic> detailsPicRep;

		public CatDetailVMRepository()
		{
			//Instantiate repositories			
			detailsRep = new CatDetailRepository<CatDetail>();
			detailsPicRep = new CatDetailPicsRepository<CatDetailPic>();

			//Instantiate models
			cdvm = new CatDetailsVM();
			//get dropdowns
			cdvm.catDetailPicList = new CatDetailPicsRepository<CatDetailPic>().GetAll().ToList();			
			
		}

		public CatDetailsVM GetAll()
		{
			//cdvm.catDetailList = new CatDetailRepository<CatDetail>().GetAll().ToList();
			return cdvm;
			//throw new NotImplementedException();
		}

		public CatDetailsVM GetById(int id)
		{
			//cdvm.catDetailList = new List<CatDetail>();
			cdvm.catDetail = new CatDetail();
			cdvm.catName = new CatRepository<Cat>().GetById(id).name.ToString();
			cdvm.catId = id;

			cdvm.catDetailPicList = cdvm.catDetailPicList.FindAll(x => x.catId == id);

			int did = Convert.ToInt32(new CatRecVMRepository().GetById(id).catList[0].detailsId); //get the cat detail id						
			cdvm.catDetail = detailsRep.GetById(did);


			cdvm.catLocation = new LocationRepository<CatLocation>().GetById((int) cdvm.catDetail.locationId).locationName.ToString();
			cdvm.catPersonality = new CatPersonalityRepository<CatPersonality>().GetById(cdvm.catDetail.personalityId).personalityType.ToString();

			cdvm.stateId = cdvm.catDetail.stateId;
			cdvm.cityId = cdvm.catDetail.cityId;
			cdvm.locationId = cdvm.catDetail.locationId;
			cdvm.stateList = new StateRepository<State>().GetAll().OrderBy(x=>x.name).ToList();

			//Temp lists which will be replaced by dynamic cascade lists within the ui (javascript and ajax)
			cdvm.cityList = new CityRepository<City>().GetAll().OrderBy(x => x.cityName).ToList();
			cdvm.locationList = new LocationRepository<Location>().GetAll().OrderBy(x => x.locationName).ToList();

			return cdvm;
		}
	}
}
