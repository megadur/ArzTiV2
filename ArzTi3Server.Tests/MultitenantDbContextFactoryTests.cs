using ArzTi3Server.Services;
using Xunit;

namespace ArzTi3Server.Tests
{
    public class MultitenantDbContextFactoryTests
    {
        [Fact]
        public void CreateDbContext_WithConnectionString_ReturnsDbContext()
        {
            // Arrange
            var factory = new MultitenantDbContextFactory();
            var connectionString = "Host=localhost;Database=test;Username=test;Password=test;";

            // Act
            using var context = factory.CreateDbContext(connectionString);

            // Assert
            Assert.NotNull(context);
            Assert.Contains("test", connectionString);
        }

        [Fact]
        public void CreateDbContext_WithDifferentConnectionStrings_ReturnsDifferentContexts()
        {
            // Arrange
            var factory = new MultitenantDbContextFactory();
            var connectionString1 = "Host=localhost;Database=client1;Username=test;Password=test;";
            var connectionString2 = "Host=localhost;Database=client2;Username=test;Password=test;";

            // Act
            using var context1 = factory.CreateDbContext(connectionString1);
            using var context2 = factory.CreateDbContext(connectionString2);

            // Assert
            Assert.NotNull(context1);
            Assert.NotNull(context2);
            Assert.NotSame(context1, context2);
            Assert.Contains("client1", connectionString1);
            Assert.Contains("client2", connectionString2);
        }
    }
}