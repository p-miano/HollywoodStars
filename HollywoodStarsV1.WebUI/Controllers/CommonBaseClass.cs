using System.Configuration;
using System.Web.Mvc;

namespace HollywoodStarsV1.WebUI.Controllers
{
    public class CommonBaseClass : Controller
    {
        public string conn
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["HollywoodStarsCString"].ConnectionString;
            }
        }
    }
}