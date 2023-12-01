using Microsoft.AspNetCore.Mvc;
using ContactsApplication.Server.Services.Interfaces;
using ContactsApplication.Server.Models;

namespace ContactsApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactsService contactsService,
            ILogger<ContactsController> logger)
        {
            _contactsService = contactsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactsAsync()
        {
            try
            {
                var contacts = await _contactsService.GetContactsAsync();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactAsync(Contact contactRequest)
        {
            try
            {
                var newContact = await _contactsService.CreateContactAsync(contactRequest);

                return Ok(newContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContactAsync(int contactId)
        {
            try
            {
                var deletedContact = await _contactsService.DeleteContactAsync(contactId);

                return Ok(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{contactId}")]
        public async Task<IActionResult> UpdateContactAsync(int contactId, [FromBody] Contact contactRequest)
        {
            try
            {
                var updatedContact = await _contactsService.UpdateContactAsync(contactId, contactRequest);

                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
