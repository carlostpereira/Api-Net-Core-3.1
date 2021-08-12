using App.Shared.Entities;
using FluentValidator;
using System;

namespace App.Domain.Entities
{
    public class Provider : Entity
    {
        //Constructors
        protected Provider()
        {

        }

        public Provider(Guid id, string name, string email, string cnpj)
        {
            Id = id;
            Name = name;
            Email = email;
            Cnpj = cnpj;
            Active = false;

            new ValidationContract<Provider>(this)
                .IsRequired(x => x.Name, "Nome do usuário é requerido")
                .IsEmail(x => x.Email, "E-mail deve estar num formato válido")
                ;
        }

        //Properties
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cnpj { get; private set; }
        public bool Active { get; private set; }


        //Methods
        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

    }
}
