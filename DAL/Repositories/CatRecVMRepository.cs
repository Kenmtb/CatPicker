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
		private CatRecVM cvm; //The cat view model which serves the view(s)		
		private CatRepository<Cat> catRep;
		private CatDetailRepository<CatDetail> detailsRep;

		public CatRecVMRepository()
		{			
			//Instantiate repositories
			catRep = new CatRepository<Cat>();
			detailsRep = new CatDetailRepository<CatDetail>();
	
			//Instantiate models
			cvm = new CatRecVM();
			cvm.catBreedList = new CatBreedRepository<CatBreed>().GetAll().ToList();
		}
		
		public CatRecVM GetAll()
		{
			cvm.catList = catRep.GetAll().ToList();//get main list			
			return cvm;			
			//throw new NotImplementedEDetailon();
		}

		public CatRecVM GetById(int id)
		{			
			cvm.catList = new List<Cat>();
			cvm.catList.Add(catRep.GetById(id));			
			return cvm;
		}

	
		public void Test()
		{
			//This shows how to perform a sql join of 2 tables from different repositories

			//string str = "Select description from dbo.catDetails where catId = " + 35;
			//var dt = detailsRep.getDataObject(str);
			//string brd;
			//if (dt.Rows.Count>0)
			//	brd = dt.Rows[0].ItemArray[0].ToString();

			//	List< Cat > cl = new List<Cat>();
			//cl = catRep.GetAll().ToList();

			//List<CatDetail> dl = new List<CatDetail>();
			//dl = detailsRep.GetAll().ToList();

			//int? did = dl.First(x => x.Id == 6).catId;
			//string catname = cl.First(x => x.Id == did).name;
		}
	}

}
