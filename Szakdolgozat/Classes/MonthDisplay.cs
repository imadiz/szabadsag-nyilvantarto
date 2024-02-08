using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Classes
{
    public partial class MonthDisplay(string monthname, ObservableCollection<DateTimeOffset> displaydates) : ObservableObject
    {
        [ObservableProperty]
        private string _monthName = monthname;

        [ObservableProperty]
        private ObservableCollection<DateTimeOffset> _displayDates = displaydates;
    }
}
