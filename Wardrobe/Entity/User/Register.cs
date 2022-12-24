using FluentValidation;

namespace Entity.User
{
    public class Register
    {

        public string Email { get; set; }


        public string Password { get; set; }


        public string ConfirmPassword { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }

        public long NationalIdNumber { get; set; }
    }
    public class RegisterValidotor : AbstractValidator<Register>
    {
        public RegisterValidotor()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 20);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();

        }
    }
}
