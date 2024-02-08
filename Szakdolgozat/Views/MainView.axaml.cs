using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Szakdolgozat.Classes;
using Szakdolgozat.ViewModels;

namespace Szakdolgozat.Views;

public partial class MainView : UserControl
{
    MainViewModel? VM;
    public MainView()
    {
        InitializeComponent();
        VM = DataContext as MainViewModel;
        dp_currentyear.Bind(DatePicker.SelectedDateProperty, new Binding("Model.Currentyear", BindingMode.Default));//Ez azért kell mert XAML-ben nincsen a Binding-ban ilyen StringConversion.
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
    public void mi_leavetypenamechange(object sender, PointerPressedEventArgs args)
    {
        var ctl = sender as Control;
        PointerPoint point = args.GetCurrentPoint(sender as Control);
        if (ctl != null)
        {
            FlyoutBase.ShowAttachedFlyout(ctl);
        }
        args.Handled = true;
    }
    private void btn_namechange_cancel_click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Button? btn = sender as Button;

        FlyoutBase.GetAttachedFlyout(btn.FindLogicalAncestorOfType<MenuItem>(false)).Hide();//Tüntesd el a névváltoztatás menüt

        e.Handled = true;//Az event le lett kezelve, ne indulj el sehova.
    }
    private void btn_namechange_confirm_click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Button? btn = sender as Button;

        MessageBus.Current.SendMessage<string>(btn.Name, "Debug");

        FlyoutBase.GetAttachedFlyout(btn.FindLogicalAncestorOfType<MenuItem>(false)).Hide();//Tüntesd el a névváltoztatás menüt

        e.Handled = true;//Az event le lett kezelve, ne indulj el sehova.
    }
}