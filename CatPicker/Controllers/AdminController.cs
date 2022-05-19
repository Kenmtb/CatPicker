using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatPicker.Controllers
{
    public class AdminController : Controller
    {
        BLL.StateBLL stateBll;
        BLL.CityBLL cityBLL;
        BLL.LocationBLL locationBLL;

        public AdminController()
        {
            // the terrible hack
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            stateBll = new BLL.StateBLL();
            cityBLL = new BLL.CityBLL();
            locationBLL = new BLL.LocationBLL();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public void test()
		{
            var ans = cityBLL.test("4");
		}

        public ActionResult showStates()
		{            
            return View(stateBll.getAllStates());
		}

        public ActionResult showCities()
		{
            return View(cityBLL.getAllCities());
		}

        public ActionResult showLocations()
        {
            return View(locationBLL.getAllLocations());
        }
    }
}