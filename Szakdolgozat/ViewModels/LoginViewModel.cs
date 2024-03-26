using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Models;

namespace Szakdolgozat.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoginModelClass _model = new();
        public ReactiveCommand<string, Unit> Command_AttemptLogin { get; }
        public LoginViewModel()
        {
            Command_AttemptLogin = ReactiveCommand.Create<string>((logindata) =>
            {
                Model.AttemptLogin();
            });
        }
    }
}
