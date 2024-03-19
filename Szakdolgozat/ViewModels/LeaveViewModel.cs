using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Media.TextFormatting;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using Szakdolgozat.Classes;
using Szakdolgozat.Views;

namespace Szakdolgozat.ViewModels;

public partial class LeaveViewModel : ObservableObject
{
    //HTTPClient classal lehet kommunikálni az API-val

    [ObservableProperty]//Ez az attribútum megcsinálja az encapsulation-t, ami a binding-hoz kell és létrehoz egy publikus változót nagybetűs névvel. (Ebben az esetben Model)
    private LeaveModelClass _model = new();

    public ReactiveCommand<Unit, Unit> Backtonow { get; }//Ma gomb
    public ReactiveCommand<string, Unit> AddLeaveType { get; }//Távolléttípus hozzáadás
    public ReactiveCommand<LeaveType, Unit> LeaveTypeNameChange { get; }//Távolléttípus névváltoztatás

    public LeaveViewModel()
    {
        MessageBus.Current.Listen<LeaveType>("LeaveTypeNameChange").Subscribe((leavetype) =>
        {
            Model.Leavetypes.First(x=>x.Name.Equals(leavetype.Name)/*Azonosító alapján megkeresés*/).Displayname = leavetype.Displayname;/*Megjelenítendő név változtatás*/
        });

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