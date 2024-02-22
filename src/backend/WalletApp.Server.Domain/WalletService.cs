using WalletApp.Server.Core.Models;

namespace WalletApp.Server.Domain
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository repository;

        public WalletService(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task Create(Wallet wallet)
        {
            await repository.Create(wallet);
        }

        public async Task<Wallet?> Get(long walletId)
        {
            return await repository.Get(walletId);
        }

        public async Task AddFunds(long walletId, decimal funds)
        {
            if (funds <= 0)
            {
                throw new InvalidOperationException("Invalid funds. Please enter a positive value.");
            }

            var wallet = await repository.Get(walletId);

            if (wallet is null)
            {
                throw new ArgumentNullException();
            }

            wallet.Balance += funds;

            await repository.Update(wallet);
        }

        public async Task RemoveFunds(long walletId, decimal funds)
        {
            if (funds <= 0)
            {
                throw new InvalidOperationException("Invalid funds. Please enter a positive value.");
            }

            var wallet = await repository.Get(walletId);

            if (wallet is null)
            {
                throw new ArgumentNullException("Wallet not exists");
            }

            if (wallet.Balance < funds)
            {
                throw new ArgumentNullException("Insufficient funds.");
            }

            wallet.Balance -= funds;

            await repository.Update(wallet);
        }

        public async Task<decimal> GetBalance(long accountId)
        {
            return await repository.GetBalance(accountId);
        }

        public async Task<IEnumerable<Wallet>> GetWallets()
        {
            return await repository.GetWallets();
        }
    }

    public interface IWalletService
    {
        Task AddFunds(long walletId, decimal founds);
        Task Create(Wallet wallet);
        Task<Wallet?> Get(long walletId);
        Task<decimal> GetBalance(long accountId);
        Task<IEnumerable<Wallet>> GetWallets();
        Task RemoveFunds(long walletId, decimal founds);
    }
}
