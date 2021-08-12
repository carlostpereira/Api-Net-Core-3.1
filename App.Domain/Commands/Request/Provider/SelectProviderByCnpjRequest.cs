using App.Shared.Commands;

namespace App.Domain.Commands.Request.Provider
{
    public class SelectProviderByCnpjRequest : ICommand
    {
        public string Cnpj { get; set; }
    }
}
