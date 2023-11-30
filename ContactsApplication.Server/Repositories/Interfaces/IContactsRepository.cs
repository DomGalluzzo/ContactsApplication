﻿using ContactsApplication.Server.Models;

namespace ContactsApplication.Server.Repositories.Interfaces
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync(string filePath);
    }
}
