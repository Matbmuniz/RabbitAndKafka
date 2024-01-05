using App.Models.Entity;

namespace App.Services.Interfaces
{
    public interface IAppMessageService
    {
        void SendMessage(AppMessage message);
    }
}
