using PopsWebAPI.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace PopsWebAPI.Controllers
{
    public class PodetailsController : ApiController
    {
        private IPODbEntities db = new PODbEntities();
        public PodetailsController(IPODbEntities entities)
        {
            db = entities;
        }

        // GET: api/Podetails
        public IQueryable<PODETAIL> GetPODETAILs()
        {
            return db.PODETAILs;
        }

        // GET: api/Podetails/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult GetPODETAIL(string pono, string itcode)
        {
            PODETAIL pODETAIL = db.PODETAILs.Find(pono, itcode);
            if (pODETAIL == null)
            {
                return NotFound();
            }

            return Ok(pODETAIL);
        }

        // PUT: api/Podetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPODETAIL(string pono, string itcode, PODETAIL pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pono != pODETAIL.PONO && itcode!=pODETAIL.ITCODE)
            {
                return BadRequest();
            }

            db.MarkAsModified(pODETAIL);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PODETAILExists(pono, itcode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Podetails
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult PostPODETAIL(PODETAIL pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PODETAILs.Add(pODETAIL);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PODETAILExists(pODETAIL.PONO, pODETAIL.ITCODE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pODETAIL.PONO }, pODETAIL);
        }

        // DELETE: api/Podetails/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult DeletePODETAIL(string pono, string itcode)
        {
            PODETAIL pODETAIL = db.PODETAILs.Find(pono, itcode);
            if (pODETAIL == null)
            {
                return NotFound();
            }

            db.PODETAILs.Remove(pODETAIL);
            db.SaveChanges();

            return Ok(pODETAIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PODETAILExists(string pono, string itcode)
        {
            return db.PODETAILs.Count(e => e.PONO == pono && e.ITCODE==itcode) > 0;
        }
    }
}