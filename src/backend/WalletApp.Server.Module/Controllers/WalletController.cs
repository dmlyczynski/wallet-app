using Microsoft.AspNetCore.Mvc;
using WalletApp.Server.Core.Models;
using WalletApp.Server.Domain;

namespace WalletApp.Server.Module.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            await walletService.Create(new Wallet()
            {
                Balance = 0,
                Name = Guid.NewGuid().ToString()
            });

            return Ok();
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get(long walletId)
        {
            Wallet? wallet;

            try
            {
                wallet = await walletService.Get(walletId);
            }
            catch (Exception)
            {
                return Problem("Does not exists");
            }

            return Ok(wallet);
        }

        [HttpPut]
        [Route("addFunds")]
        public async Task<IActionResult> AddFunds(long walletId, decimal funds)
        {
            try
            {
                await walletService.AddFunds(walletId, funds);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("removeFunds")]
        public async Task<IActionResult> RemoveFunds(long walletId, decimal funds)
        {
            try
            {
                await walletService.RemoveFunds(walletId, funds);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetWallets()
        {
            var walletList = await walletService.GetWallets();

            return Ok(walletList);
        }
    }
}
