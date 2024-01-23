using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using System;
using System.Reactive.Subjects;
using Szakdolgozat.ViewModels;

namespace Szakdolgozat.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        dp_currentyear.Bind(DatePicker.SelectedDateProperty, new Binding("Model.Currentyear", BindingMode.Default));//Ez azért kell mert XAML-ben nincsen a Binding-ban StringConversion.
    }
    public void lbl_leavetype_flyout(object sender, PointerPressedEventArgs args)
    {
        var ctl = sender as Control;
        PointerPoint point = args.GetCurrentPoint(sender as Control);
        if (ctl != null && point.Properties.IsRightButtonPressed)
        {
            FlyoutBase.ShowAttachedFlyout(ctl);
        }
        args.Handled = true;
    }
}
