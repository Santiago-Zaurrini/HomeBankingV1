using HomeBanking.DTOs;
using HomeBanking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _trRepository;

        public TransactionsController(ITransactionRepository trRepository)
        {
             _trRepository = trRepository;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            try
            {
                var transactions = _trRepository.GetAllTransactions();
                var transactionsDTO = transactions.Select(tr => new TransactionDTO(tr)).ToList();
                return Ok(transactionsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionById(long id)
        {
            try
            {
                var transaction = _trRepository.GetTransactionById(id);
                var transactionDTO = new TransactionDTO(transaction);
                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
