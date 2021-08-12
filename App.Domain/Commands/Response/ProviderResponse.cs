using App.Shared.Commands;
using System;

namespace App.Domain.Commands.Response
{
    public class ProviderResponse : ICommandResult
    {
        public ProviderResponse() {}

        public ProviderResponse(Guid id, string name, string email, string cnpj)
        {
            Id = id;
            Name = name;
            Cnpj = cnpj;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
    }
}
