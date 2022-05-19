using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using Models.ViewModels;
using DAL.Repositories;

namespace BLL
{
    public class CatBLL
	{
		//DAL.MockRepositories.MockCatRepository<Cat> rep;
		CatRepository<Cat> rep;//used for updating
		CatRecVMRepository VMrep; //used for displaying 

		public CatBLL()
		{
			
			//rep = new DAL.MockRepositories.MockCatRepository<Cat>();
			rep = new CatRepository<Cat>(); //Repository used for creating, editing, saving
			VMrep = new CatRecVMRepository(); //VM Repository used for display
		}

		public CatRecVM getAllCats()
		{
			return VMrep.GetAll();
		}

		public CatRecVM getCatById(int id)
		{
			return VMrep.GetById(id);
		}

		public void saveCat(Cat Rec)
		{			
			rep.Save(Rec);	
		}

		public void addCat(Cat Rec)
		{
			rep.Insert(Rec);
		}

		public void deleteCat(int id)
		{
			rep.Delete(id);
		}

		public void test()
		{
			VMrep.Test();
		}
	}
}
