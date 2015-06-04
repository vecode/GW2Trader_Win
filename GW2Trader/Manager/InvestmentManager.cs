using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Manager
{
    public class InvestmentManager : IInvestmentManager
    {
        private InvestmentRepository _invRepository;
        private InvestmentListRepository _invListRepository;

        public void AddInvestment(Model.Investment investment, Model.InvestmentList investmentList)
        {
            _invRepository.Save(investment);
            _invListRepository.Save(investmentList);
        }

        public void DeleteInvestment(Model.Investment investment, Model.InvestmentList investmentList)
        {
            investmentList.Investments.Remove(investment);
            _invListRepository.Save(investmentList);
            _invRepository.Delete(investment);
        }

        public void UpdateInvestment(Model.Investment investment)
        {
            _invRepository.Save(investment);
        }

        public List<Model.InvestmentList> GetInvestmentLists()
        {
            return _invListRepository.GetAll().Cast<Model.InvestmentList>().ToList();
        }

        public void AddInvestmentList(string name, string description)
        {
            _invListRepository.Save(new DataLayer.Model.InvestmentList
            {
                Name = name,
                Description = description
            });
        }

        public void DeleteInvestmentList(Model.InvestmentList investmentList)
        {
            _invListRepository.Delete(investmentList);
        }

        public void UpdateInvestmentList(Model.InvestmentList investmentList)
        {
            _invListRepository.Save(investmentList);
        }
    }
}
