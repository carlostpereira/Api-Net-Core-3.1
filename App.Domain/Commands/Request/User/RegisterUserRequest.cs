using App.Shared.Commands;
using App.Shared.Enums;
using System;

namespace App.Domain.Commands.Request.User
{
    public class RegisterUserRequest : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
    }

}
