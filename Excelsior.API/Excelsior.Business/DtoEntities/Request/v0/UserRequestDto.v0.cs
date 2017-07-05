namespace Excelsior.Business.DtoEntities.Request.v0
{
    public class UserRequestDto
    {
        public string Company { get; set; }
        public string Email { get; set; }
        public string EmailVerified { get; set; }
        public string FamilyName { get; set; }
        public string Firstname { get; set; }
        public string GivenName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public object UserId { get; set; }
        public string WebSite { get; set; }
        public string ClaimType { get; set; }
        public int PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
    }
}
