using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaPlatform.Support;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SocialMediaPlatform.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        //public ActionResult Index()
        {
            var instance = new YoutubeService();

            string playListID = "PLrQMMjaEqBpNTb40EHXeD_3r5Q2UyMjC2";
            Array testOutput = await instance.getPlayList(playListID);

            ViewBag.Collection = testOutput;

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