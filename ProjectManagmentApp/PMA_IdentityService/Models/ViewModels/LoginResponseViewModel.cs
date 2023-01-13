namespace PMA_IdentityService.Models.ViewModels
{
    public class LoginResponseViewModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
    }
}
