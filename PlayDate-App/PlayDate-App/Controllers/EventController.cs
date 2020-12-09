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
    public class EventController : Controller
    {

        private IRepositoryWrapper _repo;
        private GoogleMapsService _maps;

        public EventController(IRepositoryWrapper repo, GoogleMapsService mapsService)
        {
           
            _repo = repo;
            _maps = mapsService;
        }

        // GET: EventController
        public ActionResult Index()
        {

            var playDates = _repo.Event.FindAll().ToList();
            var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));

            foreach (var date in playDates)
            {
                    date.Location = new Models.Location();
                    var locationTableInfo = _repo.Location.FindAll().Where(l => l.LocationId == date.LocationId).FirstOrDefault();
                    date.Location.Name = locationTableInfo.Name;
                    date.Location.AddressName = locationTableInfo.AddressName;
            }

            return View(playDates.Where(p => p.ParentId == parent.ParentId));
        }

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {

            var playDate = _repo.Event.FindAll().Where(e => e.EventId == id).FirstOrDefault();
            playDate.Location = new Models.Location();
            var locationTableInfo = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
            playDate.Location.Name = locationTableInfo.Name;
            playDate.Location.AddressName = locationTableInfo.AddressName;
            return View(playDate);
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            Event playDate = new Event();
            playDate.Location = new Models.Location();
            
            return View(playDate);
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event playDate)
        {
            try
            {
                var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));
                playDate.ParentId = parent.ParentId;

                Models.Location locationModel = new Models.Location();
                locationModel.Name = playDate.Location.Name;
                locationModel.AddressName = playDate.Location.AddressName;

                //GeocodeLocation locationData = await _maps.GetLatLng(playDate.Location.AddressName);
                //playDate.Location.Lat = locationData.results[0].geometry.location.lat;
                //playDate.Location.Lng = locationData.results[0].geometry.location.lng;



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

            var playDate = _repo.Event.FindAll().Where(e => e.EventId == id).FirstOrDefault();
            playDate.Location = new Models.Location();
            var locationTableInfo = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
            playDate.Location.Name = locationTableInfo.Name;
            playDate.Location.AddressName = locationTableInfo.AddressName;


            return View(playDate);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event playDate)
        {
            try
            {
                playDate.Location.LocationId = playDate.EventId;
                _repo.Event.Update(playDate);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: EventController/RegisterEvent
        public ActionResult RegisterEvent(int id)
        {
            Event registerEvent = new Event();
            registerEvent.EventId = id;
            return RedirectToAction("Create", "EventRegistration", registerEvent);
        }



        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            var playDate = _repo.Event.FindAll().Where(e => e.EventId == id).FirstOrDefault();
            playDate.Location = new Models.Location();
            var locationTableInfo = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
            playDate.Location.Name = locationTableInfo.Name;
            playDate.Location.AddressName = locationTableInfo.AddressName;

            return View(playDate);
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Event playDate)
        {
            try
            {
                _repo.Event.Delete(playDate);
                Models.Location location = new Models.Location();
                location = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
                _repo.Location.Delete(location);
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
