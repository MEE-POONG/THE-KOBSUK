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
    public class ProductTypeFormPageController : Controller
    {
        private KOBSUKDBEntities db = new KOBSUKDBEntities();

        // GET: ProductTypeFormPage
        public async Task<ActionResult> Index(string search)
        {
            var last_id = await db.Types.OrderByDescending(x => x.t_id).Take(1).ToListAsync();

            ViewBag.last_id = "T" + (int.Parse(last_id[0].t_id.Remove(0, 1)) + 1).ToString().PadLeft(4, '0');
            if (search == null)
            {
                return View(await db.Types.ToListAsync());
            } 
            else
            {
                return View(await db.Types.Where(x => x.t_name.Contains(search)).ToListAsync());
            }
        }


        // POST: Login/Create
        [HttpPost]
        public async Task<ActionResult> Index(SearchClass model)
        {
            var last_id = await db.Types.OrderByDescending(x => x.t_id).Take(1).ToListAsync();

            ViewBag.last_id = "T" + (int.Parse(last_id[0].t_id.Remove(0, 1)) + 1).ToString().PadLeft(4, '0');
            if (model.Search == null)
            {
                return View(await db.Types.ToListAsync());
            }
            else
            {
                return View(await db.Types.Where(x => x.t_name.Contains(model.Search)).ToListAsync());

            }

        }

        // GET: ProductTypeFormPage/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type type = await db.Types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: ProductTypeFormPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypeFormPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "t_id,t_name")] Type type)
        {
            if (ModelState.IsValid)
            {
                db.Types.Add(type);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(type);
        }

        // GET: ProductTypeFormPage/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type type = await db.Types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: ProductTypeFormPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "t_id,t_name")] Type type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // GET: ProductTypeFormPage/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type type = await db.Types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: ProductTypeFormPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Type type = await db.Types.FindAsync(id);
            db.Types.Remove(type);
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
