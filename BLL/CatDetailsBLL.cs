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
    public class CatDetailsBLL
	{				
		CatDetailRepository<CatDetail> rep;
		CatDetailVMRepository VMRep;
		
		public CatDetailsBLL()
		{			
			rep = new CatDetailRepository<CatDetail>();
			VMRep = new CatDetailVMRepository();			
		}

		public CatDetailsVM getAll()
		{			
			return VMRep.GetAll();			
		}

		public CatDetailsVM getById(int id)
		{
			return VMRep.GetById(id);
		}

		public void save(CatDetail Rec)
		{
			rep.Save(Rec);	
		}

		public void add(CatDetail Rec)
		{
			rep.Insert(Rec);
		}
	}
}
