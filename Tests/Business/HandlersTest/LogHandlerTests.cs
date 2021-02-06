using DataAccess.Abstract;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LogHandlerTests
    {
        Mock<ILogRepository> _logRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _logRepository = new Mock<ILogRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Log_GetQuery_Success()
        {


        }

    }
}

