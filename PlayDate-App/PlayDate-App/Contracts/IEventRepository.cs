using PlayDate_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Contracts
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        Event GetEvent(int id);

        Event GetEventDetails(int id);
    }
}
