using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_due
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
       {
        CreateMap<Expense, GetExpenseDto>();
        CreateMap<AddExpenseDto,Expense>();
       } 
    }
}