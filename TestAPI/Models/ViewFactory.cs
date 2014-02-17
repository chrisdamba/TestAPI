using TestAPI.Contracts;
using TestAPI.Data;
using TestAPI.Exceptions;

namespace TestAPI.Models
{
    public class ViewFactory : IViewFactory
    {
        private readonly IUserRepository repository;
        
        public ViewFactory(IUserRepository repository)
        {
            this.repository = repository;
        }
        public UserViewModel GetUser(int userId)
        {
            var user = repository.Get(userId);
            if (user == null)
            {
                throw new UnavailableItemException("User not found");
            }

            return new UserViewModel(user);
        }

        public User Save(UserViewModel model)
        {
            // Create or update entity
            var user = model.UserId != 0 ? repository.Get(model.UserId) : null;
            user = model.ToEntity(user);
            repository.Create(user);

            return user;
        }
    }
}