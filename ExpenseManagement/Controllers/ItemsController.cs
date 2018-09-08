using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpenseManagement.Models;

namespace ExpenseManagement.Controllers
{
    public class ItemsController : Controller
    {
        private ExpenseTrackerEntities db = new ExpenseTrackerEntities();

        // GET: Items
        public ActionResult Index()
        {                       
            var items = db.Items.Include(i => i.Category).Include(i => i.User);
            var user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var item = db.Items.Where(x => x.UserId == user.UserId).ToList();
            var itemChartArray = item.ToArray();

            decimal FoodSum = items.Where(cat => cat.ItemCategory == "Food").Select(cat => cat.ItemAmount).Sum();
            decimal HealthSum = items.Where(cat => cat.ItemCategory == "Health").Select(cat => cat.ItemAmount).Sum();
            //Nullable<decimal> TravelSum = items.Where(cat => cat.ItemCategory == "Travel").Select(cat => cat.ItemAmount).Sum();
            //Nullable<decimal> ShoppingSum = items.Where(cat => cat.ItemCategory == "Shopping").Select(cat => cat.ItemAmount).Sum();

            int[] datax = new int[itemChartArray.Length];
            for (int i=0; i< itemChartArray.Length; i++)
            {
               datax[i] = itemChartArray[i].ItemAmount;
            }

            string[] datayTemp = new string[itemChartArray.Length];
            for (int i = 0; i < itemChartArray.Length; i++)
            {
                datayTemp[i] = itemChartArray[i].ItemCategory;
            }

            string[] datay = datayTemp.Distinct().ToArray();

            return View(item);
        }

        public ActionResult MonthlyReport()
        {            
            var items = db.Items.Include(i => i.Category).Include(i => i.User);
            var user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var item = db.Items.Where(x => x.UserId == user.UserId);
            var item2 = item.ToList();
            var itemChartArray = item.ToArray();

            // Dictionary<string, decimal> dictMonthlySum = new Dictionary<string, decimal>();

            //decimal[] dictMonthlySum = new decimal[itemChartArray.Length];

            
            //decimal FoodSum = items.Where(cat => cat.ItemCategory == "Food").Select(cat => cat.ItemAmount).Sum();
            //decimal HealthSum = items.Where(cat => cat.ItemCategory == "Health").Select(cat => cat.ItemAmount).Sum();
            //decimal TravelSum = items.Where(cat => cat.ItemCategory == "Travel").Select(cat => cat.ItemAmount).Sum();
            //decimal ShoppingSum = items.Where(cat => cat.ItemCategory == "Shopping").Select(cat => cat.ItemAmount).Sum();

            //dictMonthlySum.Add(FoodSum);
            //dictMonthlySum.Add(ShoppingSum);
            //dictMonthlySum.Add(TravelSum);
            //dictMonthlySum.Add(HealthSum);

            int[] datax = new int[itemChartArray.Length];
            for (int i = 0; i < itemChartArray.Length; i++)
            {
                datax[i] = itemChartArray[i].ItemAmount;
            }

            string[] datayTemp = new string[itemChartArray.Length];
            for (int i = 0; i < itemChartArray.Length; i++)
            {
                datayTemp[i] = itemChartArray[i].ItemCategory;
            }

         

            //string[] datay = datayTemp.Distinct().ToArray();

            //decimal[] data = new decimal[4];

            //data[0] = 0;
            //data[0] = 0;
            //data[0] = 0;
            //data[0] = 0;

            var itemFiltered = item2.Where(cat => cat.ExpeseDate > System.DateTime.Now.AddMonths(-1)).AsQueryable();

            var hhh = itemFiltered.Select(e => e.ItemCategory).ToArray().Distinct().ToList();
            var ggg = itemFiltered.GroupBy(e => e.ItemCategory).Select(e => e.Sum(s => s.ItemAmount) ).ToList();

            //data[0] = itemFiltered.Where(cat => cat.ItemCategory == "Food").Select(cat => cat.ItemAmount).Sum();                   
            //data[1] = itemFiltered.Where(cat => cat.ItemCategory == "Health" ).Select(cat => cat.ItemAmount).Sum();
            //data[2] = itemFiltered.Where(cat => cat.ItemCategory == "Shopping" ).Select(cat => cat.ItemAmount).Sum();
            //data[3] = itemFiltered.Where(cat => cat.ItemCategory == "Travel" ).Select(cat => cat.ItemAmount).Sum();          
            
            //data.Add(items.Where(cat => cat.ItemCategory == "Food").Select(cat => cat.ItemAmount).Sum());
            //data.Add(items.Where(cat => cat.ItemCategory == "Health").Select(cat => cat.ItemAmount).Sum());         
            //data.Add(items.Where(cat => cat.ItemCategory == "Shopping").Select(cat => cat.ItemAmount).Sum());
            //data.Add(items.Where(cat => cat.ItemCategory == "Travel").Select(cat => cat.ItemAmount).Sum());

            ViewBag.dataForX = hhh;
            ViewBag.dataForY = ggg;
            return View();
        }

        public ActionResult WeeklyReport()
        {
            var items = db.Items.Include(i => i.Category).Include(i => i.User);
            var user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var item = db.Items.Where(x => x.UserId == user.UserId);
            var item2 = item.ToList();
            var itemChartArray = item.ToArray();

            var itemFiltered = item2.Where(cat => cat.ExpeseDate > System.DateTime.Now.AddDays(-7)).AsQueryable();

            var hhh = itemFiltered.Select(e => e.ItemCategory).ToArray().Distinct().ToList();
            var ggg = itemFiltered.GroupBy(e => e.ItemCategory).Select(e => e.Sum(s => s.ItemAmount)).ToList();

            ViewBag.dataForX = hhh;
            ViewBag.dataForY = ggg;

            return View();
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemName,ItemCategory,ItemAmount,ExpeseDate,CategoryId,UserId")] Item item)
        {
            if (ModelState.IsValid)
            {
              //  var  = User.Identity.Name;

                item.UserId =  db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().UserId;
                item.ItemCategory =  db.Categories.Where(x => x.CategoryID == item.CategoryId).FirstOrDefault().CategoryName;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName", item.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", item.UserId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName", item.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", item.UserId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemName,ItemCategory,ItemAmount,ExpeseDate,CategoryId,UserId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryID", "CategoryName", item.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", item.UserId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
