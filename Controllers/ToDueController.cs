using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace to_due.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDueController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        // constructor: dependency injection
        public ToDueController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("Expenses")]
        public async Task<ActionResult<ServiceResponse<List<GetExpenseDto>>>> GetAll()
        {
            return Ok(await _expenseService.GetAllExpenses());
        }

        [HttpGet("{ExpenseId}")]
        public async Task<ActionResult<ServiceResponse<GetExpenseDto>>> GetById(Guid ExpenseId)
        {
            return Ok(await _expenseService.GetExpenseById(ExpenseId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetExpenseDto>>>> AddExpense(AddExpenseDto newExpense)
        {
            return Ok(await _expenseService.AddExpense(newExpense));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetExpenseDto>>>> UpdateExpense(UpdateExpenseDto updatedExpense)
        {
            var res = await _expenseService.UpdateExpense(updatedExpense);

            if(res == null) {
                return NotFound(res);
            }

            return Ok(res);
        }

        [HttpDelete("{ExpenseId}")]
        public async Task<ActionResult<ServiceResponse<GetExpenseDto>>> DeleteExpense(Guid ExpenseId)
        {
             var res = await _expenseService.DeleteExpense(ExpenseId);

            if(res == null) {
                return NotFound(res);
            }

            return Ok(res);
        }
    }
}