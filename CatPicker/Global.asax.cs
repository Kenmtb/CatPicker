//using DAL.Contexts;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CatPicker
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			//Code below added to disconnect database migration tracking so changes to the context can be done without errors.
			//Database.SetInitializer<CatPersonalityContext>(new DropCreateDatabaseIfModelChanges<CatPersonalityContext>());
		}
	}
}
