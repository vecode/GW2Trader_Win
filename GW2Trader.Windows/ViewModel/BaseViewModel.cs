using GW2Trader.Desktop.MVVM;

namespace GW2Trader.Desktop.ViewModel
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string ViewModelName { get; protected set; }
    }
}