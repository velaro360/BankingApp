using Application.Account;
using Application.DTO;
using AutoMapper;
using BankingApp.Request;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _autoMapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _autoMapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAsync([FromQuery] int accountId)
        {
            var account = await _accountService.GetAsync(accountId);
            return Ok(account);
        }

        [HttpGet]
        [Route("getList")]
        public async Task<IActionResult> GetListAsync([FromQuery] int userId)
        {
            //get all user's accounts
            var accounts = await _accountService.GetListAsync(userId);
            return Ok(accounts);
        }

        [HttpGet]
        [Route("balance")]
        public async Task<IActionResult> GetBalanceAsync([FromQuery] int accountId)
        {
            var balance = await _accountService.GetBalanceAsync(accountId);
            return Ok(balance);
        }

        [HttpPost]
        [Route("open")]
        public async Task<IActionResult> AddAsync([FromBody] AddAccountRequest request)
        {
            var accountDto = _autoMapper.Map<AccountDTO>(request);
            await _accountService.AddAsync(accountDto);
            return Ok("Account added");
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int accountId)
        {
            await _accountService.DeleteAsync(accountId);
            return Ok("Account deleted");
        }
    }
}
