using System.Web.Mvc;
using System.Web.Routing;

namespace Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("scripts", "scripts", "~/Scripts");
            routes.MapPageRoute("content", "content", "~/Content");
            routes.MapRoute("phone", "phone/{action}", new {Controller = "Phone", action = "Index"});
            routes.MapRoute("login", "home/{action}", new {Controller = "Home", action = "Login"});
            routes.MapRoute("logout", "home/{action}", new {Controller = "Home", action = "Logout"});
            routes.MapRoute("Default", "{controller}/{action}", new {Controller = "Home", action="Index"});            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}