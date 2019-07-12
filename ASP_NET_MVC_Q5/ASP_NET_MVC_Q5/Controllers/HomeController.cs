using System;
using ASP_NET_MVC_Q5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;

namespace ASP_NET_MVC_Q5.Controllers
{
    public class HomeController : Controller
    {
        public List<Select> ShowResult()
        {
            string filePath = Server.MapPath("~/App_Data/data.json");
            string json = System.IO.File.ReadAllText(filePath);
            List<Select> list = JsonConvert.DeserializeObject<List<Select>>(json);
            return list;
        }
        public void Locale()
        {
            ShowResult();
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "未選擇", Value = "", Selected = true });
            selectList.Add(new SelectListItem { Text = "Unite State", Value = "US" });
            selectList.Add(new SelectListItem { Text = "Germany", Value = "DE" });
            selectList.Add(new SelectListItem { Text = "Canada", Value = "CA" });
            selectList.Add(new SelectListItem { Text = "Spain", Value = "ES" });
            selectList.Add(new SelectListItem { Text = "France", Value = "FR" });
            selectList.Add(new SelectListItem { Text = "Japen", Value = "JP" });
            ViewData["SelectLocale"] = selectList;
        }
        public List<Select> SelectData(Select select)
        {
            var list = ShowResult();
            if (!string.IsNullOrEmpty(select.Locale))
            {
                list = list.Where(x => x.Locale == select.Locale).ToList();
            }
            if (select.MaxPrice != 0)
            {
                list = list.Where(x => x.Price <= select.MaxPrice).ToList();
            }
            if (select.MiniPrice != 0)
            {
                list = list.Where(x => x.Price >= select.MiniPrice).ToList();
            }
            if (!string.IsNullOrEmpty(select.Product_Name))
            {
                list = list.Where(x => x.Product_Name.IndexOf(select.Product_Name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            if (list == null)
            {
                Console.WriteLine("查無資料");
            }

            return list;
        }

        public ActionResult Index()
        {
            Locale();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Select select)
        {
            return RedirectToAction("Result", select);
        }
        public ActionResult Result(Select select, int page=1, int pageSize=5)
        {
            var list = SelectData(select).OrderBy(x => x.Id).ToPagedList(page, pageSize);           
            return View(list);          
        }
    }
}