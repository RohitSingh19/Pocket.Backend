using System.Runtime.CompilerServices;

namespace Pocket.API.Constants
{
    public class SocialProfile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string BaseUrl { get; set; }
        public string SocialProfileSeachKey { get; set; }
    }
    public class SocialProfiles
    {
        List<SocialProfile> socialProfiles;
        public SocialProfiles()
        {
            socialProfiles = new List<SocialProfile>();
            InitializeSocialProfile();   
        } 
        public void InitializeSocialProfile()
        { 
            socialProfiles.Add(new SocialProfile() {Type = "Instagram", Name = "Instagram", BaseUrl = "https://www.instagram.com/", SocialProfileSeachKey = "ig" });
            socialProfiles.Add(new SocialProfile() {Type = "LinkedIn", Name = "LinkedIn", BaseUrl = "https://www.linkedin.com/", SocialProfileSeachKey = "li" });
            socialProfiles.Add(new SocialProfile() {Type = "Facebook", Name = "Facebook", BaseUrl = "https://www.facebook.com/", SocialProfileSeachKey = "fb" });
            socialProfiles.Add(new SocialProfile() {Type = "Quora", Name = "Quora", BaseUrl = "https://www.quora.com/", SocialProfileSeachKey = "qa" });
            socialProfiles.Add(new SocialProfile() {Type = "Pintrest", Name = "Pintrest", BaseUrl = "https://pinterest.com/", SocialProfileSeachKey = "pt" });
            socialProfiles.Add(new SocialProfile() {Type = "Custom", Name = "Custom", BaseUrl = null, SocialProfileSeachKey = null });
        }

        public SocialProfile GetSocialProfile(string type) 
        {
            return socialProfiles.FirstOrDefault(profile => profile.Type == type);
        }
    }
}
