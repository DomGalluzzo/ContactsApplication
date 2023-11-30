using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactsApplication.Server.Tests.ContactsServiceTests
{
    [TestClass]
    public class GetContactsAsync : ContactsServiceTest
    {
        [TestMethod]
        public async Task ItShouldCallContactsRepositoryGetAll()
        {
            await contactsService.GetContactsAsync();

            mockContactsRepository.Verify(r => r.GetAllAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ItShouldLogErrorWhenExceptionThrown()
        {
            mockContactsRepository.Setup(r => r.GetAllAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            contactsService = GetContactsService();

            try
            {
                await contactsService.GetContactsAsync();
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
