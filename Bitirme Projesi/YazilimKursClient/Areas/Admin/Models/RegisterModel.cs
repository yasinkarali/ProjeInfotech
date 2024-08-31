namespace YazilimKursClient.Areas.Admin.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
    }
}
