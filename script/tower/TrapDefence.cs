using Godot;
using System;

public abstract partial class TrapDefence : AttackTower
{
    [Signal]
    public delegate void TrapDeletedEventHandler(string TrapName);
}