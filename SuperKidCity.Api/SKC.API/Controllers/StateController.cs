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
using SKC.Lib.Service;

namespace SKC.API.Controllers
{
    public class StateController : ApiController
    {
        private ContextModel<StateDTO> db = new ContextModel<StateDTO>();
        
        // GET: api/State
        public IQueryable<StateModel> Get()
        {
            var result = db.Manager.Context.Get();
            var model = new List<StateModel>();
            var dto = MapDTOs(result);
            return dto.AsQueryable();
        }

       

        // GET: api/State/5
        [ResponseType(typeof(StateModel))]
        public async Task<IHttpActionResult> Get(int id)
        {
            StateModel stateModel = new StateModel();
            var result = db.Manager.Context.GetById(id);

            stateModel = new StateModel
            {
                Active = result.Active,
                UpdatedAtUTC = result.UpdatedAt,
                Name = result.Name,
                Id = result.Id,
                Code = result.Code,
                CreatedAtUTC = result.CreatedAt,
                Deleted = result.Deleted
            };

            if (stateModel == null)
            {
                return NotFound();
            }

            return Ok(stateModel);
        }

        //// PUT: api/State/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutStateModel(int id, StateModel stateModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != stateModel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(stateModel).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StateModelExists(id))
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

        //// POST: api/State
        //[ResponseType(typeof(StateModel))]
        //public async Task<IHttpActionResult> PostStateModel(StateModel stateModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.StateModels.Add(stateModel);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = stateModel.Id }, stateModel);
        //}

        //// DELETE: api/State/5
        //[ResponseType(typeof(StateModel))]
        //public async Task<IHttpActionResult> DeleteStateModel(int id)
        //{
        //    StateModel stateModel = await db.StateModels.FindAsync(id);
        //    if (stateModel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.StateModels.Remove(stateModel);
        //    await db.SaveChangesAsync();

        //    return Ok(stateModel);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool StateModelExists(int id)
        //{
        //    return db.StateModels.Count(e => e.Id == id) > 0;
        //}

        private static List<StateModel> MapDTOs(IList<StateDTO> result)
        {
            var dto = result.Select(r => new StateModel
            {
                Active = r.Active,
                Deleted = r.Deleted,
                Code = r.Code,
                CreatedAtUTC = r.CreatedAt,
                Id = r.Id,
                Name = r.Name,
                UpdatedAtUTC = r.UpdatedAt
            }).ToList();
            return dto;
        }
    }
}