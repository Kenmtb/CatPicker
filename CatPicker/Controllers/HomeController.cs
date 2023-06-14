using System.Collections.Generic;
using System.Web.Mvc;
using Models.Models;
using Models.ViewModels;
using BLL;
using System;

//Todo: Add cascading dropdowns for State, city, location

namespace CatPicker.Controllers
{
	public class HomeController : Controller
	{
		
		CatBLL catBLL;
		CatDetailsBLL catDetailsBLL;
		CatDetailPicBLL CatDetailPicBLL;

		public HomeController()
		{
			

			// the terrible hack
			var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;


			catBLL = new CatBLL();
			catDetailsBLL = new CatDetailsBLL();
			CatDetailPicBLL = new CatDetailPicBLL();

		}

		//
		//public ActionResult Index()
		//{			
		//	return View();
		//}

		public List<string> Index()
		{

			catBLL.test();
			return null;
		}

		//*****Display
		public ActionResult showAllCats()
		{			
			var RecList = catBLL.getAllCats();
			return View(catBLL.getAllCats());
		}


		public ActionResult getCat(int id)
		{ 
			var VMRec = catBLL.getCatById(id);			
			return View("editCat",VMRec);
		}


		public ActionResult showCatDetails(int id)
		{
			var VMRec =  catDetailsBLL.getById(id);
			return View("showCatDetails", VMRec);
		}

		public ActionResult showCatDetailPics (int id)
		{
			var VMRec = CatDetailPicBLL.getById(id);
			return View("showCatDetailPics", VMRec);
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
			var Cat = catBLL.getCatById(id);
			TempData["VM"] = Cat;
			return View("editCat", Cat);
		}

		[ActionName("editCat"), HttpPost]
		public ActionResult editCatPost(CatRecVM Rec)
		{
			
			if (ModelState.IsValid)
			{
				try
				{
					catBLL.saveCat(Rec.catList[0]);
				}
				catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
				{
					TempData["msg"] = "Error saving file. Please check for missing or incorrect form entries.";
					Rec = (CatRecVM)TempData["VM"];
					TempData["VM"] = Rec;
					return View(Rec);
				}
				catch (System.IO.IOException)
				{
					TempData["msg"] = "Error saving file. The file save could not be completed.";
					Rec = (CatRecVM)TempData["VM"];
					TempData["VM"] = Rec;
					return View(Rec);
				}
				
				return RedirectToAction("showAllCats", "Home");
				//return RedirectToAction("saveCatConfirmationMDL", "Home",catRec);
			}
			else
			{
				Rec = (CatRecVM)TempData["VM"];
				TempData["VM"] = Rec;
				return View(Rec);
			}
		}

		public ActionResult saveCat(Cat Rec)
		{			
			if (ModelState.IsValid)
			{
				catBLL.saveCat(Rec);
				return RedirectToAction("showAllCats", "Home");
			}
			else
			{
				return View(Rec);
			}
		}


		//*****Create
		[ActionName("showNewCat"), HttpGet]
		public ActionResult showNewCatGet()
		{

			//bll.test();

			//Get a new record with list and default items
			CatRecVM rec;
			rec =  catBLL.getCatById(-1);
			TempData["VM"] = rec;
			return View(rec);
		}

		[ActionName("showNewCat"), HttpPost]
		public ActionResult showNewCatPost(CatRecVM Rec)
		
		{
			try
			{				
				catBLL.addCat(Rec.catList[0]);
			}
			catch (Globals.CustomException ex)
			{				
				TempData["msg"] = ex.Message;
				Rec.catList[0] = (Cat)TempData["VM"];
				TempData["VM"] = Rec.catList[0];				
				return View("showNewCat", Rec);
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
			catBLL.deleteCat(id);
			return RedirectToAction("showAllCats", "Home");
		}

		//********Maintenance 
		[ActionName("editCatDetails"), HttpGet]
		public ActionResult editCatDetailsGet(int id)
		{
			var VMRec = catDetailsBLL.getById(id);
			TempData["VM"] = VMRec;
			return View("editCatDetails", VMRec);
		}

		[ActionName("editCatDetails"), HttpPost]
		public ActionResult editCatDetailsPost(CatDetailsVM VMRec)
		{
			
			return null;
		}


		//********Dropdowns
		public ActionResult getCityList (int id)
		{
			// Move this code to a generic <T> DAL utility?
			var cityList = new CityBLL().getCitiesByStateId(id);			
			return Json(cityList, JsonRequestBehavior.AllowGet);
		}

		public ActionResult getLocationLIst (int id)
		{
			//var locationList = new LocationBLL().
			return null;
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