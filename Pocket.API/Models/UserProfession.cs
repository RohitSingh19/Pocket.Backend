namespace Pocket.API.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserProfession
    {
        public Dictionary<string, List<Profession>> GetUserProfessions()
        {

            Dictionary<string, List<Profession>> userProfessions = new Dictionary<string, List<Profession>>(); ;
            userProfessions.Add("Business", new List<Profession>
            {
                new Profession { Id = 1, Name = "Crafts"},
                new Profession { Id = 2, Name = "Financial Services"},
                new Profession { Id = 3, Name = "HR & Recruiting"},
                new Profession { Id = 4, Name = "Real Estate"},
                new Profession { Id = 5, Name = "Startup"},
            });


            userProfessions.Add("Creative", new List<Profession>
            {
                new Profession { Id = 1, Name = "Writer"},
                new Profession { Id = 2, Name = "Designer"},
                new Profession { Id = 3, Name = "Model"},
                new Profession { Id = 4, Name = "Influencer"},
            });

            userProfessions.Add("Education",new List<Profession>
            {
                new Profession { Id = 1, Name = "Teacher"},
                new Profession { Id = 2, Name = "E Learning"},
            });

            userProfessions.Add("Entertainment",new List<Profession>
            {
                new Profession { Id = 1, Name = "Films"},
                new Profession { Id = 2, Name = "Dance"},
                new Profession { Id = 3, Name = "Actor"},
                new Profession { Id = 4, Name = "Sports"},
                new Profession { Id = 5, Name = "Music"},
            });

            userProfessions.Add("Tech",new List<Profession>
            {
                new Profession { Id = 1, Name = "Edutech"},
                new Profession { Id = 2, Name = "Fintech"},
                new Profession { Id = 3, Name = "Hardware"},
                new Profession { Id = 4, Name = "Mobile Apps"},
                new Profession { Id = 5, Name = "Web Designer"},
                new Profession { Id = 6, Name = "Social Media"},
            });

            return userProfessions;
        }
    }
}
