using App.Models.Entity;
using App.Repository.Interfaces;
using App.Services.Interfaces;

namespace App.Services
{
    public class AppMessageService : IAppMessageService
    {
        private readonly IAppMessageRepository _repository;

        public AppMessageService(IAppMessageRepository repository)
        {
            _repository = repository;
        }
        public void SendMessage(AppMessage message)
        {
            _repository.SendMessage(message);
        }
    }
}
