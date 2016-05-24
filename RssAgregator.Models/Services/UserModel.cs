namespace RssAgregator.Models.Services
{
    public class UserModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public string UserKey { get; set; }

        public bool? CreateCookie { get; set; }

        public bool? Exists { get; set; }

        public void RemovePrivateData()
        {
            Id = null;
            Password = null;
            UserKey = null;
            CreateCookie = null;
            Exists = null;
        }
    }
}
