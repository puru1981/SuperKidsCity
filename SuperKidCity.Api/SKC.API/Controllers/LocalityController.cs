using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SKC.API.Models;
using SKC.Lib.Data.Models.Entities;

namespace SKC.API.Controllers
{
    public class LocalityController : ApiController
    {
        private ContextModel<LocalityDTO> db = new ContextModel<LocalityDTO>();

        // GET: api/Locality
        public IQueryable<LocalityModel> Get()
        {
            var result = db.Manager.Context.Get();
            var model = new List<LocalityModel>();
            var dto = MapDTOs(result);
            return dto.AsQueryable();
        }

        public IQueryable<LocalityModel> Get(int id, bool citywise)
        {
            var result = db.Manager.Context.Get().Where(l=>l.CityId == id).ToList();
            var model = new List<LocalityModel>();
            var dto = MapDTOs(result);
            return dto.AsQueryable();
        }

        // GET: api/Locality/5
        [ResponseType(typeof(LocalityModel))]
        public async Task<IHttpActionResult> Get(int id)
        {
            LocalityModel localityModel = new LocalityModel();
             var result = await db.Manager.Context.GetByIdAsync(id);
             localityModel = new LocalityModel
             {
                 Active = result.Active,
                 UpdatedAtUTC = result.UpdatedAt,
                 Name = result.Name,
                 Id = result.Id,
                 Code = result.Code,
                 CreatedAtUTC = result.CreatedAt,
                 Deleted = result.Deleted,
                 CityId = result.CityId
             };
            if (localityModel == null)
            {
                return NotFound();
            }

            return Ok(localityModel);
        }

        //// PUT: api/Locality/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutLocalityModel(int id, LocalityModel localityModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != localityModel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(localityModel).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LocalityModelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Locality
        //[ResponseType(typeof(LocalityModel))]
        //public async Task<IHttpActionResult> PostLocalityModel(LocalityModel localityModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.LocalityModels.Add(localityModel);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = localityModel.Id }, localityModel);
        //}

        //// DELETE: api/Locality/5
        //[ResponseType(typeof(LocalityModel))]
        //public async Task<IHttpActionResult> DeleteLocalityModel(int id)
        //{
        //    LocalityModel localityModel = await db.LocalityModels.FindAsync(id);
        //    if (localityModel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.LocalityModels.Remove(localityModel);
        //    await db.SaveChangesAsync();

        //    return Ok(localityModel);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool LocalityModelExists(int id)
        //{
        //    return db.LocalityModels.Count(e => e.Id == id) > 0;
        //}

        private static List<LocalityModel> MapDTOs(IList<LocalityDTO> result)
        {
            var dto = result.Select(r => new LocalityModel
            {
                Active = r.Active,
                Deleted = r.Deleted,
                Code = r.Code,
                CreatedAtUTC = r.CreatedAt,
                Id = r.Id,
                Name = r.Name,
                UpdatedAtUTC = r.UpdatedAt,
                CityId = r.CityId,
            }).ToList();
            return dto;
        }
    }
}