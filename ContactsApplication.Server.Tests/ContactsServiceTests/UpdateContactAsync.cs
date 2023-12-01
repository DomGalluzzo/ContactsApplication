using ContactsApplication.Server.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactsApplication.Server.Tests.ContactsServiceTests
{
    [TestClass]
    public class UpdateContactAsync : ContactsServiceTest
    {
        [TestMethod]
        public async Task ItShouldCallContactsRepositoryGetAllAsync()
        {
            await contactsService.UpdateContactAsync(It.IsAny<int>(), It.IsAny<Contact>());

            mockContactsRepository.Verify(r => r.GetAllAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ItShouldCallContactsRepositoryUpdateAsync()
        {
            await contactsService.UpdateContactAsync(It.IsAny<int>(), It.IsAny<Contact>());

            mockContactsRepository.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<IEnumerable<Contact>>(), It.IsAny<Contact>(), It.IsAny<string>()),
                Times.Once);
        }

        [TestMethod]
        public async Task ItShouldLogErrorWhenExceptionThrown()
        {
            mockContactsRepository.Setup(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<IEnumerable<Contact>>(), It.IsAny<Contact>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            contactsService = GetContactsService();

            try
            {
                await contactsService.UpdateContactAsync(It.IsAny<int>(), It.IsAny<Contact>());
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
