using Godot;
using System;

public partial class Goldmine : PassiveTower
{
    [Signal]
    public delegate void MoneyGeneratedEventHandler(int moneyAmount);

    private int _moneyPerCycle = 50;

    public override void _Ready()
    {
        Timer _moneyTimer = GetNode<Timer>("MoneyTimer");
        _moneyTimer.Start(25);
    }

    private void OnMoneyTimerTimeout()
    {
        EmitSignal(SignalName.MoneyGenerated, _moneyPerCycle);
    }
}
