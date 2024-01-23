﻿using System;
using System.Reactive;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
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
    public ReactiveCommand<string, Unit> ModifyLeaveTypeName { get; }//Távolléttípus névmódosítás
    public MainViewModel()
    {
        Backtonow = ReactiveCommand.Create(() => { Model.Currentyear = DateTimeOffset.Now; });//Visszaugrás idénre
        AddLeaveType = ReactiveCommand.Create<string>((string leavename/*<-CommandParameter*/) =>
        {
            Model.Leavetypes.Add(new LeaveType(leavename, $"_{leavename}"));//Távolléttípus hozzáadása
        });
        ModifyLeaveTypeName = ReactiveCommand.Create((string newname) =>
        {
            var test = 0;
        });
    }
}
//ThereforeCustomAPI ws19-06 mappástól másold ki!