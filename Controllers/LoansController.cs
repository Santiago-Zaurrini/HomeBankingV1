using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public IActionResult GetAllLoans()
        {
            try
            {
                var loans = _loanService.GetAllLoans();
                return Ok(loans.Select(l => new LoanDTO(l)));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLoanById(long id)
        {
            try
            {
                var loan = _loanService.GetLoanById(id);
                return Ok(new LoanDTO(loan));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult PostLoan([FromBody] LoanAplicationDTO loanAplication)
        {
            try
            {
                string clientEmail = User.FindFirst("Client")?.Value ?? string.Empty;
                _loanService.CreateLoan(loanAplication, clientEmail);

                return StatusCode(201, "Préstamo creado");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
