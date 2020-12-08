﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayDate_App.Contracts;
using PlayDate_App.Models;

namespace PlayDate_App.Controllers
{
    public class EventController : Controller
    {

        private IRepositoryWrapper _repo;

        public EventController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: EventController
        public ActionResult Index(int id)
        {
          
            return View(_repo.Event.FindAll());
        }

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            var playDate = _repo.Event.GetEvent(id);
            playDate.Location = _repo.Location.FindByCondition(l => l.LocationId == playDate.LocationId).FirstOrDefault();

            return View(playDate);
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            Event playDate = new Event();

            return View(playDate);
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event playDate)
        {
            try
            {

                _repo.Event.Create(playDate);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            var playDate = _repo.Event.GetEventDetails(id);


            return View(playDate);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event playDate)
        {
            try
            {
                _repo.Event.Update(playDate);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
