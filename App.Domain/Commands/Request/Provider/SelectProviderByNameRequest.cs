using App.Shared.Commands;

namespace App.Domain.Commands.Request.Provider
{
    public class SelectProviderByNameRequest : ICommand
    {
        public string Name { get; set; }
    }
}
