using TestAPI.Data;

namespace TestAPI.Models
{
    public interface IViewFactory
    {
        UserViewModel GetUser(int userId);

        User Save(UserViewModel model);
    }
}
