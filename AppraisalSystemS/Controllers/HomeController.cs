using AppraisalSystemS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppraisalSystemS.Controllers
{
    public class HomeController : Controller
    {

        AppraisalSysEntities db = new AppraisalSysEntities();
        
        
        public ActionResult Index()
        {
            return View();
        }









        public ActionResult Admin()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Admin(Admin_tbl a)
        {

            Admin_tbl ad = db.Admin_tbl.Where(x => x.admin_name == a.admin_name && x.admin_password == a.admin_password).SingleOrDefault();

            if (ad != null)
            {
                Session["admin_id"] = ad.admin_id;
                return RedirectToAction("AdminTAB");
            }

            else
            {
                ViewBag.msg = "Логин немесе Құпия сөз дұрыс терілмеді!";
            }
            return View();
        }



        public ActionResult AdminTAB()
        {
            if (Session["admin_id"] == null)
            {

                return RedirectToAction("Index");
            }
            return View();
        }








        public ActionResult CompOrgan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CompOrgan(Comp_Organ c)
        {

            Comp_Organ co = db.Comp_Organ.Where(x => x.comp_organ_name == c.comp_organ_name && x.comp_organ_password == c.comp_organ_password).SingleOrDefault();

            if (co != null)
            {
                Session["comp_organ_id"] = co.comp_organ_id;
                Session["comp_organ_login"] = co.comp_organ_name;
                Session["comp_organ_fullname"] = co.comp_organ_full_name;
                Session["comp_organ_position"] = co.comp_organ_position;
                return RedirectToAction("CompOrganTAB");
            }

            else
            {
                ViewBag.msg = "Логин немесе Құпия cөз дұрыс терілмеді!";
            }
            return View();
        }


        public ActionResult CompOrganTAB()
        {
            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CompOrganTAB(Indicat_of_physiol i)
        {
           
            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }
            
            // av.rufie = indi;
            // db.average_index.Add(av);
            //db.SaveChanges();


            return View();
        }



        [HttpGet]

        public ActionResult CompOrgan_physio()
        {

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_fullname");
            return View();
        }


        [HttpPost]
        public ActionResult CompOrgan_physio(Indicat_of_physiol indi)
        {
            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_fullname");

            Indicat_of_physiol i = new Indicat_of_physiol();
           

            try
            {
                //prosto vvoditsya
                i.Iheight = indi.Iheight;
                i.Iweight = indi.Iweight;
                i.Iogk = indi.Iogk;
                i.IogkVd = indi.IogkVd;
                i.IogkVyd = indi.IogkVyd;
                i.Ichss = indi.Ichss;
                i.Iadsys = indi.Iadsys;
                i.Iaddias = indi.Iaddias;
                i.Izhel = indi.Izhel;
                i.Ido = indi.Ido;
                i.Imod = indi.Imod;
                i.Ishtang = indi.Ishtang;
                i.Igenchi = indi.Igenchi;
                i.Impk = indi.Impk;
                i.Ipwc = indi.Ipwc;

                //жизненный индекс
                //TempData["zhel"] = indi.Izhel;
                //TempData["weight"] = indi.Iweight;
                //int zhel = Convert.ToInt32(TempData["zhel"]);
                //int weight = Convert.ToInt32(TempData["weight"]);
                //i.Izhi = zhel / weight;
                i.Izhi = indi.Izhel / indi.Iweight;

                //Адаптационный потенц надо добавить возраст 

                i.Ichp = indi.Ichp;

                double e = 0.011;
                double ee = 0.014;
                double eee = 0.008;
                double eeee = 0.009;
                double eeeee = 0.27;

                decimal e1 = Convert.ToDecimal(e);
                decimal ee1 = Convert.ToDecimal(ee);
                decimal eee1 = Convert.ToDecimal(eee);
                decimal eeee1 = Convert.ToDecimal(eeee);
                decimal eeeee1 = Convert.ToDecimal(eeeee);

                i.Iadaptp = (e1 * indi.Ichp + ee1*indi.Iadsys)  + (eee1*indi.Iaddias + ee1*indi.Iweight) - (eeee1* indi.Iheight + eeeee1);

                // Индекс Скибински

                i.Ivremz = indi.Ivremz;

                i.Iskibin = indi.Izhel * indi.Ivremz / indi.Ichss;

                //•	Индекс Руфье 
                i.Ipone = indi.Ipone;
                i.Iptwo = indi.Iptwo;
                i.Ipthree = indi.Ipthree;


               i.Irufie = ((4 * (indi.Ipone + indi.Iptwo + indi.Ipthree)) - 200) / 10;
                // •	Индекс Шаповаловой , 
               i.Ikolnaklon = indi.Ikolnaklon;

               i.Ishapal = indi.Iweight / indi.Iheight * indi.Ikolnaklon / 60;

                // •	Индекс Робинсона  
               i.Irobinson = indi.Ichss * indi.Iadsys / 100;

               i.Indicat_of_physiol_FK_STU = indi.Indicat_of_physiol_FK_STU;
                i.Indicat_of_physiol_FK_Organ = (int?)Session["comp_organ_id"];
                db.Indicat_of_physiol.Add(i);
                db.SaveChanges();
                return RedirectToAction("CompOrgan_physio");

            }
            catch (Exception)
            {
                ViewBag.msg = "Дұрыс еңгізілмеді ! ";
            }



            return View();
        }







        [HttpGet]
        public ActionResult CompOrgan_pedagog()
        {

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_fullname");
            return View();
        }


        [HttpPost]
        public ActionResult CompOrgan_pedagog(Pedag_test ped)
        {
            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_fullname");

            Pedag_test p = new Pedag_test();

            try
            {
                //prosto vvoditsya
                p.begsto = ped.begsto;
                p.begtys = ped.begtys;
                p.pryzhvdlin = ped.pryzhvdlin;
                p.standinam = ped.standinam;
                p.kistdinamlev = ped.kistdinamlev;
                p.kistdinamprav = ped.kistdinamprav;
                p.podtyagiv = ped.podtyagiv;
                p.pres = ped.podtyagiv;
                p.pryzhkisrazbega = ped.pryzhkisrazbega;
                p.begtrid = ped.begtrid;
                p.metangran = ped.metangran;
                p.chelnochnbeg = ped.chelnochnbeg;
                p.testkuper = ped.testkuper;
                p.Pedag_test_FK_STU = ped.Pedag_test_FK_STU;
                p.Pedag_test_FK_Organ = (int?)Session["comp_organ_id"];


                db.Pedag_test.Add(p);
                db.SaveChanges();
                return RedirectToAction("CompOrgan_pedagog");

            }
            catch (Exception)
            {
                ViewBag.msg = "Дұрыс еңгізілмеді ! ";
            }



            return View();
        }




        public ActionResult tolyqkorset()
        {

            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }

            decimal Iheight = (decimal)db.Indicat_of_physiol.Average(x => x.Iheight);
            ViewBag.Iheight = Iheight;
            decimal Iweight = (decimal)db.Indicat_of_physiol.Average(x => x.Iweight);
            ViewBag.Iweight = Iweight;
            decimal Iogk = (decimal)db.Indicat_of_physiol.Average(x => x.Iogk);
            ViewBag.Iogk = Iogk;
            decimal IogkVd = (decimal)db.Indicat_of_physiol.Average(x => x.IogkVd);
            ViewBag.IogkVd = IogkVd;
            decimal IogkVyd = (decimal)db.Indicat_of_physiol.Average(x => x.IogkVyd);
            ViewBag.IogkVyd = IogkVyd;
            decimal Izhi = (decimal)db.Indicat_of_physiol.Average(x => x.Izhi);
            ViewBag.Izhi = Izhi;
            decimal Ichss = (decimal)db.Indicat_of_physiol.Average(x => x.Ichss);
            ViewBag.Ichss = Ichss;
            decimal Ido = (decimal)db.Indicat_of_physiol.Average(x => x.Ido);
            ViewBag.Ido = Ido;
            decimal Imod = (decimal)db.Indicat_of_physiol.Average(x => x.Imod);
            ViewBag.Imod = Imod;
            decimal Istang = (decimal)db.Indicat_of_physiol.Average(x => x.Ishtang);
            ViewBag.Istang = Istang;
            decimal Igenchi = (decimal)db.Indicat_of_physiol.Average(x => x.Igenchi);
            ViewBag.Igenchi = Igenchi;
            decimal Ichp = (decimal)db.Indicat_of_physiol.Average(x => x.Ichp);
            ViewBag.Ichp = Ichp;
            decimal Iadaptp = (decimal)db.Indicat_of_physiol.Average(x => x.Iadaptp);
            ViewBag.Iadaptp = Iadaptp;
            decimal Impk = (decimal)db.Indicat_of_physiol.Average(x => x.Impk);
            ViewBag.Impk = Impk;
            decimal Ipwc = (decimal)db.Indicat_of_physiol.Average(x => x.Ipwc);
            ViewBag.Ipwc = Ipwc;
            decimal rufie = (decimal)db.Indicat_of_physiol.Average(x => x.Irufie);
            ViewBag.rufie = rufie;
            decimal shapal = (decimal)db.Indicat_of_physiol.Average(x => x.Ishapal);
            ViewBag.shapal = shapal;
            decimal robinson = (decimal)db.Indicat_of_physiol.Average(x => x.Irobinson);
            ViewBag.robinson = robinson;
            decimal skibin = (decimal)db.Indicat_of_physiol.Average(x => x.Iskibin);
            ViewBag.skibin = skibin;


            //педагогикалық 

            decimal begsto = (decimal)db.Pedag_test.Average(x => x.begsto);
            ViewBag.begsto = begsto;
            decimal begtys = (decimal)db.Pedag_test.Average(x => x.begtys);
            ViewBag.begtys = begtys;
            decimal pryzhvdlin = (decimal)db.Pedag_test.Average(x => x.pryzhvdlin);
            ViewBag.pryzhvdlin = pryzhvdlin;
            decimal standinam = (decimal)db.Pedag_test.Average(x => x.standinam);
            ViewBag.standinam = standinam;
            decimal kistdinamlev = (decimal)db.Pedag_test.Average(x => x.kistdinamlev);
            ViewBag.kistdinamlev = kistdinamlev;
            decimal kistdinamprav = (decimal)db.Pedag_test.Average(x => x.kistdinamprav);
            ViewBag.kistdinamprav = kistdinamprav;
            decimal podtyagiv = (decimal)db.Pedag_test.Average(x => x.podtyagiv);
            ViewBag.podtyagiv = podtyagiv;
            decimal pres = (decimal)db.Pedag_test.Average(x => x.pres);
            ViewBag.pres = pres;
            decimal pryzhkisrazbega = (decimal)db.Pedag_test.Average(x => x.pryzhkisrazbega);
            ViewBag.pryzhkisrazbega = pryzhkisrazbega;
            decimal begtrid = (decimal)db.Pedag_test.Average(x => x.begtrid);
            ViewBag.begtrid = begtrid;
            decimal metangran = (decimal)db.Pedag_test.Average(x => x.metangran);
            ViewBag.metangran = metangran;
            decimal chelnochnbeg = (decimal)db.Pedag_test.Average(x => x.chelnochnbeg);
            ViewBag.chelnochnbeg = chelnochnbeg;
            decimal testkuper = (decimal)db.Pedag_test.Average(x => x.testkuper);
            ViewBag.testkuper = testkuper;
         

            return View();
        }

        public ActionResult tolyqkorsetI(int? id,int? page)
        {

            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }



            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

           

            List<Indicat_of_physiol> qwe = db.Indicat_of_physiol.ToList();

            IPagedList<Indicat_of_physiol> asd = qwe.ToPagedList(pageindex, pagesize);


            return View(asd);
        }
        


        public ActionResult tolyqkorsetP(int? id, int? page)
        {

            if (Session["comp_organ_id"] == null)
            {

                return RedirectToAction("Index");
            }


            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

            
            List<Pedag_test> li = db.Pedag_test.ToList();

            IPagedList<Pedag_test> stu = li.ToPagedList(pageindex, pagesize);

            return View(stu);
        }



        [HttpGet]
        public ActionResult EditCompPedagog(int? id)
        {
            var model = db.Pedag_test.Find(id);

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_login");
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCompPedagog(Pedag_test qas)
        {

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_login");

            db.Entry(qas).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("tolyqkorsetP");
        }




        public async Task<ActionResult> DetailsCompPedagog(int? id)
        {
            if (id != null)
            {
                Pedag_test pract = await db.Pedag_test.FirstOrDefaultAsync(x => x.Pedag_test_id == id);
                if (pract != null)
                    return View(pract);
            }
            return HttpNotFound();
        }



        public ActionResult DeleteCompPedagog(int id)
        {

            var model = db.Pedag_test.Find(id);
            db.Pedag_test.Remove(model);

            db.SaveChanges();

            return RedirectToAction("tolyqkorsetP");
        }










        [HttpGet]
        public ActionResult EditCompPhysiol(int? id)
        {
            var model = db.Indicat_of_physiol.Find(id);

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_login");
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCompPhysiol(Pedag_test qas)
        {

            int sid = Convert.ToInt32(Session["comp_organ_id"]);
            List<Student_tbl> li = db.Student_tbl.ToList();
            ViewBag.list = new SelectList(li, "student_id", "student_login");

            db.Entry(qas).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("tolyqkorsetI");
        }




        public async Task<ActionResult> DetailsCompPhysiol(int? id)
        {
            if (id != null)
            {
                Indicat_of_physiol pract = await db.Indicat_of_physiol.FirstOrDefaultAsync(x => x.indicat_of_physiol_id == id);
                if (pract != null)
                    return View(pract);
            }
            return HttpNotFound();
        }



        public ActionResult DeleteCompPhysiol(int id)
        {

            var model = db.Indicat_of_physiol.Find(id);
            db.Indicat_of_physiol.Remove(model);

            db.SaveChanges();

            return RedirectToAction("tolyqkorsetI");
        }


















        [HttpGet]
        public ActionResult studentregister()
        {
            return View();
        }


        [HttpPost]
        public ActionResult studentregister(Student_tbl stu)
        {
            Student_tbl s = new Student_tbl();
            
            try
            {
                s.student_login = stu.student_login;
                s.student_password = stu.student_password;
                s.student_fullname = stu.student_fullname;
                s.student_course = stu.student_course;
                s.student_speciality = stu.student_speciality;
                s.student_univer = stu.student_univer;
                s.student_gender = stu.student_gender;


                db.Student_tbl.Add(s);
                db.SaveChanges();
                return RedirectToAction("StudentLogin");
            }
            catch (Exception)
            {
                ViewBag.msg = "Тіркелу сәтсіз өтті ! ";
            }

           

            return View();
        }

        [HttpGet]
        public ActionResult newpers()
        {
            return View();
        }


        [HttpPost]
        public ActionResult newpers(Comp_Organ com)
        {
            Comp_Organ c = new Comp_Organ();

            try
            {
                c.comp_organ_full_name = com.comp_organ_full_name;
                c.comp_organ_position = com.comp_organ_position;
                c.comp_organ_name = com.comp_organ_name;
                c.comp_organ_password = com.comp_organ_password;
              

                db.Comp_Organ.Add(c);
                db.SaveChanges();
                return RedirectToAction("Allpedagog");
            }
            catch (Exception)
            {
                ViewBag.msg = "Тіркелу сәтсіз өтті ! ";
            }



            return View();
        }






        public ActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(Student_tbl s)
        {
            Student_tbl st = db.Student_tbl.Where(x => x.student_login == s.student_login && x.student_password == s.student_password).SingleOrDefault();

            if (st == null)
            {
                ViewBag.msg = "Логин немесе Құпия cөз дұрыс терілмеді!";
            }

            else
            {
                Session["student_id"] = st.student_id;
                Session["student_login"] = st.student_login;
                Session["student_fullname"] = st.student_fullname;
                Session["student_course"] = st.student_course;
                Session["student_speciality"] = st.student_speciality;
                Session["student_univer"] = st.student_univer;
                Session["student_gender"] = st.student_gender;
                
                return RedirectToAction("StudentTab");
            }


            return View();
        }



        public ActionResult StudentTab()
        {
            if (Session["student_id"] == null)
            {

                return RedirectToAction("StudentLogin");
            }


            return View();
        }


        public ActionResult Physiol(int? id, int? page)
        {
            
            
            if (Session["student_id"] == null)
            {

                return RedirectToAction("StudentLogin");
            }

     
            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;


            int sid = Convert.ToInt32(Session["student_id"]);
            List<Indicat_of_physiol> li = db.Indicat_of_physiol.Where(x => x.Indicat_of_physiol_FK_STU == sid).ToList();

            IPagedList<Indicat_of_physiol> ind = li.ToPagedList(pageindex, pagesize);

            return View(ind);

        }



        public ActionResult Allstudents(int? id, int? page)
        {


            if (Session["admin_id"] == null)
            {
                return RedirectToAction("Index");
            }


            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;


            
            List<Student_tbl> li = db.Student_tbl.ToList();

            IPagedList<Student_tbl> ind = li.ToPagedList(pageindex, pagesize);

            return View(ind);

        }




        public ActionResult Allpedagog(int? id, int? page)
        {


            if (Session["admin_id"] == null)
            {
                return RedirectToAction("Index");
            }


            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;



            List<Comp_Organ> li = db.Comp_Organ.ToList();

            IPagedList<Comp_Organ> ind = li.ToPagedList(pageindex, pagesize);

            return View(ind);

        }






        public ActionResult Pedagog(int? id, int? page)
        {
            if (Session["student_id"] == null)
            {

                return RedirectToAction("StudentLogin");
            }


            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

            int sid = Convert.ToInt32(Session["student_id"]);

            List<Pedag_test> li = db.Pedag_test.Where(x => x.Pedag_test_FK_STU == sid).ToList();

            IPagedList<Pedag_test> ped = li.ToPagedList(pageindex, pagesize);

            return View(ped);
            
        }








        public ActionResult tolygyraq(int? id, int? page)
        {

            if (Session["student_id"] == null)
            {

                return RedirectToAction("Index");
            }



            int pagesize = 15, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

            int sid = Convert.ToInt32(Session["student_id"]);
          
            List<Indicat_of_physiol> qwe = db.Indicat_of_physiol.Where(x => x.Indicat_of_physiol_FK_STU == sid).ToList();

            IPagedList<Indicat_of_physiol> asd = qwe.ToPagedList(pageindex, pagesize);



            return View(asd);
        }









        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("Index");

        }







        public ActionResult About()
        {
            return View();
        }




        public ActionResult Contact()
        {
            return View();
        }
    }
}