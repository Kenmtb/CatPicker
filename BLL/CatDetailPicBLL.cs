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
	public class CatDetailPicBLL
	{
		CatDetailPicsRepository<CatDetailPic> rep;//used for updating
		CatDetailPicVMRepository VMrep; //used for displaying 

		public CatDetailPicBLL()
		{

			//rep = new DAL.MockRepositories.MockCatRepository<Cat>();
			rep = new CatDetailPicsRepository<CatDetailPic>(); //Repository used for creating, editing, saving
			VMrep = new CatDetailPicVMRepository(); //VM Repository used for display
		}

		public CatDetailPicVM getAll()
		{
			return VMrep.GetAll();
		}

		public CatDetailPicVM getById(int id)
		{
			return VMrep.GetById(id);
		}

	}
}
