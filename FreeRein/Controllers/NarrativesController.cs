using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FreeRein.Models;

namespace FreeRein.Controllers
{
    public class NarrativesController : ApiController
    {
        private FreeReinContext db = new FreeReinContext();

        // GET api/Narratives
        public IQueryable<Narrative> GetNarratives()
        {
            return db.Narratives;
        }

        // GET api/Narratives/5
        [ResponseType(typeof(Narrative))]
        public IHttpActionResult GetNarrative(int id)
        {
            Narrative narrative = db.Narratives.Find(id);
            if (narrative == null)
            {
                return NotFound();
            }

            return Ok(narrative);
        }

        // PUT api/Narratives/5
        public IHttpActionResult PutNarrative(int id, Narrative narrative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != narrative.ID)
            {
                return BadRequest();
            }

            db.Entry(narrative).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarrativeExists(id))
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

        // POST api/Narratives
        [ResponseType(typeof(Narrative))]
        public IHttpActionResult PostNarrative(Narrative narrative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Narratives.Add(narrative);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = narrative.ID }, narrative);
        }

        // DELETE api/Narratives/5
        [ResponseType(typeof(Narrative))]
        public IHttpActionResult DeleteNarrative(int id)
        {
            Narrative narrative = db.Narratives.Find(id);
            if (narrative == null)
            {
                return NotFound();
            }

            db.Narratives.Remove(narrative);
            db.SaveChanges();

            return Ok(narrative);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NarrativeExists(int id)
        {
            return db.Narratives.Count(e => e.ID == id) > 0;
        }
    }
}