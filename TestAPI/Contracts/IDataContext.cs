using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TestAPI.Data;

namespace TestAPI.Contracts
{
    /// <summary>
    /// The interface for the users data context, to allow dependency injection and proxies.
    /// </summary>
    public interface IDataContext : IDisposable
    {
        IDbSet<User> Users { get; }

        DbEntityEntry Entry(object entity);

        int SaveChanges();
    }
}
