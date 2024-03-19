using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Classes;
using Szakdolgozat.Views;

namespace Szakdolgozat.ViewModels
{
    public partial class ControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl currentPage = new LoginView();//Jelenleg megjelenítendő UC

        public ControlViewModel()
        {
            MessageBus.Current.Listen<string>("ChangeView").Subscribe(async (changelogin) =>
            {
                await Task.Delay(50);//Animációvárás
                CurrentPage = new LeaveView();//Beléptetés
            });
        }
    }
}