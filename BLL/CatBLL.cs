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
		//CatRepository<Cat> rep;
		CatRecVMRepository VMrep;

		public CatBLL()
		{
			
			//rep = new DAL.MockRepositories.MockCatRepository<Cat>();
			//rep = new CatRepository<Cat>();
			VMrep = new CatRecVMRepository();
		}

		public List<Cat> getAllCats()
		{
			return VMrep.GetAll().ToList();
		}

		public CatRecVM getCatById(int id)
		{
			return VMrep.GetById(id);
		}

		public void saveCat(CatRecVM catRec)
		{			
			VMrep.Save(catRec);	
		}

		public void addCat(CatRecVM catVMRec)
		{
			VMrep.Insert(catVMRec);
		}

		public void deleteCat(int id)
		{
			VMrep.Delete(id);
		}
	}
}
