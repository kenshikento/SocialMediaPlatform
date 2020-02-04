using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaPlatform.Support;
using System.Threading.Tasks;

namespace SocialMediaPlatform.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            System.Diagnostics.Debug.WriteLine(123);
            var instance = new YoutubeService();

            // Dummy playlist 
            string playListID = "PLH5ogLzrBuBVi4S14Bmwf3VBLc4FfzljG";
            object hello = await instance.getPlayList(playListID); 

            System.Diagnostics.Debug.WriteLine(instance);
            return View();

        }

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