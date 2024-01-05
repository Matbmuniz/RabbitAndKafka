using App.Models.Entity;

namespace App.Repository.Interfaces
{
    public interface IAppMessageRepository
    {
        void SendMessage(AppMessage message);
    }
}
