using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Contracts
{
    public interface IRepositoryWrapper
    {
        IParentRepository Parent { get; }
        IKidRepository Kid { get; }
        IEventRepository Event { get; }
        IEventRegistrationRepository EventRegistration { get; }
        ILocationRepository Location { get; }
        IFriendshipRepository Friendship { get; }

        void Save();
    }
}
