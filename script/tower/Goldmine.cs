using Godot;
using System;

public partial class Goldmine : PassiveTower
{
    [Signal]
    public delegate void MoneyGeneratedEventHandler(int moneyAmount);

    private AnimatedSprite2D _mine;
    private Timer _moneytimer;
    private int _money_per_time = 50;

    public override void Init()
    {
        _name = "goldmine";
        
        _moneytimer = GetNode<Timer>("money_timer");
        _mine= GetNode<AnimatedSprite2D>("goldmine");
        _mine.Play();
    }

    public override void _Ready()
    {
        _moneytimer.Start(25);
    }
    private void _on_money_timer_timeout()
    {
        EmitSignal(SignalName.MoneyGenerated, _money_per_time);
    }
}
