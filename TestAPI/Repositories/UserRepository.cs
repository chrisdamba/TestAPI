using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using TestAPI.Contracts;
using TestAPI.Data;

namespace TestAPI.Repositories
{
    public class UserRepository : RepositoryBase<IDataContext, DataContext>, IUserRepository
    {
        public void Create(User user)
        {
            using (var context = CreateContext)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Update(User user)
        {
            using (var context = CreateContext)
            {
                user.State = EntityState.Modified;
                context.Entry(user).State = user.State;
                context.SaveChanges();
            }
        }

        public User Get(int id)
        {
            using (var context = CreateContext)
            {
                return context.Users.SingleOrDefault(x => x.UserId == id);
            }
        }

        public IEnumerable<User> Get(IEnumerable<int> ids)
        {
            using (var context = CreateContext)
            {
                return context.Users.Where(x => ids.Contains(x.UserId)).ToArray();
            }
        }

        public IEnumerable<int> List(out int totalCount, Expression<Func<User, bool>> criteria, int skip = 1, int take = Int32.MaxValue)
        {
            using (var context = CreateContext)
            {
                var query = context.Users.Where(criteria);
                totalCount = query.Count();
                return query.Skip(skip).Take(take).Select(x => x.UserId).ToArray();
            }
        }
    }
}