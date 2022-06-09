using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PersonController : Controller
    {
        private List<Person> _person;

        public PersonController()
        {
            _person = new List<Person> {
                new Person{ Id = 1, Code = "01", Name = "ロイド・フォージャー" },
                new Person{ Id = 2, Code = "02", Name = "アーニャ・フォージャー" },
                new Person{ Id = 3, Code = "03", Name = "ヨル・フォージャー" }
            };
        }

        // GET: PersonController
        public ActionResult Index()
        {
            return View(_person);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            var model = _person.SingleOrDefault(x => x.Id == id);
            return View(model);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person target)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(target);
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _person.SingleOrDefault(x => x.Id == id);
            return View(model);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Person target)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(target);
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _person.SingleOrDefault(x => x.Id == id);
            return View(model);
        }

        // POST: PersonController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
