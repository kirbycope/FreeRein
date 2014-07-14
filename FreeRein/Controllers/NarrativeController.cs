using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using FreeRein.Models;

namespace FreeRein.Controllers
{
    public class NarrativeController : Controller
    {
        private FreeReinContext db = new FreeReinContext();

        // GET: /Narrative/
        [Authorize]
        public ActionResult Index(int? StoryID)
        {
            if ((StoryID != null) && (StoryID != 0))
            {
                return View(db.Narratives.Find((int)StoryID));
            }
            else
            {
                if (db.Narratives.FirstOrDefault() != null)
                {
                    return View(db.Narratives.FirstOrDefault());
                }
                else
                {
                    return View("Create");
                }
            }
        }

        // GET: /Narrative/Next
        [Authorize]
        public ActionResult Next(int CurrentID, int SelectedOption)
        {
            int? storyID = (from v in db.Narratives
                          where v.ParentID == CurrentID
                          && v.ParentOptionID == SelectedOption
                          select v.ID).FirstOrDefault();
            if ((storyID != null) && (storyID != 0))
            {
                return RedirectToAction("Index", new { StoryID = storyID });
            }
            return RedirectToAction("Create", new { ParentID = CurrentID, ParentOptionID = SelectedOption });
        }

        // GET: /Narrative/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narrative Narrative = db.Narratives.Find(id);
            if (Narrative == null)
            {
                return HttpNotFound();
            }
            return View(Narrative);
        }

        // GET: /Narrative/Create
        [Authorize]
        public ActionResult Create(int? ParentID, int? ParentOptionID)
        {
            ViewBag.ParentID = ParentID;
            ViewBag.ParentOptionID = ParentOptionID;
            
            return View();
        }

        // POST: /Narrative/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ParentID,ParentOptionID,UserID,Story,Option1,Option2")] Narrative Narrative)
        {
            if (ModelState.IsValid)
            {
                if (Narrative.UserID == null) { Narrative.UserID = User.Identity.GetUserId(); }
                db.Narratives.Add(Narrative);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Narrative);
        }

        // GET: /Narrative/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narrative Narrative = db.Narratives.Find(id);
            if (Narrative == null)
            {
                return HttpNotFound();
            }
            return View(Narrative);
        }

        // POST: /Narrative/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,UserID,Story,Option1,Option2")] Narrative Narrative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Narrative).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Narrative);
        }

        // GET: /Narrative/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narrative Narrative = db.Narratives.Find(id);
            if (Narrative == null)
            {
                return HttpNotFound();
            }
            return View(Narrative);
        }

        // POST: /Narrative/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narrative Narrative = db.Narratives.Find(id);
            db.Narratives.Remove(Narrative);
            db.SaveChanges();
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
