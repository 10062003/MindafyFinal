using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mindafy.Models;
using Mindafy.Models.ViewModels;

namespace MindafyCore.Controllers
{
    public class ActivityController : Controller
    {
        public ActionResult Activity()
        {
            List<ListActivity> lst;
            using (MindafyEntities db = new MindafyEntities())
            {
                lst = (from d in db.Activity
                       select new ListActivity
                       {
                           IdActivity = d.id_Activity,
                           NameActivity = d.name_Activity,
                           DescriptionActivity = d.description_Activity,
                           StartDateActivity = d.start_Date_Activity,
                           EndDateActivity = d.end_Date_Activity,
                           State = d.state,
                           Note = d.Note,

                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult NewActivity()
        {
            return View();
        }

        //CREAR
        [HttpPost]
        public ActionResult NewActivity(ActivityModel model)
        {

            if (ModelState.IsValid)
            {
                using (MindafyEntities db = new MindafyEntities())
                {
                    var oActivity = new Activity();
                    oActivity.id_Activity = model.IdActivity;
                    oActivity.name_Activity = model.NameActivity;
                    oActivity.description_Activity = model.DescriptionActivity;
                    oActivity.start_Date_Activity = model.StartDateActivity;
                    oActivity.end_Date_Activity = model.EndDateActivity;
                    oActivity.state = model.State;
                    oActivity.Note = model.Note;
                    oActivity.Conforms = model.Conforms;
                    db.Activity.Add(oActivity);
                    db.SaveChanges();
                    
                }
                return Redirect("~/Activity/Activity");
            }
            return View(model);

        }


        //EDITAR
        public ActionResult EditActivity(int ID)
        {
            ActivityModel model = new ActivityModel();
            using (MindafyEntities db = new MindafyEntities())
            {
                string Delivered="true";
                string Undelivered="false";
                var oActivity = db.Activity.Find(ID);
                model.NameActivity = oActivity.name_Activity;
                model.DescriptionActivity = oActivity.description_Activity;
                model.StartDateActivity = oActivity.start_Date_Activity;
                model.EndDateActivity = oActivity.end_Date_Activity;
                model.State = oActivity.state;
                model.Note = oActivity.Note;
                model.IdActivity = oActivity.id_Activity;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditActivity(ActivityModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MindafyEntities db = new MindafyEntities())
                    {
                        var oActivity = db.Activity.Find(model.IdActivity);
                        oActivity.id_Activity = model.IdActivity;
                        oActivity.name_Activity = model.NameActivity;
                        oActivity.description_Activity = model.DescriptionActivity;
                        oActivity.start_Date_Activity = model.StartDateActivity;
                        oActivity.end_Date_Activity = model.EndDateActivity;
                        oActivity.state = model.State;
                        oActivity.Note = model.Note;
                        oActivity.Conforms = model.Conforms;

                        db.Entry(oActivity).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Redirect("~/Activity/Activity");
                }
                return View(model);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //ELIMINAR
        [HttpGet]
        public ActionResult Delete(int ID)
        {

            using (MindafyEntities db = new MindafyEntities())
            {
                var oActivity = db.Activity.Find(ID);
                db.Activity.Remove(oActivity);
                db.SaveChanges();

            }
            return Redirect("~/Activity/Activity");
        }

    }
}
