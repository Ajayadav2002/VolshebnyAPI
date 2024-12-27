namespace Volshebny_API.Models
{
    public class Login
    {
    }

    public class LoginAdmin
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class SuperAdminlogin
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } // Nullable in case it's not always set
        public bool IsActive { get; set; }
        public string? UserType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int result { get; set; }
    }

    public class Adminlogin
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string? UserType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int result { get; set; }
    }

    public class SaleUserLogin
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string? UserType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int result { get; set; }
    }
}
