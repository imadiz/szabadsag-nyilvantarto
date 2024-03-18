﻿using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private SolidColorBrush _usernameBoxBorderColor = new(Colors.DarkGray);

        [ObservableProperty]
        private SolidColorBrush _passwordBoxBorderColor = new(Colors.DarkGray);
    }
}
