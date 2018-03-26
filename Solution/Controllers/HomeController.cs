using Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Solution.Controllers
{
    public class HomeController : Controller
    {
        //Db connection 
        private SfiDbEntities db = new SfiDbEntities();

        public ActionResult Index()
        {
            ViewBag.Url = (from s in db.StartPageMovies
                          orderby s.Id descending
                          select s.URL).FirstOrDefault();
            var getSegments = (from s in db.Segments
                               orderby s.Name
                               select s).ToList();

            return View(getSegments);
        }

        public ActionResult Category(string segment)
        {
            try
            {
                int id = int.Parse(segment);
                var viewCategory = (from c in db.Categories
                                    join s in db.Segments
                                    on c.Segment_ID equals s.Id
                                    where c.Segment_ID == id
                                    select new JoinModelCategory { categories = c, segment = s }).ToList();
                if (viewCategory.Count() > 0)
                {
                    return View(viewCategory);
                }
               return RedirectToAction("Error","Home");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        //Gå till subkategori-sida
        public ActionResult SubCategory(string category)
        {
            try
            {
                int id = int.Parse(category);
                var viewSubCategory = (from s in db.SubCategories
                                       where s.Category_ID == id
                                       select s).ToList();
                if (viewSubCategory.Count() > 0)
                {
                    return View(viewSubCategory);
                }
                else if (viewSubCategory.Count() <= 0)
                {
                    return RedirectToAction("Assignment", "Home", new { SubCategory = id });
                }

                return RedirectToAction("Error", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
        //View all assignmentss
        public ActionResult Assignment(string SubCategory)
        {
            try { 
                int id = int.Parse(SubCategory);
                var getAssignments = GetAssignments(id);
           
                //var getAssignments = RadomAssignment(id);
                if (getAssignments.Count() > 0)
                {
                    foreach (var item in getAssignments)
                    {
                        if (item.Answer_One != null)
                        {
                            item.Answer_One = Regex.Replace(item.Answer_One, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Answer_One = Regex.Replace(item.Answer_One, @"\)", "</span>");
                        }
                        if (item.Answer_Two != null)
                        {
                            item.Answer_Two = Regex.Replace(item.Answer_Two, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Answer_Two = Regex.Replace(item.Answer_Two, @"\)", "</span>");
                        }
                        if (item.Answer_Three != null)
                        {
                            item.Answer_Three = Regex.Replace(item.Answer_Three, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Answer_Three = Regex.Replace(item.Answer_Three, @"\)", "</span>");
                        }
                        if (item.Answer_Four != null)
                        {
                            item.Answer_Four = Regex.Replace(item.Answer_Four, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Answer_Four = Regex.Replace(item.Answer_Four, @"\)", "</span>");
                        }
                        if (item.Answer_Five != null)
                        {
                            item.Answer_Five = Regex.Replace(item.Answer_Five, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Answer_Five = Regex.Replace(item.Answer_Five, @"\)", "</span>");
                        }
                        if (item.Answer_Six != null)
                        {
                            item.Answer_Six = Regex.Replace(item.Answer_Six, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Answer_Six = Regex.Replace(item.Answer_Six, @"\)", "</span>");
                        }
                        if (item.Correct_Answer != null)
                        {
                            item.Correct_Answer = Regex.Replace(item.Correct_Answer, @"\(", "<span style=\"text-decoration: underline; text-decoration-color: red;\">");
                            item.Correct_Answer = Regex.Replace(item.Correct_Answer, @"\)", "</span>");
                        }
                    }
                    return View(getAssignments);
                }
                return RedirectToAction("Error", "Home");
                    
            }
            //Return error
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        //Just return view of error for global
        public ActionResult Error()
        {
            return View();
        }
        public List<Assignment> GetAssignments(int id)
        {
            List<Assignment> getAssignments = new List<Models.Assignment>();
            var check = (from s in db.Assignments
                         where s.SubCategories_ID == id
                         select s.SubCategories_ID).ToList();
            if (check.Count() == 0)
            {
                var getDoing = (from a in db.Assignments
                                where a.Assignment_Type == "Svara rätt"
                                && a.Categories_ID == id
                                orderby Guid.NewGuid()
                                select a).Take(6).ToList();
                var getHearing = (from a in db.Assignments
                                  where a.Assignment_Type == "Spela in"
                                  && a.Categories_ID == id
                                  orderby Guid.NewGuid()
                                  select a).Take(4).ToList();
                getAssignments.AddRange(getDoing);
                getAssignments.AddRange(getHearing);
            }
            else
            {
                var getDoing = (from a in db.Assignments
                                   where a.Assignment_Type == "Svara rätt"
                                   && a.SubCategories_ID == id
                                   orderby Guid.NewGuid()
                                   select a).Take(6).ToList();
                var getHearing = (from a in db.Assignments
                                where a.Assignment_Type == "Spela in"
                                && a.SubCategories_ID == id
                                orderby Guid.NewGuid()
                                select a).Take(4).ToList();
                getAssignments.AddRange(getDoing);
                getAssignments.AddRange(getHearing);
            }
            return getAssignments;
        }
    }
}