﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Repositories;
using DogGo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DogGo.Controllers
{
    public class DogController : Controller

    {
        private readonly IDogRepository _dogRepo;

        public DogController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }

        // GET: DogController
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        // GET: Dog/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: Dog/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog); //Does this have access to the field _ownerRepo because of line 14?

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: Dogs/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            
            Dog dog = _dogRepo.GetDogById(id);
            dog.OwnerId = GetCurrentUserId();

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: Dogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: Dog/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            dog.OwnerId = GetCurrentUserId();

            return View(dog);
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }



        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

    }
}
