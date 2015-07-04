using Android.Text;
using Java.Lang;
using Exception = System.Exception;

namespace GW2Trader.Android.Filter
{
    public class RangeInputFilter : Object, IInputFilter
    {
        private readonly int _max;
        private readonly int _min;

        public RangeInputFilter(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart,
            int dend)
        {
            try
            {
                var val = dest.ToString().Insert(dstart, source.ToString());
                var input = int.Parse(val);
                if (IsInRange(_min, _max, input))
                    return null;
            }
            catch (Exception ex)
            {
            }

            return new String(string.Empty);
        }

        private bool IsInRange(int min, int max, int input)
        {
            return max > min ? input >= min && input <= max : input >= max && input <= min;
        }
    }
}