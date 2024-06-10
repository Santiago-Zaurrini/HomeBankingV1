using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _accountService.GetAllAccounts();
                return Ok(accounts.Select(ac => new AccountDTO(ac)).ToList());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(long id)
        {
            try
            {
                var account = _accountService.FindAccountById(id);
                return Ok(new AccountDTO(account));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
