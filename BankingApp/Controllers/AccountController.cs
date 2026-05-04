using Application.Account;
using Application.DTO;
using Application.User;
using AutoMapper;
using BankingApp.Controllers.Base;
using BankingApp.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    //To do: Consider use fluent validation in the future
    public class AccountController: BankingAppBaseController
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
            var currentUserId = GetCurrentUserId();
            var account = await _accountService.GetAsync(accountId, currentUserId);
            return Ok(account);
        }

        [HttpGet]
        [Route("getList")]
        public async Task<IActionResult> GetListAsync()
        {
            var userId = GetCurrentUserId();
            var accounts = await _accountService.GetListAsync(userId);
            return Ok(accounts);
        }

        [HttpGet]
        [Route("balance")]
        public async Task<IActionResult> GetBalanceAsync([FromQuery] int accountId)
        {
            var currentUserId = GetCurrentUserId();
            var balance = await _accountService.GetBalanceAsync(accountId, currentUserId);
            return Ok(balance);
        }

        [HttpPost]
        [Route("open")]
        public async Task<IActionResult> AddAsync([FromBody] AddAccountRequest request)
        {
            var currentUserId = GetCurrentUserId();
            var accountDto = _autoMapper.Map<AccountDTO>(request);
            await _accountService.AddAsync(accountDto, currentUserId);
            return Ok("Account added");
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int accountId)
        {
            var currentUserId = GetCurrentUserId();
            await _accountService.DeleteAsync(accountId, currentUserId);
            return Ok("Account deleted");
        }
    }
}
