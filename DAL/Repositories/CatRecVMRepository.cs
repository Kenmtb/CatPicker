using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;
using Models.Models;


namespace DAL.Repositories
{

	public class CatRecVMRepository 
	{
		//private Contexts.CatsContext catContext = null;
		//private Contexts.CatPersonalityContext catPersonalityContext = null;

		private CatRecVM cvm;

		

		private CatRepository<Cat> catRep;
		private CatDetailsRepository<CatDetail> detailsRep;
		private CatPersonalityRepository<CatPersonality> personalityRep;
		private CatBreedRepository<CatBreed> breedRep;
		
		public CatRecVMRepository()
		{

			//Instantiate repositories

			

			catRep = new CatRepository<Cat>();
			detailsRep = new CatDetailsRepository<CatDetail>();
			personalityRep = new CatPersonalityRepository<CatPersonality>();
			breedRep = new CatBreedRepository<CatBreed>();

			//Instantiate models
			cvm = new CatRecVM();
			
			cvm.personalityRec = new CatPersonality();
			cvm.breedRec = new CatBreed();
			cvm.detailRec = new CatDetail();
			cvm.locationRec = new CatLocation();
		}

		void Delete(object id)
		{
			throw new NotImplementedException();
		}

		public CatRecVM GetAll()
		{
			throw new NotImplementedException();
		}

		public CatRecVM GetById(int id)
		{
			//Get a new cat record object (id=-1) or and existing cat record object
			cvm.catRec = catRep.GetById(id);			

			//Get drop list data
			cvm.catPersonalityList = personalityRep.GetAll().ToList();
			cvm.catBreedList = breedRep.GetAll().ToList();
			cvm.catDetailList = detailsRep.GetAll().ToList();
			
			if ((int)id != -1)
			{
				//Fill the data object for drop lists and defaults - Tables that are forigen keys of main table (catRec)
				if (cvm.catRec.catPersonalityId != null) cvm.personalityRec.personalityType = personalityRep.GetById(cvm.catRec.catPersonalityId).personalityType.ToString();				
				
				if (cvm.catRec.breedId != null) cvm.breedRec.breedName = breedRep.GetById(cvm.catRec.breedId).breedName.ToString();

				//Fill the data object for drop lists and defaults - Tables where the main table (catRec) is a foriegn key			
				cvm.detailRec = cvm.catDetailList.FirstOrDefault(x => x.catId == cvm.catRec.Id);
			}
			else
			{
				//Set new obj defaults
				cvm.catRec = new Cat();
				cvm.catRec.pic = "default.jpg";
				cvm.catRec.mainColor = "N/A";
				cvm.catRec.secondColor = "N/A";
				cvm.catRec.thirdColor = "N/A";
				cvm.catRec.arrivalDate = DateTime.Now;
			}
			return cvm;
		}

		//New record
		public void Insert(CatRecVM obj)
		{
			//Prepare a new cat record for editing - Get the drop list and default items.
			//obj =  GetById(-1);

			//Insert cat record
			catRep.Insert(obj.catRec);

			

			//**Handle foriegn key tables
			//Also insert the foriegn table details. This is because details data is in a separate table.
			obj.detailRec.catId = catRep.getLastCatRecordID();
			
			//Update the foriegn key table (Master insert triggered creating foriegn key table entry for master ID but we need to store the master's data via update			
			detailsRep.Insert(obj.detailRec);
			
			
		}

		//Update record
		public void Save(CatRecVM obj)
		{
			//Preserve the foriegn keys that are not part of the UI
			obj.catRec.catDetailsId = obj.detailRec.Id;
			//obj.catRec.locationId = obj.locationRec.Id;

			catRep.Save(obj.catRec);
			detailsRep.Save(obj.detailRec);
			
		}

		public void Delete(int id)
		{
			catRep.Delete(id);
		}

		//public void Delete(object id)
		//{
		//	T existing = table.Find(id);
		//	table.Remove(existing);
		//}

	}

}
