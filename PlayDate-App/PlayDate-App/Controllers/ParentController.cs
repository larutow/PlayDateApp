using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayDate_App.Contracts;
using PlayDate_App.Models;
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

        public ParentController(IRepositoryWrapper repo)
        {
            _repo = repo;
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

        // GET: ParentController/CreateEvent
        public ActionResult CreateEvent()
        {
            Event playDate = new Event();

            return View(playDate);
        }

        // POST: ParentController/EventPlayDate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(Event playDate)
        {

            try
            {
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

        // POST: ParentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Parent parent)
        {
            try
            {
                parent.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _repo.Parent.Create(parent);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
            var foundByZip = _repo.Parent.FindByCondition(p => p.LocationZip == parentIndexView.ZipSearch).ToList();
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
            //allfoundparents = list of found parent objects using search params
            return View(AllFoundParents);
        }

        public ActionResult FriendshipRequest(int parentOneId, int parentTwoId)
        {
            //Find exsisting friendship between these two Id's
            var FriendshipRequest = _repo.Friendship.FindByCondition(p => p.ParentOneId == parentOneId);
            var FriendshipRequestTwo = _repo.Friendship.FindByCondition(p => p.ParentTwoId == parentOneId);
            var AllParentOneFriends = FriendshipRequest.Concat(FriendshipRequestTwo);
            var FindOtherParent = AllParentOneFriends.Where(p => p.ParentOneId == parentTwoId);
            var FindOtherParentTwo = AllParentOneFriends.Where(p => p.ParentTwoId == parentOneId);
            var Friendship = FindOtherParent.Concat(FindOtherParentTwo).ToList();
            if (Friendship == null)
            {
                Friendship newRequest = new Friendship();
                newRequest.ParentOneId = parentOneId;
                newRequest.ParentTwoId = parentTwoId;
                newRequest.FriendshipRequest = true;
                newRequest.FriendshipConfirmed = false;
                

            }return View();


            //return DoParentIdsMatch.TrueForAll(ParentOneId.Contains) == ParentTwoId.TrueForAll(DoParentIdsMatch.Contains);
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