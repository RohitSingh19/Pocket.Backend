namespace Pocket.API.DTO
{
    public class PocketProfileUpdateDTO
    {
        public string ProfileUserName { get; set; }
        public string ProfileName { get; set; } 
        public bool IsVisibleToOthers { get; set; }
    }
}
