using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayDate_App.Contracts;
using PlayDate_App.Data.APIData;
using PlayDate_App.Models;
using PlayDate_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PlayDate_App.Controllers
{
    public class ParentController : Controller
    {
        private IRepositoryWrapper _repo;
        private MailKitService _email;
        private GoogleMapsService _maps;

        public ParentController(IRepositoryWrapper repo, MailKitService mailKitService, GoogleMapsService mapsService)
        {
            _repo = repo;
            _email = mailKitService;
            _maps = mapsService;
        }

        // GET: ParentController
        public ActionResult Index()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var parent = _repo.Parent.FindByCondition(p => p.IdentityUserId == identityUserId).FirstOrDefault();
            var parent = _repo.Parent.GetParent(identityUserId);
            if (parent == null)
            {
                return RedirectToAction("Create");
            }

            ParentIndexViewModel indexViewModel = new ParentIndexViewModel();
            indexViewModel.Parent = parent;
            var kids = _repo.Kid.FindByCondition(k => k.ParentId == parent.ParentId).ToList();
            indexViewModel.Kids = kids;

            //TODO index view logic - home screen

            return View(indexViewModel);
        }

        // GET: ParentController/Details/5
        public ActionResult Details(int id)
        {
            var parent = _repo.Parent.GetParentDetails(id);
            ParentDetailsViewModel viewmodel = new ParentDetailsViewModel();
            viewmodel.Parent = parent;
            var kids = _repo.Kid.FindByCondition(k => k.ParentId == id).ToList();
            viewmodel.Kids = kids;
            return View(viewmodel);
        }

        // GET: ParentController/Create
        public ActionResult Create()
        {
            Parent parent = new Parent();
            return View(parent);
        }

        // POST: ParentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Parent parent)
        {
            try
            {
                parent.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                GeocodeLocation locationData = await _maps.GetLatLng(parent.LocationZip.ToString());
                parent.Lat = locationData.results[0].geometry.location.lat;
                parent.Lng = locationData.results[0].geometry.location.lng;
                _repo.Parent.Create(parent);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParentController/CreateEvent
        public ActionResult CreateEvent()
        {
            Event playDate = new Event();
            playDate.Location = new Location();

            return View(playDate);
        }

        // POST: ParentController/EventPlayDate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(Event playDate)
        {

            try
            {
                var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));
                playDate.ParentId = parent.ParentId;
                playDate.Location.Name = playDate.Location.Name;
                playDate.Location.AddressName = playDate.Location.AddressName;


                _repo.Event.Create(playDate);
                _repo.Save();
                return RedirectToAction("Index", "Event");
            }
            catch
            {
                return View();
            }

        }


        //GET : ParentController/AddKid
        public ActionResult AddKid()
        {
            Kid kid = new Kid();
            var parent = _repo.Parent.GetParent(User.FindFirstValue(ClaimTypes.NameIdentifier));
            kid.ParentId = parent.ParentId;
            return View(kid);
        }

        //POST: ParentController/AddKid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddKid(Kid addedkid)
        {
            _repo.Kid.Create(addedkid);
            _repo.Save();
            Console.WriteLine(addedkid);
            return RedirectToAction("Index");
        }


        // GET: ParentController/Edit/5
        public ActionResult Edit(int id)
        {
            var parent = _repo.Parent.GetParentDetails(id);

            return View(parent);
        }

        // POST: ParentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Parent parent)
        {
            try
            {

                _repo.Parent.Update(parent);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        ///   ///    ///   ///
        public ActionResult EditKid(int id)
        {
            var kid = _repo.Kid.FindByCondition(k => k.KidId == id).FirstOrDefault();
            return View(kid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKid(Kid kid)
        {
            try
            {
                _repo.Kid.Update(kid);
                _repo.Save();
                return RedirectToAction ("Index");
            }
            catch
            {
                return View();
            }
        }

        //FirstOrDefault() maybe//

        // GET: ParentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchResults(ParentIndexViewModel parentIndexView)
        {
            var searchingParent = _repo.Parent.GetParent(parentIndexView.Parent.IdentityUserId);

            List<Parent> AllParentsInZip = new List<Parent>();
            List<Kid> AllFoundKidsInZip = new List<Kid>();

            List<Parent> AllFoundParents = new List<Parent>();
            
            //default behavior is zip searches for local zip code parents
            
            //search for parents by zip location
            var foundByZip = _repo.Parent.FindByCondition(p => p.LocationZip == parentIndexView.ZipSearch && p.ParentId != searchingParent.ParentId).ToList();
            AllParentsInZip.AddRange(foundByZip);
            


            //Find every kid in zip (use local parents)
            foreach (Parent parent in AllParentsInZip)
            {
                var foundKidsOfParent = _repo.Kid.FindByCondition(k => k.ParentId == parent.ParentId).ToList();
                AllFoundKidsInZip.AddRange(foundKidsOfParent);
            }

            

            //check name input - if not null perform search
            if (parentIndexView.NameSearch != null)
            {
                //search for parents by first name and last name and build a list of distinct results
                var foundByFirstName = AllParentsInZip.Where(p => p.FirstName.Contains(parentIndexView.NameSearch)).ToList();
                var foundByLastName = AllParentsInZip.Where(p => p.LastName.Contains(parentIndexView.NameSearch)).ToList();
                
                var foundByName = foundByFirstName.Union(foundByLastName).ToList();
                //add found parents by name to AllFoundParents
                AllFoundParents.AddRange(foundByName);
            }

            //age range
            if (parentIndexView.AgeLow != null || parentIndexView.AgeHigh != null)
            {
                List<Parent> foundParentsByKidAges = new List<Parent>();
                if(parentIndexView.AgeLow != null)
                {
                    var foundKidsAboveLower = AllFoundKidsInZip.Where(k => k.Age >= parentIndexView.AgeLow);
                    foreach(Kid kid in foundKidsAboveLower)
                    {
                        var parent = _repo.Parent.GetParentDetails(kid.ParentId);
                        foundParentsByKidAges.Add(parent);
                    }
                }
                if (parentIndexView.AgeHigh != null)
                {
                    var foundKidsBelowUpper = AllFoundKidsInZip.Where(k => k.Age <= parentIndexView.AgeHigh);
                    foreach (Kid kid in foundKidsBelowUpper)
                    {
                        var parent = _repo.Parent.GetParentDetails(kid.ParentId);
                        foundParentsByKidAges.Add(parent);
                    }
                }
                AllFoundParents.AddRange(foundParentsByKidAges);
                
            }

            //health - immunizations
            if (parentIndexView.ImmunizedSearch == true)
            {
                var foundKidsImmunized = _repo.Kid.FindByCondition(k => k.Immunized == true);
                foreach (Kid kid in foundKidsImmunized)
                {
                    var parent = _repo.Parent.GetParentDetails(kid.ParentId);
                    AllFoundParents.Add(parent);
                }
            }

            //health - wears a mask
            if (parentIndexView.WearsMaskSearch == true)
            {
                var foundKidsWearsMask = _repo.Kid.FindByCondition(k => k.WearsMask == true);
                foreach (Kid kid in foundKidsWearsMask)
                {
                    var parent = _repo.Parent.GetParentDetails(kid.ParentId);
                    AllFoundParents.Add(parent);
                }

            }

            if(AllFoundParents.Count == 0)
            {
                AllFoundParents = AllParentsInZip;
            }

            

            AllFoundParents = AllFoundParents.GroupBy(p => p.ParentId).Select(p => p.Last()).ToList();
            ViewBag.FoundFriends = FoundFriends(searchingParent.ParentId, AllFoundParents, CurrentParentFriendsList(searchingParent));
            ViewBag.FoundRequests = FoundRequests(searchingParent.ParentId, AllFoundParents, CurrentParentRequestedList(searchingParent));
            //var CurrentParentFriendsList = _repo.Friendship.FindByCondition(f => (f.ParentOneId == searchingParent.ParentId || f.ParentTwoId == searchingParent.ParentId) && f.FriendshipConfirmed == true).ToList();
            //var CurrentParentRequestedList = _repo.Friendship.FindByCondition(f => (f.ParentOneId == searchingParent.ParentId || f.ParentTwoId == searchingParent.ParentId) && f.FriendshipRequest == true && f.FriendshipConfirmed == false).ToList();
            //var FoundFriends = FindCurrentFriends(searchingParent.ParentId, AllFoundParents, CurrentParentFriendsList);
            //var FoundRequests = FindCurrentFriends(searchingParent.ParentId, AllFoundParents, CurrentParentRequestedList);
            //ViewBag.FoundFriends = FoundFriends;
            //ViewBag.FoundRequests = FoundRequests;
            return View(AllFoundParents);
           
        }
        private List<Friendship> CurrentParentFriendsList(Parent searchingParent)
        {
            var CurrentParentFriendsList = _repo.Friendship.FindByCondition(f => (f.ParentOneId == searchingParent.ParentId || f.ParentTwoId == searchingParent.ParentId) && f.FriendshipConfirmed == true).ToList();
            return CurrentParentFriendsList;
        }
        private List<Friendship> CurrentParentRequestedList(Parent searchingParent)
        {
            var CurrentParentRequestedList = _repo.Friendship.FindByCondition(f => (f.ParentOneId == searchingParent.ParentId || f.ParentTwoId == searchingParent.ParentId) && f.FriendshipRequest == true && f.FriendshipConfirmed == false).ToList();
            return CurrentParentRequestedList;
        }
        private List<int> FoundFriends(int seachingParentId, List<Parent> AllFoundParents, List<Friendship> CurrentParentFriendsList)
        {
            var FoundFriends = FindCurrentFriends(seachingParentId, AllFoundParents, CurrentParentFriendsList);
            return FoundFriends;
        }
        private List<int> FoundRequests(int seachingParentId, List<Parent> AllFoundParents, List<Friendship> CurrentParentRequestedList)
        {
            var FoundRequests = FindCurrentFriends(seachingParentId, AllFoundParents, CurrentParentRequestedList);
            return FoundRequests;
        }
        private List<int> FindCurrentFriends(int searchingParentId, List<Parent> AllFoundParents, List<Friendship> ListOfFriendship)
        {
            List<int> FoundFriends = new List<int>();
            foreach (var parent in AllFoundParents)
            {
                var foundParentId = parent.ParentId;
                var CurrentParentFriend = _repo.Parent.GetParentDetails(foundParentId);
                foreach (var relationship in ListOfFriendship)
                {
                    if((relationship.ParentOneId == searchingParentId && relationship.ParentTwoId == foundParentId)||(relationship.ParentOneId == foundParentId && relationship.ParentTwoId == searchingParentId))
                    {
                        FoundFriends.Add(CurrentParentFriend.ParentId);
                    }
                }
            }
            return FoundFriends;
        }  

        public ActionResult FriendshipRequest(int parentTwoId)
        {
            //Find exsisting friendship between these two Id's
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parentOneId = _repo.Parent.GetParent(identityUserId).ParentId;
            var FriendshipRequest = _repo.Friendship.FindByCondition(p => p.ParentOneId == parentOneId);
            var FriendshipRequestTwo = _repo.Friendship.FindByCondition(p => p.ParentTwoId == parentOneId);
            var AllParentOneFriends = FriendshipRequest.Concat(FriendshipRequestTwo);
            var FindOtherParent = AllParentOneFriends.Where(p => p.ParentOneId == parentTwoId);
            var FindOtherParentTwo = AllParentOneFriends.Where(p => p.ParentTwoId == parentOneId);
            var Friendship = FindOtherParent.Concat(FindOtherParentTwo).ToList();
            if (Friendship.Count == 0)
            {
                Friendship newRequest = new Friendship();
                newRequest.ParentOneId = parentOneId;
                newRequest.ParentTwoId = parentTwoId;
                newRequest.FriendshipRequest = true;
                newRequest.FriendshipConfirmed = false;

                _repo.Friendship.Create(newRequest);
                _repo.Save();
                var friendshipId = newRequest.FriendshipId;
                SendRequest(friendshipId);
            }
            return View("Details");
        }

        public ActionResult ConfrimFriendship(int friendshipId)
        {
            var confirmFriendship = _repo.Friendship.GetFriendship(friendshipId);
            confirmFriendship.FriendshipConfirmed = true;
            return View();
        }

        public void SendRequest(int friendshipId)
        {
            
        }


        // POST: ParentController/Delete/5
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