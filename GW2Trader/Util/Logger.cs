using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GW2Trader.MVVM;

namespace GW2Trader.Util
{
    public class Logger : ObservableObject
    {
        private static Logger _instance;
        private static List<String> _logs;

        private Logger()
        {
            _logs = new List<string>();
        }

        public ObservableCollection<String> Logs
        {
            get { return new ObservableCollection<string>(_logs); }
        }

        public static string LastLog
        {
            get { return _logs.Count == 0 ? string.Empty : _logs.Last(); }
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }

        public void AddLog(string log)
        {
            _logs.Add(DateTime.Now.TimeOfDay + ": " + log);
            Console.WriteLine(LastLog);
            RaisePropertyChanged("LastLog");
        }
    }
}