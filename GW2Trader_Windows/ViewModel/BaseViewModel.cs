using GW2Trader_Windows.MVVM;

namespace GW2Trader_Windows.ViewModel
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string ViewModelName { get; protected set; }
    }
}