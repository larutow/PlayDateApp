using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayDate_App.Contracts;
using PlayDate_App.Data.APIData;
using PlayDate_App.Models;
using PlayDate_App.Services;

namespace PlayDate_App.Controllers
{
    public class EventRegistrationController : Controller
    {
        private IRepositoryWrapper _repo;
        private GoogleMapsService _maps;

        public EventRegistrationController(IRepositoryWrapper repo, GoogleMapsService mapsService)
        {
            _repo = repo;
            _maps = mapsService;
        }

        // GET: EventRegistrationController
        public ActionResult Index()
        {
            var registeredPlayDates = _repo.EventRegistration.FindAll().ToList();
            var playDates = _repo.Event.FindAll().ToList();
            var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));

            foreach (var date in registeredPlayDates)
            {
                date.Event = new Models.Event();
                date.Event.Location = new Models.Location();
                date.Event.Location = _repo.Location.FindAll().Where(l => l.LocationId == date.Event.LocationId).FirstOrDefault();
            }

            return View(registeredPlayDates.Where(p => p.ParentId == parent.ParentId));
        }


        // GET: EventRegistrationController/Details/5
        public ActionResult Details(int id)
        {
            var playDate = _repo.EventRegistration.FindAll().Where(e => e.EventRegistrationId == id).FirstOrDefault();
           

            return View(playDate);
        }

        // GET: EventRegistrationController/Create
        public ActionResult Create(Event registerEvent)
        {
            
            var playDate = _repo.Event.FindAll().Where(e => e.EventId == registerEvent.EventId).FirstOrDefault();
            playDate.Location = new Models.Location();
            var locationTableInfo = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
            playDate.Location.Name = locationTableInfo.Name;
            playDate.Location.AddressName = locationTableInfo.AddressName;

            var registeredPlayDate = new EventRegistration();
            registeredPlayDate.ParentId = playDate.ParentId;
            registeredPlayDate.EventId = playDate.EventId;
            registeredPlayDate.Event = playDate;

            return View(registeredPlayDate);
        }

        // POST: EventRegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventRegistration playDate)
        {
            try
            {
                var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));
                playDate.ParentId = parent.ParentId;

                playDate.Event = new Models.Event();
                playDate.Event.Location = new Models.Location();
                playDate.Event.Location.Name = playDate.Event.Location.Name;
                playDate.Event.Location.AddressName = playDate.Event.Location.AddressName;


                //GeocodeLocation locationData = new GeocodeLocation();

                //locationData = await _maps.GetLatLng(playDate.Event.Location.AddressName);
                //playDate.Event.Location.Lat = locationData.results[0].geometry.location.lat;
                //playDate.Event.Location.Lng = locationData.results[0].geometry.location.lng;



                _repo.EventRegistration.Create(playDate);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventRegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            var playDate = _repo.EventRegistration.FindAll().Where(e => e.EventRegistrationId == id).FirstOrDefault();

            return View(playDate);
        }

        // POST: EventRegistrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventRegistration playDate)
        {
            try
            {
                
                _repo.EventRegistration.Update(playDate);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventRegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            var playDate = _repo.EventRegistration.FindAll().Where(e => e.EventRegistrationId == id).FirstOrDefault();

            return View(playDate);
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventRegistration playDate)
        {
            try
            {
                
                _repo.EventRegistration.Delete(playDate);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
