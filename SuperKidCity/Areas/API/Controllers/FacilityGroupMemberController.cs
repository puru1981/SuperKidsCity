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
    public class FacilityGroupMemberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: API/FacilityGroupMember
        public ActionResult Index()
        {
            return View(db.FacilityGroupMembers.ToList());
        }

        // GET: API/FacilityGroupMember/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityGroupMemberDTO facilityGroupMemberDTO = await db.FacilityGroupMembers.FindAsync(id);
            if (facilityGroupMemberDTO == null)
            {
                return HttpNotFound();
            }
            return View(facilityGroupMemberDTO);
        }

        // GET: API/FacilityGroupMember/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.FacilityGroups.Where(fg => !fg.Deleted).OrderBy(fg => fg.Id).Select(fg => new SelectListItem()
            {
                Text = fg.GroupName,
                Value = fg.Id.ToString()
            });
            return View();
        }

        // POST: API/FacilityGroupMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,ValueType,Required,Type,Active,GroupId")] FacilityGroupMemberDTO facilityGroupMemberDTO)
        {
            if (ModelState.IsValid)
            {
                facilityGroupMemberDTO.Group = db.FacilityGroups.Find(new[] { facilityGroupMemberDTO.GroupId });

                db.FacilityGroupMembers.Add(facilityGroupMemberDTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(facilityGroupMemberDTO);
        }

        // GET: API/FacilityGroupMember/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityGroupMemberDTO facilityGroupMemberDTO = await db.FacilityGroupMembers.FindAsync(id);
            if (facilityGroupMemberDTO == null)
            {
                return HttpNotFound();
            }
            return View(facilityGroupMemberDTO);
        }

        // POST: API/FacilityGroupMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ValueType,GUID,Required,Type,UpdatedAt,CreatedAt,UserId,Deleted,Active,SessionId")] FacilityGroupMemberDTO facilityGroupMemberDTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilityGroupMemberDTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(facilityGroupMemberDTO);
        }

        // GET: API/FacilityGroupMember/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityGroupMemberDTO facilityGroupMemberDTO = await db.FacilityGroupMembers.FindAsync(id);
            if (facilityGroupMemberDTO == null)
            {
                return HttpNotFound();
            }
            return View(facilityGroupMemberDTO);
        }

        // POST: API/FacilityGroupMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FacilityGroupMemberDTO facilityGroupMemberDTO = await db.FacilityGroupMembers.FindAsync(id);
            db.FacilityGroupMembers.Remove(facilityGroupMemberDTO);
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
