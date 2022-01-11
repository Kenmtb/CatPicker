using System.Collections.Generic;
using System.Web.Mvc;
using Models.Models;
using Models.ViewModels;
using BLL;
using System;


namespace CatPicker.Controllers
{
	public class HomeController : Controller
	{
		
		BLL.CatBLL bll;

		public HomeController()
		{
			// the terrible hack
			var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
			bll = new BLL.CatBLL();
		}

		//
		//public ActionResult Index()
		//{			
		//	return View();
		//}

		public List<string> Index()
		{
			return new List<string>()
			{
				"aaa",
				"bbb",
				"ccc"
			};
		}

		//*****Display
		public ActionResult showAllCats()
		{
			var RecList = bll.getAllCats();
			return View(bll.getAllCats());
		}


		public ActionResult getCat(int id)
		{ 
			var VMRec = bll.getCatById(id);			
			return View("editCat",VMRec);
		}


		//[ActionName("editCat"), HttpGet]
		//public ActionResult editCatGet(CatRecVM catRec)
		//{
		//	TempData["Mode"] = "Save";
		//	return View(catRec);
		//}


		//*****Edit
		[ActionName("editCat"), HttpGet]
		public ActionResult editCatGet(int id)
		{			
			var VMRec = bll.getCatById(id);
			TempData["VM"] = VMRec;
			return View("editCat", VMRec);
		}

		[ActionName("editCat"), HttpPost]
		public ActionResult editCatPost(CatRecVM vmRec)
		{
			
			if (ModelState.IsValid)
			{
				try
				{
					bll.saveCat(vmRec);
				}
				catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
				{
					TempData["msg"] = "Error saving file. Please check for missing or incorrect form entries.";
					vmRec = (CatRecVM)TempData["VM"];
					TempData["VM"] = vmRec;
					return View(vmRec);
				}
				catch (System.IO.IOException)
				{
					TempData["msg"] = "Error saving file. The file save could not be completed.";
					vmRec = (CatRecVM)TempData["VM"];
					TempData["VM"] = vmRec;
					return View(vmRec);
				}
				
				return RedirectToAction("showAllCats", "Home");
				//return RedirectToAction("saveCatConfirmationMDL", "Home",catRec);
			}
			else
			{
				vmRec = (CatRecVM)TempData["VM"];
				TempData["VM"] = vmRec;
				return View(vmRec);
			}
		}

		public ActionResult saveCat(CatRecVM VMRec)
		{			
			if (ModelState.IsValid)
			{
				bll.saveCat(VMRec);
				return RedirectToAction("showAllCats", "Home");
			}
			else
			{
				return View(VMRec);
			}
		}


		//*****Create
		[ActionName("showNewCat"), HttpGet]
		public ActionResult showNewCatGet()
		{
			//Get a new record with list and default items
			CatRecVM VMRec;
			VMRec =  bll.getCatById(-1);
			TempData["VM"] = VMRec;
			return View(VMRec);
		}

		[ActionName("showNewCat"), HttpPost]
		public ActionResult showNewCatPost(CatRecVM VMRec)
		
		{
			try
			{				
				bll.addCat(VMRec);
			}
			catch (Globals.CustomException ex)
			{				
				TempData["msg"] = ex.Message;
				VMRec = (CatRecVM)TempData["VM"];
				TempData["VM"] = VMRec;				
				return View("showNewCat", VMRec);
				//return RedirectToAction("showNewCat", "Home");
			}
			//New cat record save successful
			return RedirectToAction("showAllCats", "Home");
		}

		public ActionResult saveCatConfirmationMDL(Cat Rec)
		{
			return View(Rec);
		}

		public ActionResult deleteCat(int id)
		{
			bll.deleteCat(id);
			return RedirectToAction("showAllCats", "Home");
		}
		

		//********Utilities
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}