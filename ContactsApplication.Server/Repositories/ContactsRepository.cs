﻿using ContactsApplication.Server.Repositories.Interfaces;
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

        private async Task<IEnumerable<Contact>> TryGetAllAsync(string filePath)
        {
            var jsonContacts = await TryParseAsync(filePath);

            return new List<Contact>();
        }

        private async Task<IEnumerable<Contact>> TryParseAsync(string filePath)
        {
            return new List<Contact>();
        }
    }
}
