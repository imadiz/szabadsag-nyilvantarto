using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.TextFormatting;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using Szakdolgozat.Classes;

namespace Szakdolgozat.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]//Ez az attribútum megcsinálja az encapsulation-t, ami a binding-hoz kell és létrehoz egy publikus változót nagybetűs névvel. (Ebben az esetben Model)
    private ModelClass _model = new();

    public ReactiveCommand<Unit, Unit> Backtonow { get; }//Ma gomb
    public ReactiveCommand<string, Unit> AddLeaveType { get; }//Távolléttípus hozzáadás
    public ReactiveCommand<LeaveType, Unit> LeaveTypeNameChange { get; }//Távolléttípus névváltoztatás

    public MainViewModel()
    {
        MessageBus.Current.Listen<string>("Debug").Subscribe(x => Model.DebugText = x);

        #region Commandok elkészítése
        Backtonow = ReactiveCommand.Create(() => { Model.Currentyear = DateTimeOffset.Now; });//Visszaugrás idénre

        AddLeaveType = ReactiveCommand.Create<string>((string leavename/*<-CommandParameter*/) =>
        {
            Model.Leavetypes.Add(new LeaveType(leavename, $"_{leavename}"));//Távolléttípus hozzáadása
        });

        LeaveTypeNameChange = ReactiveCommand.Create<LeaveType>((leave) =>
        {
            var teszt = 0;
        });
        #endregion

        Model.Leavetypes.Add(new LeaveType("Teszttávollét", "testleave"));
    }
}
//ThereforeCustomAPI ws19-06 mappástól másold ki!