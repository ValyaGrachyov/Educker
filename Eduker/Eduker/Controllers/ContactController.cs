using Eduker.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using Persistence.services.EmailService;

namespace Eduker.Controllers;

[Route("/contact")]
public class ContactController : Controller
{
    private IEmailService _emailService;

    public ContactController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index([FromForm]ContactVm contactVm)
    {
        var message = new ResponseVm();
        try
        {
            await _emailService.SendMessageAsync(
                "oris.testmessage1@gmail.com",
                contactVm.Subject,
                $"User {contactVm.Name} with email {contactVm.Email} write you a message:\n{contactVm.Message}");
            message.Message = "Thanks for your comment";
        }
        catch (Exception e)
        {
            message.Message = "Error";
        }
        return View(message);
    }
}