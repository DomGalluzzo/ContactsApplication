using ContactsApplication.Server.Models;

namespace ContactsApplication.Server.Services.Interfaces
{
    public interface IContactsService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> CreateContactAsync(Contact contactRequest);
    }
}
