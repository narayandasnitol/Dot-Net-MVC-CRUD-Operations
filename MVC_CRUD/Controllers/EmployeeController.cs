using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD.Models;

namespace MVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (Database1Entities1 db = new Database1Entities1())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if(id == 0)
            {
                return View(new Employee());
            }
            else
            {
                using (Database1Entities1 db = new Database1Entities1())
                {
                    return View(db.Employees.Where(x => x.Id == id).FirstOrDefault<Employee>());
                }
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using(Database1Entities1 db = new Database1Entities1())
            {
                if (emp.Id == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }     
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using(Database1Entities1 db = new Database1Entities1())
            {
                Employee emp = db.Employees.Where(x => x.Id == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();

                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}