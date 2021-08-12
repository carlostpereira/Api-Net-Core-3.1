using App.Domain.Commands.Request.User;
using App.Domain.Commands.Response;
using App.Domain.Entities;
using App.Domain.Repositories;
using App.Shared.Commands;
using App.SharedKernel.Library;
using AutoMapper;
using FluentValidator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Commands.Handlers
{
    public class UserCommandHandler : Notifiable,
                                            ICommandHandler<RegisterUserRequest>,
                                            ICommandHandler<UpdateUserRequest>,
                                            ICommandHandler<DeleteUserRequest>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region GET VERBS

        public async Task<ICollection<UserResponse>> Handle()
        {
            var commandObject = await _repository.GetAsync();

            return _mapper.Map<List<UserResponse>>(commandObject);
        }

        public async Task<UserResponse> Handle(string email, string password)
        {
            var commandObject = await _repository.LoginUserAsync(email, SharedFunctions.EncryptPassword(password));

            if (commandObject == null)
            {
                var objectNotification = new Message();
                objectNotification.AddNotification(new Notification("Login", "Usuário ou senha inválidos"));
                AddNotifications(objectNotification.Notifications);
                return null;
            }

            if (!commandObject.Active)
            {
                var objectNotification = new Message();
                objectNotification.AddNotification(new Notification("User", "Este e-mail não está ativo, é necessário ativar seu e-mail para logar no sistema."));
                AddNotifications(objectNotification.Notifications);
                return null;
            }

            return _mapper.Map<UserResponse>(commandObject);
        }

        #endregion

        #region POST, UPDATE AND DELETE VERBS

        public async Task<ICommandResult> Handle(RegisterUserRequest command)
        {
            // 1. Criar instância do objeto
            var commandObject = new User(default, command.Username, command.Password, command.ConfirmPassword, command.Email, command.Gender, command.Role);

            // 2. Adicionar notificações
            AddNotifications(commandObject.Notifications);
            await ValidateToCreate(commandObject);

            // 3. Verificar se é válido (se existem notificações
            if (!IsValid())
                return null;

            // 4. Salva as alterações no EF
            await _repository.SaveAsync(commandObject);

            // 5. Retorna os dados que foram cadastrados
            return new UserResponse(commandObject.Id, commandObject.Name, commandObject.Email, commandObject.Gender, commandObject.Role);

        }

        public async Task<ICommandResult> Handle(UpdateUserRequest command)
        {
            // 1. Criar instância do objeto
            var commandObject = new User(default, command.Username, command.Password, command.ConfirmPassword, command.Email, command.Gender, command.Role);

            // 2. Adicionar notificações
            AddNotifications(commandObject.Notifications);

            // 3. Verificar se é válido (se existem notificações
            if (!IsValid())
                return null;

            // 4. Salva as alterações no EF
            await _repository.UpdateAsync(commandObject);

            // 5. Retorna os dados que foram cadastrados
            return new UserResponse(commandObject.Id, commandObject.Name, commandObject.Email, commandObject.Gender, commandObject.Role);

        }

        public async Task<ICommandResult> Handle(DeleteUserRequest command)
        {
            var commandObject = await _repository.GetByIdAsync(command.Id);

            if (commandObject == null)
            {
                var objectNotification = new Message();
                objectNotification.AddNotification(new Notification("Usuário", "Email não encontrado"));
                AddNotifications(objectNotification.Notifications);
                return null;
            }

            // 4. Deleta o registro
            await _repository.DeleteAsync(commandObject);

            // 5. Retorna os dados deletados
            return new UserResponse(commandObject.Id, commandObject.Name, commandObject.Email, commandObject.Gender, commandObject.Role);

        }

        private async Task<User> ValidateToCreate(User user)
        {
            var count = await _repository.GetAsync();

            if (count.Count > 5)
            {
                var objectNotification = new Message();
                objectNotification.AddNotification(new Notification("Usuário", "Número de inserções alcançadas. Delete alguns registros para poder incluir novos."));
                AddNotifications(objectNotification.Notifications);

                return null;
            }

            return user;
        }

        #endregion
    }
}
