using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Models;
using HomeBanking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IClientService _clientService;

        public TransactionsController(ITransactionService transactionService, 
            IClientService clientService)
        {            
            _transactionService = transactionService;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            try
            {
                var transactions = _transactionService.GetAllTransactions();
                var transactionsDTO = transactions.Select(tr => new TransactionDTO(tr)).ToList();
                return Ok(transactionsDTO);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionById(long id)
        {
            try
            {
                var transaction = _transactionService.GetTransactionById(id);
                var transactionDTO = new TransactionDTO(transaction);
                return Ok(transactionDTO);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult Transfer([FromBody] TransferRequestDTO transferRequest)
        {
            try
            {
                string email = User.FindFirst("Client")?.Value ?? string.Empty;

                Client currentClient = _clientService.GetCurrent(email);

                var clientAccounts = currentClient.Accounts.Select(a => a.Number);

                if(!clientAccounts.Contains(transferRequest.FromAccountNumber))
                    return StatusCode(403, "La cuenta de origen no pertenece al cliente actual.");

                _transactionService.Transfer(transferRequest.FromAccountNumber, transferRequest.ToAccountNumber,
                    transferRequest.Amount, transferRequest.Description);

                return StatusCode(201, "Transferencia realizada con éxito.");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
