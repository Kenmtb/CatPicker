using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;
using Models.Models;


namespace DAL.Repositories
{
	public class CatDetailPicVMRepository
	{
		private CatDetailPicVM cdvm;
		//private CatDetailPicsRepository<CatDetailPic> detailsPicRep;

		public CatDetailPicVMRepository()
		{
			//detailsPicRep = new CatDetailPicsRepository<CatDetailPic>();
			cdvm = new CatDetailPicVM();
			cdvm.catDetailPicList = new CatDetailPicsRepository<CatDetailPic>().GetAll().ToList();
		}

		public CatDetailPicVM GetAll()
		{			
			//cdvm.catDetailPicList = new CatDetailPicsRepository<CatDetailPic>().GetAll().ToList();
			return cdvm;			
		}

		public CatDetailPicVM GetById(int id)
		{
			cdvm.catDetailPicList =  cdvm.catDetailPicList.Where(x => x.catId == id).ToList();
			//if list is null then create blank record
			if (cdvm.catDetailPicList.Count == 0) cdvm.catDetailPicList.Add(new CatDetailPic() { catId = id,catPicDescription="",catPicUrl= "default.jpg" });
			return cdvm;
		}


	}
}
