
namespace to_due.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task<ServiceResponse<List<GetExpenseDto>>> GetAllExpenses();
        Task<ServiceResponse<GetExpenseDto>> GetExpenseById(Guid id);
        Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense);
        Task<ServiceResponse<GetExpenseDto>> UpdateExpense(UpdateExpenseDto updatedExpense);
        Task<ServiceResponse<List<GetExpenseDto>>> DeleteExpense(Guid id);
    }
}