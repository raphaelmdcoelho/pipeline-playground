using Moq;
using PipelinePlayground.Configurations;
using PipelinePlayground.Entities;
using PipelinePlayground.Infrastructure.Data.Contexts;
using System.Data;

namespace PipelinePlayground.Tests
{
    public class AppTests
    {
        [Fact]
        public async Task Run_Should_Call_OdbcConnection()
        {
            var odbcContext = new Mock<IOdbcContext>();
            var dbConnectionMock = new Mock<IDbConnection>();

            odbcContext
                .Setup(odbcConnectionFactoryMock => odbcConnectionFactoryMock
                .OpenConnection(It.IsAny<string>()));

            odbcContext
                .Setup(odbcConnectionFactoryMock => odbcConnectionFactoryMock
                .ExecuteAsync<AppUser>(It.IsAny<string>()))
                .ReturnsAsync(new List<AppUser>());


            var app = new App(odbcContext.Object, () => new OdbcConfiguration
            {
                ConnectionString = "connectionString"
            });

            await app.Run();

            odbcContext.Verify(x => x.OpenConnection("connectionString"), Times.Once);
        }
    }
}