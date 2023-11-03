namespace Pocket.API.Models
{
    public class UserProfile
    {
        public string ProfileTypeId { get; set; }   
        public string ProfileUserName { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsVisibleToOthers { get; set; }
    }
}
