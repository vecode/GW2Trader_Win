using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public abstract class GenericContextProvider<T> where T : DbContext, new()
    {
        public virtual T GetContext()
        {
            return new T();
        }
    }
}
