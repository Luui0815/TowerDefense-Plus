using Godot;
using System;

public partial class goldmine : defender
{
    private AnimatedSprite2D _mine;
    private Timer _moneytimer;
    private int _money_per_time = 50;

    public override void Init()
    {
        _name = "goldmine";
        //_cost = get_Tower_cost(_name);
        _cost = 250;

        _moneytimer = GetNode<Timer>("money_timer");
        _mine= GetNode<AnimatedSprite2D>("goldmine");
        _mine.Play();
    }

    public override void _Ready()
    {
        _moneytimer.Start(25);
    }

    public override void _Process(double delta)
    {
        //if( _moneytimer.TimeLeft==0 )
        //{
            //EmitSignal(SignalName.generated_mine_money, _money_per_time);
        //}
    }

    private void _on_money_timer_timeout()
    {
        EmitSignal(SignalName.generated_mine_money, _money_per_time);
    }

}
