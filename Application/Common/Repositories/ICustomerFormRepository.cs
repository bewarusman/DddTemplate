using Application.Common.Services;
using Domain.SamtContext;

namespace Application.Common.Repositories;

public interface ICustomerFormRepository : ISingletonService
{
    Task<IList<CustomerForm>> FindByDate(string from, string to);
}