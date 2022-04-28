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
    public class UserFormController : Controller
    {
        private KOBSUKDBEntities db = new KOBSUKDBEntities();

        // GET: UserForm
        public async Task<ActionResult> Index()
        {

            var last_id = await db.Customers.OrderByDescending(x => x.c_id).Take(1).ToListAsync();
            ViewBag.last_id = last_id.Count() > 0 ? "C" + (int.Parse(last_id[0].c_id.Remove(0, 1)) + 1).ToString().PadLeft(4, '0') : "C0001";

            return View(await db.Customers.ToListAsync());
        }


        // POST: Login/Create
        [HttpPost]
        public async Task<ActionResult> Index(SearchClass model)
        {
            var last_id = await db.Customers.OrderByDescending(x => x.c_id).Take(1).ToListAsync();
            ViewBag.last_id = last_id.Count() > 0 ? "C" + (int.Parse(last_id[0].c_id.Remove(0, 1)) + 1).ToString().PadLeft(4, '0') : "C0001";

            if (model.Search == null)
            {
                return View(await db.Customers.ToListAsync());
            }
            else
            {
                return View(await db.Customers.Where(x => x.c_name.Contains(model.Search)).ToListAsync());

            }

        }
        // GET: UserForm/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: UserForm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "c_id,c_name,c_tell")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: UserForm/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: UserForm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "c_id,c_name,c_tell")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: UserForm/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: UserForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
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
