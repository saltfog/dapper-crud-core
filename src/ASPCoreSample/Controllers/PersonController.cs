using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ASPCoreSample.Models;
using ASPCoreSample.Repository;

namespace ASPCoreSample.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonRepository personRepository;

        public PersonController(IConfiguration configuration)
        {
            personRepository = new PersonRepository(configuration);
        }


        public IActionResult Index()
        {
            return View(personRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: person/Create
        [HttpPost]
        public IActionResult Create(Person cust)
        {
            if (ModelState.IsValid)
            {
                personRepository.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /person/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Person obj = personRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /person/Edit
        [HttpPost]
        public IActionResult Edit(Person obj)
        {

            if (ModelState.IsValid)
            {
                personRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/person/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            personRepository.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}
