using PlayDate_App.Contracts;
using PlayDate_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private ApplicationDbContext _context;
        private IParentRepository _parent;
        private IKidRepository _kid;
        private IEventRepository _event;
        private IEventRegistrationRepository _eventRegistration;
        private ILocationRepository _location;
        private IFriendshipRepository _friendship;
        public IParentRepository Parent
        {
            get
            {
                if (_parent == null)
                {
                    _parent = new ParentRepository(_context);
                }
                return _parent;
            }
        }
        public IKidRepository Kid
        {
            get
            {
                if (_kid== null)
                {
                    _kid = new KidRepository(_context);
                }
                return _kid;
            }
        }

        public IEventRepository Event
        {
            get
            {
                if (_event == null)
                {
                    _event = new EventRepository(_context);
                }
                return _event;
            }
        }

        public IEventRegistrationRepository EventRegistration
        {
            get
            {
                if (_eventRegistration == null)
                {
                    _eventRegistration = new EventRegistrationRepository(_context);
                }
                return _eventRegistration;
            }
        }

        public ILocationRepository Location
        {
            get
            {
                if (_location == null)
                {
                    _location = new LocationRepository(_context);
                }
                return _location;
            }
        }

        public IFriendshipRepository Friendship
        {
            get
            {
                if (_friendship == null)
                {
                    _friendship = new FriendshipRepository(_context);
                }
                return _friendship;
            }
        }



        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
