using System.Collections.Generic;
using GW2Trader.Model;

namespace GW2Trader.Manager
{
    public interface IInvestmentManager
    {        
        List<InvestmentList> GetInvestmentLists();
        void AddInvestmentList(string name, string description);
        void DeleteInvestmentList(InvestmentList investmentList);
        void UpdateInvestmentList(InvestmentList investmentList);

        void AddInvestment(Investment investment, InvestmentList investmentList);
        void DeleteInvestment(Investment investment, InvestmentList investmentList);
        void UpdateInvestment(Investment investment);
    }
}
