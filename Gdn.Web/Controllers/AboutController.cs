using System.Web.Mvc;

namespace Gdn.Web.Controllers
{
    public class AboutController : GdnControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}