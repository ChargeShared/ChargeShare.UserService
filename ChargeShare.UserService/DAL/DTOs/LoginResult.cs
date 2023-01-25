namespace ChargeShare.UserService.DAL.DTOs
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
