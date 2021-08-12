using App.Shared.Entities;
using App.Shared.Enums;
using App.SharedKernel.Library;
using FluentValidator;
using System;

namespace App.Domain.Entities
{
    public class User : Entity
    {
        //Constructors
        protected User()
        {

        }

        public User(Guid id, string name, string password, string confirmPassword, string email, Gender gender, Role role)
        {
            Id = id;
            Name = name;
            Password = SharedFunctions.EncryptPassword(password);
            Email = email;
            Gender = gender;
            Role = role;
            Active = false;

            new ValidationContract<User>(this)
                .IsRequired(x => x.Name, "Nome do usuário é requerido")
                .IsEmail(x => x.Email, "E-mail deve estar num formato válido")
                .AreEquals(x => x.Password, SharedFunctions.EncryptPassword(confirmPassword), "As senhas não coincidem")
                ;
        }

        //Properties
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }
        public Gender Gender { get; private set; }
        public Role Role { get; private set; }


        //Methods
        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

    }
}
