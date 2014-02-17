using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TestAPI.Contracts;

namespace TestAPI.Data
{
    public class DataContext : TestDataContext, IDataContext
    {
        public DataContext()
            : base(DataContextUtility.CreateEdmxConnectionString("Data.Model1"))
        {
        }

        public IDbSet<User> Users
        {
            get { return base.Users; }
        }
    }
}