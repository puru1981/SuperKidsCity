using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SKC.Lib.Data.Models.Entities;
using SuperKidCity.Models;

namespace SuperKidCity.Areas.API.Controllers
{
    [Authorize]
    public class SchoolApiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: API/School
        public ActionResult Index()
        {
            return View(db.Schools.ToList());
        }

        // GET: API/School/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolDTO schoolDTO = await db.Schools.FindAsync(id);
            if (schoolDTO == null)
            {
                return HttpNotFound();
            }
            return View(schoolDTO);
        }

        // GET: API/School/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: API/School/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,RegistrationNumber,EstablishedAt,WebUrl,ShortBio,UpdatedAt,CreatedAt,UserId,Deleted,Active,SessionId")] SchoolDTO schoolDTO)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(schoolDTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(schoolDTO);
        }

        // GET: API/School/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolDTO schoolDTO = await db.Schools.FindAsync(id);
            if (schoolDTO == null)
            {
                return HttpNotFound();
            }
            return View(schoolDTO);
        }

        // POST: API/School/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,RegistrationNumber,EstablishedAt,WebUrl,ShortBio,UpdatedAt,CreatedAt,UserId,Deleted,Active,SessionId")] SchoolDTO schoolDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolDTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolDTO);
        }

        // GET: API/School/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolDTO schoolDTO = await db.Schools.FindAsync(id);
            if (schoolDTO == null)
            {
                return HttpNotFound();
            }
            return View(schoolDTO);
        }

        // POST: API/School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolDTO schoolDTO = await db.Schools.FindAsync(id);
            db.Schools.Remove(schoolDTO);
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
