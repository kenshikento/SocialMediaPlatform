﻿using System;
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
            System.Diagnostics.Debug.WriteLine(123);
            var instance = new YoutubeService();

            string playListID = "PLrQMMjaEqBpNTb40EHXeD_3r5Q2UyMjC2";
            object hello = await instance.getPlayList(playListID);

            System.Diagnostics.Debug.WriteLine(hello);
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