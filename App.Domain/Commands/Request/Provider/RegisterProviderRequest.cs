using App.Shared.Commands;

namespace App.Domain.Commands.Request.Provider
{
    public class RegisterProviderRequest : ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
    }
}
