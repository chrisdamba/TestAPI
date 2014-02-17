using System.Linq;
using MbUnit.Framework;
using Rhino.Mocks;
using TestAPI.Contracts;
using TestAPI.Data;
using TestAPI.Repositories;

namespace TestAPI.Tests.Domain.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private UserRepository target;
        private MockRepository mocks;

        private IDataContext dataContext;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            dataContext = mocks.StrictMock<IDataContext>();

            target = new UserRepository { Resolve = () => dataContext };
        }

        [Test]
        public void Create_AddsEntity()
        {
            var users = new FakeDbSet<User>();
            var entity = new User();

            using (mocks.Record())
            {
                SetupResult.For(dataContext.Users).Return(users);
                Expect.Call(dataContext.SaveChanges()).Return(1);
                Expect.Call(dataContext.Dispose);
            }

            using (mocks.Playback())
            {
                target.Create(entity);

                Assert.AreEqual(0, entity.UserId);
            }
        }

        [Test]
        public void Get_Single_Returns()
        {
            var user = new User { UserId = 2 };
            var users = new FakeDbSet<User>(new[] { user });

            using (mocks.Record())
            {
                SetupResult.For(dataContext.Users).Return(users);
                Expect.Call(dataContext.Dispose);
            }
            using (mocks.Playback())
            {
                var result = target.Get(2);
                Assert.AreSame(user, result);
            }
        }

        [Test]
        public void Get_Multiple_Returns()
        {
            var users = new[]
                {
                    new User { UserId = 1 },
                    new User { UserId = 2 },
                    new User { UserId = 3 }
                };

            using (mocks.Record())
            {
                SetupResult.For(dataContext.Users).Return(new FakeDbSet<User>(users));
                Expect.Call(dataContext.Dispose);
            }
            using (mocks.Playback())
            {
                var result = target.Get(users.Take(2).Select(x => x.UserId)).ToArray();

                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(1, result[0].UserId);
                Assert.AreEqual(2, result[1].UserId);
            }
        }
    }
}
