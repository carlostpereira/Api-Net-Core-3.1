using App.Shared.Commands;
using System;

namespace App.Domain.Commands.Request.Provider
{

    public class UpdateProviderRequest : RegisterProviderRequest, ICommand
    {
        public Guid Id { get; set; }
    }
}
