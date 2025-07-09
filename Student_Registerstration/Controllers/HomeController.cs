using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Student_Registerstration.Models;
using Student_Registerstration.Student_Data_Access_Layer;

namespace Student_Registerstration.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDAL stddal;
        public HomeController(StudentDAL stddal)
        {
            this.stddal = stddal;
            
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = stddal.Getallstudent().ToList();   
            return View(data);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(StudentModel Stdmodel)
        {
            try
            {
                stddal.Addstudent(Stdmodel);
                return RedirectToAction("Index",Stdmodel);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            StudentModel stdmodel = stddal.GetStudentid(Id);
            return View(stdmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentModel stdmodel)
        {
            try
            {
                stddal.Addstudent(stdmodel);
                return RedirectToAction("Index", stdmodel);
            }
            catch(Exception ex) 
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            StudentModel stdmodel = stddal.GetStudentid(Id);
            return View(stdmodel);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            StudentModel stdmodel = stddal.GetStudentid(Id);
            return View(stdmodel);
        }
        [HttpPost]
        public IActionResult Delete(StudentModel stdmodel)
        {
            try
            {
                stddal.Deletestudent(stdmodel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public IActionResult jQuery()
        {
            return View();
        }
        [HttpPost]
        public int Add(int num1,int num2)
        {
            return num1+num2;
        }
        [HttpPost]
        public int Sub(int num1, int num2)
        {
            return num1 - num2;
        }
        [HttpPost]
        public Calculator calculate(int num1, int num2) 
        {
            Calculator mod = new Calculator();
            mod.add=num1+num2;
            mod.sub=num1-num2;
            mod.mul=num1*num2;  
            mod.div=num1/num2;
            return mod;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(StudentModel stdmodel)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = stddal.ValidateUser(stdmodel.Email, stdmodel.Password);
                if (isValidUser)
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    ViewBag.Error = "Invalid Email or Password.";
                }
            }
            return View(stdmodel);
        }
    }
}
