using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Classes
{
    public partial class LeaveType : ObservableObject
    {
        [ObservableProperty]
        private string _displayname;

        [ObservableProperty]
        private string _name;
        public LeaveType(string displayname, string name)
        {
            Displayname = displayname;
            Name = name;
        }
    }
}
