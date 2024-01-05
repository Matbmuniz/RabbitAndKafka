using App.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using App.Services.Interfaces;

namespace App.Publisher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMessagesController : ControllerBase
    {
        private readonly IAppMessageService _service;

        public RabbitMessagesController(IAppMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public void AddMessage(AppMessage message)
        {
            _service.SendMessage(message);
        }
    }
}
