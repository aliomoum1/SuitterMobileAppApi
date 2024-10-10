namespace SuitterAppApi.Application.Identity.Users;

public class CreateUserByPhoneRequest
{
    public string PhoneNumber { get; set; }
    public string PassWord { get; set; }
}