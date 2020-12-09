using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayDate_App.Contracts;
using PlayDate_App.Models;

namespace PlayDate_App.Controllers
{
    public class EventRegistrationController : Controller
    {
        private IRepositoryWrapper _repo;

        public EventRegistrationController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

      
    }
}
