using App.Shared.Commands;

namespace App.Domain.Commands.Request.User
{
    public class SelectUserByUserEmailRequest : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
