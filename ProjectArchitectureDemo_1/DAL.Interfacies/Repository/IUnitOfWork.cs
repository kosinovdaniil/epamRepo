using System;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        //Rollback
    }
}