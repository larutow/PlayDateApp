using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private MailKitService _email;

        public EventController(IRepositoryWrapper repo, GoogleMapsService mapsService, MailKitService email)
        {
           
            _repo = repo;
            _maps = mapsService;
            _email = email;
        }

        // GET: EventController
        public ActionResult Index()
        {
            var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var playDateRegistrations = _repo.EventRegistration.FindByCondition(e => e.ParentId == parent.ParentId).Include("Event.Location").ToList();
            ViewBag.PendingRequest = PendingRequests();
            return View(playDateRegistrations);

        }

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {

            var playDate = _repo.Event.GetEvent(id);
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

                //Geocoding below via maps service
                GeocodeLocation eventLocationApiCall = await _maps.GetLatLng(playDate.Location.AddressName);
                playDate.Location.Lat = eventLocationApiCall.results[0].geometry.location.lat;
                playDate.Location.Lng = eventLocationApiCall.results[0].geometry.location.lng;

                EventRegistration newEventRegistration = new EventRegistration()
                {
                    EventId = 0,
                    Event = playDate,
                    ParentId = parent.ParentId,
                    Accepted = true,
                    Role = "Organizer",
                    ConfirmedAttendance = false
                };

                _repo.EventRegistration.Create(newEventRegistration);
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
        public async Task<ActionResult> Edit(Event playDate)
        {
            try
            {
                playDate.Location.LocationId = playDate.EventId;
                GeocodeLocation eventLocationApiCall = await _maps.GetLatLng(playDate.Location.AddressName);
                playDate.Location.Lat = eventLocationApiCall.results[0].geometry.location.lat;
                playDate.Location.Lng = eventLocationApiCall.results[0].geometry.location.lng;

                _repo.Event.Update(playDate);
                _repo.Save();

                var registrations = _repo.EventRegistration.FindByCondition(e => e.EventId == playDate.EventId).Include("Parent");
                foreach (EventRegistration registration in registrations)
                {
                    _email.EditEvent(registration.Parent, playDate);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: EventController/RegisterEvent
        //public ActionResult RegisterEvent(int id)
        //{
        //    Event registerEvent = new Event();
        //    registerEvent.EventId = id;
        //    return RedirectToAction("Create", "EventRegistration", registerEvent);
        //}


        public ActionResult EventRequest(int parentTwoId, int eventId)
        {
            //var requestedFriend = _repo.Parent.GetParentDetails(parentTwoId);
            //var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var parentOneId = _repo.Parent.GetParent(identityUserId).ParentId;
            //var findEvent = _repo.Event.FindByCondition(e => e.ParentId == parentOneId).FirstOrDefault();
            //var eventId = findEvent.EventId;

            EventRegistration newEventRegistration = new EventRegistration()
            {
                EventId = eventId,
                ParentId = parentTwoId,
                Accepted = false,
                Role = "Attendee",
                ConfirmedAttendance = false,
            };

            _repo.EventRegistration.Create(newEventRegistration);
            _repo.Save();
            return RedirectToAction("Index", "Event");
        }


        public ActionResult AcceptInvite(int registrationNumber)
        {
            //var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var parentOneId = _repo.Parent.GetParent(identityUserId).ParentId;

            var InvitedEvent = _repo.EventRegistration.GetEventRegistration(registrationNumber);

            InvitedEvent.Accepted = true;
            _repo.EventRegistration.Update(InvitedEvent);
            _repo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult DeclineInvite(int registrationNumber)
        {
            //var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var parentOneId = _repo.Parent.GetParent(identityUserId).ParentId;

            var InvitedEvent = _repo.EventRegistration.GetEventRegistration(registrationNumber);

            _repo.EventRegistration.Delete(InvitedEvent);
            _repo.Save();
            return RedirectToAction("Index");
        }

        private List<EventRegistration> PendingRequests()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parentObject = _repo.Parent.GetParent(identityUserId);
            var PendingEventRequests = _repo.EventRegistration.FindByCondition(e => e.ParentId == parentObject.ParentId && e.Accepted == false).Include("Event").ToList();
            //List<Event> PendingEvent = new List<Event>();
            //foreach (var item in PendingEventRequests)
            //{
            //    var eventId = item.EventId;
            //    var currentEvent = _repo.Event.FindByCondition(e => e.EventId == eventId).FirstOrDefault();
            //    PendingEvent.Add(currentEvent);
            //}
            return PendingEventRequests;
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            var playDate = _repo.Event.FindAll().Where(e => e.EventId == id).FirstOrDefault();
            var reigstrationsOfGuests = _repo.EventRegistration.FindByCondition(e => e.EventId == id);
            foreach(var registration in reigstrationsOfGuests)
            {
                _repo.EventRegistration.Delete(registration);
            }
            _repo.Event.Delete(playDate);
            _repo.Save();

            return RedirectToAction("Index");
        }

        // POST: EventController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var playDate = _repo.Event.FindAll().Where(e => e.EventId == id).FirstOrDefault();
        //        _repo.Event.Delete(playDate);
        //        Models.Location location = new Models.Location();
        //        location = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
        //        _repo.Location.Delete(location);
        //        _repo.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
