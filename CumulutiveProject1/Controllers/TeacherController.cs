using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulutiveProject1.Models;

namespace CumulutiveProject1.Controllers
{
    public class TeacherController : Controller     
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

        
            return View(SelectedTeacher);
        }


        //GET : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");

        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        
        // ^ I don't think this is right bc the data types should match the form inputs from the New.cshtml form
        { 
            //Identify that this method is running
            //Identify the inputs provided from the form 

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //GET : /Teacher/Update/{id}
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current 
        /// information of the teacher and asks the user for new information as part of a form</returns>
        /// <example>GET : /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST : /Teacher/Update/{id}
        /// <summary>
        /// Recieves a POST request containing information about an existing teacher in the 
        /// system, with new values. Conveys this informaiton to the API, and redirects to the "Teacher Show" page of our updated Teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="EmployeeNumber"></param>
        /// <param name="HireDate"></param>
        /// <param name="Salary"></param>
        /// <returns>Dynamic Webpage providing the current information of the teacher</returns>
        /// <example>POST : /Teacher/Update/{id}</example>
        
        
        [HttpPost]

        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}