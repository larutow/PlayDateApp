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
