using App.Domain.Commands.Request.Provider;
using App.Domain.Commands.Response;
using App.Domain.Entities;
using App.Domain.Repositories;
using App.Shared.Commands;
using AutoMapper;
using FluentValidator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Commands.Handlers
{
    public class ProviderCommandHandler : Notifiable,
                                            ICommandHandler<RegisterProviderRequest>,
                                            ICommandHandler<UpdateProviderRequest>,
                                            ICommandHandler<DeleteProviderRequest>
    {
        private readonly IProviderRepository _repository;
        private readonly IMapper _mapper;

        public ProviderCommandHandler(IProviderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region GET VERBS

        public async Task<ICollection<ProviderResponse>> Handle()
        {
            var commandObject = await _repository.GetAsync();

            return _mapper.Map<List<ProviderResponse>>(commandObject);
        }

        public async Task<ProviderResponse> Handle(SelectProviderByNameRequest command)
        {
            var commandObject = await _repository.GetByNameAsync(command.Name);

            return _mapper.Map<ProviderResponse>(commandObject);
        }

        public async Task<ProviderResponse> Handle(SelectProviderByCnpjRequest command)
        {
            var commandObject = await _repository.GetByNameAsync(command.Cnpj);

            return _mapper.Map<ProviderResponse>(commandObject);
        }

        #endregion

        #region POST, UPDATE AND DELETE VERBS

        public async Task<ICommandResult> Handle(RegisterProviderRequest command)
        {
            // 1. Criar instância do objeto
            var commandObject = new Provider(default, command.Name, command.Email, command.Cnpj);

            // 2. Adicionar notificações
            AddNotifications(commandObject.Notifications);
            await ValidateToCreate(commandObject);

            // 3. Verificar se é válido (se existem notificações
            if (!IsValid())
                return null;

            // 4. Salva as alterações no EF
            await _repository.SaveAsync(commandObject);

            // 5. Retorna os dados que foram cadastrados
            return new ProviderResponse(commandObject.Id, commandObject.Name, commandObject.Email, commandObject.Cnpj);

        }

        public async Task<ICommandResult> Handle(UpdateProviderRequest command)
        {
            // 1. Criar instância do objeto
            var commandObject = new Provider(default, command.Name, command.Email, command.Cnpj);

            // 2. Adicionar notificações
            AddNotifications(commandObject.Notifications);

            // 3. Verificar se é válido (se existem notificações
            if (!IsValid())
                return null;

            // 4. Salva as alterações no EF
            await _repository.UpdateAsync(commandObject);

            // 5. Retorna os dados que foram cadastrados
            return new ProviderResponse(commandObject.Id, commandObject.Name, commandObject.Email, commandObject.Cnpj);

        }

        public async Task<ICommandResult> Handle(DeleteProviderRequest command)
        {
            var commandObject = await _repository.GetByIdAsync(command.Id);

            if (commandObject == null)
            {
                var objectNotification = new Message();
                objectNotification.AddNotification(new Notification("Fornecedor", "Fornecedor não encontrado"));
                AddNotifications(objectNotification.Notifications);
                return null;
            }

            // 4. Deleta o registro
            await _repository.DeleteAsync(commandObject);

            // 5. Retorna os dados deletados
            return new ProviderResponse(commandObject.Id, commandObject.Name, commandObject.Email, commandObject.Cnpj);

        }

        private async Task<Provider> ValidateToCreate(Provider provider)
        {
            var count = await _repository.GetAsync();

            if (count.Count > 5)
            {
                var objectNotification = new Message();
                objectNotification.AddNotification(new Notification("Usuário", "Número de inserções alcançadas. Delete alguns registros para poder incluir novos."));
                AddNotifications(objectNotification.Notifications);

                return null;
            }

            return provider;
        }

        #endregion
    }
}
