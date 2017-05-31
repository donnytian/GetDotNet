using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Gdn.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : GdnControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}