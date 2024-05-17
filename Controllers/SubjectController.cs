using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mindafy.Models;
using Mindafy.Models.ViewModels;

namespace Mindafy.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            List<ListaSubject> lst;
         using(MindafyEntities db = new MindafyEntities())
            {
                lst = (from d in db.Subject
                           select new ListaSubject
                           {
                               IDSubject = d.id_Subject,
                               NameSubject = d.name_Subject,
                               DescriptionSubject = d.description_Subject,
                               AverageSubject = d.average_Subject,

                           }).ToList();
                String Email1 = (string)TempData["Email1"];
                var idStudent = db.Student.Where(d => d.mail_Student == Email1).Select(d => d.id_Student).FirstOrDefault();
                /*
                 lst = (from d in db.Subjects
                        join Establish in db.Establishes on d.IdSubject equals Establish.IdSubject
                        join student in db.Students on Establish.IdStudent equals idStudent
                        select new ListaSubject
                        {
                            IdSubject = d.IdSubject,
                            NameSubject = d.NameSubject,
                            DescriptionSubject = d.DescriptionSubject,
                            AverageSubject = d.AverageSubject,

                        }).ToList();*/
            }
            return View(lst);
        }
        //CREAR
        public ActionResult NewSubject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewSubject(SubjectModel model)
        {
                if (ModelState.IsValid)
                {
                    using (MindafyEntities db = new MindafyEntities())
                    {
                        var oSubject = new Subject();
                        oSubject.id_Subject = model.ID;
                        oSubject.name_Subject = model.Name;
                        oSubject.description_Subject = model.Description;
                        oSubject.average_Subject = model.Average;
                        oSubject.Conforms = model.Student;

                        db.Subject.Add(oSubject);
                        db.SaveChanges();
                        String Email2 = (string)TempData["Email2"];


                        var oEstablish = new Establishes();
                        var idStudent = db.Student.Where(d => d.mail_Student == Email2).Select(d => d.id_Student).FirstOrDefault();
                        oEstablish.id_Student = idStudent;
                        oEstablish.id_Subject = oSubject.id_Subject;

                        db.Establishes.Add(oEstablish);
                        db.SaveChanges();

                    }
                    return Redirect("~/Subject/");
                }
                return View(model);

            

        }

        //EDITAR
        public ActionResult Edit(int ID)
        {
            SubjectModel model = new SubjectModel();
            using (MindafyEntities db = new MindafyEntities())
            {
                var oSubject = db.Subject.Find(ID);
                model.Name = oSubject.name_Subject;
                model.Description = oSubject.description_Subject;
                model.Average = oSubject.average_Subject;
                model.ID = oSubject.id_Subject;
            }
                return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SubjectModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MindafyEntities db = new MindafyEntities())
                    {
                        var oSubject = db.Subject.Find(model.ID);
                        oSubject.id_Subject = model.ID;
                        oSubject.name_Subject = model.Name;
                        oSubject.description_Subject = model.Description;
                        oSubject.average_Subject = model.Average;
                        oSubject.Conforms = model.Student;

                        db.Entry(oSubject).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Redirect("~/Subject/");
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //ELIMINAR
        [HttpGet]
        public ActionResult Delete(int ID)
        {

            using (MindafyEntities db = new MindafyEntities())
            {
                var oSubject = db.Subject.Find(ID);

                String Email2 = (string)TempData["Email2"];
                var oEstablish = new Establishes();
                var idEstablish = db.Establishes.Where(d => d.id_Subject == ID).Select(d => d.id_Establishes).FirstOrDefault();
          
                oEstablish = db.Establishes.Find(idEstablish);
          
                db.Establishes.Remove(oEstablish);
                db.SaveChanges();

                db.Subject.Remove(oSubject);
                db.SaveChanges();
                
            }
            return Redirect("~/Subject/");
        }


    }
}