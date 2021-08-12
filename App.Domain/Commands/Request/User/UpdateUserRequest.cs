using App.Shared.Commands;
using System;

namespace App.Domain.Commands.Request.User
{
    public class UpdateUserRequest : RegisterUserRequest, ICommand
    {
        public Guid Id { get; set; }
    }
}
