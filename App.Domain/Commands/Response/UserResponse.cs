using App.Shared.Commands;
using App.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Commands.Response
{
    public class UserResponse : ICommandResult
    {
        public UserResponse() {}

        public UserResponse(Guid id, string name, string email, Gender gender, Role role)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
            Role = role;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
