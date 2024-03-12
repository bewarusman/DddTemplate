using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.SamtContext;
using static Domain.SamtContext.CustomerForm;

namespace Infrastructure.Repositories;

public class CustomerFormRepository : ICustomerFormRepository
{
    private readonly IDbService _dbService;

    public CustomerFormRepository(IDbService dbService)
    {

        _dbService = dbService;
    }

    public async Task<IList<CustomerForm>> FindByDate(string from, string to)
    {
        var sql = @"
            SELECT C.Id,
                   C.LostMsisdn, 
                   C.Status AS ""CurrentStatus"", 
                   P.Name AS ""Shop"", 
                   A.Name AS ""Agent"", 
                   C.CreatedAt, 
                   F.FlowType, 
                   F.CreatedAt AS ""FlowDate"", 
                   AB.Name AS ""User"", 
                   F.Remark, 
                   F.Message 
            FROM CustomerForms C 
            INNER JOIN PosShops P ON C.ShopId = P.Id
            INNER JOIN AdAccounts A ON C.CreatedBy = A.Id
            INNER JOIN CustomerFormFlows F ON F.FormId = C.Id
            INNER JOIN AdAccounts AB ON F.CreatedBy = AB.Id 
            WHERE C.CreatedAt BETWEEN TO_DATE(:FromDate, 'YYYY-MM-DD') AND TO_DATE(:ToDate, 'YYYY-MM-DD') 
            ORDER BY C.CreatedAt DESC, F.CreatedAt ASC";

        var lookup = new Dictionary<string, CustomerForm>();
        return await _dbService.Query<CustomerForm, CustomerFormFlow, CustomerForm>(
            sql,
            new { FromDate = from, ToDate = to },
            (cForm, flow) =>
            {
                CustomerForm customerForm;
                if (!lookup.TryGetValue(cForm.Id, out customerForm))
                {
                    lookup.Add(cForm.Id, customerForm = cForm);
                }
                if (customerForm.Flows == null)
                    customerForm.Flows = new List<CustomerFormFlow>();
                customerForm.Flows.Add(flow);
                return customerForm;
            },
            "FlowType");
    }
}