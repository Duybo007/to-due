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
        private readonly DataContext _context;
        public ExpenseService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense)
        {
            var serviceResponse = new ServiceResponse<List<GetExpenseDto>>();
            var expense = _mapper.Map<Expense>(newExpense); //map AddExpenseDto to Expense

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Expenses.Select(e => _mapper.Map<GetExpenseDto>(e)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetExpenseDto>>> DeleteExpense(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetExpenseDto>>();

            try
            {
            // var expense = expenses.FirstOrDefault(expense => expense.Id == id);

            var expense = await _context.Expenses.FirstOrDefaultAsync( e => e.Id == id);

            if(expense == null) 
                throw new Exception($"Expense with ID '{id}' not found.");

            _context.Expenses.Remove(expense);

            serviceResponse.Data = await _context.Expenses.Select(e => _mapper.Map<GetExpenseDto>(e)).ToListAsync();
    
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
            var dbExpenses = await _context.Expenses.ToListAsync();
            serviceResponse.Data = dbExpenses.Select(e => _mapper.Map<GetExpenseDto>(e)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDto>> GetExpenseById(Guid ExpenseId)
        {
            var serviceResponse = new ServiceResponse<GetExpenseDto>();
            var dbExpense = await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == ExpenseId);

            serviceResponse.Data = _mapper.Map<GetExpenseDto>(dbExpense);//which type the value of expense should be mapped to 

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDto>> UpdateExpense(UpdateExpenseDto updatedExpense)
        {
            var serviceResponse = new ServiceResponse<GetExpenseDto>();

            try
            {
            var expense = await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == updatedExpense.Id);

            if(expense == null) 
                throw new Exception($"Expense with ID '{updatedExpense.Id}' not found.");

            expense.Amount = updatedExpense.Amount;
            expense.DueDate = updatedExpense.DueDate;
            expense.Recurring = updatedExpense.Recurring;
            expense.AutoPayment = updatedExpense.AutoPayment;
            expense.Note = updatedExpense.Note;

            await _context.SaveChangesAsync();
            
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