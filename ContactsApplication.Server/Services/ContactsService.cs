using ContactsApplication.Server.Models;
using ContactsApplication.Server.Models.Options;
using ContactsApplication.Server.Repositories.Interfaces;
using ContactsApplication.Server.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace ContactsApplication.Server.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly ContactsOption _contactsOption;
        private readonly ILogger<ContactsService> _logger;
        public ContactsService(IContactsRepository contactsRepository,
            IOptions<ContactsOption> contactsOption,
            ILogger<ContactsService> logger)
        {
            _contactsRepository = contactsRepository;
            _contactsOption = contactsOption.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            try
            {
                return await TryGetContactsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Contact> CreateContactAsync(Contact contactRequest)
        {
            try
            {
                var existingContacts = await GetContactsAsync();

                return await TryCreateContactAsync(contactRequest, existingContacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<IEnumerable<Contact>> TryGetContactsAsync()
        {
            return await _contactsRepository.GetAllAsync(_contactsOption.FilePath);
        }

        private async Task<Contact> TryCreateContactAsync(Contact contactRequest,
            IEnumerable<Contact> existingContacts)
        {
            return await _contactsRepository.CreateAsync(contactRequest,
                existingContacts,
                _contactsOption.FilePath);
        }
    }
}
