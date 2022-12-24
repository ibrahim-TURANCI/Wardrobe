using FluentValidation;

namespace Entity.User
{
    public class Login
    {

        public string Username { get; set; }

        public string Password { get; set; }
    }
    public class LoginValidotor : AbstractValidator<Login>
    {
        public LoginValidotor()
        {
            RuleFor(x => x.Username).NotNull();
            RuleFor(x => x.Password).NotNull();
        }
    }
}
