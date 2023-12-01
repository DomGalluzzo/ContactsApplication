using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ContactsApplication.Server.Models;

namespace ContactsApplication.Server.Tests.ContactsServiceTests
{
    [TestClass]
    public class CreateContactAsync : ContactsServiceTest
    {
        [TestMethod]
        public async Task ItShouldCallContactsRepositoryGetAllAsync()
        {
            await contactsService.CreateContactAsync(It.IsAny<Contact>());

            mockContactsRepository.Verify(r => r.GetAllAsync(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task ItShouldCallContactsRepositoryCreateAsync()
        {
            await contactsService.CreateContactAsync(It.IsAny<Contact>());

            mockContactsRepository.Verify(r => r.CreateAsync(It.IsAny<Contact>(),
                It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()),
                Times.Once);
        }

        [TestMethod]
        public async Task ItShouldLogErrorWhenExceptionThrown()
        {
            mockContactsRepository.Setup(r => r.CreateAsync(It.IsAny<Contact>(),
                It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            contactsService = GetContactsService();

            try
            {
                await contactsService.CreateContactAsync(It.IsAny<Contact>());
            }
            catch (Exception)
            {
                Assert.IsTrue(logger.Count() > 0);

                return;
            }

            Assert.Fail();
        }
    }
}
