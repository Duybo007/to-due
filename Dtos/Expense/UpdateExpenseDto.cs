using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_due.Dtos.Expense
{
    public class UpdateExpenseDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
        public ExpenseClass Recurring { get; set; } = ExpenseClass.None;
        public bool AutoPayment { get; set; } = false;
        public string Note { get; set;} = "";
    }
}