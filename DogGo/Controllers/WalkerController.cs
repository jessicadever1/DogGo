using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Repositories;
using DogGo.Models;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{
    public class WalkerController : Controller
    {

        // GET: WalkerController
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkerController(
            IWalkerRepository walkerRepository,
            IWalkRepository walkRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
        }

        // GET: Walkers
        public ActionResult Index()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();

            return View(walkers);
        }

        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);

            WalkerFormViewModel vm = new WalkerFormViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walker walker)
        {
            try
            {
                _walkerRepo.AddWalker(walker);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Walker/Edit/5
        public ActionResult Edit(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // POST: Walker/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Walker walker)
        {
            try
            {
                _walkerRepo.UpdateWalker(walker);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walker);
            }
        }

        // GET: Walker/Delete/5
        public ActionResult Delete(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            return View(walker);
        }

        // POST: Walker/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walker walker)
        {
            try
            {
                _walkerRepo.DeleteWalker(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walker);
            }
        }
    }
}
