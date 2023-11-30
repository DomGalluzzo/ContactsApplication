using Moq;
using Microsoft.Extensions.Options;
using ContactsApplication.Server.Services;
using ContactsApplication.Server.Models.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactsApplication.Server.Repositories.Interfaces;
using ContactsApplication.Server.Models;
using Microsoft.Extensions.Logging;

namespace ContactsApplication.Server.Tests.ContactsServiceTests
{
    [TestClass]
    public abstract class ContactsServiceTest
    {
        protected ContactsService contactsService;
        protected Mock<IContactsRepository> mockContactsRepository;
        protected Mock<IOptions<ContactsOption>> mockContactsOption;
        protected IList<string> logger;
        protected ContactsOption contactsOption;

        [TestInitialize]
        public virtual void Setup()
        {
            contactsOption = new ContactsOption();
            mockContactsRepository = GetMockContactsRepository();
            mockContactsOption = GetMockContactsOption();
            logger = new List<string>();

            contactsService = GetContactsService();
        }

        protected Mock<IContactsRepository> GetMockContactsRepository()
        {
            var mockRepo = new Mock<IContactsRepository>();
            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Contact>());

            return mockRepo;
        }
        
        protected Mock<IOptions<ContactsOption>> GetMockContactsOption()
        {
            var mockOption = new Mock<IOptions<ContactsOption>>();
            mockOption.Setup(o => o.Value)
                .Returns(contactsOption);

            return mockOption;
        }

        protected Mock<ILogger<ContactsService>> GetMockLogger(Action<LogLevel, EventId, object, Exception, object> callBack)
        {
            var mockLogger = new Mock<ILogger<ContactsService>>();
            mockLogger.Setup((ILogger<ContactsService> x) => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Callback(callBack);

            return mockLogger;
        }

        protected ContactsService GetContactsService()
        {
            return new ContactsService(mockContactsRepository.Object,
                mockContactsOption.Object,
                GetMockLogger((l, e, o, ex, f) => logger.Add(o.ToString())).Object);
        }
    }
}
