using Mindafy.Models;
using Mindafy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows;   

namespace User.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        string urlDomain = "http://Mindafy.somee.com/";
        // GET: Acceso


        [HttpGet]
        public ActionResult IniciarSesion()
        {
            Models.ViewModel.IniciarSesion model = new Models.ViewModel.IniciarSesion();
            ViewBag.IniciarSesion = model;
            return View(model);
        }

        [HttpGet]
        public ActionResult Registro()
        {
            Models.ViewModel.RegisterUser model = new Models.ViewModel.RegisterUser();
            
            ViewBag.Registro = model;
            return View(model);
        }

        [HttpGet]
        public ActionResult StartRecovery()
        {
            Models.ViewModel.Recovery model = new Models.ViewModel.Recovery();
            return View(model);
        }

        [HttpGet]
        public ActionResult Recovery(string token)
        {
            Models.ViewModel.RecoveryPass model = new Models.ViewModel.RecoveryPass();
            model.token = token;
            using (MindafyEntities db = new MindafyEntities())
            {
                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View();
                }
                var oUser = db.Student.Where(d => d.token_recovery == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Token Invalido";
                    return View("IniciarSesion");
                }
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult StartRecovery(Models.ViewModel.Recovery model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string token = GetSha256(Guid.NewGuid().ToString());
                using (MindafyEntities db = new MindafyEntities())
                {
                    var oUser = db.Student.Where(d => d.mail_Student == model.Email).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.token_recovery = token;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        //Send Email
                        SendEmail(oUser.mail_Student, token);
                        ViewData["Message"] = "Revisa tu correo electrónico";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Recovery(Models.ViewModel.RecoveryPass model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(model);

                }
                using (MindafyEntities db = new MindafyEntities())
                {
                    var oUser = db.Student.Where(d => d.token_recovery == model.token).FirstOrDefault();
                    if (oUser != null)
                    {

                        oUser.pass_Student = GetSha256(model.Password);
                        oUser.token_recovery = null;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return View("IniciarSesion");
        }

        [HttpPost]
        public ActionResult IniciarSesion(Models.ViewModel.IniciarSesion model)
        {
            using (MindafyEntities db = new MindafyEntities())
            {
                TempData["Email1"] = model.Email;
                TempData["Email2"] = model.Email;
                string email = model.Email;
                string pass = GetSha256(model.password);
                ;

                if ((from d in db.Student where d.mail_Student == email && d.pass_Student == pass select d).Count() > 0)
                {

                    
                    return Redirect("~/Subject/");


                }
                
            }
            return View();

        }

        [HttpPost]
        public ActionResult Registro(Models.ViewModel.RegisterUser model)
        {
            using (MindafyEntities db = new MindafyEntities())
            {
                var oPerson = new Student();
                var name = model.Name;
                oPerson.name_Student = model.Name;
                oPerson.mail_Student = model.Email;
                oPerson.pass_Student = GetSha256(model.password);
                oPerson.phone_Student = model.phone;
                db.Student.Add(oPerson);
                db.SaveChanges();
            }
            return View("IniciarSesion");
        }


        #region HELPERS
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "mindafyrecovery@gmail.com";
            string Contraseña = "Mindafy_Recovery5";
            string url = urlDomain + "/User/Recovery/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña", "<p>Por favor ingrese al siguiente link para restablecer su contraseña:</p><br>" + "<a href='" + url + "'>Click para recuperar</a>");
            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            // oSmtpClient. Host = "smtp.gmail.com";
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();


        }
    
     
        
        #endregion
    }



}