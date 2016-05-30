using RssAgregator.Models.Services;

namespace RssAgregator.DAL
{
    public partial class User
    {
        public UserModel GetModel()
        {
            return new UserModel
            {
                Id = Id,
                Name = Name,
                Password = Password,
                Email = Email,
                UserKey = UserKey,
                IsActive = IsActive,
                Exists = true
            };
        }
    }
}
