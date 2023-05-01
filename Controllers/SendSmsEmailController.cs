using Microsoft.AspNetCore.Mvc;
using TP1examuml.Interface;

namespace TP1examuml.Controllers
{
    public class SendSmsEmailController : Controller
    {
        private readonly ISendSmsEmailRepository _sendSmsEmailRepository;
    
        public SendSmsEmailController(ISendSmsEmailRepository sendSmsEmailRepository) {
            _sendSmsEmailRepository = sendSmsEmailRepository;        
        }

        //public async Task<IActionResult> Index()
        //{
        //    await _sendSmsEmailRepository.SendEmailAsync("moctardiallo1956@gmail.com", "Test", "Bonjour Moctar");
        //    return View();
        //}
        //public async Task<string> Index()
        //{
        //    await _sendSmsEmailRepository.SendSmsAsync("+221781397254", "Bonjour Moctar");
        //    return "Test mail";
        //}

        public async Task<string> Index()
        {
            await _sendSmsEmailRepository.SendSmsAsync( "Bonjour Moctar");
            return "Test mail";
        }
    }
   
}
