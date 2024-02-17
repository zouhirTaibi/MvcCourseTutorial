using Microsoft.AspNetCore.Mvc;
using MvcCourseTutorial.Models.Domain;

namespace MvcCourseTutorial.Controllers
{
    public class PersonController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        public PersonController(DatabaseContext databaseContext)
        {
                _databaseContext = databaseContext; 
        }
        public IActionResult Index()
        {
            ViewBag.greeting = "Hello zouhir";
            ViewData["greeting2"] = "view data,iam zouhir";
            //ViewBag and ViewData can send data only from controller to view
            //tempdata can send data only from controller action to another controller action
            TempData["greetings3"] = "its temp data message";
            return View();
        }
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _databaseContext.Persons.Add(person);
                _databaseContext.SaveChanges();
                TempData["msg"] = "Added succefully";
                return RedirectToAction("AddPerson");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added";
                return View(person);
            }
            
        }
     
        public IActionResult DisplayPersons() 
        {
            var persons=_databaseContext.Persons.ToList();
            return View(persons);
        }

        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person=_databaseContext.Persons.Find(id);
                if(person != null)
                {
                    _databaseContext.Remove(person);
                    _databaseContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("DisplayPersons");
        }

        public IActionResult EditPerson(int id)
        {
            var person = _databaseContext.Persons.Find(id);
            return View(person);

        }
        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _databaseContext.Persons.Update(person);
                _databaseContext.SaveChanges();
                return RedirectToAction("DisplayPersons");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update";
                return View();
            }

        }
    }
}
