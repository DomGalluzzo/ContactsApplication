using ContactsApplication.Server.Repositories.Interfaces;
using ContactsApplication.Server.Models;
using Newtonsoft.Json;

namespace ContactsApplication.Server.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ILogger<ContactsRepository> _logger;

        public ContactsRepository(ILogger<ContactsRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync(string filePath)
        {
            try
            {
                return await TryGetAllAsync(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Contact> CreateAsync(Contact contactRequest,
            IEnumerable<Contact> existingContacts, string filePath)
        {
            try
            {
                return await TryCreateAsync(contactRequest, existingContacts, filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<IEnumerable<Contact>> TryGetAllAsync(string filePath)
        {
            var jsonContent = await File.ReadAllTextAsync(filePath);

            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(jsonContent);
        }

        private async Task<Contact> TryCreateAsync(Contact contactRequest,
            IEnumerable<Contact> existingContacts, string filePath)
        {
            var maxId = existingContacts.Any() ? existingContacts.Max(c => c.Id) : 0;
            contactRequest.Id = maxId + 1;
            existingContacts = existingContacts.Append(contactRequest);

            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(existingContacts));

            return contactRequest;
        }
    }
}
