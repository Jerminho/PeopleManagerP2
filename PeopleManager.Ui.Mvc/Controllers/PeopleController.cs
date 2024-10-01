using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonService _personService;
        private readonly OrganizationService _organizationService;

        public PeopleController(
            PersonService personService,
            OrganizationService organizationService)
        {
            _personService = personService;
            _organizationService = organizationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _personService.Find();

            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView(person);
            }

            _personService.Create(person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _personService.Get(id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView(person);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [FromForm]Person person)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView(person);
            }

            _personService.Update(id, person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var person = _personService.Get(id);

            return View(person);
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _personService.Delete(id);

            return RedirectToAction("Index");
        }
        
        private IActionResult CreateEditView(Person? person = null)
        {
            var organizations = _organizationService.Find();
            ViewBag.Organizations = organizations;
            return View(person);
        }
    }
}
