using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System;
using System.Reactive.Subjects;
using Szakdolgozat.ViewModels;

namespace Szakdolgozat.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        dp_currentyear.Bind(DatePicker.SelectedDateProperty, new Binding("Model.Currentyear", BindingMode.Default));
    }
}
