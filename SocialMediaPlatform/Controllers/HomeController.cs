using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaPlatform.Support;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FormTwo(Models.SocialMedia model)
        {
            ViewBag.Collection = null;
            if (ModelState.IsValid)
            {
                if(model.Type.ToString() =="Youtube")
                {
                    var instance = new YoutubeService();
                    string playListID = "PLrQMMjaEqBpNTb40EHXeD_3r5Q2UyMjC2";
                    //string playListID = model.id;
                    Array testOutput = await instance.getPlayList(playListID);
                    ViewBag.Collection = testOutput;
                }

                if (model.Type.ToString() == "Spotify")
                {

                }

            }

            return View("index",model);
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