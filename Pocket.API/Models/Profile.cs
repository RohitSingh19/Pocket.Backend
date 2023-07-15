namespace Pocket.API.Models
{
    public class Profile
    {
        public string ProfileUserName { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsVisibleToOthers { get; set; }
    }
}
