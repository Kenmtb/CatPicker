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
			return View(bll.getAllCats());
		}


		public ActionResult getCat(int id)
		{ 
			var catRec = bll.getCatById(id);			
			return View("editCat",catRec);
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
			var catRec = bll.getCatById(id);
			TempData["VM"] = catRec;
			return View("editCat", catRec);
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
				return View(vmRec);
			}
		}

		public ActionResult saveCat(CatRecVM catRec)
		{			
			if (ModelState.IsValid)
			{
				bll.saveCat(catRec);
				return RedirectToAction("showAllCats", "Home");
			}
			else
			{
				return View(catRec);
			}
		}


		//*****Create
		[ActionName("showNewCat"), HttpGet]
		public ActionResult showNewCatGet()
		{
			//Get a new record with list and default items
			CatRecVM newCatRec;
			newCatRec =  bll.getCatById(-1);
			TempData["VM"] = newCatRec;
			return View(newCatRec);
		}

		[ActionName("showNewCat"), HttpPost]
		public ActionResult showNewCatPost(CatRecVM catVMRec)
		
		{
			try
			{				
				bll.addCat(catVMRec);
			}
			catch (Globals.CustomException ex)
			{				
				TempData["msg"] = ex.Message;
				catVMRec = (CatRecVM)TempData["VM"];
				TempData["VM"] = catVMRec;				
				return View("showNewCat", catVMRec);
				//return RedirectToAction("showNewCat", "Home");
			}
			//New cat record save successful
			return RedirectToAction("showAllCats", "Home");
		}

		public ActionResult saveCatConfirmationMDL(Cat catRec)
		{
			return View(catRec);
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