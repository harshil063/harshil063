using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using studentmvc.Models;

namespace studentmvc.Controllers
{
    public class tblproductsController : Controller
    {
        private exampracticeEntities2 db = new exampracticeEntities2();

        // GET: tblproducts
        //public ActionResult Index()
        //{
        //    var tblproducts = db.tblproducts.Include(t => t.tblcategory);
        //    return View(tblproducts.ToList());
        //}

        public async Task<ActionResult> Index(string searchstring)
        {
            var prods = from p in db.tblproducts select p;
            if (!String.IsNullOrEmpty(searchstring))
            {
                prods = prods.Where(s => s.tblcategory.cat_name.Contains(searchstring));

                ViewBag.emptyerr = "not found result";

            }
            return View(await prods.ToListAsync());
        }

        // GET: tblproducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproduct = db.tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // GET: tblproducts/Create
        public ActionResult Create()
        {
            ViewBag.cat_id = new SelectList(db.tblcategories, "cat_id", "cat_name");
            return View();
        }

        // POST: tblproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prod_id,prod_name,cat_id")] tblproduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                db.tblproducts.Add(tblproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_id = new SelectList(db.tblcategories, "cat_id", "cat_name", tblproduct.cat_id);
            return View(tblproduct);
        }

        // GET: tblproducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproduct = db.tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_id = new SelectList(db.tblcategories, "cat_id", "cat_name", tblproduct.cat_id);
            return View(tblproduct);
        }

        // POST: tblproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prod_id,prod_name,cat_id")] tblproduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_id = new SelectList(db.tblcategories, "cat_id", "cat_name", tblproduct.cat_id);
            return View(tblproduct);
        }

        // GET: tblproducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproduct = db.tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // POST: tblproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblproduct tblproduct = db.tblproducts.Find(id);
            db.tblproducts.Remove(tblproduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
