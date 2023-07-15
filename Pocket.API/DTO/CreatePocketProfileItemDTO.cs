namespace Pocket.API.DTO
{
    public class CreatePocketProfileItemDTO
    {
        public string UserId { get; set; }  
        public string ProfileUserName { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public bool IsVisibleToOthers { get; set; } = true;
    }
}
