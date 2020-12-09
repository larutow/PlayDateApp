using Microsoft.EntityFrameworkCore;
using PlayDate_App.Contracts;
using PlayDate_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Data
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository 
    {
        public EventRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public Event GetEvent(int id) => FindByCondition(e => e.EventId == id).Include("Location").FirstOrDefault();

        public Event GetEventDetails(int id) => FindByCondition(e => e.EventId == id).FirstOrDefault();
    }
}
