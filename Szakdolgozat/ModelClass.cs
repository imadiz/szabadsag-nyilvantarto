using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat
{
    public partial class ModelClass : ObservableObject
    {
        //Adatok és logika
        [ObservableProperty]
        private DateTimeOffset _currentyear = new(DateTime.Now);//Az évszámválasztó értéke
    }
}
