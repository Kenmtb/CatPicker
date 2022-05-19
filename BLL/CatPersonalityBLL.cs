using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using DAL.Repositories;

namespace BLL
{
    public class CatPersonalityBLL
	{				
		DAL.Repositories.CatDetailRepository<CatDetail> rep;
		public CatPersonalityBLL()
		{
			//rep = new DAL.MockRepositories.MockCatRepository<Cat>();
			rep = new DAL.Repositories.CatDetailRepository<CatDetail>();
		}

		public List<CatDetail> getAllCats()
		{			
			return rep.GetAll().ToList();			
		}

		public CatDetail getCatById(int id)
		{
			return rep.GetById(id);
		}

		public void saveCat(CatDetail catRec)
		{
			rep.Save(catRec);	
		}

		public void addCat(CatDetail catRec)
		{
			rep.Insert(catRec);
		}
	}
}
