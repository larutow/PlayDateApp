using Microsoft.EntityFrameworkCore;
using PlayDate_App.Contracts;
using PlayDate_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Data
{
    public class EventRegistrationRepository : RepositoryBase<EventRegistration>, IEventRegistrationRepository 
    {
        public EventRegistrationRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public EventRegistration GetEventRegistration(int id) => FindByCondition(e => e.EventRegistrationId == id).Include("Event").Include("Event.Location").FirstOrDefault();

        public EventRegistration GetEventRegistrationDetails(int id) => FindByCondition(e => e.EventRegistrationId == id).FirstOrDefault();


    }
}
