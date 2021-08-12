using App.Shared.Commands;
using System;

namespace App.Domain.Commands.Request.User
{
    public class DeleteUserRequest : ICommand
    {
        public Guid Id { get; set; }
    }
}
