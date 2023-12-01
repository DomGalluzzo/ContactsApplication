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

        public async Task<int> DeleteAsync(int contactId,
            IEnumerable<Contact> existingContacts, string filePath)
        {
            try
            {
                return await TryDeleteAsync(contactId, existingContacts, filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Contact> UpdateAsync(int contactId,
            IEnumerable<Contact> existingContacts, Contact contactRequest, string filePath)
        {
            try
            {
                return await TryUpdateAsync(contactId, existingContacts, contactRequest, filePath);
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

        private async Task<int> TryDeleteAsync(int contactId,
            IEnumerable<Contact> existingContacts, string filePath)
        {
            var existingList = existingContacts.ToList();
            var contactToDelete = existingContacts.FirstOrDefault(c => c.Id == contactId);

            if (contactToDelete == null)
                return -1;

            existingList.Remove(contactToDelete);

            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(existingList));

            return contactToDelete.Id;
        }

        private async Task<Contact> TryUpdateAsync(int contactId,
            IEnumerable<Contact> existingContacts, Contact contactRequest, string filePath)
        {
            var existingList = existingContacts.ToList();
            var contactToUpdateIndex = existingList.FindIndex(c => c.Id == contactId);

            if (contactToUpdateIndex == -1)
                throw new Exception();

            existingList[contactToUpdateIndex] = contactRequest;
            contactRequest.Id = contactId;

            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(existingList));

            return contactRequest;
        }
    }
}
