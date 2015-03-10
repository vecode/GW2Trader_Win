using GW2Trader.MVVM;

namespace GW2Trader.ViewModel
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string ViewModelName { get; protected set; }
    }
}