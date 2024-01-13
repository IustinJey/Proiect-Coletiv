using System.ComponentModel.DataAnnotations;
using skillz_backend.models;

public class UserUpdateDto
{
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
    public string Location { get; set; }
    public bool Verified { get; set; }
    public string ProfilePicture { get; set; }
}