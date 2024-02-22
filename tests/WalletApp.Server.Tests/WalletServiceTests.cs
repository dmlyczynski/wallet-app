using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using WalletApp.Server.Core.Models;
using WalletApp.Server.Domain;

namespace WalletApp.Server.Tests
{
    public class WalletServiceTests
    {
        private readonly IFixture _fixture;

        public WalletServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void AddFunds_WhenFundsIsLessThanZero_ShouldThrownException()
        {
            // Arrange 
            _fixture.Freeze<Mock<IWalletRepository>>()
                .Setup(x => x.Get(It.IsAny<long>()))
                .ReturnsAsync(() => new Wallet());

            // Assert 
            Assert.ThrowsAsync<InvalidOperationException>(() => CreateSut().AddFunds(10, -1));
        }

        [Fact]
        public void RemoveFunds_WhenFundsIsLessThanZero_ShouldThrownException()
        {
            // Arrange 
            _fixture.Freeze<Mock<IWalletRepository>>()
                .Setup(x => x.Get(It.IsAny<long>()))
                .ReturnsAsync(() => new Wallet());

            // Assert 
            Assert.ThrowsAsync<InvalidOperationException>(() => CreateSut().RemoveFunds(10, -1));
        }

        private IWalletService CreateSut() => _fixture.Create<WalletService>();
    }
}