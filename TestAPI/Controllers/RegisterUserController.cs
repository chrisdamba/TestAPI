using System.Collections.Generic;
using System.Web.Http;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [JsonSerializable]
    public class RegisterUserController : ApiController
    {
        private readonly IViewFactory viewFactory;

        public RegisterUserController()
        {
        }

        public RegisterUserController(IViewFactory viewFactory)
        {
            this.viewFactory = viewFactory;
        }

        // POST api/registeruser/
        public UserViewModel Post([FromBody]UserViewModel model)
        {
            int returnId;
            if (model != null && ModelState.IsValid && model.UserId == 0)
            {
                returnId = viewFactory.Save(model).UserId;
            }
            else
            {
                throw this.BadRequestException("User is null or invalid");
            }

            return viewFactory.GetUser(returnId);
        }

        // PUT api/registeruser/
        public UserViewModel Put([FromBody]UserViewModel model)
        {
            if (model == null || !ModelState.IsValid || model.UserId == 0)
            {
                throw this.BadRequestException("User is null or invalid");
            }

            var returnId = viewFactory.Save(model).UserId;
            return viewFactory.GetUser(returnId);
        }

        // GET api/registeruser/{userId}
        public UserViewModel Get(int userId)
        {
            return viewFactory.GetUser(userId);
        }
    }
   
}