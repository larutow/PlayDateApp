using PlayDate_App.Contracts;
using PlayDate_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Data
{
    public class ParentRepository: RepositoryBase<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {

        }

        public Parent GetParent(string parentUserId) => FindByCondition(p => p.IdentityUserId == parentUserId).FirstOrDefault();

        public Parent GetParentDetails(int id) => FindByCondition(p => p.ParentId == id).FirstOrDefault();

        //public Parent GetKids(int id) => FindByCondition(p => p.ParentId == id).Where(k => k.) // Find kids attached to parent

    }
}
