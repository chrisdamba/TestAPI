using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TestAPI.Data;

namespace TestAPI.Contracts
{
    public interface IUserRepository
    {
        void Create(User user);

        void Update(User user);

        User Get(int id);

        IEnumerable<User> Get(IEnumerable<int> ids);

        IEnumerable<int> List(out int totalCount, Expression<Func<User, bool>> criteria = null, int page = 1,
                               int count = int.MaxValue);
    }
}
