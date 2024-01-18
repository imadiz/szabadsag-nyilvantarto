using System;
using System.Reactive;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Szakdolgozat.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]//Ez az attribútum megcsinálja az encapsulation-t, ami a binding-hoz kell és létrehoz egy publikus változót nagybetűs névvel. (Ebben az esetben Model)
    private ModelClass _model = new();

    public ReactiveCommand<Unit, Unit> Backtonow { get; }//Ma gomb
    public ReactiveCommand<string, Unit> AddLeaveType { get; }//Szabadság hozzáadás
    public MainViewModel()
    {
        Backtonow = ReactiveCommand.Create(() => { Model.Currentyear = DateTimeOffset.Now; });//Visszaugrás idénre
        AddLeaveType = ReactiveCommand.Create<string>((string leavename/*<-CommandParameter*/) =>
        {
            Model.Leavetypes.Add(new Classes.LeaveType(leavename, $"_{leavename}"));//Távolléttípus hozzáadása
        });
    }
}
//ThereforeCustomAPI ws19-06 mappástól másold ki!