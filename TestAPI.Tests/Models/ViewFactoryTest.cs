

using MbUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using TestAPI.Contracts;
using TestAPI.Data;
using TestAPI.Exceptions;
using TestAPI.Models;

namespace TestAPI.Tests.Models
{
    [TestFixture]
    public class ViewFactoryTest
    {
        private ViewFactory target;
        private MockRepository mocks;

        private IUserRepository userRepository;
        
        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            userRepository = mocks.StrictMock<IUserRepository>();

            target = mocks.PartialMock<ViewFactory>(userRepository);
        }

        [Test]
        public void GetUser_Id_ReturnsModel()
        {
            var user = new User { UserId = 3 };
            
            using (mocks.Record())
            {
                Expect.Call(userRepository.Get(user.UserId)).Return(user);
            }
            using (mocks.Playback())
            {
                var result = target.GetUser(user.UserId);

                Assert.IsNotNull(result);
                Assert.AreEqual(user.UserId, result.UserId);
            }
        }

        [Test]
        [ExpectedException(typeof(UnavailableItemException))]
        public void GetUser_NotExists_Throws()
        {
            using (mocks.Record())
            {
                Expect.Call(userRepository.Get(457)).Return(null);
            }
            using (mocks.Playback())
            {
                target.GetUser(457);
            }
        }

        [Test]
        public void Save_New_SavesAndReturns()
        {
            var model = new UserViewModel{FirstName = "Test"};
            var entity = new User { FirstName = "Test" };

            using (mocks.Record())
            {
                Expect.Call(() => userRepository.Create(entity)).Constraints(Is.Matching<User>(x => { entity = x; return true; }));
            }
            using (mocks.Playback())
            {
                var result = target.Save(model);

                Assert.AreSame(entity, result);
            }
        }

        [Test]
        public void SaveUser_Exists_SavesAndReturns()
        {
            var model = new UserViewModel { UserId = 10, FirstName = "John" };
            var user  = new User { UserId = model.UserId };
            
            using (mocks.Record())
            {
                Expect.Call(userRepository.Get(model.UserId)).Return(user);
                Expect.Call(() => userRepository.Create(user));
            }
            using (mocks.Playback())
            {
                var result = target.Save(model);

                Assert.AreSame(user, result);
                Assert.AreEqual("John", result.FirstName);
            }
        }
    }
}
