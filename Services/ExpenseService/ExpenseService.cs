using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace to_due.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private static List<Expense> expenses = new List<Expense> {
            new Expense(),
            new Expense{
                Amount = 10
            }
        };

        private readonly IMapper _mapper;
        public ExpenseService(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense)
        {
            var serviceResponse = new ServiceResponse<List<GetExpenseDto>>();
            expenses.Add(_mapper.Map<Expense>(newExpense)); //map AddExpenseDto to Expense
            serviceResponse.Data = expenses.Select(e => _mapper.Map<GetExpenseDto>(e)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetExpenseDto>>> DeleteExpense(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetExpenseDto>>();

            try
            {
            var expense = expenses.FirstOrDefault(expense => expense.Id == id);

            if(expense == null) 
                throw new Exception($"Expense with ID '{id}' not found.");

            expenses.Remove(expense);

            serviceResponse.Data = expenses.Select(e => _mapper.Map<GetExpenseDto>(e)).ToList();
    
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetExpenseDto>>> GetAllExpenses()
        {
            var serviceResponse = new ServiceResponse<List<GetExpenseDto>>();
            serviceResponse.Data = expenses.Select(e => _mapper.Map<GetExpenseDto>(e)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDto>> GetExpenseById(Guid ExpenseId)
        {
            var serviceResponse = new ServiceResponse<GetExpenseDto>();
            var expense = expenses.FirstOrDefault(expense => expense.Id == ExpenseId);

            serviceResponse.Data = _mapper.Map<GetExpenseDto>(expense);//which type the value of expense should be mapped to 

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDto>> UpdateExpense(UpdateExpenseDto updatedExpense)
        {
            var serviceResponse = new ServiceResponse<GetExpenseDto>();

            try
            {
            var expense = expenses.FirstOrDefault(expense => expense.Id == updatedExpense.Id);

            if(expense == null) 
                throw new Exception($"Expense with ID '{updatedExpense.Id}' not found.");

            expense.Amount = updatedExpense.Amount;
            expense.DueDate = updatedExpense.DueDate;
            expense.Recurring = updatedExpense.Recurring;
            expense.AutoPayment = updatedExpense.AutoPayment;
            expense.Note = updatedExpense.Note;

            serviceResponse.Data = _mapper.Map<GetExpenseDto>(expense);
    
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}