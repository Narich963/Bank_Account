using System.ComponentModel.DataAnnotations;

namespace ControlWork9.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Login field is empty")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password field is empty")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}
