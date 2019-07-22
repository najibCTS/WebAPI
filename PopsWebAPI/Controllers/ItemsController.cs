using PopsWebAPI.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace PopsWebAPI.Controllers
{
    public class ItemsController : ApiController
    {
        private IPODbEntities db = new PODbEntities();

        public ItemsController(IPODbEntities entities)
        {
            db = entities;
        }
        // GET: api/Items
        public IQueryable<ITEM> GetITEMs()
        {
            return db.ITEMs;
        }

        // GET: api/Items/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult GetITEM(string id)
        {
            ITEM iTEM = db.ITEMs.Find(id);
            if (iTEM == null)
            {
                return NotFound();
            }

            return Ok(iTEM);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutITEM(string id, ITEM iTEM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iTEM.ITCODE)
            {
                return BadRequest();
            }

            db.MarkAsModified(iTEM);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ITEMExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult PostITEM(ITEM iTEM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ITEMs.Add(iTEM);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ITEMExists(iTEM.ITCODE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iTEM.ITCODE }, iTEM);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult DeleteITEM(string id)
        {
            ITEM iTEM = db.ITEMs.Find(id);
            if (iTEM == null)
            {
                return NotFound();
            }

            db.ITEMs.Remove(iTEM);
            db.SaveChanges();

            return Ok(iTEM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ITEMExists(string id)
        {
            return db.ITEMs.Count(e => e.ITCODE == id) > 0;
        }
    }
}