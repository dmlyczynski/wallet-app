using Microsoft.Extensions.DependencyInjection;

namespace WalletApp.Server.Domain
{
    public static class SevicesExtension
    {
        public static IServiceCollection AddWalletServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IWalletRepository, WalletReposiotry>()
                .AddTransient<IWalletService, WalletService>();
        }
    }
}
