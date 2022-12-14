namespace ChargeShare.UserService.DAL.DTOs;

public class UserRegisterDTO
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string Province { get; set; }
    public string HouseNumber { get; set; }
    public string HouseAddition { get; set; }
}