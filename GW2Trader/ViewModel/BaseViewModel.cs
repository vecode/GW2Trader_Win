using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.MVVM;

namespace GW2Trader.ViewModel
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string ViewModelName { get; protected set; }
    }
}
