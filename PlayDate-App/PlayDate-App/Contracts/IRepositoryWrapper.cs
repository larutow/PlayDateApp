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
        void Save();
    }
}
