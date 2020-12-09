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

            var playDate = _repo.Event.GetEvent(id);
            //playDate.Location = new Models.Location();
            //var locationTableInfo = _repo.Location.FindAll().Where(l => l.LocationId == playDate.LocationId).FirstOrDefault();
            //playDate.Location.Name = locationTableInfo.Name;
            //playDate.Location.AddressName = locationTableInfo.AddressName;
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
        //public ActionResult RegisterEvent(int id)
        //{
        //    Event registerEvent = new Event();
        //    registerEvent.EventId = id;
        //    return RedirectToAction("Create", "EventRegistration", registerEvent);
        //}

        public ActionResult InviteFriends(int eventId)
        {

            return RedirectToAction("InviteList", "Parent", eventId);
        }

        //public ActionResult EventRequest(int parentTwoId)
        //{
        //    var requestedFriend = _repo.Parent.GetParentDetails(parentTwoId);
        //    var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var parentOneId = _repo.Parent.GetParent(identityUserId).ParentId;
        //    var currentEvent = _repo.EventRegistration.FindByCondition(e => e.ParentId == parentOneId).FirstOrDefault();
        //    var eventId = _repo.Event.FindAll().Where(e => e.EventId == currentEvent.EventId).FirstOrDefault();

        //    //var eventId = currentEvent.EventId;


        //}
        //public ActionResult FriendshipRequest(int parentTwoId)
        //{
        //    var requestedFriend = _repo.Parent.GetParentDetails(parentTwoId);
        //    var parentOneId = GetParentId();
        //    var parentOneOnFriendsTable = _repo.Friendship.FindByCondition(p => p.ParentOneId == parentOneId || p.ParentTwoId == parentOneId);
        //    var currentFriend = parentOneOnFriendsTable.Where(p => p.ParentOneId == parentTwoId || p.ParentTwoId == parentTwoId).ToList();
        //    if (currentFriend.Count == 0)
        //    {
        //        Friendship newRequest = new Friendship();
        //        newRequest.ParentOneId = parentOneId;
        //        newRequest.ParentTwoId = parentTwoId;
        //        newRequest.FriendshipRequest = true;
        //        newRequest.FriendshipConfirmed = false;
        //        var parentOne = _repo.Parent.GetParentDetails(parentOneId);
        //        _email.FriendRequestEmail(parentOne, requestedFriend);
        //        _repo.Friendship.Create(newRequest);
        //        _repo.Save();
        //    }
        //    return RedirectToAction("Index");
        //}




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
