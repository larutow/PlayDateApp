using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayDate_App.Contracts;
using PlayDate_App.Models;
using System;
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

            //TODO index view logic - home screen

            return View(parent);
        }

        // GET: ParentController/Details/5
        public ActionResult Details(int id)
        {
            var parent = _repo.Parent.GetParentDetails(id);

            return View(parent);
        }

        // GET: ParentController/Create
        public ActionResult Create()
        {
            Parent parent = new Parent();
            return View(parent);
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

        // GET: ParentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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