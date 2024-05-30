using HomeBanking.DTOs;
using HomeBanking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        public LoansController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpGet]
        public IActionResult GetAllLoans()
        {
            try
            {
                var loans = _loanRepository.GetAllLoans();
                var loansDTO = loans.Select(l => new LoanDTO(l));
                return Ok(loansDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLoanById(long id)
        {
            try
            {
                var loan = _loanRepository.GetLoanById(id);
                var loanDTO = new LoanDTO(loan);
                return Ok(loanDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
