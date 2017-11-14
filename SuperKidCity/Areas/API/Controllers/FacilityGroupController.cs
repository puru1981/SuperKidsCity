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
    public class FacilityGroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: API/FacilityGroup
        public ActionResult Index()
        {
            return View(db.FacilityGroups.ToList());
        }

        // GET: API/FacilityGroup/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityGroupDTO facilityGroupDTO = await db.FacilityGroups.FindAsync(id);
            if (facilityGroupDTO == null)
            {
                return HttpNotFound();
            }
            return View(facilityGroupDTO);
        }

        // GET: API/FacilityGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: API/FacilityGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GroupName,UpdatedAt,CreatedAt,UserId,Deleted,Active,SessionId")] FacilityGroupDTO facilityGroupDTO)
        {
            if (ModelState.IsValid)
            {
                facilityGroupDTO.SessionId = Request.RequestContext.HttpContext.Session.SessionID;
                db.FacilityGroups.Add(facilityGroupDTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(facilityGroupDTO);
        }

        // GET: API/FacilityGroup/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityGroupDTO facilityGroupDTO = await db.FacilityGroups.FindAsync(id);
            if (facilityGroupDTO == null)
            {
                return HttpNotFound();
            }
            return View(facilityGroupDTO);
        }

        // POST: API/FacilityGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GroupName,UpdatedAt,CreatedAt,UserId,Deleted,Active,SessionId")] FacilityGroupDTO facilityGroupDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilityGroupDTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(facilityGroupDTO);
        }

        // GET: API/FacilityGroup/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityGroupDTO facilityGroupDTO = await db.FacilityGroups.FindAsync(id);
            if (facilityGroupDTO == null)
            {
                return HttpNotFound();
            }
            return View(facilityGroupDTO);
        }

        // POST: API/FacilityGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FacilityGroupDTO facilityGroupDTO = await db.FacilityGroups.FindAsync(id);
            db.FacilityGroups.Remove(facilityGroupDTO);
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
