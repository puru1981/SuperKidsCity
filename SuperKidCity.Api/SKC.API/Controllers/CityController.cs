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
using System.Web.Http.Cors;

namespace SKC.API.Controllers
{
    public class CityController : ApiController
    {
        private ContextModel<CityDTO> db = new ContextModel<CityDTO>();

        // GET: api/City
        public IQueryable<CityModel> Get()
        {
            var result = db.Manager.Context.Get();
            var model = new List<CityModel>();
            var dto = MapDTOs(result);
            return dto.AsQueryable();
        }

        // GET: api/City/5
        [ResponseType(typeof(CityModel))]
        [EnableCors(origins:"http://localhost:49681", headers:"*", methods:"GET", SupportsCredentials=true)]
        public async Task<IQueryable<CityModel>> GetByState(int id, bool stateWise)
        {
            CityModel cityModel = new CityModel();
            var data = await db.Manager.Context.GetAsync();
            var result = data.Where(c => c.StateId == id).ToList();
            var model = MapDTOs(result);
            return model.AsQueryable();
        }

        // GET: api/City/5
        [ResponseType(typeof(CityModel))]
        public async Task<IHttpActionResult> Get(int id)
        {
            CityModel cityModel = new CityModel();
            var result = await db.Manager.Context.GetByIdAsync(id);
            cityModel = new CityModel
            {
                Active = result.Active,
                UpdatedAtUTC = result.UpdatedAt,
                Name = result.Name,
                Id = result.Id,
                Code = result.Code,
                CreatedAtUTC = result.CreatedAt,
                Deleted = result.Deleted,
                StateId = result.StateId,
                Localities = result.Localities.Count
            };
            if (cityModel == null)
            {
                return NotFound();
            }

            return Ok(cityModel);
        }

        //// PUT: api/City/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutCityModel(int id, CityModel cityModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != cityModel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(cityModel).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityModelExists(id))
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

        //// POST: api/City
        //[ResponseType(typeof(CityModel))]
        //public async Task<IHttpActionResult> PostCityModel(CityModel cityModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.CityModels.Add(cityModel);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = cityModel.Id }, cityModel);
        //}

        //// DELETE: api/City/5
        //[ResponseType(typeof(CityModel))]
        //public async Task<IHttpActionResult> DeleteCityModel(int id)
        //{
        //    CityModel cityModel = await db.CityModels.FindAsync(id);
        //    if (cityModel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.CityModels.Remove(cityModel);
        //    await db.SaveChangesAsync();

        //    return Ok(cityModel);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool CityModelExists(int id)
        //{
        //    return db.CityModels.Count(e => e.Id == id) > 0;
        //}

        private static List<CityModel> MapDTOs(IList<CityDTO> result)
        {
            var dto = result.Select(r => new CityModel
            {
                Active = r.Active,
                Deleted = r.Deleted,
                Code = r.Code,
                CreatedAtUTC = r.CreatedAt,
                Id = r.Id,
                Name = r.Name,
                UpdatedAtUTC = r.UpdatedAt,
                StateId = r.StateId,
                Localities = r.Localities.Count
            }).ToList();
            return dto;
        }
    }
}