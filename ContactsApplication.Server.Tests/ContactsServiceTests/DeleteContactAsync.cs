using ContactsApplication.Server.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactsApplication.Server.Tests.ContactsServiceTests
{
    [TestClass]
    public class DeleteContactAsync : ContactsServiceTest
    {
        [TestMethod]
        public async Task ItShouldCallContactsRepositoryDeleteAsync()
        {
            await contactsService.DeleteContactAsync(It.IsAny<int>());

            mockContactsRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ItShouldLogErrorWhenExceptionThrown()
        {
            mockContactsRepository.Setup(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<IEnumerable<Contact>>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            contactsService = GetContactsService();

            try
            {
                await contactsService.DeleteContactAsync(It.IsAny<int>());
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
