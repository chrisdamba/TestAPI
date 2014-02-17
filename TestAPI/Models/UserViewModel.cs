using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Serialization;
using TestAPI.Data;

namespace TestAPI.Models
{
    [JsonSerializable]
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(User user)
        {
            UserId = user.UserId;
            Title = user.Title;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address1 = user.Address1;
            Address2 = user.Address2;
            PostCode = user.Postcode;
        }
        
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string PostCode { get; set; }

        public User ToEntity(User user)
        {
            // Create new user if doesn't exist
            if (user == null)
            {
                user = new User {State = EntityState.Added};
            }
            else
            {
                user.State = EntityState.Modified;
            }

            user.Title = Title;
            user.LastName = LastName;
            user.FirstName = FirstName;
            user.Address1 = Address1;
            user.Address2 = Address2;
            user.Postcode = PostCode;

            return user;
        }
    }
}