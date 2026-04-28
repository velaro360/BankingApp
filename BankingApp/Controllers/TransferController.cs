using Application.DTO;
using Application.Transfer;
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
    public class TransferController : BankingAppBaseController
    {
        private readonly ITransferService _transferService;
        private readonly IMapper _autoMapper;

        public TransferController(ITransferService transferService, IMapper autoMapper)
        {
            _transferService = transferService;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> TransferAsync([FromBody] TransferRequest request)
        {
            var currentUserId = GetCurrentUserId();
            var transferDTO = _autoMapper.Map<TransferDTO>(request);
            await _transferService.TransferAsync(transferDTO, currentUserId);
            return Ok("Transfer completed");
        }
    }
}
