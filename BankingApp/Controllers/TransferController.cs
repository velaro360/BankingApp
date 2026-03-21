using Application.DTO;
using Application.Transfer;
using AutoMapper;
using BankingApp.Request;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
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
            var transferDTO = _autoMapper.Map<TransferDTO>(request);
            await _transferService.TransferAsync(transferDTO);
            return Ok("Transfer completed");
        }
    }
}
