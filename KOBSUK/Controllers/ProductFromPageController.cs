using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KOBSUK;
using KOBSUK.Models;

namespace KOBSUK.Controllers
{
    public class ProductFromPageController : Controller
    {
        private KOBSUKDBEntities db = new KOBSUKDBEntities();

        // GET: ProductFromPage
        public async Task<ActionResult> Index()
        {
            var last_id = await db.Products.OrderByDescending(x => x.t_id).Take(1).ToListAsync();
            ViewBag.last_id = last_id.Count() > 0 ? "P" + (int.Parse(last_id[0].t_id.Remove(0, 1)) + 1).ToString().PadLeft(4, '0') : "P0001";


            ViewBag.t_id = new SelectList(db.Types, "t_id", "t_name");


            var products = db.Products.Include(p => p.Type);
            return View(await products.ToListAsync());
        }


        // POST: Login/Create
        [HttpPost]
        public async Task<ActionResult> Index(SearchClass model)
        {
            var last_id = await db.Products.OrderByDescending(x => x.t_id).Take(1).ToListAsync();
            ViewBag.last_id = last_id.Count() > 0 ? "P" + (int.Parse(last_id[0].t_id.Remove(0, 1)) + 1).ToString().PadLeft(4, '0') : "P0001";


            ViewBag.t_id = new SelectList(db.Types, "t_id", "t_name");


            if (model.Search == null)
            {
                return View(await db.Products.Include(p => p.Type).ToListAsync());
            }
            else
            {
                return View(await db.Products.Include(p => p.Type).Where(x => x.p_name.Contains(model.Search)).ToListAsync());

            }

        }
        // GET: ProductFromPage/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductFromPage/Create
        public ActionResult Create()
        {
            ViewBag.t_id = new SelectList(db.Types, "t_id", "t_name");
            return View();
        }

        // POST: ProductFromPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "p_id,t_id,p_name,p_size,p_price,p_detail")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.t_id = new SelectList(db.Types, "t_id", "t_name", product.t_id);
            return View(product);
        }

        // GET: ProductFromPage/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_id = new SelectList(db.Types, "t_id", "t_name", product.t_id);
            return View(product);
        }

        // POST: ProductFromPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "p_id,t_id,p_name,p_size,p_price,p_detail")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.t_id = new SelectList(db.Types, "t_id", "t_name", product.t_id);
            return View(product);
        }

        // GET: ProductFromPage/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductFromPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
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
